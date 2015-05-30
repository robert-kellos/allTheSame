using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace AllTheSame.Entity.Model.Metadata
{
    /// <summary>
    ///     KioskStatusMetadata
    /// </summary>
    public class KioskStatusMetadata : EntityTypeConfiguration<KioskStatus>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="KioskStatusMetadata" /> class.
        /// </summary>
        public KioskStatusMetadata()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            Property(t => t.Code)
                .IsRequired()
                .HasMaxLength(50);

            Property(t => t.Label)
                .HasMaxLength(100);

            // Table & Column Mappings
            ToTable("KioskStatus");
            Property(t => t.Id).HasColumnName("Id");
            Property(t => t.Code).HasColumnName("Code");
            Property(t => t.Label).HasColumnName("Label");
            Property(t => t.LastSyncId).HasColumnName("LastSyncId");
            Property(t => t.Version).HasColumnName("Version");
        }

        [Required]
        [MaxLength(50)]
        public string Code { get; set; }

        [MaxLength(100)]
        public string Label { get; set; }
    }
}