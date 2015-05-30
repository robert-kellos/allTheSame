using System.Data.Entity.ModelConfiguration;

namespace AllTheSame.Entity.Model.Metadata
{
    /// <summary>
    ///     FamilyMemberMetadata
    /// </summary>
    public class FamilyMemberMetadata : EntityTypeConfiguration<FamilyMember>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="FamilyMemberMetadata" /> class.
        /// </summary>
        public FamilyMemberMetadata()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            ToTable("FamilyMember");
            Property(t => t.Id).HasColumnName("Id");
            Property(t => t.PersonId).HasColumnName("PersonId");
            Property(t => t.ResidentId).HasColumnName("ResidentId");
            Property(t => t.LastSyncId).HasColumnName("LastSyncId");
            Property(t => t.Version).HasColumnName("Version");

            // Relationships
            HasRequired(t => t.Person)
                .WithMany(t => t.FamilyMembers)
                .HasForeignKey(d => d.PersonId);
            HasRequired(t => t.Resident)
                .WithMany(t => t.FamilyMembers)
                .HasForeignKey(d => d.ResidentId);
        }
    }
}