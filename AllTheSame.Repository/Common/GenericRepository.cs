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
using AllTheSame.Common.Logging;
using AllTheSame.Entity.Model;
using AllTheSame.Common.Interfaces.Generic;
using log4net.Config;

namespace AllTheSame.Repository.Common
{
    /// <summary>
    /// GenericRepository of T
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    [Serializable]
    public class GenericRepository<TEntity> : Entity<int>, IGenericRepository<TEntity>
        where TEntity : class, IEntity<int>
    {
        /// <summary>
        /// The Dbset
        /// </summary>
        protected readonly IDbSet<TEntity> CurrentDbSet;

        /// <summary>
        /// The DbContext
        /// </summary>
        [NonSerialized]
        protected DbContext _context;

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
        /// Initializes a new instance of the <see cref="GenericRepository{TEntity}" /> class.
        /// </summary>
        public GenericRepository()
        {
            XmlConfigurator.Configure();

            //
            if (CurrentDbContext != null)
                CurrentDbSet = CurrentDbContext.Set<TEntity>();

            Audit.Log.Info("GenericRepository() :: initialized in constructor");
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GenericRepository{TEntity}" /> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public GenericRepository(DbContext context)
        {
            _context = context;

            AppUtility.ValidateContext(CurrentDbContext);

            try
            {
                CurrentDbSet = CurrentDbContext.Set<TEntity>();
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
        /// Gets all.
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<TEntity> GetAll()
        {
            AppUtility.ValidateDbSet(CurrentDbSet);

            try
            {
                return CurrentDbSet.AsEnumerable();
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
        /// Gets the list asynchronous.
        /// </summary>
        /// <param name="where">The where.</param>
        /// <param name="navigationProperties">The navigation properties.</param>
        /// <returns></returns>
        public virtual async Task<List<TEntity>> GetListAsync(Func<TEntity, bool> @where,
            params Expression<Func<TEntity, object>>[] @navigationProperties)
        {
            List<TEntity> list;

            AppUtility.ValidateContext(CurrentDbContext);

            try
            {
                IQueryable<TEntity> dbQuery = CurrentDbContext.Set<TEntity>();

                if (navigationProperties != null)
                {
                    //Apply eager loading
                    dbQuery = navigationProperties.Aggregate(
                        dbQuery,
                        (current, navigationProperty) => current.Include(navigationProperty)
                        );
                }

                if (dbQuery == null) return null;

                list = await dbQuery
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
        /// Finds the by.
        /// </summary>
        /// <param name="where">The predicate.</param>
        /// <returns></returns>
        public virtual IEnumerable<TEntity> FindBy(Expression<Func<TEntity, bool>> @where)
        {
            IEnumerable<TEntity> query;

            AppUtility.ValidateDbSet(CurrentDbSet);

            try
            {
                query = CurrentDbSet.Where(where).AsEnumerable();
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
        public virtual async Task<IEnumerable<TEntity>> FindByAsync(Expression<Func<TEntity, bool>> @where)
        {
            IEnumerable<TEntity> result;

            AppUtility.ValidateContext(CurrentDbContext);

            try
            {
                var dbQuery = CurrentDbContext.Set<TEntity>();
                result = await dbQuery.Where(where).ToListAsync();
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
        /// Finds the by.
        /// </summary>
        /// <param name="where">The where.</param>
        /// <param name="include">The include.</param>
        /// <returns></returns>
        public virtual IEnumerable<TEntity> FindBy(Expression<Func<TEntity, bool>> @where,
            Expression<Func<TEntity, object>> @include)
        {
            var result = default(IEnumerable<TEntity>);

            AppUtility.ValidateContext(CurrentDbContext);

            try
            {
                var dbQuery = CurrentDbContext.Set<TEntity>();

                if (dbQuery != null)
                {
                    result = dbQuery.Where(where).Include(include).ToList();
                }
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
        /// <param name="include"></param>
        /// <returns></returns>
        public virtual async Task<IEnumerable<TEntity>> FindByAsync(Expression<Func<TEntity, bool>> @where,
            Expression<Func<TEntity, object>> @include)
        {
            IEnumerable<TEntity> result;

            AppUtility.ValidateContext(CurrentDbContext);

            try
            {
                var dbQuery = CurrentDbContext.Set<TEntity>();
                result = await dbQuery.Where(where).Include(include).ToListAsync();
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
        /// Adds the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public virtual TEntity Add(TEntity entity)
        {
            AppUtility.ValidateDbSet(CurrentDbSet);

            AppUtility.ValidateEntity(entity);

            try
            {
                entity.CreatedOn = DateTime.UtcNow;
                //this sets model value
                (entity as TEntity).CreatedOn = base.CreatedOn.HasValue ? base.CreatedOn.Value : DateTime.UtcNow;

                var added = CurrentDbSet.Add(entity);

                CurrentDbContext.Entry(entity).State = EntityState.Added;

                return added;
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
        /// Adds the many.
        /// </summary>
        /// <param name="items">The items.</param>
        public virtual void AddMany(params TEntity[] items)
        {
            AppUtility.ValidateContext(CurrentDbContext);

            if (items == null) return;

            try
            {
                foreach (var item in items)
                {
                    CurrentDbContext.Entry(item).State = EntityState.Added;

                    //this sets base value
                    item.CreatedOn = DateTime.UtcNow;
                    //this sets model value
                    (item as TEntity).CreatedOn = base.CreatedOn.HasValue ? base.CreatedOn.Value : DateTime.UtcNow;

                }
                CurrentDbContext.SaveChanges();
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
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public virtual TEntity Delete(TEntity entity)
        {
            AppUtility.ValidateDbSet(CurrentDbSet);

            AppUtility.ValidateEntity(entity);

            CurrentDbContext.Entry(entity).State = EntityState.Deleted;

            var result = CurrentDbSet.Remove(entity);
            //CurrentDbContext.SaveChanges();

            return result;
        }

        /// <summary>
        /// Removes the many.
        /// </summary>
        /// <param name="items">The items.</param>
        public virtual void RemoveMany(params TEntity[] items)
        {
            AppUtility.ValidateDbSet(CurrentDbSet);

            if (items == null) return;

            try
            {
                foreach (var item in items)
                {
                    CurrentDbContext.Entry(item).State = EntityState.Deleted;
                    //this sets base value
                    base.UpdatedOn = DateTime.UtcNow;
                    //this sets model value
                    (item as TEntity).UpdatedOn = base.UpdatedOn.HasValue ? base.UpdatedOn.Value : DateTime.UtcNow;

                    base.EntityState = EntityState.Deleted;
                }
                CurrentDbContext.SaveChanges();
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
        /// Edits the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public virtual void Edit(TEntity entity)
        {
            AppUtility.ValidateContext(CurrentDbContext);

            AppUtility.ValidateEntity(entity);

            //this sets base value
            base.UpdatedOn = DateTime.UtcNow;
            //this sets model value
            (entity as TEntity).UpdatedOn = base.UpdatedOn.HasValue ? base.UpdatedOn.Value : DateTime.UtcNow;

            base.EntityState = EntityState.Modified;
            CurrentDbContext.Entry(entity).State = EntityState.Modified;
        }

        /// <summary>
        /// Updates the many.
        /// </summary>
        /// <param name="items">The items.</param>
        public virtual void UpdateMany(params TEntity[] items)
        {
            AppUtility.ValidateContext(CurrentDbContext);

            if (items == null) return;

            try
            {
                foreach (var item in items)
                {
                    //this sets base value
                    base.UpdatedOn = DateTime.UtcNow;
                    //this sets model value
                    (item as TEntity).UpdatedOn = base.UpdatedOn.HasValue ? base.UpdatedOn.Value : DateTime.UtcNow;

                    base.EntityState = EntityState.Modified;
                    CurrentDbContext.Entry(item).State = EntityState.Modified;
                }
                CurrentDbContext.SaveChanges();
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
        /// Saves this instance.
        /// </summary>
        /// <returns></returns>
        public virtual int Save()
        {
            var result = -1;

            AppUtility.ValidateContext(CurrentDbContext);

            try
            {
                result = CurrentDbContext.SaveChanges();

                Audit.Log.Info(string.Format("Repository Save() :: result: {0}", result));
            }
            catch (DbEntityValidationException ex)
            {
                var errors = ex.EntityValidationErrors.SelectMany(
                    x => x.ValidationErrors.Select(y => new ValidationResult(y.ErrorMessage, new[] {y.PropertyName})));

                Audit.Log.Error(
                    string.Format("{0} :: {1} ", AppConstants.ErrorMessages.DbEntityValidationExceptionMessage, errors),
                    ex);
            }
            catch (DbUpdateException ex)
            {
                var decodedErrors = TryDecodeDbUpdateException(ex);
                if (decodedErrors == null)
                    throw; //it isn't something we understand 

                Audit.Log.Error(
                    string.Format("{0} :: {1} ", AppConstants.ErrorMessages.DbExceptionMessage, decodedErrors), ex);
            }
            catch (Exception ex)
            {
                Audit.Log.Error(AppConstants.ErrorMessages.ExceptionMessage, ex);

                throw;
            }

            return result;
        }

        //
        /// <summary>
        /// Gets all.
        /// </summary>
        /// <param name="navigationProperties">The navigation properties.</param>
        /// <returns></returns>
        public virtual IList<TEntity> GetAll(params Expression<Func<TEntity, object>>[] @navigationProperties)
        {
            //usage example: 
            //IGenericDataRepository<Department> repository = new GenericDataRepository<Department>();
            //IList<Department> departments = repository.GetAll(d => d.Employees);

            var list = default(List<TEntity>);

            AppUtility.ValidateContext(CurrentDbContext);

            try
            {
                IQueryable<TEntity> dbQuery = CurrentDbContext.Set<TEntity>();

                if (navigationProperties != null)
                {
                    //Apply eager loading
                    dbQuery = navigationProperties.Aggregate(
                        dbQuery,
                        (current, navigationProperty) => current.Include(navigationProperty)
                        );
                }

                if (dbQuery != null)
                {
                    list = dbQuery
                        .AsNoTracking()
                        .ToList();
                }
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
        /// Gets all asynchronous.
        /// </summary>
        /// <returns></returns>
        public virtual async Task<List<TEntity>> GetAllAsync()
        {
            List<TEntity> list;

            AppUtility.ValidateContext(CurrentDbContext);

            try
            {
                IQueryable<TEntity> dbQuery = CurrentDbContext.Set<TEntity>();

                if (dbQuery == null) return null;

                //Apply eager loading, async
                list = await dbQuery
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
        public virtual IList<TEntity> GetList(Func<TEntity, bool> @where,
            params Expression<Func<TEntity, object>>[] navigationProperties)
        {
            List<TEntity> list;

            AppUtility.ValidateContext(CurrentDbContext);

            try
            {
                IQueryable<TEntity> dbQuery = CurrentDbContext.Set<TEntity>();

                if (navigationProperties != null)
                {
                    //Apply eager loading
                    dbQuery = navigationProperties.Aggregate(
                        dbQuery,
                        (current, navigationProperty) => current.Include(navigationProperty)
                        );
                }

                if (dbQuery == null) return null;

                list = dbQuery
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
        /// Gets the single.
        /// </summary>
        /// <param name="where">The where.</param>
        /// <param name="navigationProperties">The navigation properties.</param>
        /// <returns></returns>
        public virtual TEntity GetSingle(Func<TEntity, bool> @where,
            params Expression<Func<TEntity, object>>[] @navigationProperties)
        {
            TEntity item;

            AppUtility.ValidateDbSet(CurrentDbSet);

            try
            {
                IQueryable<TEntity> dbQuery = CurrentDbContext.Set<TEntity>();

                if (navigationProperties != null)
                {
                    //Apply eager loading
                    dbQuery = navigationProperties.Aggregate(
                        dbQuery,
                        (current, navigationProperty) => current.Include(navigationProperty)
                        );
                }

                if (dbQuery == null) return null;

                item = dbQuery
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
        public virtual async Task<TEntity> GetSingleAsync(Func<TEntity, bool> @where,
            params Expression<Func<TEntity, object>>[] @navigationProperties)
        {
            TEntity item;

            AppUtility.ValidateContext(CurrentDbContext);

            try
            {
                IQueryable<TEntity> dbQuery = CurrentDbContext.Set<TEntity>();

                if (navigationProperties != null)
                {
                    //Apply eager loading
                    dbQuery = navigationProperties.Aggregate(
                        dbQuery,
                        (current, navigationProperty) => current.Include(navigationProperty)
                        );
                }

                if (dbQuery == null) return null;

                item = await dbQuery
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
        /// GetSortedFieldFilterResult
        /// </summary>
        /// <param name="searchField">The search field.</param>
        /// <param name="criteria">The criteria.</param>
        /// <param name="sortDirection">The sort direction.</param>
        /// <returns></returns>
        public virtual PagedListResult<TEntity> GetSortedFieldFilterResult(string searchField = null,
            string criteria = null, SortDirection sortDirection = SortDirection.Ascending)
        {
            var repository = new SearchRepository<TEntity>();

            searchField = !string.IsNullOrEmpty(searchField) ? searchField : "Id";
                //TODO: once UpdatedOn is implemented - this will be default
            criteria = !string.IsNullOrEmpty(criteria) ? criteria : "";

            var q = new SearchQuery<TEntity>();

            if (!string.IsNullOrEmpty(searchField))
                q.AddSortCriteria(
                    new FieldSortCriteria<TEntity>(searchField, sortDirection));

            if (!string.IsNullOrEmpty(criteria))
                q.AddSortCriteria(
                    new FieldSortCriteria<TEntity>(criteria, sortDirection));

            var result = repository.Search(q);

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

                var sqlException = (SqlException) dbuex.InnerException.InnerException;
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

        //translate state from model between disconnected set
        /// <summary>
        /// Gets the state of the entity.
        /// </summary>
        /// <param name="entityState">State of the entity.</param>
        /// <returns></returns>
        protected static EntityState GetEntityState(DisconnectedEntityState entityState)
        {
            switch (entityState)
            {
                case DisconnectedEntityState.Unchanged:
                    return EntityState.Unchanged;
                case DisconnectedEntityState.Added:
                    return EntityState.Added;
                case DisconnectedEntityState.Modified:
                    return EntityState.Modified;
                case DisconnectedEntityState.Deleted:
                    return EntityState.Deleted;
                default:
                    return EntityState.Detached;
            }
        }

        /// <summary>
        /// Validate Entity, get list of errors
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ValidationResult> Validate()
        {
            var validationErrors = new List<ValidationResult>();

            var ctx = new ValidationContext(this, null, null);
            Validator.TryValidateObject(this, ctx, validationErrors, true);

            return validationErrors;
        }

        /// <summary>
        /// Attaches the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void Attach(TEntity entity)
        {
            AppUtility.ValidateContext(CurrentDbContext);

            try
            {
                var dbset = CurrentDbContext.Set<TEntity>();

                if (dbset != null)
                {
                    dbset.Attach(entity);
                }
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
        private void Dispose(bool disposing)
        {
            if (!disposing) return;

            if (CurrentDbContext != null)
            {
                CurrentDbContext.Dispose();
            }
            if (_context != null)
            {
                _context.Dispose();
                _context = null;
            }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);

            Audit.Log.Info("GenericRepository Dispose :: CurrentDbContext destroyed");
        }
    }
}