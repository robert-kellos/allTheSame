using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AllTheSame.WebAPI.Controllers.Base;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AllTheSame.Entity.Model;
namespace AllTheSame.WebAPI.Controllers.Base.Tests
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
            var list = base.Get();

            Assert.IsNotNull(list);
        }

        [TestMethod()]
        public void GetByIdTest()
        {
            var id = 3;
            var result = base.GetById(id);
            Assert.IsNotNull(result);

            var item = Context.Set<Person>().Where(r => r.Id == id).SingleOrDefault();
            Assert.IsNotNull(item);
        }

        [TestMethod()]
        public void PutTest()
        {
            var id = 3;
            var result = base.Service.FindBy(r => r.Id == id).SingleOrDefault();
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

            base.Put(temp.Id, temp);
            var res = base.Context.SaveChanges();
        }

        [TestMethod()]
        public void PostTest()
        {
            var p1 = new Person() { FirstName = "Person1_First", LastName = "Add", Email = "person1@repos.com" };

            //add
            var added = base.Post(p1);
            var res = Context.SaveChanges();
            //Assert.IsTrue(res > 0);

            Assert.IsNotNull(added);
            //Assert.IsTrue((p1.FirstName == added.FirstName && (p1.LastName == added.LastName)));
        }

        [TestMethod()]
        public void DeleteTest()
        {
            var p1 = new Person() { FirstName = "Person1_First", LastName = "Delete", Email = "person1@repos.com" };

            //add so we can delete
            var added = base.Post(p1);

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
            var id = 2;
            Assert.IsTrue(base.Exists(id));
            var item = Context.Set<Person>().Where(r => r.Id == id).SingleOrDefault();
            Assert.IsTrue(item.Id == id);
        }
    }
}
