using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Accushield.Entity.Model.Models.Mapping
{
    public class PolicyMap : EntityTypeConfiguration<Policy>
    {
        public PolicyMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.DocumentURL)
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("Policy");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.CommunityId).HasColumnName("CommunityId");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.DocumentURL).HasColumnName("DocumentURL");
            this.Property(t => t.CreatedOn).HasColumnName("CreatedOn");
            this.Property(t => t.UpdatedOn).HasColumnName("UpdatedOn");

            // Relationships
            this.HasRequired(t => t.Community)
                .WithMany(t => t.Policies)
                .HasForeignKey(d => d.CommunityId);

        }
    }
}
