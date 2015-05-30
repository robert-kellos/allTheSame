using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace AllTheSame.Entity.Model.Metadata
{
    /// <summary>
    ///     VendorTypeMetadata
    /// </summary>
    public class VendorTypeMetadata : EntityTypeConfiguration<VendorType>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="VendorTypeMetadata" /> class.
        /// </summary>
        public VendorTypeMetadata()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            Property(t => t.Label)
                .IsRequired()
                .HasMaxLength(100);

            // Table & Column Mappings
            ToTable("VendorType");
            Property(t => t.Id).HasColumnName("Id");
            Property(t => t.Label).HasColumnName("Label");
        }

        [MaxLength(100)]
        public string Label { get; set; }

    }
}