using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Accushield.Entity.Model.Models.Mapping
{
    public class ResidentMap : EntityTypeConfiguration<Resident>
    {
        public ResidentMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.AssistantPhone)
                .HasMaxLength(50);

            this.Property(t => t.Version)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("Resident");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.CommunityId).HasColumnName("CommunityId");
            this.Property(t => t.PersonId).HasColumnName("PersonId");
            this.Property(t => t.AssistantPhone).HasColumnName("AssistantPhone");
            this.Property(t => t.Version).HasColumnName("Version");
            this.Property(t => t.CreatedOn).HasColumnName("CreatedOn");
            this.Property(t => t.UpdatedOn).HasColumnName("UpdatedOn");

            // Relationships
            this.HasRequired(t => t.Community)
                .WithMany(t => t.Residents)
                .HasForeignKey(d => d.CommunityId);
            this.HasRequired(t => t.Person)
                .WithMany(t => t.Residents)
                .HasForeignKey(d => d.PersonId);

        }
    }
}
