using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace AllTheSame.Entity.Model.Metadata
{
    /// <summary>
    ///     CommunityMetadata
    /// </summary>
    public class CommunityMetadata : EntityTypeConfiguration<Community>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="CommunityMetadata" /> class.
        /// </summary>
        public CommunityMetadata()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(100);

            Property(t => t.Description)
                .HasMaxLength(500);

            // Table & Column Mappings
            ToTable("Community");
            Property(t => t.Id).HasColumnName("Id");
            Property(t => t.OrgId).HasColumnName("OrgId");
            Property(t => t.CommunityTypeId).HasColumnName("CommunityTypeId");
            Property(t => t.IndustryId).HasColumnName("IndustryId");
            Property(t => t.Name).HasColumnName("Name");
            Property(t => t.Description).HasColumnName("Description");
            Property(t => t.Raiting).HasColumnName("Raiting");
            Property(t => t.NumBeds).HasColumnName("NumBeds");
            Property(t => t.LastSyncId).HasColumnName("LastSyncId");
            Property(t => t.Version).HasColumnName("Version");

            // Relationships
            HasOptional(t => t.CommunityType)
                .WithMany(t => t.Communities)
                .HasForeignKey(d => d.CommunityTypeId);
            HasRequired(t => t.Organization)
                .WithMany(t => t.Communities)
                .HasForeignKey(d => d.OrgId);
        }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }
    }
}