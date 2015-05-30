using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Accushield.Entity.Model.Models.Mapping
{
    public class RequirementMap : EntityTypeConfiguration<Requirement>
    {
        public RequirementMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Description)
                .HasMaxLength(500);

            // Table & Column Mappings
            this.ToTable("Requirement");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.CommunityId).HasColumnName("CommunityId");
            this.Property(t => t.RequirementTypeId).HasColumnName("RequirementTypeId");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.CreatedOn).HasColumnName("CreatedOn");
            this.Property(t => t.UpdatedOn).HasColumnName("UpdatedOn");

            // Relationships
            this.HasRequired(t => t.Community)
                .WithMany(t => t.Requirements)
                .HasForeignKey(d => d.CommunityId);
            this.HasOptional(t => t.RequirementType)
                .WithMany(t => t.Requirements)
                .HasForeignKey(d => d.RequirementTypeId);

        }
    }
}
