using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Routing;
using Accushield.Common.Core;
using Accushield.Common.Extensions;
using Accushield.Common.Interfaces.Generic;
using Accushield.Common.Logging;
using Accushield.Entity.Model;
using Accushield.Repository.Common;
using Accushield.Service;
using Accushield.Service.Common;
using Accushield.WebAPI.Models;

namespace Accushield.WebAPI.Controllers.Base
{
    /// <summary>
    /// ControllerProxy
    /// </summary>
    public class ControllerProxy
    {
        #region Local Fields/Properties

        
        /// <summary>
        /// The _context
        /// </summary>
        private readonly AccushieldDbContext _context;
        private static ControllerProxy _instance;


        public static ControllerProxy Instance
        {
            get
            {
                _instance = _instance ?? new ControllerProxy();

                //var _repository = new GenericRepository<TEntity>(Instance._dbContext);
                //var _service = _service ?? new EntityService<TEntity, TRepository>(Instance, _repository);
                
                return _instance;
            }
        }

        public ControllerProxy()
        {
            _context = new AccushieldDbContext();
        }

        /// <summary>
        /// The _url helper
        /// </summary>
        private UrlHelper _urlHelper;

        #region Properties
        //

        /// <summary>
        /// Gets the URL helper.
        /// </summary>
        /// <value>
        /// The URL helper.
        /// </value>
        protected UrlHelper UrlHelper
        {
            get { return _urlHelper ?? (_urlHelper = new UrlHelper()); }
        }

        //

        #endregion Properties

        //
        //

        #endregion Local Fields/Properties

        //protected BaseApiController(IEntityService<TEntity> service)
        //{
        //    _service = service;
        //}

        
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
        public IEnumerable<TEntity> Get()
        {
            return _service.GetAll();
        }

        /// <summary>
        /// FindByAsync
        /// </summary>
        /// <param name="where">The where.</param>
        /// <returns></returns>
        protected async Task<IEnumerable<TEntity>> FindByAsync(Expression<Func<TEntity, bool>> @where)
        {
            return await _service.FindByAsync(where);
        }

        // GET: api/{controller}/5
        /// <summary>
        /// Gets by id.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public IHttpActionResult GetById(int id)
        {
            var item = _service.GetSingle(r => r.Id == id);
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
        public IHttpActionResult Put(int id, [FromBody]TEntity item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != item.Id)
            {
                return BadRequest();
            }

            _context.Entry(item).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
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

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/{controller}/Post
        /// <summary>
        /// Add the item via POST.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        public IHttpActionResult Post([FromBody]TEntity item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _service.Add(item);
            _service.Save();

            return CreatedAtRoute("DefaultApi", new {id = item.Id}, item);
        }

        // DELETE: api/{controller}/Delete/5
        /// <summary>
        /// Deletes the item.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public IHttpActionResult Delete(int id)
        {
            var item = _service.Repository.GetSingle(r => r.Id == id);
            if (item == null)
            {
                return NotFound();
            }

            _service.Delete(item);
            _service.Save();

            return Ok(item);
        }

        // GET: api/{controller}/Exists/5
        /// <summary>
        /// Check if the item exists.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public bool Exists(int id)
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
                {
                    _context.Dispose();
                }

                //if (_service != null)
                //{
                //    _service.Dispose();
                //}
            }
            base.Dispose(disposing);
        }
    }
}