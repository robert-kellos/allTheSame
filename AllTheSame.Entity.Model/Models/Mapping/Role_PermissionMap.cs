using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Accushield.Entity.Model.Models.Mapping
{
    public class Role_PermissionMap : EntityTypeConfiguration<Role_Permission>
    {
        public Role_PermissionMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("Role_Permission");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.RoleId).HasColumnName("RoleId");
            this.Property(t => t.PermissionId).HasColumnName("PermissionId");
            this.Property(t => t.CreatedOn).HasColumnName("CreatedOn");
            this.Property(t => t.UpdatedOn).HasColumnName("UpdatedOn");

            // Relationships
            this.HasRequired(t => t.Permission)
                .WithMany(t => t.Role_Permission)
                .HasForeignKey(d => d.PermissionId);
            this.HasRequired(t => t.Role)
                .WithMany(t => t.Role_Permission)
                .HasForeignKey(d => d.RoleId);

        }
    }
}
