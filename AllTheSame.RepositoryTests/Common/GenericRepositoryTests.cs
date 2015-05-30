using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AllTheSame.Repository.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AllTheSame.Repository.UserData.interfaces;
using AllTheSame.Repository.UserData.implementation;
using System.Data.Entity;
using AllTheSame.Entity.Model;
using System.Diagnostics;
using System.Threading;
namespace AllTheSame.Repository.Common.Tests
{
    [TestClass()]
    public class GenericRepositoryTests
    {
        DbContext _context;
        IPersonRepository _personRepository;
        IAddressRepository _addressRepository;

        [TestMethod()]
        public void GenericRepositoryTest()
        {
            using (_context = new Entity.Model.AllTheSameDbContext())
            {
                var personGenRepos1 = new GenericRepository<Person>(_context);

                Assert.IsNotNull(personGenRepos1);

                Assert.IsTrue(personGenRepos1 is IGenericRepository<Person>);

                Assert.IsNotNull(_context);
                Assert.IsNotNull(_context.Configuration);
                Assert.IsNotNull(_context.Database);

                _personRepository = new PersonRepository(_context);
                Assert.IsNotNull(_personRepository);

                var list1 = _personRepository.GetAll();
                Assert.IsNotNull(list1);

                var list2 = personGenRepos1.GetAll().AsQueryable();//IQueryable
                Assert.IsNotNull(list2);

                int a1 = (list1.ToList().Count);
                int a2 = (list2.ToList().Count);

                Assert.IsTrue(a1 == a2);

                var b1 = (list1.ToList());
                var b2 = (list2.ToList());

                if (a1 > 0 && a2 > 0)
                {
                    Assert.IsNotNull(b1[0]);
                    Assert.IsNotNull(b2[0]);

                    var c1 = b1[0] as Person;
                    var c2 = b2[0] as Person;

                    Assert.IsNotNull(c1);
                    Assert.IsNotNull(c2);

                    Assert.IsTrue((c1 as Person).Id > 0 && (c2 as Person).Id > 0);
                    Assert.IsTrue((c1 as Person).Id == (c2 as Person).Id);
                }
            }

        }

        [TestMethod()]
        public void GenericRepositoryTest1()
        {
            var obj = new GenericRepository<Person>();
            Assert.IsNotNull(obj);
            var list = obj.GetAll();

            Assert.IsNotNull(list);

            var c = list.ToList().Count;
            if (c > 0)
            {
                var lst = list.ToList();
                Assert.IsNotNull(lst[0]);
            }
        }

        [TestMethod()]
        public void FilterTest()
        {

        }

        [TestMethod()]
        public void GetListAsyncTest()
        {
            using (_context = new Entity.Model.AllTheSameDbContext())
            {
                using (_personRepository = new PersonRepository(_context))
                {
                    var list = _personRepository.GetListAsync(w => w.LastName == "AddMany");
                    Assert.IsNotNull(list);
                }
            }
        }

        [TestMethod()]
        public void FindByTest()
        {
            using (_context = new Entity.Model.AllTheSameDbContext())
            {
                using (_personRepository = new PersonRepository(_context))
                {
                    var list = _personRepository.FindBy(w => w.LastName == "AddMany");
                    Assert.IsNotNull(list);
                }
            }
        }

        [TestMethod()]
        public void FindByAsyncTest()
        {
            using (_context = new Entity.Model.AllTheSameDbContext())
            {
                using (_personRepository = new PersonRepository(_context))
                {
                    var list = _personRepository.FindByAsync(w => w.LastName == "AddMany");
                    Assert.IsNotNull(list);
                }
            }
        }

        [TestMethod()]
        public void AddTest()
        {
            var p1 = new Person() { FirstName = "Person1_First", LastName = "Add", Email = "person1@repos.com" };

            Person item;
            using (_context = new Entity.Model.AllTheSameDbContext())
            {
                using (_personRepository = new PersonRepository(_context))
                {
                    //add
                    var added = _personRepository.Add(p1);
                    var res = _context.SaveChanges();
                    Assert.IsTrue(res > 0);

                    Assert.IsNotNull(added);
                    Assert.IsTrue((p1.FirstName == added.FirstName && (p1.LastName == added.LastName)));
                }
            }
        }

