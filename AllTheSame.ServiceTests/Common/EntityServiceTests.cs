using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AllTheSame.Service.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AllTheSame.Repository.Common;
using AllTheSame.Entity.Model;
using System.Data.Entity;
using System.Threading;
namespace AllTheSame.Service.Common.Tests
{
    [TestClass()]
    public class EntityServiceTests
    {

        #region Local Fields/Properties
        //

        /// <summary>
        /// The _context
        /// </summary>
        public DbContext Context { get; private set; }

        /// <summary>
        /// The _service
        /// </summary>
        public IEntityService<Person, IGenericRepository<Person>> Service { get; private set; }

        /// <summary>
        /// The proxy
        /// </summary>
        protected ServiceProxy Proxy = new ServiceProxy();
        //
        #endregion Local Fields/Properties

        public EntityServiceTests() 
        {
            Init();
        }

        private void Init()
        {
            Context = new Entity.Model.AllTheSameDbContext();

            var uow = new UnitOfWork(Context);
            var respository = new GenericRepository<Person>(Context);

            Service = new EntityService<Person, IGenericRepository<Person>>(uow, respository);
        }


        [TestMethod()]
        public void EntityServiceTest()
        {
            Init();

            Assert.IsNotNull(Context);
            Assert.IsNotNull(Service);
        }

        
        [TestMethod()]
        public void FilterTest()
        {

        }

        [TestMethod()]
        public void AddTest()
        {
            //Service.Add()
            var p1 = new Person() { FirstName = "Person1_First", LastName = "Service_Add", Email = "person1@repos.com", CreatedOn = DateTime.UtcNow };

            Person item;
            
            //add
            var added = Service.Add(p1);
            var res = Context.SaveChanges();

            //Assert.IsTrue(res > 0);

            Assert.IsNotNull(added);
            Assert.IsTrue((p1.FirstName == added.FirstName && (p1.LastName == added.LastName)));
        }

        [TestMethod()]
        public void AddManyTest()
        {
            var p1 = new Person() { FirstName = "Person1_First", LastName = "Service_AddMany", Email = "person1@repos.com", CreatedOn = DateTime.UtcNow };
            var p2 = new Person() { FirstName = "Person2_First", LastName = "Service_AddMany", Email = "person2@repos.com", CreatedOn = DateTime.UtcNow };
            var p3 = new Person() { FirstName = "Person3_First", LastName = "Service_AddMany", Email = "person3@repos.com", CreatedOn = DateTime.UtcNow };

            var pList = new List<Person>() { p1, p2, p3 };

            IEnumerable<Person> list;
            
            //add many
            Service.AddMany(pList.ToArray());

            list = Service.GetAll();
            list = Service.FindBy(p => p.LastName == "Service_AddMany");
            list = list.ToList();

            var found = list.Count() >= 3;
            Assert.IsTrue(found == true);
        }

        [TestMethod()]
        public void DeleteTest()
        {
            var p1 = new Person() { FirstName = "Person1_First", LastName = "Delete", Email = "person1@repos.com" };

            Person item;
            
            //add so we can delete
            var added = Service.Add(p1);

            Assert.IsNotNull(added);
            Assert.IsTrue((p1.FirstName == added.FirstName && (p1.LastName == added.LastName)));

            //now delete
            var deleted = Service.Delete(added);
            var res = Context.SaveChanges();

            var found = Service.FindBy(p => p.Id == deleted.Id).Count();
            Assert.IsTrue(found == 0);//should be missing, returning a null on find
        }

        [TestMethod()]
        public void RemoveManyTest()
        {
            var p1 = new Person() { FirstName = "Person1_First", LastName = "RemoveMany", Email = "person1@repos.com", UpdatedOn = DateTime.UtcNow };
            var p2 = new Person() { FirstName = "Person2_First", LastName = "RemoveMany", Email = "person2@repos.com", UpdatedOn = DateTime.UtcNow };
            var p3 = new Person() { FirstName = "Person3_First", LastName = "RemoveMany", Email = "person3@repos.com", UpdatedOn = DateTime.UtcNow };

            var pList = new List<Person>() { p1, p2, p3 };

            IEnumerable<Person> list;
            
            //add RemoveMany, so we can actually remove them
            Service.AddMany(pList.ToArray());

            Thread.Sleep(2000);

            list = Service.GetAll();
            var initialCount = list.Count();

            list = Service.FindBy(p => p.LastName == "RemoveMany");
            list = list.ToList();

            var found = list.Count() >= 3;
            Assert.IsTrue(found == true);

            Service.RemoveMany(pList.ToArray());

            list = Service.GetAll();
            var afterCount = list.Count();

            Assert.IsTrue(initialCount > afterCount);
        }

