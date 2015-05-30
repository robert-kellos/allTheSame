using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Accushield.Entity.Model.Models.Mapping
{
    public class AppointmentMap : EntityTypeConfiguration<Appointment>
    {
        public AppointmentMap()
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
            this.ToTable("Appointment");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.ResidentId).HasColumnName("ResidentId");
            this.Property(t => t.VendorWorkerId).HasColumnName("VendorWorkerId");
            this.Property(t => t.AppointmentTypeId).HasColumnName("AppointmentTypeId");
            this.Property(t => t.StartTime).HasColumnName("StartTime");
            this.Property(t => t.EndTime).HasColumnName("EndTime");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.RemindVendor).HasColumnName("RemindVendor");
            this.Property(t => t.AlertOnVendorSignIn).HasColumnName("AlertOnVendorSignIn");
            this.Property(t => t.AlertOnVendorSignOut).HasColumnName("AlertOnVendorSignOut");
            this.Property(t => t.Version).HasColumnName("Version");
            this.Property(t => t.CreatedOn).HasColumnName("CreatedOn");
            this.Property(t => t.UpdatedOn).HasColumnName("UpdatedOn");

            // Relationships
            this.HasRequired(t => t.AppointmentType)
                .WithMany(t => t.Appointments)
                .HasForeignKey(d => d.AppointmentTypeId);
            this.HasRequired(t => t.Resident)
                .WithMany(t => t.Appointments)
                .HasForeignKey(d => d.ResidentId);
            this.HasRequired(t => t.VendorWorker)
                .WithMany(t => t.Appointments)
                .HasForeignKey(d => d.VendorWorkerId);

        }
    }
}
