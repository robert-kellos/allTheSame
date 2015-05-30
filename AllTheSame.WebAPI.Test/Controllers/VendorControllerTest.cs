using System.Collections.Generic;
using System.Web.Mvc;
using Accushield.Entity.Model;
using Accushield.Service.Interfaces;
using Accushield.WebAPI.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace Accushield.WebAPI.Test.Controllers
{
    [TestClass]
    public class VendorControllerTest : IDisposable
    {
        private List<Vendor> _listVendor;
        private VendorMvcController _objController;
        private Mock<IVendorService> _objServiceMock;
        private int _listCount = -1;

        [TestInitialize]
        public void Initialize()
        {
            _objServiceMock = new Mock<IVendorService>();
            _objController = new VendorMvcController(_objServiceMock.Object);
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
            //Arrange
            _objServiceMock.Setup(x => x.GetAll()).Returns(_listVendor);

            //Act
            var viewResult = _objController.VendorIndex() as ViewResult;
            var result = (viewResult.Model) as List<Vendor>;

            //Assert
            Assert.IsNotNull(result);
            _listCount = result.Count;

            Assert.IsTrue(_listCount >= 0);
        }

        [TestMethod]
        public void Valid_Vendor_Create()
        {
            //Arrange
            var vendor = new Vendor {Name = "ACME2"};

            //Act
            var result = (RedirectToRouteResult) _objController.Create(vendor);

            //Assert 
            _objServiceMock.Verify(m => m.Add(vendor), Times.Once);
            Assert.AreEqual("VendorIndex", result.RouteValues["action"]);
        }

        [TestMethod]
        public void Invalid_Vendor_Create()
        {
            // Arrange
            var vendor = new Vendor {Name = ""};
            _objController.ModelState.AddModelError("Error", "Something went wrong");

            //Act
            var result = (ViewResult) _objController.Create(vendor);

            //Assert
            _objServiceMock.Verify(m => m.Repository.Add(vendor), Times.Never);
            Assert.AreEqual("", result.ViewName);
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