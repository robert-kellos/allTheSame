//using Microsoft.AspNet.Identity.EntityFramework;

namespace Accushield.WebAPI.Models
{
    //- Moved IUser to Accushield.Entity.Model
    //// You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    //public class ApplicationUser : User, IUser<int>, IUser
    //{
    //    //public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType)
    //    //{
    //    //    // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
    //    //    var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
    //    //    // Add custom user claims here

    //    //    return userIdentity;
    //    //}

    //    string IUser<string>.Id
    //    {
    //        get { return Id.ToString(); }
    //    }

    //    public string UserName
    //    {
    //        get
    //        {
    //            return Username;
    //        }
    //        set { Username = value; }
    //    }

    //    public Task<ClaimsIdentity> GenerateUserIdentityAsync(ApplicationUserManager userManager, string authenticationType)
    //    {
    //        throw new System.NotImplementedException();
    //    }
    //}

    ////public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    ////{
    ////    public ApplicationDbContext()
    ////        : base("DefaultConnection", throwIfV1Schema: false)
    ////    {
    ////    }

    ////    public static ApplicationDbContext Post()
    ////    {
    ////        return new ApplicationDbContext();
    ////    }
    ////}
}