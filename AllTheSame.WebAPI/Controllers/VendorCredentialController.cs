using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using AllTheSame.Entity.Model;
using AllTheSame.WebAPI.Controllers.Base;
using AllTheSame.WebAPI.Models;

namespace AllTheSame.WebAPI.Controllers
{
    /// <summary>
    ///     VendorCredentialController to expose REST service endpoints for VendorCredential data.
    /// </summary>
    /// <note>
    ///     Initializes a new instance of the controller for this REST service.
    ///     The database context reference may be accessed as Context.[method]; Ex: Context.SaveChanges();
    ///     The web service reference may be accessed as Service.[method]; Ex: Service.Get();
    ///     The data repository reference may be accessed as Proxy.[DbSet]; Ex: Proxy.Users;
    /// </note>
    [RoutePrefix("api/VendorCredential")]
    //[Authorize(Roles = AppConstants.PermissionCode.RegisterVendor)]
    public class VendorCredentialController : BaseApiController<VendorCredential>
    {
        /// <summary>
        ///     Gets the requirement.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [ResponseType(typeof (Requirement))]
        [HttpGet]
        [Route("Requirement/{id}")]
        public IHttpActionResult GetRequirement([FromBody] int id)
        {
            var item = Proxy.VendorCredentials.Where(m => m.Id == id).Select(m => m.Requirement).SingleOrDefault();
            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        /// <summary>
        ///     Gets the user.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [ResponseType(typeof (User))]
        [HttpGet]
        [Route("User/{id}")]
        public IHttpActionResult GetUser([FromBody] int id)
        {
            var item = Proxy.VendorCredentials.Where(m => m.Id == id).Select(m => m.User).SingleOrDefault();
            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        /// <summary>
        ///     Gets the vendor cred document.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [ResponseType(typeof (VendorCredDocument))]
        [HttpGet]
        [Route("VendorCredDocument/{id}")]
        public IHttpActionResult GetVendorCredDocument([FromBody] int id)
        {
            var item = Proxy.VendorCredentials.Where(m => m.Id == id).Select(m => m.VendorCredDocuments);
            if (!item.Any())
            {
                return NotFound();
            }

            return Ok(item);
        }

        /// <summary>
        ///     Gets the vendor worker.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [ResponseType(typeof (User))]
        [HttpGet]
        [Route("VendorWorker/{id}")]
        public IHttpActionResult GetVendorWorker([FromBody] int id)
        {
            var item = Proxy.VendorCredentials.Where(m => m.Id == id).Select(m => m.VendorWorker).SingleOrDefault();
            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        #region Common CRUD

        //
        //

        /// <summary>
        ///     Get all the items, of this type, from the database.
        /// </summary>
        /// <returns>
        ///     Item a List or Array of the objects returned.
        /// </returns>
        [ResponseType(typeof (IEnumerable<VendorCredential>))]
        [HttpGet]
        public override IEnumerable<VendorCredential> Get()
        {
            return Service.GetAll();
        }

        // GET: api/VendorCredential/5
        /// <summary>
        ///     Get the item by id from the database.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        ///     Item object returned.
        /// </returns>
        [ResponseType(typeof (VendorCredential))]
        [HttpGet]
        public override IHttpActionResult GetById(long? id)
        {
            return base.GetById(id);
        }

        // PUT: api/VendorCredential/5
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
        public override IHttpActionResult Put(long? id, [FromBody] VendorCredential item)
        {
            return base.Put(id, item);
        }

        // POST: api/VendorCredential
        /// <summary>
        ///     Post a new item into the database.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>
        ///     Newly created Id in a returned wrapper.
        /// </returns>
        [ResponseType(typeof (IdReturnModel))]
        [HttpPost]
        public override IHttpActionResult Post([FromBody] VendorCredential item)
        {
            return base.Post(item);
        }

        // DELETE: api/VendorCredential/5
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