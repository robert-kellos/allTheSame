using System.Data.Entity.ModelConfiguration;

namespace AllTheSame.Entity.Model.Metadata
{
    /// <summary>
    ///     Role_PermissionMetadata
    /// </summary>
    public class Role_PermissionMetadata : EntityTypeConfiguration<Role_Permission>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Role_PermissionMetadata" /> class.
        /// </summary>
        public Role_PermissionMetadata()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            ToTable("Role_Permission");
            Property(t => t.Id).HasColumnName("Id");
            Property(t => t.RoleId).HasColumnName("RoleId");
            Property(t => t.PermissionId).HasColumnName("PermissionId");

            // Relationships
            HasRequired(t => t.Permission)
                .WithMany(t => t.Role_Permission)
                .HasForeignKey(d => d.PermissionId);
            HasRequired(t => t.Role)
                .WithMany(t => t.Role_Permission)
                .HasForeignKey(d => d.RoleId);
        }
    }
}