        [TestMethod()]
        public void AddManyTest()
        {
            var p1 = new Person() { FirstName = "Person1_First", LastName = "AddMany", Email = "person1@repos.com" };
            var p2 = new Person() { FirstName = "Person2_First", LastName = "AddMany", Email = "person2@repos.com" };
            var p3 = new Person() { FirstName = "Person3_First", LastName = "AddMany", Email = "person3@repos.com" };

            var pList = new List<Person>() { p1, p2, p3 };

            IEnumerable<Person> list;
            using (_context = new Entity.Model.AllTheSameDbContext())
            {
                using (_personRepository = new PersonRepository(_context))
                {
                    //add many
                    _personRepository.AddMany(pList.ToArray());

                    list = _personRepository.GetAll();
                    list = _personRepository.FindBy(p => p.LastName == "AddMany");
                    list = list.ToList();

                    var found = list.Count() >= 3;
                    Assert.IsTrue(found == true);
                }
            }
        }

        [TestMethod()]
        public void DeleteTest()
        {
            var p1 = new Person() { FirstName = "Person1_First", LastName = "Delete", Email = "person1@repos.com" };

            Person item;
            using (_context = new Entity.Model.AllTheSameDbContext())
            {
                using (_personRepository = new PersonRepository(_context))
                {
                    //add so we can delete
                    var added = _personRepository.Add(p1);

                    Assert.IsNotNull(added);
                    Assert.IsTrue((p1.FirstName == added.FirstName && (p1.LastName == added.LastName)));

                    //now delete
                    var deleted = _personRepository.Delete(added);
                    var res = _context.SaveChanges();

                    //var found = _personRepository.FindBy(p => p.Id == deleted.Id).Count();
                    //Assert.IsTrue(found == 0);//should be missing, returning a null on find
                }
            }
        }

        [TestMethod()]
        public void RemoveManyTest()
        {
            var p1 = new Person() { FirstName = "Person1_First", LastName = "RemoveMany", Email = "person1@repos.com", UpdatedOn = DateTime.UtcNow };
            var p2 = new Person() { FirstName = "Person2_First", LastName = "RemoveMany", Email = "person2@repos.com", UpdatedOn = DateTime.UtcNow };
            var p3 = new Person() { FirstName = "Person3_First", LastName = "RemoveMany", Email = "person3@repos.com", UpdatedOn = DateTime.UtcNow };

            var pList = new List<Person>() { p1, p2, p3 };

            IEnumerable<Person> list;
            using (_context = new Entity.Model.AllTheSameDbContext())
            {
                using (_personRepository = new PersonRepository(_context))
                {
                    //add RemoveMany, so we can actually remove them
                    _personRepository.AddMany(pList.ToArray());

                    Thread.Sleep(2000);

                    list = _personRepository.GetAll();
                    var initialCount = list.Count();

                    list = _personRepository.FindBy(p => p.LastName == "RemoveMany");
                    list = list.ToList();

                    var found = list.Count() >= 3;
                    Assert.IsTrue(found == true);

                    _personRepository.RemoveMany(pList.ToArray());

                    list = _personRepository.GetAll();
                    var afterCount = list.Count();

                    Assert.IsTrue(initialCount > afterCount);
                }
            }
        }

        [TestMethod()]
        public void EditTest()
        {
            //_context = new AllTheSameDbContext();
            using (_context = new Entity.Model.AllTheSameDbContext())
            {
                //_personRepository = new PersonRepository(_context);
                using (_personRepository = new PersonRepository(_context))
                {
                    var item = _personRepository.GetSingle(r => r.Id == 2);
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

                    _personRepository.Edit(temp);
                    var res = _context.SaveChanges();
                    Assert.IsTrue(res > 0);

                    //get it again
                    var edited_item = _personRepository.GetSingle(r => r.Id == 2);
                    Assert.IsNotNull(edited_item);
                }
            }
        }

