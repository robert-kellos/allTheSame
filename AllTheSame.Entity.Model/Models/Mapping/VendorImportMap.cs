using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Accushield.Entity.Model.Models.Mapping
{
    public class VendorImportMap : EntityTypeConfiguration<VendorImport>
    {
        public VendorImportMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.VendorName)
                .HasMaxLength(100);

            this.Property(t => t.OrgName)
                .HasMaxLength(100);

            this.Property(t => t.BillingLine1)
                .HasMaxLength(100);

            this.Property(t => t.BillingLine2)
                .HasMaxLength(100);

            this.Property(t => t.BillingCity)
                .HasMaxLength(50);

            this.Property(t => t.BillingState)
                .HasMaxLength(50);

            this.Property(t => t.BillingCountry)
                .HasMaxLength(50);

            this.Property(t => t.BillingPostalCode)
                .HasMaxLength(50);

            this.Property(t => t.ShippingLine1)
                .HasMaxLength(100);

            this.Property(t => t.ShippingLine2)
                .HasMaxLength(100);

            this.Property(t => t.ShippingCity)
                .HasMaxLength(50);

            this.Property(t => t.ShippingState)
                .HasMaxLength(50);

            this.Property(t => t.ShippingCountry)
                .HasMaxLength(50);

            this.Property(t => t.ShippingPostalCode)
                .HasMaxLength(50);

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

            this.Property(t => t.Industry)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("VendorImport");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.ImportId).HasColumnName("ImportId");
            this.Property(t => t.VendorId).HasColumnName("VendorId");
            this.Property(t => t.OrgId).HasColumnName("OrgId");
            this.Property(t => t.VendorName).HasColumnName("VendorName");
            this.Property(t => t.OrgName).HasColumnName("OrgName");
            this.Property(t => t.BillingLine1).HasColumnName("BillingLine1");
            this.Property(t => t.BillingLine2).HasColumnName("BillingLine2");
            this.Property(t => t.BillingCity).HasColumnName("BillingCity");
            this.Property(t => t.BillingState).HasColumnName("BillingState");
            this.Property(t => t.BillingCountry).HasColumnName("BillingCountry");
            this.Property(t => t.BillingPostalCode).HasColumnName("BillingPostalCode");
            this.Property(t => t.ShippingLine1).HasColumnName("ShippingLine1");
            this.Property(t => t.ShippingLine2).HasColumnName("ShippingLine2");
            this.Property(t => t.ShippingCity).HasColumnName("ShippingCity");
            this.Property(t => t.ShippingState).HasColumnName("ShippingState");
            this.Property(t => t.ShippingCountry).HasColumnName("ShippingCountry");
            this.Property(t => t.ShippingPostalCode).HasColumnName("ShippingPostalCode");
            this.Property(t => t.Facebook).HasColumnName("Facebook");
            this.Property(t => t.Twitter).HasColumnName("Twitter");
            this.Property(t => t.GooglePlus).HasColumnName("GooglePlus");
            this.Property(t => t.AnnualRevenue).HasColumnName("AnnualRevenue");
            this.Property(t => t.NumEmployees).HasColumnName("NumEmployees");
            this.Property(t => t.WebURL).HasColumnName("WebURL");
            this.Property(t => t.OfficePhone).HasColumnName("OfficePhone");
            this.Property(t => t.AltPhone).HasColumnName("AltPhone");
            this.Property(t => t.TickerSymbol).HasColumnName("TickerSymbol");
            this.Property(t => t.ProcessedOn).HasColumnName("ProcessedOn");
            this.Property(t => t.Industry).HasColumnName("Industry");
        }
    }
}
