using System;
using System.Linq;
using AllTheSame.Entity.Model;
using AllTheSame.WebAPI.Controllers.Base;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AllTheSame.WebAPITests.Controllers.Base
{
    [TestClass()]
    public class BaseApiControllerTests : BaseApiController<Person>
    {
        public void Init()
        {
            //
        }

        [TestMethod()]
        public void GetTest()
        {
            var list = Get();

            Assert.IsNotNull(list);
        }

        [TestMethod()]
        public void GetByIdTest()
        {
            const int id = 3;
            var result = GetById(id);
            Assert.IsNotNull(result);

            var item = Context.Set<Person>().SingleOrDefault(r => r.Id == id);
            Assert.IsNotNull(item);
        }

        [TestMethod()]
        public void PutTest()
        {
            const int id = 3;
            var result = Service.FindBy(r => r.Id == id).SingleOrDefault();
            Assert.IsNotNull(result);

            var temp = new Person()
            {
                Id = result.Id,
                FirstName = result.FirstName,
                LastName = result.LastName,
                Email = result.Email,
                MobilePhone = result.MobilePhone,
                UpdatedOn = DateTime.UtcNow
            };

            //now, modify the lastname field
            var text = string.Format("|_{0}_", "test");
            temp.LastName = text;

            Put(temp.Id, temp);
            Context?.SaveChanges();
        }

        [TestMethod()]
        public void PostTest()
        {
            var p1 = new Person() { FirstName = "Person1_First", LastName = "Add", Email = "person1@repos.com" };

            //add
            var added = Post(p1);
            Context?.SaveChanges();

            Assert.IsNotNull(added);
            //Assert.IsTrue((p1.FirstName == added.FirstName && (p1.LastName == added.LastName)));
        }

        [TestMethod()]
        public void DeleteTest()
        {
            var p1 = new Person() { FirstName = "Person1_First", LastName = "Delete", Email = "person1@repos.com" };

            //add so we can delete
            var added = Post(p1);

            Assert.IsNotNull(added);
            //Assert.IsTrue((p1.FirstName == added.FirstName && (p1.LastName == added.LastName)));

            //now delete
            //var deleted = base.Delete(p1.Id);
            //var res = _context.SaveChanges();

            //var found = _personRepository.FindBy(p => p.Id == deleted.Id).Count();
            //Assert.IsTrue(found == 0);//should be missing, returning a null on find
        }

        [TestMethod()]
        public void ExistsTest()
        {
            const int id = 2;
            Assert.IsTrue(Exists(id));
            var item = Context.Set<Person>().SingleOrDefault(r => r.Id == id);
            Assert.IsTrue(item != null && item.Id == id);
        }
    }
}
