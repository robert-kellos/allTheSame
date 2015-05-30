using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace AllTheSame.Entity.Model.Metadata
{
    /// <summary>
    ///     RequirementMetadata
    /// </summary>
    public class RequirementMetadata : EntityTypeConfiguration<Requirement>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="RequirementMetadata" /> class.
        /// </summary>
        public RequirementMetadata()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            Property(t => t.Description)
                .HasMaxLength(500);

            // Table & Column Mappings
            ToTable("Requirement");
            Property(t => t.Id).HasColumnName("Id");
            Property(t => t.CommunityId).HasColumnName("CommunityId");
            Property(t => t.RequirementTypeId).HasColumnName("RequirementTypeId");
            Property(t => t.Description).HasColumnName("Description");

            // Relationships
            HasRequired(t => t.Community)
                .WithMany(t => t.Requirements)
                .HasForeignKey(d => d.CommunityId);
            HasOptional(t => t.RequirementType)
                .WithMany(t => t.Requirements)
                .HasForeignKey(d => d.RequirementTypeId);
        }

        [Required]
        [MaxLength(500)]
        public string Description { get; set; }
    }
}