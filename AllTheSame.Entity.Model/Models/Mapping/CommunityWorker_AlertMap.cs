using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Accushield.Entity.Model.Models.Mapping
{
    public class CommunityWorker_AlertMap : EntityTypeConfiguration<CommunityWorker_Alert>
    {
        public CommunityWorker_AlertMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("CommunityWorker_Alert");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.CommunityWorkerId).HasColumnName("CommunityWorkerId");
            this.Property(t => t.AlertId).HasColumnName("AlertId");
            this.Property(t => t.IsRead).HasColumnName("IsRead");
            this.Property(t => t.CreatedOn).HasColumnName("CreatedOn");
            this.Property(t => t.UpdatedOn).HasColumnName("UpdatedOn");

            // Relationships
            this.HasRequired(t => t.Alert)
                .WithMany(t => t.CommunityWorker_Alert)
                .HasForeignKey(d => d.AlertId);
            this.HasRequired(t => t.CommunityWorker)
                .WithMany(t => t.CommunityWorker_Alert)
                .HasForeignKey(d => d.CommunityWorkerId);

        }
    }
}
