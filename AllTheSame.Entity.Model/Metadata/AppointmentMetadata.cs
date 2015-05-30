using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace AllTheSame.Entity.Model.Metadata
{
    /// <summary>
    ///     AppointmentMetadata
    /// </summary>
    public class AppointmentMetadata : EntityTypeConfiguration<Appointment>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="AppointmentMetadata" /> class.
        /// </summary>
        public AppointmentMetadata()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            ToTable("Appointment");
            Property(t => t.Id).HasColumnName("Id");
            Property(t => t.ResidentId).HasColumnName("ResidentId");
            Property(t => t.VendorWorkerId).HasColumnName("VendorWorkerId");
            Property(t => t.AppointmentTypeId).HasColumnName("AppointmentTypeId");
            Property(t => t.StartTime).HasColumnName("StartTime");
            Property(t => t.EndTime).HasColumnName("EndTime");
            Property(t => t.Description).HasColumnName("Description");
            Property(t => t.RemindVendor).HasColumnName("RemindVendor");
            Property(t => t.AlertOnVendorSignIn).HasColumnName("AlertOnVendorSignIn");
            Property(t => t.AlertOnVendorSignOut).HasColumnName("AlertOnVendorSignOut");
            Property(t => t.LastSyncId).HasColumnName("LastSyncId");
            Property(t => t.Version).HasColumnName("Version");

            // Relationships
            HasRequired(t => t.AppointmentType)
                .WithMany(t => t.Appointments)
                .HasForeignKey(d => d.AppointmentTypeId);
            HasRequired(t => t.Resident)
                .WithMany(t => t.Appointments)
                .HasForeignKey(d => d.ResidentId);
            HasRequired(t => t.VendorWorker)
                .WithMany(t => t.Appointments)
                .HasForeignKey(d => d.VendorWorkerId);
        }

        [Required]
        public string Description { get; set; }
    }
}