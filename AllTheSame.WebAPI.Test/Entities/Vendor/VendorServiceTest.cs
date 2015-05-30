using System.Collections.Generic;
using Accushield.Repository.Common;
using Accushield.Repository.UserData.interfaces;
using Accushield.Service.Implementation;
using Accushield.Service.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace Accushield.WebAPI.Test.Entities.Vendor
{
    [TestClass]
    public class VendorServiceTest : IDisposable
    {
        private List<Entity.Model.Vendor> _listVendor;
        private Mock<IVendorRepository> _mockRepository;
        private Mock<IUnitOfWork> _mockUnitWork;
        private IVendorService _service;
        private int _listCount = -1;

        [TestInitialize]
        public void Initialize()
        {
            _mockRepository = new Mock<IVendorRepository>();
            _mockUnitWork = new Mock<IUnitOfWork>();
            _service = new VendorService(_mockUnitWork.Object, _mockRepository.Object);

            _listVendor = new List<Entity.Model.Vendor>
            {
                new Entity.Model.Vendor {Id = 1, Name = "ACME"},
                new Entity.Model.Vendor {Id = 2, Name = "USA"},
                new Entity.Model.Vendor {Id = 3, Name = "NewJack"}
            };
        }

        [TestMethod]
        public void Vendor_Get_All()
        {
            //Arrange
            _mockRepository.Setup(x => x.GetAll()).Returns(_listVendor);

            //Act
            var results = _service.GetAll() as List<Entity.Model.Vendor>;

            //Assert
            Assert.IsNotNull(results);
            _listCount = results.Count;

            Assert.IsTrue(_listCount >= 0);
        }

        [TestMethod]
        public void Can_Add_Vendor()
        {
            //Arrange
            var Id = 1;
            var pers = new Entity.Model.Vendor {Name = "ACME2"};

            _mockRepository.Setup(m => m.Add(pers)).Returns((Entity.Model.Vendor e) =>
            {
                e.Id = Id;
                return e;
            });

            //Act
            var added = _service.Add(pers);
            Assert.IsNotNull(added);

            //Assert
            Assert.AreEqual(Id, pers.Id);
            _mockUnitWork.Verify(m => m.Commit(), Times.Once);
        }

        public RetObj MyMethod(IObject obj)
        {
            var ret = obj.AnotherMethod();
            return ret;
        }

        public void Test()
        {
            //Arrange           
            var mockObj = new Mock<IObject>();

            var dummyVal = new RetObj();
            mockObj.Setup(x => x.AnotherMethod()).Returns(dummyVal);

            //Act
            var result = MyMethod(mockObj.Object);


            //Assert
            Assert.Equals(dummyVal, result);
        }

        /////////// dummy 

        /// <summary>
        ///     dummy start here
        /// </summary>
        public class RetObj
        {
        }

        public interface IObject
        {
            RetObj AnotherMethod();
        }

        /// <summary>
        /// Disposes all external resources.
        /// </summary>
        /// <param name="disposing">The dispose indicator.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_service != null)
                    _service.Dispose();
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