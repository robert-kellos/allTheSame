using System.Linq;
using Accushield.Repository.UserData.implementation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Accushield.Entity.Model;

namespace Accushield.WebAPI.Test.Entities.Vendor
{
    [TestClass]
    public class VendorRepositoryTestWithDb : IDisposable
    {
        private AccushieldDbContext _databaseContext;
        private VendorRepository _objRepo;
        private int _listCount = -1;

        [TestInitialize]
        public void Initialize()
        {
            _databaseContext = new AccushieldDbContext();
            _objRepo = new VendorRepository(_databaseContext);
        }

        [TestMethod]
        public void Vendor_Repository_Get_ALL()
        {
            //Act
            var result = _objRepo.GetAll().ToList();

            //Assert
            Assert.IsNotNull(result);
            _listCount = result.Count;

            Assert.IsTrue(_listCount >= 0);
        }

        [TestMethod]
        public void Vendor_Repository_Create_List_Delete()
        {
            //get quantity before actions
            var lst = _objRepo.GetAll().ToList();
            var initialCount = lst.Count;

            //Arrange
            var vendor = new Accushield.Entity.Model.Vendor { Name = string.Format("ACME_{0}", initialCount), CreatedOn = DateTime.UtcNow };

            //OrgId = NULL

            //Act
            var result = _objRepo.Add(vendor);
            if (result != null)
            {
                //result.CreatedOn = DateTime.Now;
                var res = _databaseContext.SaveChanges();
                Assert.IsTrue(res == 1);
            }

            lst = _objRepo.GetAll().ToList();

            //Assert
            Assert.IsNotNull(lst);
            _listCount = lst.Count;

            Assert.IsTrue(_listCount > initialCount);
            Assert.AreEqual(string.Format("ACME_{0}", initialCount), lst.Last().Name);

            //Remove last added obj
            var dP = _objRepo.Delete(vendor);
            _databaseContext.SaveChanges();

            lst = _objRepo.GetAll().ToList();

            //Assert
            Assert.IsNotNull(lst);
            Assert.AreEqual(initialCount, lst.Count);
        }

        /// <summary>
        /// Disposes all external resources.
        /// </summary>
        /// <param name="disposing">The dispose indicator.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_databaseContext != null)
                    _databaseContext.Dispose();
                if (_objRepo != null)
                    _objRepo.Dispose();
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