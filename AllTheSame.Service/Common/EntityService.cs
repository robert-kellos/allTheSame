using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AllTheSame.Common.Core;
using AllTheSame.Common.Extensions;
using AllTheSame.Common.Helpers;
using AllTheSame.Common.Interfaces.Generic;
using AllTheSame.Common.Logging;
using AllTheSame.Entity.Model;
using AllTheSame.Repository.Common;
using log4net.Config;

namespace AllTheSame.Service.Common
{
    /// <summary>
    /// EntityService
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <typeparam name="TRepository">The type of the repository.</typeparam>
    [Serializable]
    public class EntityService<TEntity, TRepository> : IEntityService<TEntity, TRepository>
        where TEntity : class, IEntity<int>
        where TRepository : class, IGenericRepository<TEntity>
    {
        /// <summary>
        /// The _repository
        /// </summary>
        private readonly TRepository _repository;

        /// <summary>
        /// The _unit of work
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// DbContext
        /// </summary>
        [NonSerialized]
        private DbContext _context;

        /// <summary>
        /// DbSet
        /// </summary>
        protected readonly IDbSet<TEntity> CurrentDbSet;

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="repository">The repository.</param>
        public EntityService(IUnitOfWork unitOfWork, TRepository repository)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
        }

        /// <summary>
        /// Gets the current database context.
        /// </summary>
        /// <value>
        /// The current database context.
        /// </value>
        public DbContext CurrentDbContext
        {
            get { return _context ?? (_context = new Entity.Model.AllTheSameDbContext()); }
        }

        /// <summary>
        /// EntityService cstr
        /// </summary>
        public EntityService()
        {
            XmlConfigurator.Configure();
            //
            if (CurrentDbContext != null)
                CurrentDbSet = CurrentDbContext.Set<TEntity>();

            Audit.Log.Info("EntityService :: contructor initialized");
        }

        /// <summary>
        /// EntityService
        /// </summary>
        /// <param name="currentDbContext">The current database context.</param>
        public EntityService(DbContext currentDbContext)
        {
            _context = currentDbContext;

            Audit.Log.Info(string.Format("EntityService(DbContext) :: constructor initialized context: {0}", currentDbContext)); 
        }

        /// <summary>
        /// Gets objects from database with filting and paging.
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="filter">Specified a filter</param>
        /// <param name="total">Returns the total records count of the filter.</param>
        /// <param name="index">Specified the page index.</param>
        /// <param name="size">Specified the page size</param>
        /// <returns></returns>
        public IQueryable<TEntity> Filter<TKey>(Expression<Func<TEntity, bool>> filter,
            out int total, int index = 0, int size = 50)
        {
            var skipCount = index * size;

            var resetSet = filter != null
                ? CurrentDbSet.Where(filter).AsQueryable()
                : CurrentDbSet.AsQueryable();

            resetSet = skipCount == 0
                ? resetSet.Take(size)
                : resetSet.Skip(skipCount).Take(size);

            total = resetSet.Count();

            return resetSet.AsQueryable();
        }

        /// <summary>
        /// Count of current db table set
        /// </summary>
        public int Count
        {
            get { return CurrentDbSet != null ? CurrentDbSet.Count() : -1; }
        }

        /// <summary>
        /// Adds the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">entity</exception>
        public virtual TEntity Add(TEntity entity)
        {
            AppUtility.ValidateEntity(entity);

            //calls respository
            var result = _repository.Add(entity);
            _unitOfWork.Commit();

            return result;
        }

        /// <summary>
        /// Adds the many.
        /// </summary>
        /// <param name="items">The items.</param>
        public void AddMany(params TEntity[] items)
        {
            //calls repository
            _repository.AddMany(items);
            _unitOfWork.Commit();
        }

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">entity</exception>
        public virtual TEntity Delete(TEntity entity)
        {
            AppUtility.ValidateEntity(entity);

            //calls respository
            var result = _repository.Delete(entity);
            _unitOfWork.Commit();

            return result;
        }

        /// <summary>
        /// Removes the many.
        /// </summary>
        /// <param name="items">The items.</param>
        public void RemoveMany(params TEntity[] items)
        {
            //calls repository
            _repository.RemoveMany(items);
            _unitOfWork.Commit();
        }

