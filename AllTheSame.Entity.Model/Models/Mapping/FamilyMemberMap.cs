using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Accushield.Entity.Model.Models.Mapping
{
    public class FamilyMemberMap : EntityTypeConfiguration<FamilyMember>
    {
        public FamilyMemberMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("FamilyMember");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.PersonId).HasColumnName("PersonId");
            this.Property(t => t.ResidentId).HasColumnName("ResidentId");
            this.Property(t => t.CreatedOn).HasColumnName("CreatedOn");
            this.Property(t => t.UpdatedOn).HasColumnName("UpdatedOn");

            // Relationships
            this.HasRequired(t => t.Person)
                .WithMany(t => t.FamilyMembers)
                .HasForeignKey(d => d.PersonId);
            this.HasRequired(t => t.Resident)
                .WithMany(t => t.FamilyMembers)
                .HasForeignKey(d => d.ResidentId);

        }
    }
}
