using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Accushield.Entity.Model.Models.Mapping
{
    public class VisitImportMap : EntityTypeConfiguration<VisitImport>
    {
        public VisitImportMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.VisitType)
                .HasMaxLength(100);

            this.Property(t => t.CommunityName)
                .HasMaxLength(100);

            this.Property(t => t.CommunityPhone)
                .HasMaxLength(50);

            this.Property(t => t.ResidentFirstName)
                .HasMaxLength(50);

            this.Property(t => t.ResidentLastName)
                .HasMaxLength(50);

            this.Property(t => t.ResidentEmail)
                .HasMaxLength(100);

            this.Property(t => t.ResidentPhone)
                .HasMaxLength(50);

            this.Property(t => t.VendorType)
                .HasMaxLength(50);

            this.Property(t => t.VendorCompanyName)
                .HasMaxLength(50);

            this.Property(t => t.VendorCompanyPhone)
                .HasMaxLength(50);

            this.Property(t => t.VendorWorkerFirstName)
                .HasMaxLength(50);

            this.Property(t => t.VendorWorkerLastName)
                .HasMaxLength(50);

            this.Property(t => t.VendorWorkerEmail)
                .HasMaxLength(50);

            this.Property(t => t.VendorWorkerPhone)
                .HasMaxLength(50);

            this.Property(t => t.VisitorFirstName)
                .HasMaxLength(50);

            this.Property(t => t.VisitorLastName)
                .HasMaxLength(50);

            this.Property(t => t.VisitorEmail)
                .HasMaxLength(50);

            this.Property(t => t.VisitorPhone)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("VisitImport");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.VisitId).HasColumnName("VisitId");
            this.Property(t => t.ImportId).HasColumnName("ImportId");
            this.Property(t => t.TimeIn).HasColumnName("TimeIn");
            this.Property(t => t.TimeOut).HasColumnName("TimeOut");
            this.Property(t => t.VisitType).HasColumnName("VisitType");
            this.Property(t => t.CommunityName).HasColumnName("CommunityName");
            this.Property(t => t.CommunityPhone).HasColumnName("CommunityPhone");
            this.Property(t => t.ResidentFirstName).HasColumnName("ResidentFirstName");
            this.Property(t => t.ResidentLastName).HasColumnName("ResidentLastName");
            this.Property(t => t.ResidentEmail).HasColumnName("ResidentEmail");
            this.Property(t => t.ResidentPhone).HasColumnName("ResidentPhone");
            this.Property(t => t.VendorType).HasColumnName("VendorType");
            this.Property(t => t.VendorCompanyName).HasColumnName("VendorCompanyName");
            this.Property(t => t.VendorCompanyPhone).HasColumnName("VendorCompanyPhone");
            this.Property(t => t.VendorWorkerFirstName).HasColumnName("VendorWorkerFirstName");
            this.Property(t => t.VendorWorkerLastName).HasColumnName("VendorWorkerLastName");
            this.Property(t => t.VendorWorkerEmail).HasColumnName("VendorWorkerEmail");
            this.Property(t => t.VendorWorkerPhone).HasColumnName("VendorWorkerPhone");
            this.Property(t => t.VisitorFirstName).HasColumnName("VisitorFirstName");
            this.Property(t => t.VisitorLastName).HasColumnName("VisitorLastName");
            this.Property(t => t.VisitorEmail).HasColumnName("VisitorEmail");
            this.Property(t => t.VisitorPhone).HasColumnName("VisitorPhone");
            this.Property(t => t.ProcessedOn).HasColumnName("ProcessedOn");
        }
    }
}
