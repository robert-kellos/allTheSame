using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;
using Accushield.Entity.Model;
using Accushield.WebAPI.Controllers.Base;
using Accushield.WebAPI.Models;

namespace Accushield.WebAPI.Controllers
{
    /// <summary>
    ///     Role_PermissionController to expose REST service endpoints for Role_Permission data.
    /// </summary>
    /// <note>
    ///     Initializes a new instance of the controller for this REST service.
    ///     The database context reference may be accessed as Context.[method]; Ex: Service.Get();
    ///     The web service reference may be accessed as Service.[method]; Ex: Context.SaveChanges();
    ///     The data repository reference may be accessed as Proxy.[DbSet]; Ex: Proxy.Users;
    /// </note>
    [RoutePrefix("api/Role_Permission")]
    //[Authorize(Roles = AppConstants.PermissionCode.ViewVendor)]
    public class Role_PermissionController : BaseApiController<Role_Permission>
    {
        #region Common CRUD

        //
        //

        /// <summary>
        ///     Get all the items, of this type, from the database.
        /// </summary>
        /// <returns>
        ///     Item a List or Array of the objects returned.
        /// </returns>
        [ResponseType(typeof(IEnumerable<Role_Permission>))]
        [HttpGet]
        public override IEnumerable<Role_Permission> Get()
        {
            return base.Get();
        }

        // GET: api/Role_Permission/5
        /// <summary>
        ///     Get the item by id from the database.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        ///     Item object returned.
        /// </returns>
        [ResponseType(typeof(Role_Permission))]
        [HttpGet]
        public override IHttpActionResult GetById(long? id)
        {
            return base.GetById(id);
        }

        // PUT: api/Role_Permission/5
        /// <summary>
        ///     Put the edited item in the database.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="item">The item.</param>
        /// <returns>
        ///     bool: success/fail in a returned wrapper.
        /// </returns>
        [ResponseType(typeof(BoolReturnModel))]
        [HttpPut]
        public override IHttpActionResult Put(long? id, [FromBody] Role_Permission item)
        {
            return base.Put(id, item);
        }

        // POST: api/Role_Permission
        /// <summary>
        ///     Post a new item into the database.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>
        ///     Newly created Id in a returned wrapper.
        /// </returns>
        [ResponseType(typeof(IdReturnModel))]
        [HttpPost]
        public override IHttpActionResult Post([FromBody] Role_Permission item)
        {
            return base.Post(item);
        }

        // DELETE: api/Role_Permission/5
        /// <summary>
        ///     Deletes the item from the database.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        ///     bool: success/fail in a returned wrapper.
        /// </returns>
        [ResponseType(typeof(BoolReturnModel))]
        [HttpDelete]
        public override IHttpActionResult Delete(long? id)
        {
            return base.Delete(id);
        }

        // GET: api/{controller}/Exists/5
        /// <summary>
        ///     Check if the item exists.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [Route("Exists/{id}")] //--> requires a unique route
        [HttpGet]
        [ResponseType(typeof(BoolReturnModel))]
        public override bool Exists(long? id)
        {
            return base.Exists(id);
        }

        //
        //

        #endregion Common CRUD
    }
}