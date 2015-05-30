using System.Linq;
using Accushield.Entity.Model;
using Accushield.Repository.UserData.implementation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Accushield.WebAPI.Test.Entities.Address
{
    [TestClass]
    public class AddressRepositoryTestWithDb : IDisposable
    {
        [NonSerialized]
        private AccushieldDbContext _databaseContext;
        private AddressRepository _objRepo;

        [TestInitialize]
        public void Initialize()
        {
            _databaseContext = new AccushieldDbContext();
            _objRepo = new AddressRepository(_databaseContext);
        }

        [TestMethod]
        public void Address_Repository_Get_ALL()
        {
            //Act
            var result = _objRepo.GetAll().ToList();

            //Assert
            Assert.IsNotNull(result);

            //Assert.AreEqual(3, result.Count);
            //Assert.AreEqual("123 First Street", result[0].Line1);
            //Assert.AreEqual("1122 Valley Road", result[1].Line1);
            //Assert.AreEqual("9000 East Blvd", result[2].Line1);
        }

        [TestMethod]
        public void Address_Repository_Create_List_Delete()
        {
            //get quantity before actions
            var lst = _objRepo.GetAll().ToList();
            var initialCount = lst.Count;

            //Arrange - City, State PostalCode required
            var obj = new Entity.Model.Address {Line1 = "1000 Exit Ave", City = "NewCity", State="NY", PostalCode="11020", Country="USA"};

            //Act
            var result = _objRepo.Add(obj);
            _databaseContext.SaveChanges();

            lst = _objRepo.GetAll().ToList();

            //Assert
            Assert.AreEqual(initialCount + 1, lst.Count);
            Assert.AreEqual("NewCity", lst.Last().City);

            //Remove last added obj
            var dP = _objRepo.Delete(obj);
            _databaseContext.SaveChanges();

            lst = _objRepo.GetAll().ToList();

            //Assert
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