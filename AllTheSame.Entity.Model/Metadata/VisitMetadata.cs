using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace AllTheSame.Entity.Model.Metadata
{
    /// <summary>
    ///     VisitMetadata
    /// </summary>
    public class VisitMetadata : EntityTypeConfiguration<Visit>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="VisitMetadata" /> class.
        /// </summary>
        public VisitMetadata()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            Property(t => t.VisitType)
                .HasMaxLength(50);

            // Table & Column Mappings
            ToTable("Visit");
            Property(t => t.Id).HasColumnName("Id");
            Property(t => t.ResidentId).HasColumnName("ResidentId");
            Property(t => t.VendorWorkerId).HasColumnName("VendorWorkerId");
            Property(t => t.VisitorId).HasColumnName("VisitorId");
            Property(t => t.TimeIn).HasColumnName("TimeIn");
            Property(t => t.TimeOut).HasColumnName("TimeOut");
            Property(t => t.VisitType).HasColumnName("VisitType");
            Property(t => t.LastSyncId).HasColumnName("LastSyncId");
            Property(t => t.Version).HasColumnName("Version");

            // Relationships
            HasRequired(t => t.Resident)
                .WithMany(t => t.Visits)
                .HasForeignKey(d => d.ResidentId);
        }

        [Display(Name = "Time In")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public string TimeIn { get; set; }

        [Display(Name = "Time Out")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public string TimeOut { get; set; }

        [Display(Name = "Time Visit Type")]
        [MaxLength(50)]
        public string VisitType { get; set; }
    }
}