using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using AllTheSame.Common.Core;
using AllTheSame.Common.Logging;
using AllTheSame.Entity.Model;
using AllTheSame.Repository.UserData.implementation;
using AllTheSame.Repository.UserData.interfaces;
using AllTheSame.Service;
using AllTheSame.Service.Common;

namespace AllTheSame.WebAPI.Controllers
{
    /// <summary>
    ///     AddressProxyController
    /// </summary>
    public class AddressProxyController : ApiController //BaseApiController<Address, AddressRepository>
    {
        private readonly AllTheSameDbContext _context;
        private readonly IEntityService<Address, IAddressRepository> _serviceProxy;

        /// <summary>
        ///     Initializes a new instance of the class.
        /// </summary>
        public AddressProxyController()
        {
            _context = new AllTheSameDbContext();
            _serviceProxy = ServiceProxy.GetServiceRefernce<Address, IAddressRepository>(new AddressRepository(_context));
        }

        // GET: api/AddressProxy
        /// <summary>
        ///     Gets the addresses.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Address> GetAddresses()
        {
            return _serviceProxy.GetAll();
        }

        // GET: api/AddressProxy/5
        /// <summary>
        ///     Gets the address.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [ResponseType(typeof (Address))]
        public async Task<IHttpActionResult> GetAddress(int id)
        {
            //var address = await _context.Addresses.FindAsync(id);
            var address = await _serviceProxy.FindByAsync(r => r.Id == id);
            if (address == null)
            {
                return NotFound();
            }

            return Ok(address);
        }

        // PUT: api/AddressProxy/5
        /// <summary>
        ///     Puts the address.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="address">The address.</param>
        /// <returns></returns>
        [ResponseType(typeof (void))]
        public async Task<IHttpActionResult> PutAddress(int id, Address address)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != address.Id)
            {
                return BadRequest();
            }

            _context.Entry(address).State = EntityState.Modified;

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

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/AddressProxy
        /// <summary>
        ///     Posts the address.
        /// </summary>
        /// <param name="address">The address.</param>
        /// <returns></returns>
        [ResponseType(typeof (Address))]
        public async Task<IHttpActionResult> PostAddress(Address address)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //_context.Addresses.Add(address);
            _serviceProxy.Add(address);
            await _context.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new {id = address.Id}, address);
        }

        // DELETE: api/AddressProxy/5
        /// <summary>
        ///     Deletes the address.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [ResponseType(typeof (Address))]
        public async Task<IHttpActionResult> DeleteAddress(int id)
        {
            //var address = await _context.Addresses.FindAsync(id);
            var address = _serviceProxy.FindBy(r => r.Id == id).SingleOrDefault();
            if (address == null)
            {
                return NotFound();
            }

            //_context.Addresses.Remove(address);
            _serviceProxy.Delete(address);
            await _context.SaveChangesAsync();

            return Ok(address);
        }

        /// <summary>
        ///     Releases the unmanaged resources that are used by the object and, optionally, releases the managed resources.
        /// </summary>
        /// <param name="disposing">
        ///     true to release both managed and unmanaged resources; false to release only unmanaged
        ///     resources.
        /// </param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context?.Dispose();

                _serviceProxy?.Dispose();
            }
            base.Dispose(disposing);
        }

        /// <summary>
        ///     Addresses the exists.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        private bool Exists(int id)
        {
            return _serviceProxy.FindBy(r => r.Id == id).SingleOrDefault() != null;
        }
    }
}