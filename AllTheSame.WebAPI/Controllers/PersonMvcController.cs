using System;
using System.Data;
using System.Net;
using System.Web.Mvc;
using AllTheSame.Common.Logging;
using AllTheSame.Entity.Model;
using AllTheSame.Repository.Common;
using AllTheSame.Repository.UserData.implementation;
using AllTheSame.Service.Implementation;
using AllTheSame.Service.Interfaces;

namespace AllTheSame.WebAPI.Controllers
{
    /// <summary>
    ///     Controller for Person data
    /// </summary>
    [System.Web.Http.RoutePrefix("api/PersonMvc")]
    public class PersonMvcController : Controller //ApiController
    {
        /// <summary>
        ///     The _person service
        /// </summary>
        private readonly Entity.Model.AllTheSameDbContext _context;

        /// <summary>
        ///     The _service
        /// </summary>
        private readonly IPersonService _service;

        /// <summary>
        ///     Initializes a new instance of the <see cref="PersonController" /> class.
        /// </summary>
        public PersonMvcController()
        {
            _context = new Entity.Model.AllTheSameDbContext();
            _service = new PersonService(new UnitOfWork(_context), new PersonRepository(_context));
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="PersonController" /> class.
        /// </summary>
        /// <param name="personService">The vendor service.</param>
        public PersonMvcController(IPersonService personService)
        {
            _service = personService;
        }

        // GET: /Person/
        /// <summary>
        ///     Persons the index.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult PersonIndex()
        {
            if (_service == null)
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);

            return View(_service.GetAll());
        }

        // GET: /Person/Details/5
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
            var person = _service.GetSingle(p => p.Id == id);
            if (person == null)
            {
                return HttpNotFound();
            }

            return View(person);
        }

        // GET: /Person/Post
        /// <summary>
        ///     Creates this instance.
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            if (_service == null)
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);

            ViewBag.Id = new SelectList(_service.GetAll(), "Id", "FirstName",
                "LastName, Email, MobilePhone");
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
        public ActionResult Create([Bind(Include = "FirstName,LastName,Email,MobilePhone,UpdatedOn")] Person item)
        {
            if (_service == null)
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            try
            {
                item.CreatedOn = DateTime.UtcNow;
                item.UpdatedOn = item.UpdatedOn == null ? DateTime.UtcNow : item.UpdatedOn;

                if (ModelState.IsValid)
                {
                    var added = _service.Add(item);

                    if (added == null) return RedirectToAction("PersonIndex");
                    var res = _context.SaveChanges();

                    return RedirectToAction("PersonIndex");
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

        // GET: /Person/Post/5
        /// <summary>
        ///     Edits the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public ActionResult Edit(long? id)
        {
            if (_service == null)
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var person = _service.GetById(id.Value);
            if (person == null)
            {
                return HttpNotFound();
            }

            return View(person);
        }

        // POST: /Person/Post/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        ///     Edits the specified person.
        /// </summary>
        /// <param name="item">The person.</param>
        /// <returns></returns>
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,Email,MobilePhone,CreatedOn,UpdatedOn")] Person item)
        {
            if (_service == null)
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            try
            {
                item.CreatedOn = item.CreatedOn == null ? DateTime.UtcNow : item.CreatedOn;
                item.UpdatedOn = DateTime.UtcNow;

                if (ModelState.IsValid)
                {
                    _service.Edit(item);

                    var res = _context.SaveChanges();
                    return RedirectToAction("PersonIndex");
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

        // GET: /Person/Delete/5

        /// <summary>
        ///     Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="saveChangesError">The save changes error.</param>
        /// <returns></returns>
        public ActionResult Delete(long? id, bool? saveChangesError = false)
        {
            if (_service == null)
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage =
                    "Delete failed. Try again, and if the problem persists see your system administrator.";
            }

            var person = _service.GetById(id.Value);
            if (person == null)
            {
                return HttpNotFound();
            }

            return View(person);
        }

        // POST: /Person/Delete/5
        /// <summary>
        ///     Deletes the confirmed.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            if (_service == null)
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            try
            {
                var item = _service.GetById(id);

                item.CreatedOn = item.CreatedOn == null ? DateTime.UtcNow : item.CreatedOn;
                item.UpdatedOn = DateTime.UtcNow;

                var deleted = _service.Delete(item);
                if (deleted != null)
                {
                    //deleted.UpdatedOn = DateTime.Now;
                    var res = _context.SaveChanges();
                }
            }
            catch (DataException dex)
            {
                Audit.Log.Error("Error on deletion", dex);
                return RedirectToAction("Delete", new {id, saveChangesError = true});
            }

            return RedirectToAction("PersonIndex");
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
                if (_context != null)
                {
                    _context.Dispose();
                }

                if (_service != null)
                {
                    _service.Dispose();
                }
            }
            base.Dispose(disposing);
        }
    }
}