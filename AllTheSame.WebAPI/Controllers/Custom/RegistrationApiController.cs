using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using AllTheSame.Entity.Model;
using AllTheSame.Repository.Common;
using AllTheSame.Repository.UserData.implementation;
using AllTheSame.Repository.UserData.interfaces;
using AllTheSame.Service;
using AllTheSame.Service.Common;
using AllTheSame.Service.Implementation;
using AllTheSame.Service.Interfaces;
using AllTheSame.WebAPI.Models;

namespace AllTheSame.WebAPI.Controllers.Custom
{
    /// <summary>
    ///     IRegistrationController
    /// </summary>
    public interface IRegistrationController
    {
        //select Industry from list
        /// <summary>
        ///     GetIndustryList
        /// </summary>
        /// <returns></returns>
        IHttpActionResult GetIndustryList();

        //select OrgType from list
        /// <summary>
        ///     GetOrgTypeList
        /// </summary>
        /// <returns></returns>
        IHttpActionResult GetOrgTypeList();

        //select Organizatione from list
        /// <summary>
        ///     GetOrganizationList
        /// </summary>
        /// <returns></returns>
        IHttpActionResult GetOrganizationList();

        //select Vendor type from list
        /// <summary>
        ///     GetVendorTypeList
        /// </summary>
        /// <returns></returns>
        IHttpActionResult GetVendorTypeList();

        /*

         VendorCredDocuments, Requirements, Workers may come here
         
        */

        //enter name/pass vendor credentials
        /// <summary>
        ///     SetVendorCredentials
        /// </summary>
        /// <param name="vendorCredential"></param>
        /// <returns></returns>
        IHttpActionResult SetVendorCredentials(VendorCredential vendorCredential);

        /// <summary>
        ///     SetNewVendorData
        /// </summary>
        /// <param name="industryId"></param>
        /// <param name="orgTypeId"></param>
        /// <param name="orgId"></param>
        /// <param name="vendorTypeId"></param>
        /// <param name="addressId"></param>
        /// <param name="vendor"></param>
        /// <param name="vendorCredential"></param>
        /// <returns></returns>
        IHttpActionResult SetNewVendorData(int industryId, int orgTypeId, int orgId, int vendorTypeId, int addressId,
            Vendor vendor,
            VendorCredential vendorCredential);
    }

    /// <summary>
    ///     RegistrationApiController
    /// </summary>
    public class RegistrationApiController : ApiController, IRegistrationController
    {
        /// <summary>
        ///     Initializes a new instance of the class.
        /// </summary>
        public RegistrationApiController()
        {
            _context = new Entity.Model.AllTheSameDbContext();

            _vendorCredentialService = new VendorCredentialService(new UnitOfWork(_context),
                new VendorCredentialRepository(_context));
            _industryService = new IndustryService(new UnitOfWork(_context), new IndustryRepository(_context));
            _organizationService = new OrganizationService(new UnitOfWork(_context),
                new OrganizationRepository(_context));
            _orgTypeService = new OrgTypeService(new UnitOfWork(_context), new OrgTypeRepository(_context));
            _vendorService = new VendorService(new UnitOfWork(_context), new VendorRepository(_context));
            _vendorTypeService = new VendorTypeService(new UnitOfWork(_context), new VendorTypeRepository(_context));
            _vendorAdminService = new VendorAdminService(new UnitOfWork(_context), new VendorAdminRepository(_context));
            _vendorWorkerService = new VendorWorkerService(new UnitOfWork(_context),
                new VendorWorkerRepository(_context));
            _vendorCredDocumentService = new VendorCredDocumentService(new UnitOfWork(_context),
                new VendorCredDocumentRepository(_context));

            _addressService = new AddressService(new UnitOfWork(_context), new AddressRepository(_context));

            //var vendorCredentialList = _context.VendorCredentials;
            //var industryList = _context.Industries;
            //var organizationList = _context.Organizations;
            //var orgTypeList = _context.OrgTypes;
            //var vendorList = _context.Vendors;
            //var vendorTypeList = _context.VendorTypes;
            //var vendorAdminList = _context.VendorAdmins;
            //var vendorWorkerList = _context.VendorWorkers;
            //var vendorCredDocumentList = _context.VendorCredDocuments;

            //var addressList = _context.Addresses;

            //var c = Repository<Person>.Instance.Count;
            //var c2 = Repository<Person>.Instance.Count;
            //var s = new ServiceProxy<Person,IPersonRepository>().Count;
            
        }

        /// <summary>
        /// </summary>
        /// <param name="vendorCredential"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [ResponseType(typeof (BoolReturnModel))]
        [HttpPost]
        [Route("api/Registration/SetVendorCredentials")]
        public IHttpActionResult SetVendorCredentials(VendorCredential vendorCredential)
        {
            //temp, place-holder until finalize methods for screen
            throw new NotImplementedException();
        }

