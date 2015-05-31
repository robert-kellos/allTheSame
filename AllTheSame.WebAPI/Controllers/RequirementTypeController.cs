using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;
using AllTheSame.Entity.Model;
using AllTheSame.WebAPI.Controllers.Base;
using AllTheSame.WebAPI.Models;

namespace AllTheSame.WebAPI.Controllers
{
    /// <summary>
    ///     RequirementTypeController to expose REST service endpoints for RequirementType data.
    /// </summary>
    /// <note>
    ///     Initializes a new instance of the controller for this REST service.
    ///     The database context reference may be accessed as Context.[method]; Ex: Context.SaveChanges();
    ///     The web service reference may be accessed as Service.[method]; Ex: Service.Get();
    ///     The data repository reference may be accessed as Proxy.[DbSet]; Ex: Proxy.Users;
    /// </note>
    [RoutePrefix("api/RequirementType")]
    //[Authorize(Roles = AppConstants.PermissionCode.RegisterVendor)]
    public class RequirementTypeController : BaseApiController<RequirementType>
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
        [ResponseType(typeof (IEnumerable<RequirementType>))]
        [HttpGet]
        public override IEnumerable<RequirementType> Get()
        {
            return base.Get();
        }

        // GET: api/RequirementType/5
        /// <summary>
        ///     Get the item by id from the database.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        ///     Item object returned.
        /// </returns>
        [ResponseType(typeof (RequirementType))]
        [HttpGet]
        public override IHttpActionResult GetById(long? id)
        {
            return base.GetById(id);
        }

        // PUT: api/RequirementType/5
        /// <summary>
        ///     Put the edited item in the database.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="item">The item.</param>
        /// <returns>
        ///     bool: success/fail in a returned wrapper.
        /// </returns>
        [ResponseType(typeof (BoolReturnModel))]
        [HttpPut]
        public override IHttpActionResult Put(long? id, [FromBody] RequirementType item)
        {
            return base.Put(id, item);
        }

        // POST: api/RequirementType
        /// <summary>
        ///     Post a new item into the database.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>
        ///     Newly created Id in a returned wrapper.
        /// </returns>
        [ResponseType(typeof (IdReturnModel))]
        [HttpPost]
        public override IHttpActionResult Post([FromBody] RequirementType item)
        {
            return base.Post(item);
        }

        // DELETE: api/RequirementType/5
        /// <summary>
        ///     Deletes the item from the database.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        ///     bool: success/fail in a returned wrapper.
        /// </returns>
        [ResponseType(typeof (BoolReturnModel))]
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