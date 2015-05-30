using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace AllTheSame.Entity.Model.Metadata
{
    /// <summary>
    ///     UserMetadata
    /// </summary>
    public class UserMetadata : EntityTypeConfiguration<User>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="UserMetadata" /> class.
        /// </summary>
        public UserMetadata()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            Property(t => t.Username)
                .IsRequired()
                .HasMaxLength(100);

            Property(t => t.PasswordHash)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            ToTable("User");
            Property(t => t.Id).HasColumnName("Id");
            Property(t => t.PersonId).HasColumnName("PersonId");
            Property(t => t.Username).HasColumnName("Username");
            Property(t => t.PasswordHash).HasColumnName("PasswordHash");
            Property(t => t.Version).HasColumnName("Version");

            // Relationships
            HasRequired(t => t.Person)
                .WithMany(t => t.Users)
                .HasForeignKey(d => d.PersonId);
        }

        [MaxLength(100)]
        [Required]
        public string Username { get; set; }

        [MaxLength(68)]
        [Required]
        public string PasswordHash { get; set; }
    }
}