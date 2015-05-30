using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Accushield.Entity.Model.Models.Mapping
{
    public class User_Org_PermissionMap : EntityTypeConfiguration<User_Org_Permission>
    {
        public User_Org_PermissionMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("User_Org_Permission");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.UserId).HasColumnName("UserId");
            this.Property(t => t.OrgId).HasColumnName("OrgId");
            this.Property(t => t.PermissionId).HasColumnName("PermissionId");
            this.Property(t => t.IsAllowed).HasColumnName("IsAllowed");
            this.Property(t => t.CreatedOn).HasColumnName("CreatedOn");
            this.Property(t => t.UpdatedOn).HasColumnName("UpdatedOn");

            // Relationships
            this.HasRequired(t => t.Organization)
                .WithMany(t => t.User_Org_Permission)
                .HasForeignKey(d => d.OrgId);
            this.HasRequired(t => t.Permission)
                .WithMany(t => t.User_Org_Permission)
                .HasForeignKey(d => d.PermissionId);
            this.HasRequired(t => t.User)
                .WithMany(t => t.User_Org_Permission)
                .HasForeignKey(d => d.UserId);

        }
    }
}
