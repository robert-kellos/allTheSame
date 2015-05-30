using System.Collections.Generic;
using Accushield.Repository.Common;
using Accushield.Repository.UserData.interfaces;
using Accushield.Service.Implementation;
using Accushield.Service.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace Accushield.WebAPI.Test.Entities.Person
{
    [TestClass]
    public class PersonServiceTest : IDisposable
    {
        private List<Entity.Model.Person> _listPerson;
        private Mock<IPersonRepository> _mockRepository;
        private Mock<IUnitOfWork> _mockUnitWork;
        private IPersonService _service;

        [TestInitialize]
        public void Initialize()
        {
            _mockRepository = new Mock<IPersonRepository>();
            _mockUnitWork = new Mock<IUnitOfWork>();
            _service = new PersonService(_mockUnitWork.Object, _mockRepository.Object);

            _listPerson = new List<Entity.Model.Person>
            {
                new Entity.Model.Person {Id = 1, FirstName = "Bob"},
                new Entity.Model.Person {Id = 2, FirstName = "Sally"},
                new Entity.Model.Person {Id = 3, FirstName = "Freddy"}
            };
        }

        [TestMethod]
        public void Person_Get_All()
        {
            //Arrange
            _mockRepository.Setup(x => x.GetAll()).Returns(_listPerson);

            //Act
            var results = _service.Repository.GetAll() as List<Entity.Model.Person>;

            //Assert
            Assert.IsNotNull(results);
            Assert.IsTrue(results.Count > 0);
        }

        [TestMethod]
        public void Can_Add_Person()
        {
            //Arrange
            var Id = 1;
            var pers = new Entity.Model.Person {FirstName = "John", LastName="Smith", Email="a@b.com", CreatedOn = DateTime.UtcNow };

            _mockRepository.Setup(m => m.Add(pers)).Returns((Entity.Model.Person e) =>
            {
                e.Id = Id;
                return e;
            });

            //Act
            _service.Add(pers);

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