using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Accushield.Entity.Model.Models.Mapping
{
    public class VisitMap : EntityTypeConfiguration<Visit>
    {
        public VisitMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.VisitType)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Visit");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.ResidentId).HasColumnName("ResidentId");
            this.Property(t => t.VendorWorkerId).HasColumnName("VendorWorkerId");
            this.Property(t => t.VisitorId).HasColumnName("VisitorId");
            this.Property(t => t.TimeIn).HasColumnName("TimeIn");
            this.Property(t => t.TimeOut).HasColumnName("TimeOut");
            this.Property(t => t.VisitType).HasColumnName("VisitType");
            this.Property(t => t.CreatedOn).HasColumnName("CreatedOn");
            this.Property(t => t.UpdatedOn).HasColumnName("UpdatedOn");

            // Relationships
            this.HasRequired(t => t.Resident)
                .WithMany(t => t.Visits)
                .HasForeignKey(d => d.ResidentId);

        }
    }
}
