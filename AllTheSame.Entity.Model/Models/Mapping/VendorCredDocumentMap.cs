using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Accushield.Entity.Model.Models.Mapping
{
    public class VendorCredDocumentMap : EntityTypeConfiguration<VendorCredDocument>
    {
        public VendorCredDocumentMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Title)
                .HasMaxLength(100);

            this.Property(t => t.URL)
                .HasMaxLength(200);

            this.Property(t => t.DocType)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("VendorCredDocument");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.VendorCredId).HasColumnName("VendorCredId");
            this.Property(t => t.Title).HasColumnName("Title");
            this.Property(t => t.URL).HasColumnName("URL");
            this.Property(t => t.Text).HasColumnName("Text");
            this.Property(t => t.DocType).HasColumnName("DocType");
            this.Property(t => t.CreatedOn).HasColumnName("CreatedOn");
            this.Property(t => t.UpdatedOn).HasColumnName("UpdatedOn");

            // Relationships
            this.HasRequired(t => t.VendorCredential)
                .WithMany(t => t.VendorCredDocuments)
                .HasForeignKey(d => d.VendorCredId);

        }
    }
}
