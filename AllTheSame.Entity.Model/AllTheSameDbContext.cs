using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AllTheSame.Common.Core;
using AllTheSame.Common.Logging;
using AllTheSame.Entity.Model.Metadata;
using log4net.Config;

namespace AllTheSame.Entity.Model
{
    public partial class AllTheSameDbContext
    {
        /// <summary>
        ///     Initializes the <see cref="AllTheSameDbContext" /> class.
        /// </summary>
        static AllTheSameDbContext()
        {
            Database.SetInitializer<AllTheSameDbContext>(null);
        }

        /// <summary>
        ///     Initializes this instance.
        /// </summary>
        private void Init()
        {
            XmlConfigurator.Configure();

            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
            Configuration.UseDatabaseNullSemantics = true;

            Database.SetInitializer<AllTheSameDbContext>(null);
        }



        /// <summary>
        ///     Saves all changes made in this context to the underlying database.
        /// </summary>
        /// <returns>
        ///     The number of state entries written to the underlying database. This can include
        ///     state entries for entities and/or relationships. Relationship state entries are created for
        ///     many-to-many relationships and relationships where there is no foreign key property
        ///     included in the entity class (often referred to as independent associations).
        /// </returns>
        public override int SaveChanges()
        {
            try
            {
                var modifiedEntries = ChangeTracker.Entries()
                    .Where(x => x.Entity != null
                                &&
                                (x.State == EntityState.Added ||
                                 x.State == EntityState.Modified ||
                                 x.State == EntityState.Deleted));

                var dbEntityEntries = modifiedEntries as IList<DbEntityEntry> ?? modifiedEntries.ToList();
                var modCount = dbEntityEntries.Count();
                if (modCount <= 0) return base.SaveChanges();

                var enumerable = default(IList<string>);
                foreach (var err in dbEntityEntries.Select(
                    entry => entry.GetValidationResult())
                    .Where(vr => !vr.IsValid)
                    .Select(vr => vr.ValidationErrors
                        .Select(r => r.ErrorMessage))
                    .SelectMany(errorList =>
                    {
                        enumerable = errorList as IList<string> ?? errorList.ToList();
                        return enumerable;


                    }).SelectMany(error => enumerable))
                {
                    Audit.Log.Error(string.Format("Validation Error: {0}", err));
                }

                return base.SaveChanges();
            }
            catch (CommitFailedException cfex)
            {
                Audit.Log.Error("Commit failed :: error: ", cfex);
                return -1;
            }
            catch (DbEntityValidationException exception)
            {
                foreach (var validationError in
                    exception.EntityValidationErrors.SelectMany(error => error.ValidationErrors))
                {
                    Audit.Log.Error(string.Format(AppConstants.ErrorMessages.DbEntityValidationExceptionMessage + " Error: {0} - {1}",
                        validationError.PropertyName,
                        validationError.ErrorMessage));
                }
                //throw;
                return 1;
            }
        }

        protected override DbEntityValidationResult ValidateEntity(
            DbEntityEntry entityEntry, IDictionary<object, object> items)
        {
            var result = base.ValidateEntity(entityEntry, items);

            if ((entityEntry.State != EntityState.Added && entityEntry.State != EntityState.Modified) ||
                !(entityEntry.Entity is Person))
                return result;

            return result;
        }

        /// <summary>
        ///     This method is called when the model for a derived context has been initialized, but
        ///     before the model has been locked down and used to initialize the context.  The default
        ///     implementation of this method does nothing, but it can be overridden in a derived class
        ///     such that the model can be further configured before it is locked down.
        /// </summary>
        /// <param name="modelBuilder">The builder that defines the model for the context being created.</param>
        /// <remarks>
        ///     Typically, this method is called only once when the first instance of a derived context
        ///     is created.  The model for that context is then cached and is for all further instances of
        ///     the context in the app domain.  This caching can be disabled by setting the ModelCaching
        ///     property on the given ModelBuidler, but note that this can seriously degrade performance.
        ///     More control over caching is provided through use of the DbModelBuilder and DbContextFactory
        ///     classes directly.
        /// </remarks>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new AddressMetadata());
            modelBuilder.Configurations.Add(new AlertMetadata());
            modelBuilder.Configurations.Add(new AlertTypeMetadata());
            modelBuilder.Configurations.Add(new AppointmentMetadata());
            modelBuilder.Configurations.Add(new AppointmentTypeMetadata());
            modelBuilder.Configurations.Add(new CommunityMetadata());
            modelBuilder.Configurations.Add(new CommunityAdminMetadata());
            modelBuilder.Configurations.Add(new CommunityTypeMetadata());
            modelBuilder.Configurations.Add(new CommunityWorkerMetadata());
            modelBuilder.Configurations.Add(new CommunityWorker_AlertMetadata());
            modelBuilder.Configurations.Add(new DataSyncMetadata());
            modelBuilder.Configurations.Add(new FamilyMemberMetadata());
            modelBuilder.Configurations.Add(new IndustryMetadata());
            modelBuilder.Configurations.Add(new KioskMetadata());
            modelBuilder.Configurations.Add(new KioskStatusMetadata());
            modelBuilder.Configurations.Add(new ModuleMetadata());
            modelBuilder.Configurations.Add(new OrganizationMetadata());
            modelBuilder.Configurations.Add(new OrgTypeMetadata());
            modelBuilder.Configurations.Add(new PermissionMetadata());
            modelBuilder.Configurations.Add(new PersonMetadata());
            modelBuilder.Configurations.Add(new PolicyMetadata());
            modelBuilder.Configurations.Add(new RequirementMetadata());
            modelBuilder.Configurations.Add(new RequirementTypeMetadata());
            modelBuilder.Configurations.Add(new ResidentMetadata());
            modelBuilder.Configurations.Add(new RoleMetadata());
            modelBuilder.Configurations.Add(new Role_PermissionMetadata());
            modelBuilder.Configurations.Add(new SignOutMetadata());
            modelBuilder.Configurations.Add(new UserMetadata());
            modelBuilder.Configurations.Add(new User_Org_PermissionMetadata());
            modelBuilder.Configurations.Add(new User_Org_RoleMetadata());
            modelBuilder.Configurations.Add(new UserSessionMetadata());
            modelBuilder.Configurations.Add(new VendorMetadata());
            modelBuilder.Configurations.Add(new VendorAdminMetadata());
            modelBuilder.Configurations.Add(new VendorCredDocumentMetadata());
            modelBuilder.Configurations.Add(new VendorCredentialMetadata());
            modelBuilder.Configurations.Add(new VendorTypeMetadata());
            modelBuilder.Configurations.Add(new VendorWorkerMetadata());
            modelBuilder.Configurations.Add(new VendorWorker_AlertMetadata());
            modelBuilder.Configurations.Add(new VisitMetadata());
            modelBuilder.Configurations.Add(new VisitorMetadata());
        }
    }
}
