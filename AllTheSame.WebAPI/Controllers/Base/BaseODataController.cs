using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.OData;
using AllTheSame.Common.Core;
using AllTheSame.Common.Interfaces.Generic;
using AllTheSame.Common.Logging;
using AllTheSame.Entity.Model;
using AllTheSame.Repository.Common;
using AllTheSame.Service;
using AllTheSame.Service.Common;

namespace AllTheSame.WebAPI.Controllers.Base
{
    /*
    
     * The WebApiConfig class may require additional changes to add a route for this controller. 
     * Merge these statements into the Register method of the WebApiConfig class as applicable. 
     * Note that OData URLs are case sensitive.

    using System.Web.Http.OData.Builder;
    using System.Web.Http.OData.Extensions;
    using AllTheSame.Entity.Model;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<TEntity>("XXXX");, where XXXX is name of TEntity
    
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
     
    */

    //[RoutePrefix("api/{controller}")]
    //[Authorize(Roles = AppConstants.PermissionCode.ViewVendor)]
    /// <summary>
    /// BaseODataController
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    public class BaseODataController<TEntity> : ODataController
        where TEntity : class, IEntity<int> 
    {

        #region Local Fields/Properties
        //
        /// <summary>
        /// The _context
        /// </summary>
        private DbContext _context;
        /// <summary>
        /// The _service
        /// </summary>
        private IEntityService<TEntity, IGenericRepository<TEntity>> _service;
        /// <summary>
        /// The proxy
        /// </summary>
        protected ServiceProxy Proxy = new ServiceProxy();
        //
        #endregion Local Fields/Properties

        /// <summary>
        /// Initializes a new instance of the BaseApiController TEntity.
        /// </summary>
        protected BaseODataController()
        {
            if (_context == null)
                _context = new Entity.Model.AllTheSameDbContext();

            GetServiceRefernce(_context);
        }

        /// <summary>
        /// Gets the service refernce.
        /// </summary>
        /// <param name="context">The context.</param>
        private void GetServiceRefernce(DbContext context)
        {
            _context = context;

            var uow = new UnitOfWork(_context);
            var respository = new GenericRepository<TEntity>(_context);

            _service = new EntityService<TEntity, IGenericRepository<TEntity>>(uow, respository);
        }

        //
        // COMMON CRUD
        //

        #region // COMMON CRUD

        // GET: odata/{controller}
        /// <summary>
        /// Gets the item.
        /// </summary>
        /// <returns></returns>
        [EnableQuery]
        public virtual IQueryable<TEntity> GetAll()
        {
            return _service.GetAll().AsQueryable();
        }

        // GET: odata/{controller}(5)
        /// <summary>
        /// Gets the item.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        [EnableQuery]
        public virtual SingleResult<TEntity> GetById([FromODataUri] long? id)
        {
            return
                SingleResult.Create(_service.FindBy(r=> r.Id == id).AsQueryable());
        }

        // PUT: odata/{controller}(5)
        /// <summary>
        /// Puts the specified id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="changes">The patch.</param>
        /// <returns></returns>
        public virtual async Task<IHttpActionResult> Edit([FromODataUri] long? id, Delta<TEntity> changes)
        {
            Validate(changes.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var item = _service.GetSingle(r => r.Id == id);
            if (item == null)
            {
                return NotFound();
            }

            changes.Put(item);

            try
            {
                await _context.SaveChangesAsync();
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

            return Updated(item);
        }

        // POST: odata/{controller}
        /// <summary>
        /// Posts the item by specified object.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        public virtual async Task<IHttpActionResult> Create(TEntity item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _service.Add(item);
            await _context.SaveChangesAsync();

            return Created(item);
        }

        // PATCH: odata/{controller}(5)
        /// <summary>
        /// Patches the item by specified id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="changes">The patch.</param>
        /// <returns></returns>
        [AcceptVerbs("PATCH", "MERGE")]
        public virtual async Task<IHttpActionResult> Update([FromODataUri] long? id, Delta<TEntity> changes)
        {
            Validate(changes.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var item = _service.GetSingle(r => r.Id == id);
            if (item == null)
            {
                return NotFound();
            }

            changes.Patch(item);

            try
            {
                await _context.SaveChangesAsync();
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

            return Updated(item);
        }

        // DELETE: odata/{controller}(5)
        /// <summary>
        /// Deletes the item by verifying by specified id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public virtual async Task<IHttpActionResult> Delete([FromODataUri] long? id)
        {
            var item = _service.GetSingle(r => r.Id == id);
            if (item == null)
            {
                return NotFound();
            }

            _service.Delete(item);
            await _context.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Checks if the item exists.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public virtual bool Exists(long? id)
        {
            return _service.GetAll().Count(r => r.Id == id) > 0;
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
                if (_context != null)
                    _context.Dispose();

                if (_service != null)
                    _service.Repository.Dispose();

                if (Proxy != null)
                    Proxy.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}