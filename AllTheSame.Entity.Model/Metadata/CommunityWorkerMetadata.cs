using System.Data.Entity.ModelConfiguration;

namespace AllTheSame.Entity.Model.Metadata
{
    /// <summary>
    ///     CommunityWorkerMetadata
    /// </summary>
    public class CommunityWorkerMetadata : EntityTypeConfiguration<CommunityWorker>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="CommunityWorkerMetadata" /> class.
        /// </summary>
        public CommunityWorkerMetadata()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            ToTable("CommunityWorker");
            Property(t => t.Id).HasColumnName("Id");
            Property(t => t.CommunityId).HasColumnName("CommunityId");
            Property(t => t.PersonId).HasColumnName("PersonId");
            Property(t => t.LastSyncId).HasColumnName("LastSyncId");
            Property(t => t.Version).HasColumnName("Version");

            // Relationships
            HasRequired(t => t.Community)
                .WithMany(t => t.CommunityWorkers)
                .HasForeignKey(d => d.CommunityId);
            HasRequired(t => t.Person)
                .WithMany(t => t.CommunityWorkers)
                .HasForeignKey(d => d.PersonId);
        }
    }
}