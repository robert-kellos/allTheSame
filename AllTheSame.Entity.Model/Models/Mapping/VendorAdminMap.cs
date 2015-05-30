using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Accushield.Entity.Model.Models.Mapping
{
    public class VendorAdminMap : EntityTypeConfiguration<VendorAdmin>
    {
        public VendorAdminMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Version)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("VendorAdmin");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.PersonId).HasColumnName("PersonId");
            this.Property(t => t.VendorId).HasColumnName("VendorId");
            this.Property(t => t.Version).HasColumnName("Version");
            this.Property(t => t.CreatedOn).HasColumnName("CreatedOn");
            this.Property(t => t.UpdatedOn).HasColumnName("UpdatedOn");

            // Relationships
            this.HasRequired(t => t.Person)
                .WithMany(t => t.VendorAdmins)
                .HasForeignKey(d => d.PersonId);
            this.HasRequired(t => t.Vendor)
                .WithMany(t => t.VendorAdmins)
                .HasForeignKey(d => d.VendorId);

        }
    }
}
