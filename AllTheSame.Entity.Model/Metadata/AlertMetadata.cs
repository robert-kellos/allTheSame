using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace AllTheSame.Entity.Model.Metadata
{
    /// <summary>
    ///     AlertMetadata
    /// </summary>
    public class AlertMetadata : EntityTypeConfiguration<Alert>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="AlertMetadata" /> class.
        /// </summary>
        public AlertMetadata()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            ToTable("Alert");
            Property(t => t.Id).HasColumnName("Id");
            Property(t => t.AlertTypeId).HasColumnName("AlertTypeId");
            Property(t => t.AppointmentId).HasColumnName("AppointmentId");
            Property(t => t.KioskId).HasColumnName("KioskId");
            Property(t => t.Description).HasColumnName("Description");
            Property(t => t.LastSyncId).HasColumnName("LastSyncId");
            Property(t => t.Version).HasColumnName("Version");

            // Relationships
            HasRequired(t => t.AlertType)
                .WithMany(t => t.Alerts)
                .HasForeignKey(d => d.AlertTypeId);
            HasOptional(t => t.Appointment)
                .WithMany(t => t.Alerts)
                .HasForeignKey(d => d.AppointmentId);
            HasOptional(t => t.Kiosk)
                .WithMany(t => t.Alerts)
                .HasForeignKey(d => d.KioskId);
        }

        [Required]
        public string Description { get; set; }
    }
}