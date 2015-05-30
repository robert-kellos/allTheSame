using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace AllTheSame.Entity.Model.Metadata
{
    /// <summary>
    ///     ResidentMetadata
    /// </summary>
    public class ResidentMetadata : EntityTypeConfiguration<Resident>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ResidentMetadata" /> class.
        /// </summary>
        public ResidentMetadata()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            Property(t => t.AssistantPhone)
                .HasMaxLength(50);

            // Table & Column Mappings
            ToTable("Resident");
            Property(t => t.Id).HasColumnName("Id");
            Property(t => t.CommunityId).HasColumnName("CommunityId");
            Property(t => t.PersonId).HasColumnName("PersonId");
            Property(t => t.AssistantPhone).HasColumnName("AssistantPhone");
            Property(t => t.LastSyncId).HasColumnName("LastSyncId");
            Property(t => t.Version).HasColumnName("Version");

            // Relationships
            HasRequired(t => t.Community)
                .WithMany(t => t.Residents)
                .HasForeignKey(d => d.CommunityId);
            HasRequired(t => t.Person)
                .WithMany(t => t.Residents)
                .HasForeignKey(d => d.PersonId);
        }

        [Display(Name="Assistant Phone")]
        [MaxLength(50)]

        [DataType(DataType.PhoneNumber)]
        public string AssistantPhone { get; set; }
    }
}