        /// <summary>
        /// Edits the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <exception cref="System.ArgumentNullException">entity</exception>
        public virtual void Edit(TEntity entity)
        {
            AppUtility.ValidateEntity(entity);

            //calls repository
            _repository.Edit(entity);
            _unitOfWork.Commit();
        }
        

        /// <summary>
        /// Updates the many.
        /// </summary>
        /// <param name="items">The items.</param>
        public void UpdateMany(params TEntity[] items)
        {
            //calls repository
            _repository.UpdateMany(items);
            _unitOfWork.Commit();
        }

        /// <summary>
        /// </summary>
        public virtual TRepository Repository
        {
            get { return _repository; }
        }

        /// <summary>
        /// Saves this instance.
        /// </summary>
        /// <returns></returns>
        public virtual int Save()
        {
            var result = -1;

            try
            {
                //result = _repository.Save();
                result = _unitOfWork.Commit();
            }
            catch (DbEntityValidationException ex)
            {
                var errors = ex.EntityValidationErrors.SelectMany(
                    x => x.ValidationErrors.Select(y => new ValidationResult(y.ErrorMessage, new[] { y.PropertyName })));

                Audit.Log.Error(
                    string.Format("Validation Error(s): {0} :: {1} ",
                        AppConstants.ErrorMessages.DbEntityValidationExceptionMessage, errors), ex);
            }
            catch (DbUpdateException ex)
            {
                var decodedErrors = TryDecodeDbUpdateException(ex);
                if (decodedErrors == null)
                    throw; //it isn't something we understand 

                Audit.Log.Error(
                    string.Format("Decoded DbUpdate Error(s): {0} :: {1} ",
                        AppConstants.ErrorMessages.DbExceptionMessage, decodedErrors), ex);
            }
            catch (Exception ex)
            {
                Audit.Log.Error(AppConstants.ErrorMessages.ExceptionMessage, ex);

                throw;
            }

            return result;
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<TEntity> GetAll()
        {
            List<TEntity> list;

            AppUtility.ValidateContext(CurrentDbContext);

            try
            {
                IQueryable<TEntity> dbSet = CurrentDbContext.Set<TEntity>();

                if (dbSet == null) return null;

                list = dbSet
                    .AsNoTracking()
                    .ToList();

            }
            catch (SqlException sqex)
            {
                Audit.Log.Error(AppConstants.ErrorMessages.SqlExceptionMessage, sqex);

                throw;
            }
            catch (DbException dbex)
            {
                Audit.Log.Error(AppConstants.ErrorMessages.DbExceptionMessage, dbex);

                throw;
            }
            catch (Exception ex)
            {
                Audit.Log.Error(AppConstants.ErrorMessages.ExceptionMessage, ex);

                throw;
            }

            return list;
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <param name="navigationProperties">The navigation properties.</param>
        /// <returns></returns>
        public virtual IList<TEntity> GetAll(params Expression<Func<TEntity, object>>[] navigationProperties)
        {
            //usage example: 
            //IGenericDataRepository<Department> repository = new GenericDataRepository<Department>();
            //IList<Department> departments = repository.GetAll(d => d.Employees);

            List<TEntity> list;

            AppUtility.ValidateContext(CurrentDbContext);

            try
            {
                IQueryable<TEntity> dbSet = CurrentDbContext.Set<TEntity>();

                if (navigationProperties != null)
                {
                    //Apply eager loading
                    dbSet = navigationProperties.Aggregate(
                        dbSet,
                        (current, navigationProperty) => current.Include(navigationProperty)
                        );
                }

                if (dbSet == null) return null;

                list = dbSet
                    .AsNoTracking()
                    .ToList();

            }
            catch (SqlException sqex)
            {
                Audit.Log.Error(AppConstants.ErrorMessages.SqlExceptionMessage, sqex);

                throw;
            }
            catch (DbException dbex)
            {
                Audit.Log.Error(AppConstants.ErrorMessages.DbExceptionMessage, dbex);

                throw;
            }
            catch (Exception ex)
            {
                Audit.Log.Error(AppConstants.ErrorMessages.ExceptionMessage, ex);

                throw;
            }

            return list;
        }

        /// <summary>
        /// GetAll async
        /// </summary>
        /// <returns></returns>
        public virtual async Task<List<TEntity>> GetAllAsync()
        {
            List<TEntity> list;

            AppUtility.ValidateContext(CurrentDbContext);

            try
            {
                IQueryable<TEntity> dbSet = CurrentDbContext.Set<TEntity>();

                if (dbSet == null) return null;

                //Apply eager loading, async
                list = await dbSet
                    .AsNoTracking()
                    .ToListAsync();

            }
            catch (SqlException sqex)
            {
                Audit.Log.Error(AppConstants.ErrorMessages.SqlExceptionMessage, sqex);

                throw;
            }
            catch (DbException dbex)
            {
                Audit.Log.Error(AppConstants.ErrorMessages.DbExceptionMessage, dbex);

                throw;
            }
            catch (Exception ex)
            {
                Audit.Log.Error(AppConstants.ErrorMessages.ExceptionMessage, ex);

                throw;
            }

            return list;
        }

        /// <summary>
        /// Gets the list.
        /// </summary>
        /// <param name="where">The where.</param>
        /// <param name="navigationProperties">The navigation properties.</param>
        /// <returns></returns>
        public virtual IList<TEntity> GetList(Func<TEntity, bool> where,
            params Expression<Func<TEntity, object>>[] navigationProperties)
        {
            List<TEntity> list;

            AppUtility.ValidateContext(CurrentDbContext);

            try
            {
                IQueryable<TEntity> dbSet = CurrentDbContext.Set<TEntity>();

                if (navigationProperties != null)
                {
                    //Apply eager loading
                    dbSet = navigationProperties.Aggregate(
                        dbSet,
                        (current, navigationProperty) => current.Include(navigationProperty)
                        );
                }

                if (dbSet == null) return null;

                list = dbSet
                    .AsNoTracking()
                    .Where(where)
                    .ToList();

            }
            catch (SqlException sqex)
            {
                Audit.Log.Error(AppConstants.ErrorMessages.SqlExceptionMessage, sqex);

                throw;
            }
            catch (DbException dbex)
            {
                Audit.Log.Error(AppConstants.ErrorMessages.DbExceptionMessage, dbex);

                throw;
            }
            catch (Exception ex)
            {
                Audit.Log.Error(AppConstants.ErrorMessages.ExceptionMessage, ex);

                throw;
            }

            return list;
        }

        /// <summary>
        /// GetListAsync
        /// </summary>
        /// <param name="where">The where.</param>
        /// <param name="navigationProperties">The navigation properties.</param>
        /// <returns></returns>
        public virtual async Task<List<TEntity>> GetListAsync(Func<TEntity, bool> where,
            params Expression<Func<TEntity, object>>[] navigationProperties)
        {
            List<TEntity> list;

            AppUtility.ValidateContext(CurrentDbContext);

            try
            {
                IQueryable<TEntity> dbSet = CurrentDbContext.Set<TEntity>();

                if (navigationProperties != null)
                {
                    //Apply eager loading
                    dbSet = navigationProperties.Aggregate(
                        dbSet,
                        (current, navigationProperty) => current.Include(navigationProperty)
                        );
                }

                if (dbSet == null) return null;

                list = await dbSet
                    .AsNoTracking()
                    .Where(where)
                    .AsQueryable()
                    .ToListAsync();

            }
            catch (SqlException sqex)
            {
                Audit.Log.Error(AppConstants.ErrorMessages.SqlExceptionMessage, sqex);

                throw;
            }
            catch (DbException dbex)
            {
                Audit.Log.Error(AppConstants.ErrorMessages.DbExceptionMessage, dbex);

                throw;
            }
            catch (Exception ex)
            {
                Audit.Log.Error(AppConstants.ErrorMessages.ExceptionMessage, ex);

                throw;
            }

            return list;
        }

        /// <summary>
        /// Gets the single.
        /// </summary>
        /// <param name="where">The where.</param>
        /// <param name="navigationProperties">The navigation properties.</param>
        /// <returns></returns>
        public virtual TEntity GetSingle(Func<TEntity, bool> where,
            params Expression<Func<TEntity, object>>[] navigationProperties)
        {
            TEntity item;

            AppUtility.ValidateContext(CurrentDbContext);

            try
            {
                IQueryable<TEntity> dbSet = CurrentDbContext.Set<TEntity>();

                if (navigationProperties != null)
                {
                    //Apply eager loading
                    dbSet = navigationProperties.Aggregate(
                        dbSet,
                        (current, navigationProperty) => current.Include(navigationProperty)
                        );
                }

                if (dbSet == null) return null;

                item = dbSet
                    .AsNoTracking() //Don't track any changes for the selected item
                    .FirstOrDefault(where); //Apply where clause

            }
            catch (SqlException sqex)
            {
                Audit.Log.Error(AppConstants.ErrorMessages.SqlExceptionMessage, sqex);

                throw;
            }
            catch (DbException dbex)
            {
                Audit.Log.Error(AppConstants.ErrorMessages.DbExceptionMessage, dbex);

                throw;
            }
            catch (Exception ex)
            {
                Audit.Log.Error(AppConstants.ErrorMessages.ExceptionMessage, ex);

                throw;
            }

            return item;
        }

        /// <summary>
        /// Gets the single asynchronous.
        /// </summary>
        /// <param name="where">The where.</param>
        /// <param name="navigationProperties">The navigation properties.</param>
        /// <returns></returns>
        public virtual async Task<TEntity> GetSingleAsync(Func<TEntity, bool> where,
            params Expression<Func<TEntity, object>>[] navigationProperties)
        {
            TEntity item;

            AppUtility.ValidateContext(CurrentDbContext);

            try
            {
                IQueryable<TEntity> dbSet = CurrentDbContext.Set<TEntity>();

                if (navigationProperties != null)
                {
                    //Apply eager loading
                    dbSet = navigationProperties.Aggregate(
                        dbSet,
                        (current, navigationProperty) => current.Include(navigationProperty)
                        );
                }

                if (dbSet == null) return null;

                item = await dbSet
                    .AsNoTracking() //Don't track any changes for the selected item
                    .FirstOrDefaultAsync(w => w.Equals(where)); //Apply where clause

            }
            catch (SqlException sqex)
            {
                Audit.Log.Error(AppConstants.ErrorMessages.SqlExceptionMessage, sqex);

                throw;
            }
            catch (DbException dbex)
            {
                Audit.Log.Error(AppConstants.ErrorMessages.DbExceptionMessage, dbex);

                throw;
            }
            catch (Exception ex)
            {
                Audit.Log.Error(AppConstants.ErrorMessages.ExceptionMessage, ex);

                throw;
            }

            return item;
        }

        /// <summary>
        /// Finds the by.
        /// </summary>
        /// <param name="where">The where.</param>
        /// <returns></returns>
        public virtual IEnumerable<TEntity> FindBy(Expression<Func<TEntity, bool>> where)
        {
            IEnumerable<TEntity> query;

            AppUtility.ValidateContext(CurrentDbContext);

            try
            {
                var dbSet = CurrentDbContext.Set<TEntity>();
                query = dbSet.Where(where).ToList();
            }
            catch (SqlException sqex)
            {
                Audit.Log.Error(AppConstants.ErrorMessages.SqlExceptionMessage, sqex);

                throw;
            }
            catch (DbException dbex)
            {
                Audit.Log.Error(AppConstants.ErrorMessages.DbExceptionMessage, dbex);

                throw;
            }
            catch (Exception ex)
            {
                Audit.Log.Error(AppConstants.ErrorMessages.ExceptionMessage, ex);

                throw;
            }

            return query;
        }

        /// <summary>
        /// Finds the by asynchronous.
        /// </summary>
        /// <param name="where">The where.</param>
        /// <returns></returns>
        public virtual async Task<IEnumerable<TEntity>> FindByAsync(Expression<Func<TEntity, bool>> where)
        {
            IEnumerable<TEntity> query;

            AppUtility.ValidateContext(CurrentDbContext);

            try
            {
                var dbSet = CurrentDbContext.Set<TEntity>();
                query = await dbSet.Where(where).ToListAsync();
            }
            catch (SqlException sqex)
            {
                Audit.Log.Error(AppConstants.ErrorMessages.SqlExceptionMessage, sqex);

                throw;
            }
            catch (DbException dbex)
            {
                Audit.Log.Error(AppConstants.ErrorMessages.DbExceptionMessage, dbex);

                throw;
            }
            catch (Exception ex)
            {
                Audit.Log.Error(AppConstants.ErrorMessages.ExceptionMessage, ex);

                throw;
            }

            return query;
        }

        /// <summary>
        /// Finds the by.
        /// </summary>
        /// <param name="where">The where.</param>
        /// <param name="include">The include.</param>
        /// <returns></returns>
        public virtual IEnumerable<TEntity> FindBy(Expression<Func<TEntity, bool>> where,
            Expression<Func<TEntity, object>> include)
        {
            IEnumerable<TEntity> result;

            AppUtility.ValidateContext(CurrentDbContext);

            try
            {
                var dbSet = CurrentDbContext.Set<TEntity>();
                result = dbSet.Where(where).Include(include).ToList();
            }
            catch (SqlException sqex)
            {
                Audit.Log.Error(AppConstants.ErrorMessages.SqlExceptionMessage, sqex);

                throw;
            }
            catch (DbException dbex)
            {
                Audit.Log.Error(AppConstants.ErrorMessages.DbExceptionMessage, dbex);

                throw;
            }
            catch (Exception ex)
            {
                Audit.Log.Error(AppConstants.ErrorMessages.ExceptionMessage, ex);

                throw;
            }

            return result;
        }

        /// <summary>
        /// Finds the by asynchronous.
        /// </summary>
        /// <param name="where">The where.</param>
        /// <param name="include">The include.</param>
        /// <returns></returns>
        public virtual async Task<IEnumerable<TEntity>> FindByAsync(Expression<Func<TEntity, bool>> where,
            Expression<Func<TEntity, object>> include)
        {
            IEnumerable<TEntity> result;

            AppUtility.ValidateContext(CurrentDbContext);

            try
            {
                var dbSet = CurrentDbContext.Set<TEntity>();
                result = await dbSet.Where(where).Include(include).ToListAsync();
            }
            catch (SqlException sqex)
            {
                Audit.Log.Error(AppConstants.ErrorMessages.SqlExceptionMessage, sqex);

                throw;
            }
            catch (DbException dbex)
            {
                Audit.Log.Error(AppConstants.ErrorMessages.DbExceptionMessage, dbex);

                throw;
            }
            catch (Exception ex)
            {
                Audit.Log.Error(AppConstants.ErrorMessages.ExceptionMessage, ex);

                throw;
            }

            return result;
        }

        /// <summary>
        /// This decodes the DbUpdateException. If there are any errors it can
        /// handle then it returns a list of errors. Otherwise it returns null
        /// which means rethrow the error as it has not been handled
        /// </summary>
        /// <param name="dbuex">The dbuex.</param>
        /// <returns>
        /// null if cannot handle errors, otherwise a list of errors
        /// </returns>
        private static IEnumerable<ValidationResult> TryDecodeDbUpdateException(DbUpdateException dbuex)
        {
            try
            {
                if (!(dbuex.InnerException is UpdateException) ||
                    !(dbuex.InnerException.InnerException is SqlException))
                    return null;

                var sqlException = (SqlException)dbuex.InnerException.InnerException;
                var result = new List<ValidationResult>();
                for (var i = 0; i < sqlException.Errors.Count; i++)
                {
                    var errorNum = sqlException.Errors[i].Number;
                    if (!string.IsNullOrWhiteSpace(errorNum.ToString()))
                        result.Add(new ValidationResult(errorNum.ToString()));
                }

                return result.Any() ? result : null;
            }
            catch (SqlException sqex)
            {
                Audit.Log.Error(AppConstants.ErrorMessages.SqlExceptionMessage, sqex);

                throw;
            }
            catch (DbException dbex)
            {
                Audit.Log.Error(AppConstants.ErrorMessages.DbExceptionMessage, dbex);

                throw;
            }
            catch (Exception ex)
            {
                Audit.Log.Error(AppConstants.ErrorMessages.ExceptionMessage, ex);

                throw;
            }
        }

        /// <summary>
        /// Disposes all external resources.
        /// </summary>
        /// <param name="disposing">The dispose indicator.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                //if (_unitOfWork != null)
                //    _unitOfWork.Dispose();

                if (CurrentDbContext != null)
                    CurrentDbContext.Dispose();
            }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);

            Audit.Log.Info(string.Format("Dispose :: CurrentDbContext Entity {0} destroyed", typeof(TEntity).DeclaringType));
        }

    }
}