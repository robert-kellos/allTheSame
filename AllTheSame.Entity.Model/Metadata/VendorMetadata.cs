using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace AllTheSame.Entity.Model.Metadata
{
    /// <summary>
    ///     VendorMetadata
    /// </summary>
    public class VendorMetadata : EntityTypeConfiguration<Vendor>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="VendorMetadata" /> class.
        /// </summary>
        public VendorMetadata()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            Property(t => t.Name)
                .HasMaxLength(100);

            // Table & Column Mappings
            ToTable("Vendor");
            Property(t => t.Id).HasColumnName("Id");
            Property(t => t.Name).HasColumnName("Name");
            Property(t => t.OrgId).HasColumnName("OrgId");

            Property(t => t.CreatedOn).HasColumnName("CreatedOn").IsOptional();
            Property(t => t.UpdatedOn).HasColumnName("UpdatedOn").IsOptional();

            Property(t => t.LastSyncId).HasColumnName("LastSyncId");
            Property(t => t.Version).HasColumnName("Version");

            // Relationships
            HasOptional(t => t.Organization)
                .WithMany(t => t.Vendors)
                .HasForeignKey(d => d.OrgId);
        }

        [Display(Name = "Name"), DisplayFormat(NullDisplayText = "")]
        [Required]
        [MinLength(2)]
        [MaxLength(100)]
        public string Name { get; set; }

    }
}
