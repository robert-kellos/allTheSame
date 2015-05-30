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
    public class PersonControllerTest : IDisposable
    {
        private List<Person> _listPerson;
        private PersonMvcController _objController;
        private Mock<IPersonService> _objServiceMock;

        [TestInitialize]
        public void Initialize()
        {
            _objServiceMock = new Mock<IPersonService>();
            _objController = new PersonMvcController(_objServiceMock.Object);
            _listPerson = new List<Person>
            {
                new Person {FirstName = "Bob"},
                new Person {FirstName = "Sally"},
                new Person {FirstName = "Freddy"}
            };
        }

        [TestMethod]
        public void Person_Get_All()
        {
            //Arrange
            _objServiceMock.Setup(x => x.GetAll()).Returns(_listPerson);

            //Act
            var viewResult = _objController.PersonIndex() as ViewResult;
            var result = (viewResult.Model) as List<Person>;

            //Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count > 0);
        }

        [TestMethod]
        public void Valid_Person_Create()
        {
            //Arrange
            var c = new Person {FirstName = "John", LastName="Smith", Email="a@b.com", CreatedOn= DateTime.UtcNow };

            //Act
            var result = (RedirectToRouteResult) _objController.Create(c);

            //Assert
            _objServiceMock.Verify(m => m.Add(c), Times.Once);
            Assert.AreEqual("PersonIndex", result.RouteValues["action"]);
        }

        [TestMethod]
        public void Invalid_Person_Create()
        {
            // Arrange
            var c = new Person {FirstName = ""};
            _objController.ModelState.AddModelError("Error", "Something went wrong");

            //Act
            var result = (ViewResult) _objController.Create(c);

            //Assert
            _objServiceMock.Verify(m => m.Add(c), Times.Never);
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
                {
                    _objController.Dispose();
                }
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