using System.Data.Entity.ModelConfiguration;

namespace AllTheSame.Entity.Model.Metadata
{
    /// <summary>
    ///     User_Org_RoleMetadata
    /// </summary>
    public class User_Org_RoleMetadata : EntityTypeConfiguration<User_Org_Role>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="User_Org_RoleMetadata" /> class.
        /// </summary>
        public User_Org_RoleMetadata()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            ToTable("User_Org_Role");
            Property(t => t.Id).HasColumnName("Id");
            Property(t => t.UserId).HasColumnName("UserId");
            Property(t => t.OrgId).HasColumnName("OrgId");
            Property(t => t.RoleId).HasColumnName("RoleId");
        }
    }
}