        [TestMethod()]
        public void UpdateManyTest()
        {
            var p1 = new Person() { Id = 26, FirstName = "Person1_First", LastName = "UpdateMany", Email = "person1@repos.com", UpdatedOn = DateTime.UtcNow };
            var p2 = new Person() { Id = 27, FirstName = "Person2_First", LastName = "UpdateMany", Email = "person2@repos.com", UpdatedOn = DateTime.UtcNow };
            var p3 = new Person() { Id = 28, FirstName = "Person3_First", LastName = "UpdateMany", Email = "person3@repos.com", UpdatedOn = DateTime.UtcNow };

            var pList = new List<Person>() { p1, p2, p3 };

            IEnumerable<Person> list;
            //_context = new AllTheSameDbContext();
            using (_context = new Entity.Model.AllTheSameDbContext())
            {
                //_personRepository = new PersonRepository(_context);
                using (_personRepository = new PersonRepository(_context))
                {
                    //add many
                    _personRepository.UpdateMany(pList.ToArray());

                    //Thread.Sleep(2000);
                    list = _personRepository.FindBy(p => (p.Id == 26 || p.Id == 27 || p.Id == 28));
                    list = list.ToList();

                    var found = list.Count() >= 3;
                    Assert.IsTrue(found == true);
                }
            }
        }

        [TestMethod()]
        public void SaveTest()
        {
            using (_context = new Entity.Model.AllTheSameDbContext())
            {
                using (_personRepository = new PersonRepository(_context))
                {
                    var list = _personRepository.FindBy(w => w.Id == 2).SingleOrDefault();
                    
                    if(list.CreatedOn == null)
                        list.CreatedOn = DateTime.UtcNow;
                    list.UpdatedOn = DateTime.UtcNow;

                    _personRepository.Edit(list);

                    var res = _personRepository.Save();

                    Assert.IsNotNull(list);
                    Assert.IsTrue(res > 0);
                }
            }
        }

        [TestMethod()]
        public void GetAllTest1()
        {
            IEnumerable<Person> list;
            using (_context = new Entity.Model.AllTheSameDbContext())
            {
                using (_personRepository = new PersonRepository(_context))
                {
                    list = _personRepository.GetAll();
                    list = list.ToList();
                    Assert.IsNotNull(list);
                }
            }
        }

        [TestMethod()]
        public void GetAllAsyncTest()
        {
            using (_context = new Entity.Model.AllTheSameDbContext())
            {
                using (_personRepository = new PersonRepository(_context))
                {
                    var list = _personRepository.GetAllAsync();
                    Assert.IsNotNull(list);
                }
            }
        }

        [TestMethod()]
        public void GetListTest()
        {
            using (_context = new Entity.Model.AllTheSameDbContext())
            {
                using (_personRepository = new PersonRepository(_context))
                {
                    var list = _personRepository.GetList(w => w.LastName == "AddMany");
                    Assert.IsNotNull(list);
                }
            }
        }

        [TestMethod()]
        public void GetSingleTest()
        {
            using (_context = new Entity.Model.AllTheSameDbContext())
            {
                using (_personRepository = new PersonRepository(_context))
                {
                    var item = _personRepository.GetSingle(r=> r.Id == 2);
                    Assert.IsNotNull(item);
                    Assert.IsTrue(item.Id > 0);
                }
            }
        }

        [TestMethod()]
        public void GetSingleAsyncTest()
        {
            using (_context = new Entity.Model.AllTheSameDbContext())
            {
                using (_personRepository = new PersonRepository(_context))
                {
                    var item = _personRepository.GetSingleAsync(r => r.Id == 2);
                    Assert.IsNotNull(item);
                    Assert.IsTrue(item.Id > 0);
                }
            }
        }

        [TestMethod()]
        public void GetSortedFieldFilterResultTest()
        {

        }

        [TestMethod()]
        public void ValidateTest()
        {
            using (_context = new Entity.Model.AllTheSameDbContext())
            {
                using (_personRepository = new PersonRepository(_context))
                {
                    var item = _personRepository.GetSingle(r => r.Id == 2);
                    Assert.IsNotNull(item);
                    Assert.IsTrue(item.Id > 0);

                    var pReposRef = (_personRepository as PersonRepository);
                    Assert.IsNotNull(pReposRef);

                    var eList = pReposRef.Validate();
                    Assert.IsNotNull(eList);

                    if(eList.Count() > 0)
                    {
                        string errors = "";
                        foreach(var vres in eList.ToList())
                        {
                            errors += vres.ErrorMessage + ", ";
                        }
                        errors = errors.TrimEnd(',');
                        Debug.Print(errors);

                        Assert.IsTrue(!string.IsNullOrWhiteSpace(errors));
                    }
                }
            }
        }
    }
}
