using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;
using AllTheSame.Entity.Model;
using AllTheSame.WebAPI.Controllers.Base;
using AllTheSame.WebAPI.Models;

namespace AllTheSame.WebAPI.Controllers
{
    /// <summary>
    ///     AlertController to expose REST service endpoints for Alert data.
    /// </summary>
    /// <note>
    ///     Initializes a new instance of the controller for this REST service.
    ///     The database context reference may be accessed as Context.[method]; Ex: Context.SaveChanges();
    ///     The web service reference may be accessed as Service.[method]; Ex: Service.Get();
    ///     The data repository reference may be accessed as Proxy.[DbSet]; Ex: Proxy.Users;
    /// </note>
    [RoutePrefix("api/Alert")]
    //[Authorize(Roles = AppConstants.PermissionCode.ViewVendor)]
    public class AlertController : BaseApiController<Alert>
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
        [ResponseType(typeof (IEnumerable<Alert>))]
        [HttpGet]
        public override IEnumerable<Alert> Get()
        {
            return base.Get();
        }

        // GET: api/Alert/5
        /// <summary>
        ///     Get the item by id from the database.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        ///     Item object returned.
        /// </returns>
        [ResponseType(typeof (Alert))]
        [HttpGet]
        public override IHttpActionResult GetById(long? id)
        {
            return base.GetById(id);
        }

        // PUT: api/Alert/5
        /// <summary>
        ///     Put the edited item in the database.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="item">The item.</param>
        /// <returns>
        ///     bool: success/fail in a returned wrapper.
        /// </returns>
        [ResponseType(typeof (IHttpActionResult))]
        [HttpPut]
        public override IHttpActionResult Put(long? id, [FromBody] Alert item)
        {
            return base.Put(id, item);
        }

        // POST: api/Alert
        /// <summary>
        ///     Post a new item into the database.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>
        ///     Newly created Id in a returned wrapper.
        /// </returns>
        [ResponseType(typeof (IHttpActionResult))]
        [HttpPost]
        public override IHttpActionResult Post([FromBody] Alert item)
        {
            return base.Post(item);
        }

        // DELETE: api/Alert/5
        /// <summary>
        ///     Deletes the item from the database.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        ///     bool: success/fail in a returned wrapper.
        /// </returns>
        [ResponseType(typeof (IHttpActionResult))]
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
        [ResponseType(typeof (BoolReturnModel))]
        public override bool Exists(long? id)
        {
            return base.Exists(id);
        }

        //
        //

        #endregion Common CRUD
    }
}