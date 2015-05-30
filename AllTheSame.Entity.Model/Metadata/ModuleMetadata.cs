using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace AllTheSame.Entity.Model.Metadata
{
    /// <summary>
    ///     ModuleMetadata
    /// </summary>
    public class ModuleMetadata : EntityTypeConfiguration<Module>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ModuleMetadata" /> class.
        /// </summary>
        public ModuleMetadata()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            Property(t => t.Name)
                .HasMaxLength(50);

            // Table & Column Mappings
            ToTable("Module");
            Property(t => t.Id).HasColumnName("Id");
            Property(t => t.Name).HasColumnName("Name");
        }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
    }
}