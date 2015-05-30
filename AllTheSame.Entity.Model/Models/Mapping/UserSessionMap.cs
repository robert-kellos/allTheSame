using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Accushield.Entity.Model.Models.Mapping
{
    public class UserSessionMap : EntityTypeConfiguration<UserSession>
    {
        public UserSessionMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Version)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("UserSession");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.UserId).HasColumnName("UserId");
            this.Property(t => t.SessionId).HasColumnName("SessionId");
            this.Property(t => t.Created).HasColumnName("Created");
            this.Property(t => t.Expiration).HasColumnName("Expiration");
            this.Property(t => t.IsValid).HasColumnName("IsValid");
            this.Property(t => t.Version).HasColumnName("Version");
            this.Property(t => t.CreatedOn).HasColumnName("CreatedOn");
            this.Property(t => t.UpdatedOn).HasColumnName("UpdatedOn");

            // Relationships
            this.HasRequired(t => t.User)
                .WithMany(t => t.UserSessions)
                .HasForeignKey(d => d.UserId);

        }
    }
}
