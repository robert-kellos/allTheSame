using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Accushield.Entity.Model.Models.Mapping
{
    public class CommunityMap : EntityTypeConfiguration<Community>
    {
        public CommunityMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.Description)
                .HasMaxLength(500);

            this.Property(t => t.Version)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("Community");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.OrgId).HasColumnName("OrgId");
            this.Property(t => t.CommunityTypeId).HasColumnName("CommunityTypeId");
            this.Property(t => t.IndustryId).HasColumnName("IndustryId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.Raiting).HasColumnName("Raiting");
            this.Property(t => t.NumBeds).HasColumnName("NumBeds");
            this.Property(t => t.Version).HasColumnName("Version");
            this.Property(t => t.CreatedOn).HasColumnName("CreatedOn");
            this.Property(t => t.UpdatedOn).HasColumnName("UpdatedOn");

            // Relationships
            this.HasOptional(t => t.CommunityType)
                .WithMany(t => t.Communities)
                .HasForeignKey(d => d.CommunityTypeId);
            this.HasRequired(t => t.Organization)
                .WithMany(t => t.Communities)
                .HasForeignKey(d => d.OrgId);

        }
    }
}
