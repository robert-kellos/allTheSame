using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using AllTheSame.Common.Core;
using AllTheSame.Common.Extensions;
using System;

namespace AllTheSame.Entity.Model.Metadata
{
    /// <summary>
    ///     PersonMetadata
    /// </summary>
    [Serializable]
    public class PersonMetadata : EntityTypeConfiguration<Person>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="PersonMetadata" /> class.
        /// </summary>
        public PersonMetadata()
        {
            /*
             * 
             * This criteria is what the database will allow
             * 
             */

            // Primary Key
            HasKey(t => t.Id);

            // Properties
            Property(t => t.FirstName)
                .HasMaxLength(50)
                .IsMaxLength();

            Property(t => t.LastName)
                .HasMaxLength(50)
                .IsMaxLength()
                .IsRequired();

            Property(t => t.Email)
                .HasMaxLength(100)
                .IsMaxLength();

            Property(t => t.Salutation)
                .HasMaxLength(50);

            Property(t => t.Title)
                .HasMaxLength(50);

            Property(t => t.Facebook)
                .HasMaxLength(50);

            Property(t => t.Twitter)
                .HasMaxLength(50);

            Property(t => t.HomePhone)
                .HasMaxLength(30);

            Property(t => t.MobilePhone)
                .HasMaxLength(30);

            Property(t => t.WorkPhone)
                .HasMaxLength(30);

            Property(t => t.CreatedOn).HasColumnName("CreatedOn");
            Property(t => t.UpdatedOn).HasColumnName("UpdatedOn");
                

            // Table & Column Mappings
            ToTable("Person");
            Property(t => t.Id).HasColumnName("Id");
            Property(t => t.LookUpId).HasColumnName("LookUpId");
            Property(t => t.FirstName).HasColumnName("FirstName");
            Property(t => t.LastName).HasColumnName("LastName");
            Property(t => t.Email).HasColumnName("Email");
            Property(t => t.BillingAddressId).HasColumnName("BillingAddressId");
            Property(t => t.ShippingAddressId).HasColumnName("ShippingAddressId");
            Property(t => t.Salutation).HasColumnName("Salutation");
            Property(t => t.Title).HasColumnName("Title");
            Property(t => t.Facebook).HasColumnName("Facebook");
            Property(t => t.Twitter).HasColumnName("Twitter");
            Property(t => t.HomePhone).HasColumnName("HomePhone");
            Property(t => t.MobilePhone).HasColumnName("MobilePhone");
            Property(t => t.WorkPhone).HasColumnName("WorkPhone");
            Property(t => t.LastSyncId).HasColumnName("LastSyncId");
            Property(t => t.Version).HasColumnName("Version");
            Property(t => t.CreatedOn).HasColumnName("CreatedOn");
            Property(t => t.UpdatedOn).HasColumnName("UpdatedOn");

            // Relationships
        }

        /*
        * 
        * This criteria is what the UI will allow
        * 
        */

        
        /// <summary>
        ///     Gets or sets the first name.
        /// </summary>
        /// <value>
        ///     The first name.
        /// </value>
        [Display(Name = "First Name"), DisplayFormat(NullDisplayText = "")]
        public string FirstName { get; set; }

        /// <summary>
        ///     Gets or sets the last name.
        /// </summary>
        /// <value>
        ///     The last name.
        /// </value>
        [Display(Name = "Last Name"), DisplayFormat(NullDisplayText = "")]
        public string LastName { get; set; }

        /// <summary>
        ///     Gets or sets the email.
        /// </summary>
        /// <value>
        ///     The email.
        /// </value>
        [Display(Name = "Email Address"), DisplayFormat(NullDisplayText = "")]
        [Required]
        [MinLength(6)]
        [MaxLength(100)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        /// <summary>
        ///     Gets or sets the home phone.
        /// </summary>
        /// <value>
        ///     The home phone.
        /// </value>
        //[DisplayFormat(DataFormatString = "(###) ###-####")]
        [DataType(DataType.PhoneNumber)]
        public string HomePhone { get; set; }

        /// <summary>
        ///     Gets or sets the mobile phone.
        /// </summary>
        /// <value>
        ///     The mobile phone.
        /// </value>
        //[DisplayFormat(DataFormatString = "(###) ###-####")]
        //[DisplayFormat(DataFormatString = "(###) ###-####")]
        [DataType(DataType.PhoneNumber)]
        public string MobilePhone { get; set; }

        /// <summary>
        ///     Gets or sets the work phone.
        /// </summary>
        /// <value>
        ///     The work phone.
        /// </value>
        //[DisplayFormat(DataFormatString = "(###) ###-####")]
        [DataType(DataType.PhoneNumber)]
        public string WorkPhone { get; set; }

        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }

        /// <summary>
        ///     Validates the specified validation context.
        /// </summary>
        /// <param name="validationContext">The validation context.</param>
        /// <returns></returns>
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            //Last Name
            if (string.IsNullOrWhiteSpace(LastName))
            {
                yield return new ValidationResult(
                    string.Format(AppConstants.ErrorMessages.ShouldNotBeEmpty, Property(r => r.LastName)),
                    new[] {Property(r => r.LastName).ToString(), "IsNullOrWhiteSpace"});
            }

            //Email
            if (!Email.IsValidEmailAddress())
            {
                yield return new ValidationResult(
                    string.Format(AppConstants.ErrorMessages.MustBeValidEmail, Property(r => r.Email)),
                    new[] {Property(r => r.Email).ToString(), "IsValidEmailAddress"});
            }

            //Phone Number
            if (!HomePhone.IsValidPhoneNumber())
            {
                yield return new ValidationResult(
                    string.Format(AppConstants.ErrorMessages.MustBeValidPhoneNumber, Property(r => r.HomePhone)),
                    new[] {Property(r => r.HomePhone).ToString(), "IsValidPhoneNumber"});
            }

            //Phone Number
            if (!MobilePhone.IsValidPhoneNumber())
            {
                yield return new ValidationResult(
                    string.Format(AppConstants.ErrorMessages.MustBeValidPhoneNumber, Property(r => r.MobilePhone)),
                    new[] {Property(r => r.MobilePhone).ToString(), "IsValidPhoneNumber"});
            }

            //Phone Number
            if (!WorkPhone.IsValidPhoneNumber())
            {
                yield return new ValidationResult(
                    string.Format(AppConstants.ErrorMessages.MustBeValidPhoneNumber, Property(r => r.WorkPhone)),
                    new[] {Property(r => r.WorkPhone).ToString(), "IsValidPhoneNumber"});
            }
        }
    }
}