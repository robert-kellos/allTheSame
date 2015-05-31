using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;
using AllTheSame.Entity.Model;
using AllTheSame.Repository.Common;
using AllTheSame.Repository.UserData.interfaces;
using AllTheSame.WebAPI.Controllers.Base;
using AllTheSame.WebAPI.Models;

namespace AllTheSame.WebAPI.Controllers
{
    /// <summary>
    /// PersonController to expose REST service endpoints for Person data.
    /// </summary>
    /// <note>
    /// Initializes a new instance of the controller for this REST service.
    /// The database context reference may be accessed as Context.[method]; Ex: Context.SaveChanges();
    /// The web service reference may be accessed as Service.[method]; Ex: Service.Get();
    /// The data repository reference may be accessed as Proxy.[DbSet]; Ex: Proxy.Users;
    /// </note>
    [RoutePrefix("api/Person")]
    //[Authorize(Roles = AppConstants.PermissionCode.ViewVendor)]
    public class PersonController : BaseApiController<Person>
    {
        #region Common CRUD

        //
        //
        /// <summary>
        /// The _context
        /// </summary>
        private AllTheSameDbContext _context;
        /// <summary>
        /// The _service
        /// </summary>
        private IPersonRepository _service;
        /// <summary>
        /// The _unit of work
        /// </summary>
        private IUnitOfWork _unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="PersonController"/> class.
        /// </summary>
        public PersonController()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PersonController"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="service">The service.</param>
        public PersonController(IUnitOfWork unitOfWork, IPersonRepository service)
        {
            _service = service;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Get all the items, of this type, from the database.
        /// </summary>
        /// <returns>
        /// Item a List or Array of the objects returned.
        /// </returns>
        [ResponseType(typeof (IEnumerable<Person>))]
        [HttpGet]
        public override IEnumerable<Person> Get()
        {
            return base.Get();
        }

        // GET: api/Person/5
        /// <summary>
        /// Get the item by id from the database.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// Item object returned.
        /// </returns>
        [ResponseType(typeof (Person))]
        [HttpGet]
        public override IHttpActionResult GetById(long? id)
        {
            return base.GetById(id);
        }

        // PUT: api/Person/5
        /// <summary>
        /// Put the edited item in the database.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="item">The item.</param>
        /// <returns>
        /// bool: success/fail in a returned wrapper.
        /// </returns>
        [ResponseType(typeof (IHttpActionResult))]
        [HttpPut]
        public override IHttpActionResult Put(long? id, [FromBody] Person item)
        {
            return base.Put(id, item);
        }

        // POST: api/Person
        /// <summary>
        /// Post a new item into the database.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>
        /// Newly created Id in a returned wrapper.
        /// </returns>
        [ResponseType(typeof (IHttpActionResult))]
        [HttpPost]
        public override IHttpActionResult Post([FromBody] Person item)
        {
            return base.Post(item);
        }

        // DELETE: api/Person/5
        /// <summary>
        /// Deletes the item from the database.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// bool: success/fail in a returned wrapper.
        /// </returns>
        [ResponseType(typeof (IHttpActionResult))]
        [HttpDelete]
        public override IHttpActionResult Delete(long? id)
        {
            return base.Delete(id);
        }

        // GET: api/{controller}/Exists/5
        /// <summary>
        /// Check if the item exists.
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