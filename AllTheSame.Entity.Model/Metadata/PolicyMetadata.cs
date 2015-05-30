using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace AllTheSame.Entity.Model.Metadata
{
    /// <summary>
    ///     PolicyMetadata
    /// </summary>
    public class PolicyMetadata : EntityTypeConfiguration<Policy>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="PolicyMetadata" /> class.
        /// </summary>
        public PolicyMetadata()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            Property(t => t.DocumentURL)
                .HasMaxLength(200);

            // Table & Column Mappings
            ToTable("Policy");
            Property(t => t.Id).HasColumnName("Id");
            Property(t => t.CommunityId).HasColumnName("CommunityId");
            Property(t => t.Description).HasColumnName("Description");
            Property(t => t.DocumentURL).HasColumnName("DocumentURL");

            // Relationships
            HasRequired(t => t.Community)
                .WithMany(t => t.Policies)
                .HasForeignKey(d => d.CommunityId);
        }

        [Required]
        public string Description { get; set; }

        [MaxLength(200)]
        [DataType(DataType.Url)]
        public string DocumentURL { get; set; }
    }
}