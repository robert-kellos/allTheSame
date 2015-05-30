using System.Collections.Generic;
using System.Linq;
using Accushield.Entity.Model;
using Accushield.Service.Interfaces;
using Accushield.WebAPI.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Web.Http;
using System.Web.Http.Results;
using System.Net;

namespace Accushield.WebAPI.Test.Controllers
{
    [TestClass]
    public class VendorApiControllerTest : IDisposable
    {
        private List<Vendor> _listVendor;
        private VendorController _objController;
        private int _listCount = -1;

        [TestInitialize]
        public void Initialize()
        {
            _objController = new VendorController();
            _listVendor = new List<Vendor>
            {
                new Vendor {Name = "ACME"},
                new Vendor {Name = "USA"},
                new Vendor {Name = "NewJack"}
            };
        }

        [TestMethod]
        public void Vendor_Get_All()
        {
            //Act
            var list = _objController.Get().ToList();

            //Assert
            Assert.IsNotNull(list);
            _listCount = list.Count;

            Assert.IsTrue(_listCount >= 0);
        }

        [TestMethod]
        public void Valid_Vendor_Create()
        {
            var list = _objController.Get().ToList();
            var increment = 0;

            //grab the count to tag on the name, so will be unique
            if (list != null)
                increment = list.Count;

            //Arrange
            var vendor = new Vendor { Name = string.Format("USURO_{0}", increment), CreatedOn = DateTime.UtcNow };

            //Act
            var obj = _objController.Post(vendor);

            //Assert 
            Assert.IsNotNull(obj);

            var lst = _objController.Get();

            Assert.AreEqual(string.Format("USURO_{0}", increment), lst.Last().Name);

            var res = (obj as IHttpActionResult);
            Assert.IsTrue(res == (IHttpActionResult)obj && _objController.ModelState.IsValid);
        }

        [TestMethod]
        public void Invalid_Vendor_Create()
        {
            // Arrange
            var v = new Vendor {Name = ""};
            _objController.ModelState.AddModelError("Error", "Something went wrong");

            //Act
            var obj = _objController.Post(v);

            //Assert
            var res = (obj as IHttpActionResult);
            Assert.IsTrue(res == (IHttpActionResult)obj && obj is InvalidModelStateResult);
        }

        /// <summary>
        /// Disposes all external resources.
        /// </summary>
        /// <param name="disposing">The dispose indicator.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_objController != null)
                    _objController.Dispose();
            }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}