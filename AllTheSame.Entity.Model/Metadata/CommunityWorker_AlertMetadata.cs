using System.Data.Entity.ModelConfiguration;

namespace AllTheSame.Entity.Model.Metadata
{
    /// <summary>
    ///     CommunityWorker_AlertMetadata
    /// </summary>
    public class CommunityWorker_AlertMetadata : EntityTypeConfiguration<CommunityWorker_Alert>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="CommunityWorker_AlertMetadata" /> class.
        /// </summary>
        public CommunityWorker_AlertMetadata()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            ToTable("CommunityWorker_Alert");
            Property(t => t.Id).HasColumnName("Id");
            Property(t => t.CommunityWorkerId).HasColumnName("CommunityWorkerId");
            Property(t => t.AlertId).HasColumnName("AlertId");
            Property(t => t.IsRead).HasColumnName("IsRead");
            Property(t => t.LastSyncId).HasColumnName("LastSyncId");
            Property(t => t.Version).HasColumnName("Version");

            // Relationships
            HasRequired(t => t.Alert)
                .WithMany(t => t.CommunityWorker_Alert)
                .HasForeignKey(d => d.AlertId);
            HasRequired(t => t.CommunityWorker)
                .WithMany(t => t.CommunityWorker_Alert)
                .HasForeignKey(d => d.CommunityWorkerId);
        }
    }
}