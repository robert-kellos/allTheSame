using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AllTheSame.Entity.Model;
using AllTheSame.Repository.Authentication;
using AllTheSame.Repository.Common;
using AllTheSame.Repository.UserData.interfaces;
using Microsoft.AspNet.Identity;

namespace AllTheSame.Repository.UserData.implementation
{
    /// <summary>
    ///     PersonRepository
    /// </summary>
    public class PersonRepository : SyncRepository<Person>, IPersonRepository
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="PersonRepository" /> class.
        /// </summary>
        /// <param name="context"></param>
        public PersonRepository(DbContext context)
            : base(context)
        {
            //
        }

        /// <summary>
        ///     Gets all.
        /// </summary>
        /// <returns></returns>
        public override IEnumerable<Person> GetAll()
        {
            return CurrentDbContext.Set<Person>().Include(x => x.FamilyMembers).AsEnumerable();
        }

        /// <summary>
        ///     Gets the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public Person GetById(long id)
        {
            //--> need to define navigation property on type: return _dbset.Include(x => x.Users.Where(u=>u.PersonId==id)).Where(x => x.Id == id).FirstOrDefault();
            return CurrentDbSet.SingleOrDefault(p => p.Id == id);
        }

        /// <summary>
        ///     Updates the password.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="person">The person.</param>
        /// <returns></returns>
        public bool UpdatePassword(int id, Person person)
        {
            
            return true;
        }
    }
}