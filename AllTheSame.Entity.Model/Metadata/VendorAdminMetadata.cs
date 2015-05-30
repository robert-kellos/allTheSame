using System.Data.Entity.ModelConfiguration;

namespace AllTheSame.Entity.Model.Metadata
{
    /// <summary>
    ///     VendorAdminMetadata
    /// </summary>
    public class VendorAdminMetadata : EntityTypeConfiguration<VendorAdmin>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="VendorAdminMetadata" /> class.
        /// </summary>
        public VendorAdminMetadata()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            ToTable("VendorAdmin");
            Property(t => t.Id).HasColumnName("Id");
            Property(t => t.PersonId).HasColumnName("PersonId");
            Property(t => t.VendorId).HasColumnName("VendorId");
            Property(t => t.LastSyncId).HasColumnName("LastSyncId");
            Property(t => t.Version).HasColumnName("Version");

            // Relationships
            HasRequired(t => t.Person)
                .WithMany(t => t.VendorAdmins)
                .HasForeignKey(d => d.PersonId);
            HasRequired(t => t.Vendor)
                .WithMany(t => t.VendorAdmins)
                .HasForeignKey(d => d.VendorId);
        }
    }
}