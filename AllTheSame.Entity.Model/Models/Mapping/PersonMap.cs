using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Accushield.Entity.Model.Models.Mapping
{
    public class PersonMap : EntityTypeConfiguration<Person>
    {
        public PersonMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.FirstName)
                .HasMaxLength(50);

            this.Property(t => t.LastName)
                .HasMaxLength(50);

            this.Property(t => t.Email)
                .HasMaxLength(100);

            this.Property(t => t.Salutation)
                .HasMaxLength(50);

            this.Property(t => t.Title)
                .HasMaxLength(50);

            this.Property(t => t.Facebook)
                .HasMaxLength(50);

            this.Property(t => t.Twitter)
                .HasMaxLength(50);

            this.Property(t => t.HomePhone)
                .HasMaxLength(30);

            this.Property(t => t.MobilePhone)
                .HasMaxLength(30);

            this.Property(t => t.WorkPhone)
                .HasMaxLength(30);

            this.Property(t => t.Version)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("Person");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.LookUpId).HasColumnName("LookUpId");
            this.Property(t => t.FirstName).HasColumnName("FirstName");
            this.Property(t => t.LastName).HasColumnName("LastName");
            this.Property(t => t.Email).HasColumnName("Email");
            this.Property(t => t.BillingAddressId).HasColumnName("BillingAddressId");
            this.Property(t => t.ShippingAddressId).HasColumnName("ShippingAddressId");
            this.Property(t => t.Salutation).HasColumnName("Salutation");
            this.Property(t => t.Title).HasColumnName("Title");
            this.Property(t => t.Facebook).HasColumnName("Facebook");
            this.Property(t => t.Twitter).HasColumnName("Twitter");
            this.Property(t => t.HomePhone).HasColumnName("HomePhone");
            this.Property(t => t.MobilePhone).HasColumnName("MobilePhone");
            this.Property(t => t.WorkPhone).HasColumnName("WorkPhone");
            this.Property(t => t.Version).HasColumnName("Version");
            this.Property(t => t.CreatedOn).HasColumnName("CreatedOn");
            this.Property(t => t.UpdatedOn).HasColumnName("UpdatedOn");

            // Relationships
            this.HasOptional(t => t.Address)
                .WithMany(t => t.People)
                .HasForeignKey(d => d.BillingAddressId);
            this.HasOptional(t => t.Address1)
                .WithMany(t => t.People1)
                .HasForeignKey(d => d.ShippingAddressId);

        }
    }
}
