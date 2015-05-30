using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace AllTheSame.Entity.Model.Metadata
{
    /// <summary>
    ///     VendorCredDocumentMetadata
    /// </summary>
    public class VendorCredDocumentMetadata : EntityTypeConfiguration<VendorCredDocument>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="VendorCredDocumentMetadata" /> class.
        /// </summary>
        public VendorCredDocumentMetadata()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            Property(t => t.Title)
                .HasMaxLength(100);

            Property(t => t.URL)
                .HasMaxLength(200);

            Property(t => t.DocType)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            ToTable("VendorCredDocument");
            Property(t => t.Id).HasColumnName("Id");
            Property(t => t.VendorCredId).HasColumnName("VendorCredId");
            Property(t => t.Title).HasColumnName("Title");
            Property(t => t.URL).HasColumnName("URL");
            Property(t => t.Text).HasColumnName("Text");
            Property(t => t.DocType).HasColumnName("DocType");

            // Relationships
            HasRequired(t => t.VendorCredential)
                .WithMany(t => t.VendorCredDocuments)
                .HasForeignKey(d => d.VendorCredId);
        }

        [MaxLength(100)]
        public string Title { get; set; }

        [MaxLength(200)]
        [DataType(DataType.Url)]
        public string URL { get; set; }

        [MaxLength(50)]
        [Required]
        public string DocType { get; set; }

    }
}