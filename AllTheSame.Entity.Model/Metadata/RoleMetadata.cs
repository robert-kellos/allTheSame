using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace AllTheSame.Entity.Model.Metadata
{
    /// <summary>
    ///     RoleMetadata
    /// </summary>
    public class RoleMetadata : EntityTypeConfiguration<Role>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="RoleMetadata" /> class.
        /// </summary>
        public RoleMetadata()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            Property(t => t.Name)
                .HasMaxLength(100);

            // Table & Column Mappings
            ToTable("Role");
            Property(t => t.Id).HasColumnName("Id");
            Property(t => t.Name).HasColumnName("Name");
        }

        [MaxLength(100)]
        public string Name { get; set; }
    }
}