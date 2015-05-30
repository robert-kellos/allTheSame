using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace AllTheSame.Entity.Model.Metadata
{
    /// <summary>
    ///     AlertTypeMetadata
    /// </summary>
    public class AlertTypeMetadata : EntityTypeConfiguration<AlertType>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="AlertTypeMetadata" /> class.
        /// </summary>
        public AlertTypeMetadata()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            Property(t => t.Code)
                .IsRequired()
                .HasMaxLength(50);

            Property(t => t.FormatText)
                .IsRequired()
                .HasMaxLength(500);

            // Table & Column Mappings
            ToTable("AlertType");
            Property(t => t.Id).HasColumnName("Id");
            Property(t => t.Code).HasColumnName("Code");
            Property(t => t.FormatText).HasColumnName("FormatText");
        }

        [Required]
        [MaxLength(50)]
        public string Code { get; set; }

        [MaxLength(500)]
        public string FormatText { get; set; }

    }
}