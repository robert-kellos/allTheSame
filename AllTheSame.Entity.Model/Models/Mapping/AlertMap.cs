using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Accushield.Entity.Model.Models.Mapping
{
    public class AlertMap : EntityTypeConfiguration<Alert>
    {
        public AlertMap()
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
            this.ToTable("Alert");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.AlertTypeId).HasColumnName("AlertTypeId");
            this.Property(t => t.AppointmentId).HasColumnName("AppointmentId");
            this.Property(t => t.KioskId).HasColumnName("KioskId");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.Version).HasColumnName("Version");
            this.Property(t => t.CreatedOn).HasColumnName("CreatedOn");
            this.Property(t => t.UpdatedOn).HasColumnName("UpdatedOn");

            // Relationships
            this.HasRequired(t => t.AlertType)
                .WithMany(t => t.Alerts)
                .HasForeignKey(d => d.AlertTypeId);
            this.HasOptional(t => t.Appointment)
                .WithMany(t => t.Alerts)
                .HasForeignKey(d => d.AppointmentId);
            this.HasOptional(t => t.Kiosk)
                .WithMany(t => t.Alerts)
                .HasForeignKey(d => d.KioskId);

        }
    }
}
