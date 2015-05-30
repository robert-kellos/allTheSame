using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Accushield.Entity.Model.Models.Mapping
{
    public class OrganizationMap : EntityTypeConfiguration<Organization>
    {
        public OrganizationMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name)
                .HasMaxLength(100);

            this.Property(t => t.Facebook)
                .HasMaxLength(50);

            this.Property(t => t.Twitter)
                .HasMaxLength(50);

            this.Property(t => t.GooglePlus)
                .HasMaxLength(50);

            this.Property(t => t.WebURL)
                .HasMaxLength(100);

            this.Property(t => t.OfficePhone)
                .HasMaxLength(50);

            this.Property(t => t.AltPhone)
                .HasMaxLength(50);

            this.Property(t => t.TickerSymbol)
                .HasMaxLength(10);

            this.Property(t => t.Version)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("Organization");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Level).HasColumnName("Level");
            this.Property(t => t.OrgTypeId).HasColumnName("OrgTypeId");
            this.Property(t => t.BillingAddressId).HasColumnName("BillingAddressId");
            this.Property(t => t.ShippingAddressId).HasColumnName("ShippingAddressId");
            this.Property(t => t.IndustryId).HasColumnName("IndustryId");
            this.Property(t => t.Facebook).HasColumnName("Facebook");
            this.Property(t => t.Twitter).HasColumnName("Twitter");
            this.Property(t => t.GooglePlus).HasColumnName("GooglePlus");
            this.Property(t => t.AnnualRevenue).HasColumnName("AnnualRevenue");
            this.Property(t => t.NumEmployees).HasColumnName("NumEmployees");
            this.Property(t => t.WebURL).HasColumnName("WebURL");
            this.Property(t => t.OfficePhone).HasColumnName("OfficePhone");
            this.Property(t => t.AltPhone).HasColumnName("AltPhone");
            this.Property(t => t.TickerSymbol).HasColumnName("TickerSymbol");
            this.Property(t => t.Version).HasColumnName("Version");
            this.Property(t => t.CreatedOn).HasColumnName("CreatedOn");
            this.Property(t => t.UpdatedOn).HasColumnName("UpdatedOn");

            // Relationships
            this.HasOptional(t => t.Address)
                .WithMany(t => t.Organizations)
                .HasForeignKey(d => d.BillingAddressId);
            this.HasOptional(t => t.Address1)
                .WithMany(t => t.Organizations1)
                .HasForeignKey(d => d.ShippingAddressId);
            this.HasOptional(t => t.Industry)
                .WithMany(t => t.Organizations)
                .HasForeignKey(d => d.IndustryId);
            this.HasOptional(t => t.OrgType)
                .WithMany(t => t.Organizations)
                .HasForeignKey(d => d.OrgTypeId);

        }
    }
}
