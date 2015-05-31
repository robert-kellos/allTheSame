using System.Data;
using System.Net;
using System.Web.Mvc;
using AllTheSame.Common.Core;
using AllTheSame.Common.Logging;
using AllTheSame.Entity.Model;
using AllTheSame.Repository.Common;
using AllTheSame.Repository.UserData.implementation;
using AllTheSame.Service.Implementation;
using AllTheSame.Service.Interfaces;
using System;

namespace AllTheSame.WebAPI.Controllers
{
    /// <summary>
    ///     VendorMvcController
    /// </summary>
    [System.Web.Http.RoutePrefix("api/VendorMvc")]
    //[Authorize(Roles = AppConstants.PermissionCode.ViewVendor)]
    public class VendorMvcController : Controller //ApiController
    {
        /// <summary>
        ///     The _context
        /// </summary>
        private readonly Entity.Model.AllTheSameDbContext _context;

        /// <summary>
        ///     The _vendor service
        /// </summary>
        private readonly IVendorService _service;

        /// <summary>
        ///     Initializes a new instance of the <see cref="VendorController" /> class.
        /// </summary>
        public VendorMvcController()
        {
            _context = new Entity.Model.AllTheSameDbContext();
            _service = new VendorService(new UnitOfWork(_context), new VendorRepository(_context));
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="VendorController" /> class.
        /// </summary>
        /// <param name="service">The vendor service.</param>
        public VendorMvcController(IVendorService service)
        {
            _service = service;
        }

        // GET: /Vendor/
        /// <summary>
        ///     Vendors the index.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult VendorIndex()
        {
            if (_service == null)
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);

            return View(_service.GetAll());
        }

        // GET: /Vendor/Details/5
        /// <summary>
        ///     Detailses the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpGet, ActionName("Details")]
        public ActionResult Details(long? id)
        {
            if (_service == null)
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var item = _service.GetSingle(p => p.Id == id);
            if (item == null)
            {
                return HttpNotFound();
            }

            return View(item);
        }

        // GET: /Vendor/Post
        /// <summary>
        ///     Creates this instance.
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            if (_service == null)
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);

            ViewBag.VendorId = new SelectList(_service.GetAll(), "Id", "Name");
            return View();
        }

        // POST: /Person/Post
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        ///     Creates the specified person.
        /// </summary>
        /// <param name="item">The person.</param>
        /// <returns></returns>
        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name")] Vendor item)
        {
            if (_service == null)
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            try
            {
                item.CreatedOn = DateTime.UtcNow;
                item.UpdatedOn = item.UpdatedOn == null ? DateTime.UtcNow : item.UpdatedOn;
                item.OrgId = null;//FK will be a prob until we get real data

                if (ModelState.IsValid)
                {
                    var added = _service.Add(item);

                    if (added != null)
                        _context.SaveChanges();

                    return RedirectToAction("VendorIndex");
                }
            }
            catch (DataException dex)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                Audit.Log.Error("Error on creation", dex);
                ModelState.AddModelError("",
                    "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }

            return View(item);
        }

        // GET: /Vendor/Post/5
        /// <summary>
        ///     Edits the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [ActionName("Edit")]
        public ActionResult Edit(long? id)
        {
            if (_service == null)
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var item = _service.GetById(id.Value);
            if (item == null)
            {
                return HttpNotFound();
            }

            return View(item);
        }

        // POST: /Vendor/Post/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        ///     Edits the specified vendor.
        /// </summary>
        /// <param name="item">The vendor.</param>
        /// <returns></returns>
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,OrgId")] Vendor item)
        {
            if (_service == null)
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            try
            {
                item.CreatedOn = item.CreatedOn == null ? DateTime.UtcNow : item.CreatedOn;
                item.UpdatedOn = DateTime.UtcNow;
                item.OrgId = null;//FK will be a prob until we get real data

                if (ModelState.IsValid)
                {
                    _service.Edit(item);

                    _context.SaveChanges();
                    return RedirectToAction("VendorIndex");
                }
            }
            catch (DataException dex)
            {
                Audit.Log.Error("Error on edit", dex);
                ModelState.AddModelError("",
                    "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }

            return View(item);
        }

        // GET: /Vendor/Delete/5
        /// <summary>
        ///     Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var vendor = _service.GetSingle(v => v.Id == id);
            if (vendor == null)
            {
                return HttpNotFound();
            }
            return View(vendor);
        }

        // POST: /Vendor/Delete/5
        /// <summary>
        ///     Deletes the confirmed.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            var item = _service.GetSingle(v => v.Id == id);

            item.CreatedOn = item.CreatedOn == null ? DateTime.UtcNow : item.CreatedOn;
            item.UpdatedOn = DateTime.UtcNow;
            item.OrgId = null;//FK will be a prob until we get real data

            _service.Delete(item);

            return RedirectToAction("VendorIndex");
        }

        /// <summary>
        ///     Releases unmanaged resources and optionally releases managed resources.
        /// </summary>
        /// <param name="disposing">
        ///     true to release both managed and unmanaged resources; false to release only unmanaged
        ///     resources.
        /// </param>
        protected override void Dispose(bool disposing)
        {
            if (_service != null && !disposing)
            {
                _service?.Dispose();

                _context?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}