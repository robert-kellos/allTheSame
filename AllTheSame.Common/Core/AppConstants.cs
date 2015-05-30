using System;

// ReSharper disable InconsistentNaming

namespace AllTheSame.Common.Core
{
    /// <summary>
    ///     App Constants
    /// </summary>
    [Serializable]
    public class AppConstants
    {
        /// <summary>
        ///     The space
        /// </summary>
        public const string Space = " ";

        /// <summary>
        ///     Provides static access to the name of custom headers used in the AllTheSame Application
        /// </summary>
        public static class CustomHeaderCode
        {
            /// <summary>
            ///     Designates the Organization Context used for the Web Method call
            ///     All Permissions are granted in the context of the specified Organization
            /// </summary>
            public const string OrgId = "orgId";
        }

        /// <summary>
        ///     PermissionCode
        /// </summary>
        public static class PermissionCode
        {
            /// <summary>
            ///     The register vendor
            /// </summary>
            public const string RegisterVendor = "reg_vendor";

            /// <summary>
            ///     The add vendor
            /// </summary>
            public const string AddVendor = "add_vendor";

            /// <summary>
            ///     The view vendor
            /// </summary>
            public const string ViewVendor = "view_vendor";

            /// <summary>
            ///     The edit vendor
            /// </summary>
            public const string EditVendor = "edit_vendor";

            /// <summary>
            ///     The archive vendor
            /// </summary>
            public const string ArchiveVendor = "archive_vendor";

            /// <summary>
            ///     The purge vendor
            /// </summary>
            public const string PurgeVendor = "purge_vendor";

            /// <summary>
            ///     The register community
            /// </summary>
            public const string RegisterCommunity = "reg_community";

            /// <summary>
            ///     The add community
            /// </summary>
            public const string AddCommunity = "add_community";

            /// <summary>
            ///     The view community
            /// </summary>
            public const string ViewCommunity = "view_community";

            /// <summary>
            ///     The edit community
            /// </summary>
            public const string EditCommunity = "edit_community";

            /// <summary>
            ///     The archive community
            /// </summary>
            public const string ArchiveCommunity = "archive_community";

            /// <summary>
            ///     The purge community
            /// </summary>
            public const string PurgeCommunity = "purge_community";
        }

        /// <summary>
        ///     ErrorMessages
        /// </summary>
        public static class ErrorMessages
        {
            /// <summary>
            ///     The na
            /// </summary>
            public const string NA = "N/A";

            /// <summary>
            ///     The not available
            /// </summary>
            public const string NotAvailable = "Not available";

            /// <summary>
            ///     The no results
            /// </summary>
            public const string NoResults = "No results";

            /// <summary>
            ///     The unable to save
            /// </summary>
            public const string UnableToSave = "Unable to save";

            /// <summary>
            ///     The unable to edit
            /// </summary>
            public const string UnableToEdit = "Unable to edit";

            /// <summary>
            ///     The application error
            /// </summary>
            public const string ApplicationError = "Application Error";

            /// <summary>
            ///     The internal server error
            /// </summary>
            public const string InternalServerError = "Internal Server Error";

            /// <summary>
            ///     The cannot be null
            /// </summary>
            public const string CannotBeNull = "{0} cannot be null.";

            /// <summary>
            ///     should not be empty
            /// </summary>
            public const string ShouldNotBeEmpty = "{0} should not be empty.";

            /// <summary>
            ///     must be a valid email
            /// </summary>
            public const string MustBeValidEmail = "{0} must be a valid email.";

            /// <summary>
            ///     must be a valid phone number
            /// </summary>
            public const string MustBeValidPhoneNumber = "{0} must be a valid phone number.";

            /// <summary>
            ///     The database update concurrency exception message
            /// </summary>
            public const string DbUpdateConcurrencyExceptionMessage = "DbUpdateConcurrencyException";

            /// <summary>
            ///     The database entity validation exception message
            /// </summary>
            public const string DbEntityValidationExceptionMessage = "DbEntityValidationException";

            /// <summary>
            ///     The database update exception message
            /// </summary>
            public const string DbUpdateExceptionMessage = "DbUpdateException";

            /// <summary>
            ///     The database exception message
            /// </summary>
            public const string DbExceptionMessage = "Db_Exception";

            /// <summary>
            ///     The SQL exception message
            /// </summary>
            public const string SqlExceptionMessage = "SqlException";

            /// <summary>
            ///     The argument null exception message
            /// </summary>
            public const string ArgumentNullExceptionMessage = "{0} - ArgumentNullException";

            /// <summary>
            ///     The null reference exception message
            /// </summary>
            public const string NullReferenceExceptionMessage = "{0} - NullReferenceException";

            /// <summary>
            ///     The exception message
            /// </summary>
            public const string ExceptionMessage = "Exception raised";

            /// <summary>
            ///     The exception message
            /// </summary>
            public const string UnhandledExceptionMessage = "UnhandledException raised";

            /// <summary>
            ///     The dispose message
            /// </summary>
            public const string DisposeMessage = "{0} - Disposed";
        }
    }
}