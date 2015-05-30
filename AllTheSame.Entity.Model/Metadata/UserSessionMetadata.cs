using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace AllTheSame.Entity.Model.Metadata
{
    public class UserSessionMetadata : EntityTypeConfiguration<UserSession>
    {
        public UserSessionMetadata()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties


            // Table & Column Mappings
            ToTable("UserSession");
            Property(t => t.Id).HasColumnName("Id");
            Property(t => t.UserId).HasColumnName("UserId");
            Property(t => t.SessionId).HasColumnName("SessionId");
            Property(t => t.Created).HasColumnName("Created");
            Property(t => t.Expiration).HasColumnName("Expiration");
            Property(t => t.IsValid).HasColumnName("IsValid");
            Property(t => t.LastSyncId).HasColumnName("LastSyncId");
            Property(t => t.Version).HasColumnName("Version");

            // Relationships
            HasRequired(t => t.User)
                .WithMany(t => t.UserSessions)
                .HasForeignKey(d => d.UserId);
        }

        [DataType(DataType.DateTime)]
        public string Created { get; set; }

        [DataType(DataType.DateTime)]
        public string Expiration { get; set; }
    }
}