using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Accushield.Entity.Model.Models.Mapping
{
    public class CommunityWorkerMap : EntityTypeConfiguration<CommunityWorker>
    {
        public CommunityWorkerMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("CommunityWorker");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.CommunityId).HasColumnName("CommunityId");
            this.Property(t => t.PersonId).HasColumnName("PersonId");
            this.Property(t => t.CreatedOn).HasColumnName("CreatedOn");
            this.Property(t => t.UpdatedOn).HasColumnName("UpdatedOn");

            // Relationships
            this.HasRequired(t => t.Community)
                .WithMany(t => t.CommunityWorkers)
                .HasForeignKey(d => d.CommunityId);
            this.HasRequired(t => t.Person)
                .WithMany(t => t.CommunityWorkers)
                .HasForeignKey(d => d.PersonId);

        }
    }
}
