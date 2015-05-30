using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace AllTheSame.Entity.Model.Metadata
{
    /// <summary>
    ///     SignOutMetadata
    /// </summary>
    public class SignOutMetadata : EntityTypeConfiguration<SignOut>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="SignOutMetadata" /> class.
        /// </summary>
        public SignOutMetadata()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            Property(t => t.SignOutType)
                .HasMaxLength(50);

            // Table & Column Mappings
            ToTable("SignOut");
            Property(t => t.Id).HasColumnName("Id");
            Property(t => t.ResidentId).HasColumnName("ResidentId");
            Property(t => t.VisitorId).HasColumnName("VisitorId");
            Property(t => t.VendorWorkerId).HasColumnName("VendorWorkerId");
            Property(t => t.TimeOut).HasColumnName("TimeOut");
            Property(t => t.TimeBack).HasColumnName("TimeBack");
            Property(t => t.SignOutType).HasColumnName("SignOutType");
            Property(t => t.LastSyncId).HasColumnName("LastSyncId");
            Property(t => t.Version).HasColumnName("Version");

            // Relationships
            HasRequired(t => t.Resident)
                .WithMany(t => t.SignOuts)
                .HasForeignKey(d => d.ResidentId);
            HasRequired(t => t.VendorWorker)
                .WithMany(t => t.SignOuts)
                .HasForeignKey(d => d.VendorWorkerId);
            HasRequired(t => t.Visitor)
                .WithMany(t => t.SignOuts)
                .HasForeignKey(d => d.VisitorId);
        }

        [Display(Name = "Time Out")]
        [MaxLength(100)]
        [DataType(DataType.Time)]
        public string TimeOut { get; set; }

        [Display(Name = "Time Back")]
        [MaxLength(100)]
        [DataType(DataType.Time)]
        public string TimeBack { get; set; }

        [Display(Name="Sign Out Type")]
        [MaxLength(50)]
        public string SignOutType { get; set; }
    }
}