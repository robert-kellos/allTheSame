using System.Data.Entity.ModelConfiguration;

namespace AllTheSame.Entity.Model.Metadata
{
    /// <summary>
    ///     VisitorMetadata
    /// </summary>
    public class VisitorMetadata : EntityTypeConfiguration<Visitor>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="VisitorMetadata" /> class.
        /// </summary>
        public VisitorMetadata()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            ToTable("Visitor");
            Property(t => t.Id).HasColumnName("Id");
            Property(t => t.PersonId).HasColumnName("PersonId");
            Property(t => t.LastSyncId).HasColumnName("LastSyncId");
            Property(t => t.Version).HasColumnName("Version");

            // Relationships
            HasRequired(t => t.Person)
                .WithMany(t => t.Visitors)
                .HasForeignKey(d => d.PersonId);
        }
    }
}