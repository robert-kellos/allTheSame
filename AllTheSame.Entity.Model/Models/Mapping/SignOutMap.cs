using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Accushield.Entity.Model.Models.Mapping
{
    public class SignOutMap : EntityTypeConfiguration<SignOut>
    {
        public SignOutMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.SignOutType)
                .HasMaxLength(50);

            this.Property(t => t.Version)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("SignOut");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.ResidentId).HasColumnName("ResidentId");
            this.Property(t => t.VisitorId).HasColumnName("VisitorId");
            this.Property(t => t.VendorWorkerId).HasColumnName("VendorWorkerId");
            this.Property(t => t.TimeOut).HasColumnName("TimeOut");
            this.Property(t => t.TimeBack).HasColumnName("TimeBack");
            this.Property(t => t.SignOutType).HasColumnName("SignOutType");
            this.Property(t => t.Version).HasColumnName("Version");
            this.Property(t => t.CreatedOn).HasColumnName("CreatedOn");
            this.Property(t => t.UpdatedOn).HasColumnName("UpdatedOn");

            // Relationships
            this.HasRequired(t => t.Resident)
                .WithMany(t => t.SignOuts)
                .HasForeignKey(d => d.ResidentId);
            this.HasRequired(t => t.VendorWorker)
                .WithMany(t => t.SignOuts)
                .HasForeignKey(d => d.VendorWorkerId);
            this.HasRequired(t => t.Visitor)
                .WithMany(t => t.SignOuts)
                .HasForeignKey(d => d.VisitorId);

        }
    }
}