        /// <summary>
        ///     SetNewVendorData
        /// </summary>
        /// <param name="industryId"></param>
        /// <param name="orgTypeId"></param>
        /// <param name="orgId"></param>
        /// <param name="vendorTypeId"></param>
        /// <param name="addressId"></param>
        /// <param name="vendor"></param>
        /// <param name="vendorCredential"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [ResponseType(typeof (IdReturnModel))]
        [HttpPost]
        [Route("api/Registration/SetNewVendorData")]
        public IHttpActionResult SetNewVendorData(int industryId, int orgTypeId, int orgId, int vendorTypeId,
            int addressId, Vendor vendor, VendorCredential vendorCredential)
        {
            //temp, place-holder until finalize methods for screen
            throw new NotImplementedException();
        }

        #region Single Items

        //
        /// <summary>
        ///     Gets the address by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [ResponseType(typeof (Address))]
        [HttpGet]
        [Route("api/Registration/VendorAddress/{id}")]
        public IHttpActionResult GetAddressById(int id)
        {
            var item = _addressService.FindBy(r => r.Id == id).SingleOrDefault();
            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        //

        #endregion Single Items

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
                _vendorCredentialService?.Dispose();
                _industryService?.Dispose();
                _organizationService?.Dispose();
                _orgTypeService?.Dispose();
                _vendorService?.Dispose();
                _vendorTypeService?.Dispose();
                _vendorAdminService?.Dispose();
                _vendorWorkerService?.Dispose();
                _vendorCredDocumentService?.Dispose();
                _addressService?.Dispose();

                _context?.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Properties/Fields

        /// <summary>
        ///     The database context refernce.
        /// </summary>
        private readonly Entity.Model.AllTheSameDbContext _context;

        /// <summary>
        ///     The service reference.
        /// </summary>
        private readonly IEntityService<VendorCredential, IGenericRepository<VendorCredential>> _vendorCredentialService;

        /// <summary>
        ///     The _industry service
        /// </summary>
        private readonly IEntityService<Industry, IGenericRepository<Industry>> _industryService;

        /// <summary>
        ///     The _organization service
        /// </summary>
        private readonly IEntityService<Organization, IGenericRepository<Organization>> _organizationService;

        /// <summary>
        ///     The _org type service
        /// </summary>
        private readonly IEntityService<OrgType, IGenericRepository<OrgType>> _orgTypeService;

        /// <summary>
        ///     The _vendor service
        /// </summary>
        private readonly IEntityService<Vendor, IGenericRepository<Vendor>> _vendorService;

        /// <summary>
        ///     The _vendor type service
        /// </summary>
        private readonly IEntityService<VendorType, IGenericRepository<VendorType>> _vendorTypeService;

        /// <summary>
        ///     The _vendor admin service
        /// </summary>
        private readonly IEntityService<VendorAdmin, IGenericRepository<VendorAdmin>> _vendorAdminService;

        /// <summary>
        ///     The _vendor worker service
        /// </summary>
        private readonly IEntityService<VendorWorker, IGenericRepository<VendorWorker>> _vendorWorkerService;

        /// <summary>
        ///     The _vendor cred document service
        /// </summary>
        private readonly IEntityService<VendorCredDocument, IGenericRepository<VendorCredDocument>>
            _vendorCredDocumentService;

        /// <summary>
        ///     The _address service
        /// </summary>
        private readonly IEntityService<Address, IGenericRepository<Address>> _addressService;

        //

        #endregion Properties/Fields

        #region Type Lists

        //
        /// <summary>
        ///     Gets the vendor type list.
        /// </summary>
        /// <returns></returns>
        [ResponseType(typeof (IEnumerable<VendorType>))]
        [HttpGet]
        [Route("api/Registration/VendorList")]
        public IHttpActionResult GetVendorTypeList()
        {
            var item = _vendorTypeService.GetAll();
            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        /// <summary>
        ///     Gets the org type list.
        /// </summary>
        /// <returns></returns>
        [ResponseType(typeof (IEnumerable<OrgType>))]
        [HttpGet]
        [Route("api/Registration/OrgTypeList")]
        public IHttpActionResult GetOrgTypeList()
        {
            var item = _orgTypeService.GetAll();


            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        /// <summary>
        ///     Gets the industry list.
        /// </summary>
        /// <returns></returns>
        [ResponseType(typeof (IEnumerable<Industry>))]
        [HttpGet]
        [Route("api/Registration/IndustryList")]
        public IHttpActionResult GetIndustryList()
        {
            var item = _industryService.GetAll();
            if (item == null)
            {
                return NotFound();
            }

            //var pL = PagedList<OrgType>.Instance.GetSortedFieldFilterResult("Code");

            return Ok(item);
        }

        /// <summary>
        ///     Gets the organization list.
        /// </summary>
        /// <returns></returns>
        [ResponseType(typeof (IEnumerable<Organization>))]
        [HttpGet]
        [Route("api/Registration/OrganizationList")]
        public IHttpActionResult GetOrganizationList()
        {
            var item = _industryService.GetAll();
            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        /// <summary>
        ///     Gets the vendor cred document list.
        /// </summary>
        /// <returns></returns>
        [ResponseType(typeof (IEnumerable<VendorCredDocument>))]
        [HttpGet]
        [Route("api/Registration/VendorCredDocumentList")]
        public IHttpActionResult GetVendorCredDocumentList()
        {
            var item = _vendorCredDocumentService.GetAll();
            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        //

        #endregion Type Lists
    }
}