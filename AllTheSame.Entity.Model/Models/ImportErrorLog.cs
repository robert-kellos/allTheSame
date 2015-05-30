using System;
using System.Collections.Generic;

namespace Accushield.Entity.Model.Models
{
    public partial class ImportErrorLog
    {
        public int Id { get; set; }
        public System.Guid ImportId { get; set; }
        public string Flat_File_Source_Error_Output_Column { get; set; }
        public Nullable<int> ErrorCode { get; set; }
        public string ErrorColumn { get; set; }
        public string ErrorDescription { get; set; }
    }
}
