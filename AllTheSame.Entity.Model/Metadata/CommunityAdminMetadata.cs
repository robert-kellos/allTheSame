using System.Data.Entity.ModelConfiguration;

namespace AllTheSame.Entity.Model.Metadata
{
    /// <summary>
    ///     CommunityAdminMetadata
    /// </summary>
    public class CommunityAdminMetadata : EntityTypeConfiguration<CommunityAdmin>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="CommunityAdminMetadata" /> class.
        /// </summary>
        public CommunityAdminMetadata()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            ToTable("CommunityAdmin");
            Property(t => t.Id).HasColumnName("Id");
            Property(t => t.LastSyncId).HasColumnName("LastSyncId");
            Property(t => t.Version).HasColumnName("Version");
        }
    }
}