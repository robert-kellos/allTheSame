using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using AllTheSame.Entity.Model;
using AllTheSame.Repository.Common;
using AllTheSame.Repository.UserData.implementation;
using AllTheSame.Repository.UserData.interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AllTheSame.RepositoryTests.Common
{
    [TestClass()]
    public class GenericRepositoryTests
    {
        DbContext _context;
        IPersonRepository _personRepository;

        [TestMethod()]
        public void GenericRepositoryTest()
        {
            using (_context = new AllTheSameDbContext())
            {
                var personGenRepos1 = new GenericRepository<Person>(_context);

                Assert.IsNotNull(personGenRepos1);

                Assert.IsTrue(personGenRepos1 != null);

                Assert.IsNotNull(_context);
                Assert.IsNotNull(_context.Configuration);
                Assert.IsNotNull(_context.Database);

                _personRepository = new PersonRepository(_context);
                Assert.IsNotNull(_personRepository);

                var list1 = _personRepository.GetAll();
                Assert.IsNotNull(list1);

                var list2 = personGenRepos1.GetAll().AsQueryable();//IQueryable
                Assert.IsNotNull(list2);

                var enumerable = list1 as IList<Person> ?? list1.ToList();
                var a1 = (enumerable.ToList().Count);
                var a2 = (list2.ToList().Count);

                Assert.IsTrue(a1 == a2);

                var b1 = (enumerable.ToList());
                var b2 = (list2.ToList());

                if (a1 <= 0 || a2 <= 0) return;
                Assert.IsNotNull(b1[0]);
                Assert.IsNotNull(b2[0]);

                var c1 = b1[0];
                var c2 = b2[0];

                Assert.IsNotNull(c1);
                Assert.IsNotNull(c2);

                Assert.IsTrue(c1.Id > 0 && c2.Id > 0);
                Assert.IsTrue(c1.Id == c2.Id);
            }

        }

        [TestMethod()]
        public void GenericRepositoryTest1()
        {
            var obj = new GenericRepository<Person>();
            Assert.IsNotNull(obj);
            var list = obj.GetAll();

            Assert.IsNotNull(list);

            var enumerable = list as IList<Person> ?? list.ToList();
            var c = enumerable.ToList().Count;
            if (c > 0)
            {
                var lst = enumerable.ToList();
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
            using (_context = new AllTheSameDbContext())
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
            using (_context = new AllTheSameDbContext())
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
            using (_context = new AllTheSameDbContext())
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

            using (_context = new AllTheSameDbContext())
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

            using (_context = new AllTheSameDbContext())
            {
                using (_personRepository = new PersonRepository(_context))
                {
                    //add many
                    _personRepository.AddMany(pList.ToArray());

                    var list = _personRepository.FindBy(p => p.LastName == "AddMany");
                    list = list.ToList();

                    var found = list.Count() >= 3;
                    Assert.IsTrue(found);
                }
            }
        }

        [TestMethod()]
        public void DeleteTest()
        {
            var p1 = new Person() { FirstName = "Person1_First", LastName = "Delete", Email = "person1@repos.com", CreatedOn = DateTime.UtcNow };
            Person added;
            using (_context = new AllTheSameDbContext())
            {
                using (_personRepository = new PersonRepository(_context))
                {
                    //add so we can delete
                    added = _personRepository.Add(p1);

                    Assert.IsNotNull(added);
                    Assert.IsTrue((p1.FirstName == added.FirstName && (p1.LastName == added.LastName)));
                }
            }

            using (_context = new AllTheSameDbContext())
            {
                using (_personRepository = new PersonRepository(_context))
                {
                    //now delete
                    var deleted = _personRepository.Delete(added);
                    //_context.SaveChanges();

                    Assert.IsNotNull(deleted);
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

            using (_context = new AllTheSameDbContext())
            {
                using (_personRepository = new PersonRepository(_context))
                {
                    //add RemoveMany, so we can actually remove them
                    _personRepository.AddMany(pList.ToArray());

                    Thread.Sleep(2000);

                    var list = _personRepository.GetAll();
                    var initialCount = list.Count();

                    list = _personRepository.FindBy(p => p.LastName == "RemoveMany");
                    list = list.ToList();

                    var found = list.Count() >= 3;
                    Assert.IsTrue(found);

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
            using (_context = new AllTheSameDbContext())
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
                    var editedItem = _personRepository.GetSingle(r => r.Id == 2);
                    Assert.IsNotNull(editedItem);
                }
            }
        }

        [TestMethod()]
        public void UpdateManyTest()
        {
            var p1 = new Person() { Id = 4, FirstName = "Person1_First", LastName = "UpdateMany", Email = "person1@repos.com", UpdatedOn = DateTime.UtcNow };
            var p2 = new Person() { Id = 5, FirstName = "Person2_First", LastName = "UpdateMany", Email = "person2@repos.com", UpdatedOn = DateTime.UtcNow };
            var p3 = new Person() { Id = 6, FirstName = "Person3_First", LastName = "UpdateMany", Email = "person3@repos.com", UpdatedOn = DateTime.UtcNow };

            var pList = new List<Person>() { p1, p2, p3 };

            //_context = new AllTheSameDbContext();
            using (_context = new AllTheSameDbContext())
            {
                //_personRepository = new PersonRepository(_context);
                using (_personRepository = new PersonRepository(_context))
                {
                    //add many
                    _personRepository.UpdateMany(pList.ToArray());

                    //Thread.Sleep(2000);
                    var list = _personRepository.FindBy(p => (p.Id == 4 || p.Id == 5 || p.Id == 6));
                    list = list.ToList();

                    var found = list.Count() >= 3;
                    Assert.IsTrue(found);
                }
            }
        }

        [TestMethod()]
        public void SaveTest()
        {
            using (_context = new AllTheSameDbContext())
            {
                using (_personRepository = new PersonRepository(_context))
                {
                    var list = _personRepository.FindBy(w => w.Id == 2).SingleOrDefault();
                    
                    if(list != null && list.CreatedOn == null)
                        list.CreatedOn = DateTime.UtcNow;
                    if (list == null) return;
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
            using (_context = new AllTheSameDbContext())
            {
                using (_personRepository = new PersonRepository(_context))
                {
                    var list = _personRepository.GetAll();
                    list = list.ToList();
                    Assert.IsNotNull(list);
                }
            }
        }

        [TestMethod()]
        public void GetAllAsyncTest()
        {
            using (_context = new AllTheSameDbContext())
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
            using (_context = new AllTheSameDbContext())
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
            using (_context = new AllTheSameDbContext())
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
            using (_context = new AllTheSameDbContext())
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
            using (_context = new AllTheSameDbContext())
            {
                using (_personRepository = new PersonRepository(_context))
                {
                    var item = _personRepository.GetSingle(r => r.Id == 2);
                    Assert.IsNotNull(item);
                    Assert.IsTrue(item.Id > 0);

                    var pReposRef = ((PersonRepository) _personRepository);
                    Assert.IsNotNull(pReposRef);

                    var eList = pReposRef.Validate();
                    Assert.IsNotNull(eList);

                    var validationResults = eList as IList<ValidationResult> ?? eList.ToList();
                    var count = validationResults.Count();
                    if (count <= 0) return;
                    var errors = validationResults.ToList().Aggregate("", (current, vres) => current + (vres.ErrorMessage + ", "));
                    errors = errors.TrimEnd(',');
                    Debug.Print(errors);

                    Assert.IsTrue(!string.IsNullOrWhiteSpace(errors));
                }
            }
        }
    }
}
