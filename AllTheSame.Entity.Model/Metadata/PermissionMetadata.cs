using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace AllTheSame.Entity.Model.Metadata
{
    /// <summary>
    ///     PermissionMetadata
    /// </summary>
    public class PermissionMetadata : EntityTypeConfiguration<Permission>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="PermissionMetadata" /> class.
        /// </summary>
        public PermissionMetadata()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            Property(t => t.Code)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(50);

            Property(t => t.Label)
                .HasMaxLength(100);

            // Table & Column Mappings
            ToTable("Permission");
            Property(t => t.Id).HasColumnName("Id");
            Property(t => t.ModuleId).HasColumnName("ModuleId");
            Property(t => t.Code).HasColumnName("Code");
            Property(t => t.Label).HasColumnName("Label");

            // Relationships
            HasRequired(t => t.Module)
                .WithMany(t => t.Permissions)
                .HasForeignKey(d => d.ModuleId);
        }

        [Required]
        [MaxLength(50)]
        public string Code { get; set; }

        [MaxLength(50)]
        public string Label { get; set; }
    }
}