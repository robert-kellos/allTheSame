using System.Data.Entity.ModelConfiguration;

namespace AllTheSame.Entity.Model.Metadata
{
    /// <summary>
    ///     VendorWorkerMetadata
    /// </summary>
    public class VendorWorkerMetadata : EntityTypeConfiguration<VendorWorker>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="VendorWorkerMetadata" /> class.
        /// </summary>
        public VendorWorkerMetadata()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            ToTable("VendorWorker");
            Property(t => t.Id).HasColumnName("Id");
            Property(t => t.PersonId).HasColumnName("PersonId");
            Property(t => t.VendorId).HasColumnName("VendorId");
            Property(t => t.VendorTypeId).HasColumnName("VendorTypeId");
            Property(t => t.LastSyncId).HasColumnName("LastSyncId");
            Property(t => t.Version).HasColumnName("Version");

            // Relationships
            HasRequired(t => t.Person)
                .WithMany(t => t.VendorWorkers)
                .HasForeignKey(d => d.PersonId);
            HasRequired(t => t.Vendor)
                .WithMany(t => t.VendorWorkers)
                .HasForeignKey(d => d.VendorId);
            HasOptional(t => t.VendorType)
                .WithMany(t => t.VendorWorkers)
                .HasForeignKey(d => d.VendorTypeId);
        }
    }
}