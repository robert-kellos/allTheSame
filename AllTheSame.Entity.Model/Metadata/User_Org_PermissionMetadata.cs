using System.Data.Entity.ModelConfiguration;

namespace AllTheSame.Entity.Model.Metadata
{
    /// <summary>
    ///     User_Org_PermissionMetadata
    /// </summary>
    public class User_Org_PermissionMetadata : EntityTypeConfiguration<User_Org_Permission>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="User_Org_PermissionMetadata" /> class.
        /// </summary>
        public User_Org_PermissionMetadata()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            ToTable("User_Org_Permission");
            Property(t => t.Id).HasColumnName("Id");
            Property(t => t.UserId).HasColumnName("UserId");
            Property(t => t.OrgId).HasColumnName("OrgId");
            Property(t => t.PermissionId).HasColumnName("PermissionId");
            Property(t => t.IsAllowed).HasColumnName("IsAllowed");

            // Relationships
            HasRequired(t => t.Organization)
                .WithMany(t => t.User_Org_Permission)
                .HasForeignKey(d => d.OrgId);
            HasRequired(t => t.Permission)
                .WithMany(t => t.User_Org_Permission)
                .HasForeignKey(d => d.PermissionId);
            HasRequired(t => t.User)
                .WithMany(t => t.User_Org_Permission)
                .HasForeignKey(d => d.UserId);
        }
    }
}