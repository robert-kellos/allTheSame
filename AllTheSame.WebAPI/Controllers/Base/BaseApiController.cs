using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using AllTheSame.Common.Core;
using AllTheSame.Common.Interfaces.Generic;
using AllTheSame.Common.Logging;
using AllTheSame.Entity.Model;
using AllTheSame.Repository.Common;
using AllTheSame.Service;
using AllTheSame.Service.Common;

namespace AllTheSame.WebAPI.Controllers.Base
{
    /// <summary>
    /// BaseApiController
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    public abstract class BaseApiController<TEntity> : ApiController 
        where TEntity : class, IEntity<int> 

    {
        #region Local Fields/Properties
        //

        /// <summary>
        /// The _context
        /// </summary>
        public DbContext Context { get; private set; }

        /// <summary>
        /// The _service
        /// </summary>
        public IEntityService<TEntity, IGenericRepository<TEntity>> Service { get; private set; }

        /// <summary>
        /// The proxy
        /// </summary>
        protected ServiceProxy Proxy = new ServiceProxy();
        //
        #endregion Local Fields/Properties

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseApiController{TEntity}" /> class.
        /// </summary>
        protected BaseApiController()
        {
            if(Context == null)
                Context = new Entity.Model.AllTheSameDbContext();

            GetServiceReference(Context);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseApiController{TEntity}"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        protected BaseApiController(DbContext context)
        {
            GetServiceReference(context);
        }

        /// <summary>
        /// Gets the service refernce.
        /// </summary>
        /// <param name="context">The context.</param>
        private void GetServiceReference(DbContext context)
        {
            Context = context;

            var uow = new UnitOfWork(Context);
            var respository = new GenericRepository<TEntity>(Context);

            Service = new EntityService<TEntity, IGenericRepository<TEntity>>(uow, respository);
        }

        //
        // COMMON CRUD
        //

        #region // COMMON CRUD

        //
        // GET: api/{controller}/Get
        /// <summary>
        /// Get items
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<TEntity> Get()
        {
            return Service.GetAll();
        }

        /// <summary>
        /// FindByAsync
        /// </summary>
        /// <param name="where">The where.</param>
        /// <returns></returns>
        protected virtual async Task<IEnumerable<TEntity>> FindByAsync(Expression<Func<TEntity, bool>> @where)
        {
            return await Service.FindByAsync(where);
        }

        // GET: api/{controller}/5
        /// <summary>
        /// Gets by id.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public virtual IHttpActionResult GetById(long? id)
        {
            var item = Service.FindBy(r=>r.Id == id).SingleOrDefault();
            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        // PUT: api/{controller}/Post/5
        /// <summary>
        /// Post via PUT.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        public virtual IHttpActionResult Put(long? id, [FromBody]TEntity item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                item.UpdatedOn = DateTime.UtcNow;
                Service.Edit(item);
                Context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException dcx)
            {
                if (!Exists(id))
                {
                    return NotFound();
                }

                Audit.Log.Error(AppConstants.ErrorMessages.DbUpdateConcurrencyExceptionMessage, dcx);

                throw;
            }

            return Ok(item);// StatusCode(HttpStatusCode.OK);
        }

        // POST: api/{controller}/Post
        /// <summary>
        /// Add the item via POST.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        public virtual IHttpActionResult Post(TEntity item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            item.UpdatedOn = DateTime.Now;

            var added = Service.Add(item);
            if (added == null) return CreatedAtRoute("DefaultApi", new {id = item.Id}, item);
            added.CreatedOn = DateTime.Now;

            Context.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = item.Id }, item);
        }

        // DELETE: api/{controller}/Delete/5
        /// <summary>
        /// Deletes the item.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public virtual IHttpActionResult Delete(long? id)
        {
            var item = Service.FindBy(r=>r.Id == id).SingleOrDefault();
            if (item == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var deleted = Service.Delete(item);
            if (deleted == null) return Ok(item);
            deleted.UpdatedOn = DateTime.UtcNow;

            Service.Save();

            return Ok(item);
        }

        // GET: api/{controller}/Exists/5
        /// <summary>
        /// Check if the item exists.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public virtual bool Exists(long? id)
        {
            return Service.FindBy(r=>r.Id == id).SingleOrDefault() != null;
        }

        //

        #endregion // COMMON CRUD


        /// <summary>
        /// Releases the unmanaged resources that are used by the object and, optionally, releases the managed resources.
        /// </summary>
        /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged
        /// resources.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (Context != null)
                {
                    Context.Dispose();
                }

                if (Service != null)
                {
                    Service.Dispose();
                }

                if (Proxy != null)
                    Proxy.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}