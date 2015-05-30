using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace AllTheSame.Entity.Model.Metadata
{
    /// <summary>
    ///     KioskMetadata
    /// </summary>
    public class KioskMetadata : EntityTypeConfiguration<Kiosk>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="KioskMetadata" /> class.
        /// </summary>
        public KioskMetadata()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            Property(t => t.Name)
                .HasMaxLength(100);

            Property(t => t.Identifier)
                .HasMaxLength(50);

            Property(t => t.IdentifierType)
                .HasMaxLength(50);

            Property(t => t.OnSiteLocationDesc)
                .HasMaxLength(200);

            // Table & Column Mappings
            ToTable("Kiosk");
            Property(t => t.Id).HasColumnName("Id");
            Property(t => t.CommunityId).HasColumnName("CommunityId");
            Property(t => t.KioskStatusId).HasColumnName("KioskStatusId");
            Property(t => t.Name).HasColumnName("Name");
            Property(t => t.Identifier).HasColumnName("Identifier");
            Property(t => t.IdentifierType).HasColumnName("IdentifierType");
            Property(t => t.OnSiteLocationDesc).HasColumnName("OnSiteLocationDesc");
            Property(t => t.BadgesRemaining).HasColumnName("BadgesRemaining");
            Property(t => t.BadgeAlertCount).HasColumnName("BadgeAlertCount");
            Property(t => t.RestartTime).HasColumnName("RestartTime");
            Property(t => t.SessionMaxHours).HasColumnName("SessionMaxHours");
            Property(t => t.LastUpdate).HasColumnName("LastUpdate");
            Property(t => t.LastSyncId).HasColumnName("LastSyncId");
            Property(t => t.Version).HasColumnName("Version");

            // Relationships
            HasOptional(t => t.Community)
                .WithMany(t => t.Kiosks)
                .HasForeignKey(d => d.CommunityId);
            //HasRequired(t => t.KioskStatus)
            //    .WithMany(t => t.Kiosks)
            //    .HasForeignKey(d => d.KioskStatusId);
        }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(50)]
        public string Identifier { get; set; }

        [DisplayName("OnSite Location Description")]
        [MaxLength(200)]
        public string OnSiteLocationDesc { get; set; }

        [DisplayName("Badges Remaining")]
        public string BadgesRemaining { get; set; }

        [DisplayName("Badge Alert Count")]
        public string BadgeAlertCount { get; set; }

        [DisplayName("Restart Time")]
        [DataType(DataType.Time)]
        public string RestartTime { get; set; }

        [DisplayName("Session Max Hours")]
        public string SessionMaxHours { get; set; }

        [DisplayName("Last Update")]
        [DataType(DataType.DateTime)]
        public string LastUpdate { get; set; }
    }
}