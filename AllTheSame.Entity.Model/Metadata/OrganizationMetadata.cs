using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace AllTheSame.Entity.Model.Metadata
{
    /// <summary>
    ///     OrganizationMetadata
    /// </summary>
    public class OrganizationMetadata : EntityTypeConfiguration<Organization>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="OrganizationMetadata" /> class.
        /// </summary>
        public OrganizationMetadata()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            Property(t => t.Name)
                .HasMaxLength(100);

            Property(t => t.Facebook)
                .HasMaxLength(50);

            Property(t => t.Twitter)
                .HasMaxLength(50);

            Property(t => t.GooglePlus)
                .HasMaxLength(50);

            Property(t => t.WebURL)
                .HasMaxLength(100);

            Property(t => t.OfficePhone)
                .HasMaxLength(50);

            Property(t => t.AltPhone)
                .HasMaxLength(50);

            Property(t => t.TickerSymbol)
                .HasMaxLength(10);

            // Table & Column Mappings
            ToTable("Organization");
            Property(t => t.Id).HasColumnName("Id");
            Property(t => t.Name).HasColumnName("Name");
            Property(t => t.Level).HasColumnName("Level");
            Property(t => t.OrgTypeId).HasColumnName("OrgTypeId");
            Property(t => t.BillingAddressId).HasColumnName("BillingAddressId");
            Property(t => t.ShippingAddressId).HasColumnName("ShippingAddressId");
            Property(t => t.IndustryId).HasColumnName("IndustryId");
            Property(t => t.Facebook).HasColumnName("Facebook");
            Property(t => t.Twitter).HasColumnName("Twitter");
            Property(t => t.GooglePlus).HasColumnName("GooglePlus");
            Property(t => t.AnnualRevenue).HasColumnName("AnnualRevenue");
            Property(t => t.NumEmployees).HasColumnName("NumEmployees");
            Property(t => t.WebURL).HasColumnName("WebURL");
            Property(t => t.OfficePhone).HasColumnName("OfficePhone");
            Property(t => t.AltPhone).HasColumnName("AltPhone");
            Property(t => t.TickerSymbol).HasColumnName("TickerSymbol");
            Property(t => t.LastSyncId).HasColumnName("LastSyncId");
            Property(t => t.Version).HasColumnName("Version");

            // Relationships
            HasOptional(t => t.Industry)
                .WithMany(t => t.Organizations)
                .HasForeignKey(d => d.IndustryId);
            HasOptional(t => t.OrgType)
                .WithMany(t => t.Organizations)
                .HasForeignKey(d => d.OrgTypeId);
        }

        [Required]
        [MaxLength(100)]
        [DataType(DataType.Password)]
        public string Name { get; set; }

        [DataType(DataType.Currency)]
        public string AnnualRevenue { get; set; }

        [DataType(DataType.Url)]
        public string WebURL { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string OfficePhone { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string AltPhone { get; set; }
    }
}