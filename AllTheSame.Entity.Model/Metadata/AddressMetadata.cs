using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace AllTheSame.Entity.Model.Metadata
{
    /// <summary>
    ///     AddressMetadata
    /// </summary>
    public class AddressMetadata : EntityTypeConfiguration<Address>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="AddressMetadata" /> class.
        /// </summary>
        public AddressMetadata()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            Property(t => t.Line1)
                .HasMaxLength(100);

            Property(t => t.Line2)
                .HasMaxLength(100);

            Property(t => t.City)
                .HasMaxLength(50);

            Property(t => t.State)
                .HasMaxLength(50);

            Property(t => t.Country)
                .HasMaxLength(50);

            Property(t => t.PostalCode)
                .HasMaxLength(50);

            // Table & Column Mappings
            ToTable("Address");
            Property(t => t.Id).HasColumnName("Id");
            Property(t => t.Line1).HasColumnName("Line1");
            Property(t => t.Line2).HasColumnName("Line2");
            Property(t => t.City).HasColumnName("City");
            Property(t => t.State).HasColumnName("State");
            Property(t => t.Country).HasColumnName("Country");
            Property(t => t.PostalCode).HasColumnName("PostalCode");
        }

        [Required]
        [MaxLength(100)]
        public string Line1 { get; set; }

        [MaxLength(100)]
        public string Line2 { get; set; }

        [Required]
        [MaxLength(50)]
        public string City { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string State { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string Country { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(12)]
        [DataType(DataType.PostalCode)]
        public string PostalCode { get; set; }
    }
}