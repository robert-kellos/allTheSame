using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace AllTheSame.Entity.Model.Metadata
{
    /// <summary>
    ///     OrgTypeMetadata
    /// </summary>
    public class OrgTypeMetadata : EntityTypeConfiguration<OrgType>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="OrgTypeMetadata" /> class.
        /// </summary>
        public OrgTypeMetadata()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            Property(t => t.Label)
                .HasMaxLength(50);

            Property(t => t.Code)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(50);

            // Table & Column Mappings
            ToTable("OrgType");
            Property(t => t.Id).HasColumnName("Id");
            Property(t => t.Label).HasColumnName("Label");
            Property(t => t.Code).HasColumnName("Code");
        }

        [Required]
        [MaxLength(50)]
        public string Code { get; set; }

        [MaxLength(50)]
        public string Label { get; set; }
    }
}