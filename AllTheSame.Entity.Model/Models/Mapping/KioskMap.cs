using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Accushield.Entity.Model.Models.Mapping
{
    public class KioskMap : EntityTypeConfiguration<Kiosk>
    {
        public KioskMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name)
                .HasMaxLength(100);

            this.Property(t => t.Identifier)
                .HasMaxLength(50);

            this.Property(t => t.IdentifierType)
                .HasMaxLength(50);

            this.Property(t => t.OnSiteLocationDesc)
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("Kiosk");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.CommunityId).HasColumnName("CommunityId");
            this.Property(t => t.KioskStatusId).HasColumnName("KioskStatusId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Identifier).HasColumnName("Identifier");
            this.Property(t => t.IdentifierType).HasColumnName("IdentifierType");
            this.Property(t => t.OnSiteLocationDesc).HasColumnName("OnSiteLocationDesc");
            this.Property(t => t.BadgesRemaining).HasColumnName("BadgesRemaining");
            this.Property(t => t.BadgeAlertCount).HasColumnName("BadgeAlertCount");
            this.Property(t => t.RestartTime).HasColumnName("RestartTime");
            this.Property(t => t.SessionMaxHours).HasColumnName("SessionMaxHours");
            this.Property(t => t.LastUpdate).HasColumnName("LastUpdate");
            this.Property(t => t.CreatedOn).HasColumnName("CreatedOn");
            this.Property(t => t.UpdatedOn).HasColumnName("UpdatedOn");

            // Relationships
            this.HasOptional(t => t.Community)
                .WithMany(t => t.Kiosks)
                .HasForeignKey(d => d.CommunityId);
            this.HasRequired(t => t.KioskStatu)
                .WithMany(t => t.Kiosks)
                .HasForeignKey(d => d.KioskStatusId);

        }
    }
}
