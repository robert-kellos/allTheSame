using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.OData;
using AllTheSame.Common.Core;
using AllTheSame.Common.Logging;
using AllTheSame.Entity.Model;
using AllTheSame.Service;

namespace AllTheSame.WebAPI.Controllers
{
    /*
    
     * The WebApiConfig class may require additional changes to add a route for this controller. 
     * Merge these statements into the Register method of the WebApiConfig class as applicable. 
     * Note that OData URLs are case sensitive.

    using System.Web.Http.OData.Builder;
    using System.Web.Http.OData.Extensions;
    using AllTheSame.Entity.Model;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<VendorCredential>("VendorCredentials");
    builder.EntitySet<Requirement>("Requirements"); 
    builder.EntitySet<User>("Users"); 
    builder.EntitySet<VendorCredDocument>("VendorCredDocuments"); 
    builder.EntitySet<VendorWorker>("VendorWorkers"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
     
    */

    /// <summary>
    ///     VendorCredentialController
    /// </summary>
    [RoutePrefix("api/VendorCredential")]
    //[Authorize(Roles = AppConstants.PermissionCode.ViewVendor)]
    public class VendorCredentialODataController : ODataController
    {
        /// <summary>
        ///     The _service proxy
        /// </summary>
        private readonly ServiceProxy _serviceProxy = new ServiceProxy();

        // GET: odata/VendorCredentials
        /// <summary>
        ///     Gets the vendor credentials.
        /// </summary>
        /// <returns></returns>
        [EnableQuery]
        public IQueryable<VendorCredential> GetVendorCredentials()
        {
            return _serviceProxy.VendorCredentials;
        }

        // GET: odata/VendorCredentials(5)
        /// <summary>
        ///     Gets the vendor credential.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        [EnableQuery]
        public SingleResult<VendorCredential> GetVendorCredential([FromODataUri] int key)
        {
            return
                SingleResult.Create(_serviceProxy.VendorCredentials.Where(vendorCredential => vendorCredential.Id == key));
        }

        // PUT: odata/VendorCredentials(5)
        /// <summary>
        ///     Puts the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="patch">The patch.</param>
        /// <returns></returns>
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<VendorCredential> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var vendorCredential = await _serviceProxy.VendorCredentials.FindAsync(key);
            if (vendorCredential == null)
            {
                return NotFound();
            }

            patch.Put(vendorCredential);

            try
            {
                await _serviceProxy.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException dcx)
            {
                if (!Exists(key))
                {
                    return NotFound();
                }
                Audit.Log.Error(AppConstants.ErrorMessages.DbUpdateConcurrencyExceptionMessage, dcx);

                throw;
            }

            return Updated(vendorCredential);
        }

        // POST: odata/VendorCredentials
        /// <summary>
        ///     Posts the specified vendor credential.
        /// </summary>
        /// <param name="vendorCredential">The vendor credential.</param>
        /// <returns></returns>
        public async Task<IHttpActionResult> Post(VendorCredential vendorCredential)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _serviceProxy.VendorCredentials.Add(vendorCredential);
            await _serviceProxy.SaveChangesAsync();

            return Created(vendorCredential);
        }

        // PATCH: odata/VendorCredentials(5)
        /// <summary>
        ///     Patches the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="patch">The patch.</param>
        /// <returns></returns>
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<VendorCredential> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var vendorCredential = await _serviceProxy.VendorCredentials.FindAsync(key);
            if (vendorCredential == null)
            {
                return NotFound();
            }

            patch.Patch(vendorCredential);

            try
            {
                await _serviceProxy.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException dcx)
            {
                if (!Exists(key))
                {
                    return NotFound();
                }
                Audit.Log.Error(AppConstants.ErrorMessages.DbUpdateConcurrencyExceptionMessage, dcx);

                throw;
            }

            return Updated(vendorCredential);
        }

        // DELETE: odata/VendorCredentials(5)
        /// <summary>
        ///     Deletes the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            var vendorCredential = await _serviceProxy.VendorCredentials.FindAsync(key);
            if (vendorCredential == null)
            {
                return NotFound();
            }

            _serviceProxy.VendorCredentials.Remove(vendorCredential);
            await _serviceProxy.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/VendorCredentials(5)/Requirement
        /// <summary>
        ///     Gets the requirement.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        [EnableQuery]
        public SingleResult<Requirement> GetRequirement([FromODataUri] int key)
        {
            return
                SingleResult.Create(_serviceProxy.VendorCredentials.Where(m => m.Id == key).Select(m => m.Requirement));
        }

        // GET: odata/VendorCredentials(5)/User
        /// <summary>
        ///     Gets the user.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        [EnableQuery]
        public SingleResult<User> GetUser([FromODataUri] int key)
        {
            return SingleResult.Create(_serviceProxy.VendorCredentials.Where(m => m.Id == key).Select(m => m.User));
        }

        // GET: odata/VendorCredentials(5)/VendorCredDocuments
        /// <summary>
        ///     Gets the vendor cred documents.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        [EnableQuery]
        public IQueryable<VendorCredDocument> GetVendorCredDocuments([FromODataUri] int key)
        {
            return _serviceProxy.VendorCredentials.Where(m => m.Id == key).SelectMany(m => m.VendorCredDocuments);
        }

        // GET: odata/VendorCredentials(5)/VendorWorker
        /// <summary>
        ///     Gets the vendor worker.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        [EnableQuery]
        public SingleResult<VendorWorker> GetVendorWorker([FromODataUri] int key)
        {
            return
                SingleResult.Create(_serviceProxy.VendorCredentials.Where(m => m.Id == key).Select(m => m.VendorWorker));
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
                if (_serviceProxy != null)
                    _serviceProxy.Dispose();
            }
            base.Dispose(disposing);
        }

        /// <summary>
        ///     Vendors the credential exists.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        private bool Exists(int key)
        {
            return _serviceProxy.VendorCredentials.Count(e => e.Id == key) > 0;
        }
    }
}