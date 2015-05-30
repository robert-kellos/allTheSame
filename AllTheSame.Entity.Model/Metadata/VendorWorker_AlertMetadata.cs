using System.Data.Entity.ModelConfiguration;

namespace AllTheSame.Entity.Model.Metadata
{
    /// <summary>
    ///     VendorWorker_AlertMetadata
    /// </summary>
    public class VendorWorker_AlertMetadata : EntityTypeConfiguration<VendorWorker_Alert>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="VendorWorker_AlertMetadata" /> class.
        /// </summary>
        public VendorWorker_AlertMetadata()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            ToTable("VendorWorker_Alert");
            Property(t => t.Id).HasColumnName("Id");
            Property(t => t.AlertId).HasColumnName("AlertId");
            Property(t => t.VendorWorkerId).HasColumnName("VendorWorkerId");
            Property(t => t.IsRead).HasColumnName("IsRead");
            Property(t => t.LastSyncId).HasColumnName("LastSyncId");
            Property(t => t.Version).HasColumnName("Version");

            // Relationships
            HasRequired(t => t.Alert)
                .WithMany(t => t.VendorWorker_Alert)
                .HasForeignKey(d => d.AlertId);
            HasRequired(t => t.VendorWorker)
                .WithMany(t => t.VendorWorker_Alert)
                .HasForeignKey(d => d.VendorWorkerId);
        }
    }
}