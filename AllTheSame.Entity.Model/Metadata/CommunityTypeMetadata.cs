using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace AllTheSame.Entity.Model.Metadata
{
    /// <summary>
    ///     CommunityTypeMetadata
    /// </summary>
    public class CommunityTypeMetadata : EntityTypeConfiguration<CommunityType>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="CommunityTypeMetadata" /> class.
        /// </summary>
        public CommunityTypeMetadata()
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
            ToTable("CommunityType");
            Property(t => t.Id).HasColumnName("Id");
            Property(t => t.Code).HasColumnName("Code");
            Property(t => t.Label).HasColumnName("Label");
        }

        [Required]
        [MaxLength(50)]
        public string Code { get; set; }

        [MaxLength(100)]
        public string Label { get; set; }
    }
}