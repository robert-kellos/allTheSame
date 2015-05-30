using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Accushield.Entity.Model.Models.Mapping
{
    public class VendorCredentialMap : EntityTypeConfiguration<VendorCredential>
    {
        public VendorCredentialMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("VendorCredential");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.RequirementId).HasColumnName("RequirementId");
            this.Property(t => t.VendorWorkerId).HasColumnName("VendorWorkerId");
            this.Property(t => t.IsAttested).HasColumnName("IsAttested");
            this.Property(t => t.IsConfirmed).HasColumnName("IsConfirmed");
            this.Property(t => t.ConfirmedOn).HasColumnName("ConfirmedOn");
            this.Property(t => t.ConfirmedByUserId).HasColumnName("ConfirmedByUserId");
            this.Property(t => t.CreatedOn).HasColumnName("CreatedOn");
            this.Property(t => t.UpdatedOn).HasColumnName("UpdatedOn");

            // Relationships
            this.HasRequired(t => t.Requirement)
                .WithMany(t => t.VendorCredentials)
                .HasForeignKey(d => d.RequirementId);
            this.HasOptional(t => t.User)
                .WithMany(t => t.VendorCredentials)
                .HasForeignKey(d => d.ConfirmedByUserId);
            this.HasRequired(t => t.VendorWorker)
                .WithMany(t => t.VendorCredentials)
                .HasForeignKey(d => d.VendorWorkerId);

        }
    }
}
