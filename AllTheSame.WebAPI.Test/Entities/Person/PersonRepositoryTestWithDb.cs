using System.Linq;
using Accushield.Repository.UserData.implementation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Accushield.WebAPI.Test.Entities.Person
{
    [TestClass]
    public class PersonRepositoryTestWithDb : IDisposable
    {
        [NonSerialized]
        private TestContext _databaseContext;
        private PersonRepository _objRepo;

        [TestInitialize]
        public void Initialize()
        {
            _databaseContext = new TestContext();
            _objRepo = new PersonRepository(_databaseContext);
        }

        [TestMethod]
        public void Person_Repository_Get_ALL()
        {
            //Act
            var result = _objRepo.GetAll().ToList();

            //Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count > 0);
        }

        [TestMethod]
        public void Person_Repository_Create_List_Delete()
        {
            //get quantity before actions
            var lst = _objRepo.GetAll().ToList();
            var initialCount = lst.Count;

            //Arrange
            var person = new Entity.Model.Person { FirstName = "John", LastName = "Doe", Email = "a@b.cd" };

            //Act
            var result = _objRepo.Add(person);
            _databaseContext.SaveChanges();

            lst = _objRepo.GetAll().ToList();

            //Assert
            Assert.AreEqual(initialCount + 1, lst.Count);
            Assert.AreEqual("John", lst.Last().FirstName);

            //Remove last added obj
            var dP = _objRepo.Delete(person);
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