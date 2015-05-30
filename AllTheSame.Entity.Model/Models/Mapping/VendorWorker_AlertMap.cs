using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Accushield.Entity.Model.Models.Mapping
{
    public class VendorWorker_AlertMap : EntityTypeConfiguration<VendorWorker_Alert>
    {
        public VendorWorker_AlertMap()
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
            this.ToTable("VendorWorker_Alert");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.AlertId).HasColumnName("AlertId");
            this.Property(t => t.VendorWorkerId).HasColumnName("VendorWorkerId");
            this.Property(t => t.IsRead).HasColumnName("IsRead");
            this.Property(t => t.Version).HasColumnName("Version");
            this.Property(t => t.CreatedOn).HasColumnName("CreatedOn");
            this.Property(t => t.UpdatedOn).HasColumnName("UpdatedOn");

            // Relationships
            this.HasRequired(t => t.Alert)
                .WithMany(t => t.VendorWorker_Alert)
                .HasForeignKey(d => d.AlertId);
            this.HasRequired(t => t.VendorWorker)
                .WithMany(t => t.VendorWorker_Alert)
                .HasForeignKey(d => d.VendorWorkerId);

        }
    }
}