        [TestMethod()]
        public void EditTest()
        {
            var item = Service.GetSingle(r => r.Id == 2);
            Assert.IsNotNull(item);
            Assert.IsTrue(item.Id > 0);

            var temp = new Person()
            {
                Id = item.Id,
                FirstName = item.FirstName,
                LastName = item.LastName,
                Email = item.Email,
                MobilePhone = item.MobilePhone,
                UpdatedOn = DateTime.UtcNow
            };

            //now, modify the lastname field
            var text = string.Format("|_{0}", temp);
            temp.LastName = text;

            Service.Edit(temp);
            var res = Context.SaveChanges();
            //Assert.IsTrue(res > 0);

            //get it again
            var edited_item = Service.GetSingle(r => r.Id == 2);
            Assert.IsNotNull(edited_item);
        }

        [TestMethod()]
        public void UpdateManyTest()
        {
            var p1 = new Person() { Id = 26, FirstName = "Person1_First", LastName = "UpdateMany", Email = "person1@repos.com", UpdatedOn = DateTime.UtcNow };
            var p2 = new Person() { Id = 27, FirstName = "Person2_First", LastName = "UpdateMany", Email = "person2@repos.com", UpdatedOn = DateTime.UtcNow };
            var p3 = new Person() { Id = 28, FirstName = "Person3_First", LastName = "UpdateMany", Email = "person3@repos.com", UpdatedOn = DateTime.UtcNow };

            var pList = new List<Person>() { p1, p2, p3 };

            IEnumerable<Person> list;
            
            //add many
            Service.UpdateMany(pList.ToArray());

            Thread.Sleep(2000);
            list = Service.FindBy(p => p.LastName == "UpdateMany");
            list = list.ToList();

            var found = list.Count() >= 3;
            Assert.IsTrue(found == true);
        }

        [TestMethod()]
        public void SaveTest()
        {
            var id = 2;
            var list = Service.FindBy(w => w.Id == id).SingleOrDefault();

            if (list.CreatedOn == null)
                list.CreatedOn = DateTime.UtcNow;
            list.UpdatedOn = DateTime.UtcNow;

            Service.Edit(list);

            var res = Service.Save();

            Assert.IsNotNull(list);
            //Assert.IsTrue(res > 0);
        }

        [TestMethod()]
        public void GetAllTest()
        {
            IEnumerable<Person> list;
            
            list = Service.GetAll();
            list = list.ToList();

            Assert.IsNotNull(list);
        }

        [TestMethod()]
        public void GetAllAsyncTest()
        {
            var list = Service.GetAllAsync();

            Assert.IsNotNull(list);
        }

        [TestMethod()]
        public void GetListTest()
        {
            var list = Service.GetList(w => w.LastName == "Service_AddMany");
            Assert.IsNotNull(list);
        }

        [TestMethod()]
        public void GetListAsyncTest()
        {
            var list = Service.GetListAsync(w => w.LastName == "Service_AddMany");
            Assert.IsNotNull(list);
        }

        [TestMethod()]
        public void GetSingleTest()
        {
            var id = 2;
            var item = Service.GetSingle(r => r.Id == id);
            Assert.IsNotNull(item);
            Assert.IsTrue(item.Id > 0);
        }

        [TestMethod()]
        public void GetSingleAsyncTest()
        {
            var id = 2;
            var item = Service.GetSingleAsync(r => r.Id == id);
            Assert.IsNotNull(item);
            Assert.IsTrue(item.Id > 0);
        }

        [TestMethod()]
        public void FindByTest()
        {
            var id = 2;
            var item = Service.FindBy(r => r.Id == id).SingleOrDefault();
            Assert.IsNotNull(item);
            Assert.IsTrue(item.Id > 0);
        }

        [TestMethod()]
        public void FindByAsyncTest()
        {
            var id = 2;
            var item = Service.FindByAsync(r => r.Id == id);
            Assert.IsNotNull(item);
            Assert.IsTrue(item.Id > 0);
        }
    }
}
