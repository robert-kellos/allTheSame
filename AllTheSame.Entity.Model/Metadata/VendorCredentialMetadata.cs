using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace AllTheSame.Entity.Model.Metadata
{
    /// <summary>
    ///     VendorCredentialMetadata
    /// </summary>
    public class VendorCredentialMetadata : EntityTypeConfiguration<VendorCredential>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="VendorCredentialMetadata" /> class.
        /// </summary>
        public VendorCredentialMetadata()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            ToTable("VendorCredential");
            Property(t => t.Id).HasColumnName("Id");
            Property(t => t.RequirementId).HasColumnName("RequirementId");
            Property(t => t.VendorWorkerId).HasColumnName("VendorWorkerId");
            Property(t => t.IsAttested).HasColumnName("IsAttested");
            Property(t => t.IsConfirmed).HasColumnName("IsConfirmed");
            Property(t => t.ConfirmedOn).HasColumnName("ConfirmedOn");
            Property(t => t.ConfirmedByUserId).HasColumnName("ConfirmedByUserId");

            // Relationships
            HasRequired(t => t.Requirement)
                .WithMany(t => t.VendorCredentials)
                .HasForeignKey(d => d.RequirementId);
            HasOptional(t => t.User)
                .WithMany(t => t.VendorCredentials)
                .HasForeignKey(d => d.ConfirmedByUserId);
            HasRequired(t => t.VendorWorker)
                .WithMany(t => t.VendorCredentials)
                .HasForeignKey(d => d.VendorWorkerId);
        }

        [DisplayName("Is Attested")]
        public string IsAttested { get; set; }

        [DisplayName("Is Confirmed")]
        public string IsConfirmed { get; set; }

        [DisplayName("Confirmed On")]
        [DataType(DataType.DateTime)]
        public string ConfirmedOn { get; set; }
    }
}