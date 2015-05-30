using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Accushield.Entity.Model.Models.Mapping
{
    public class ImportErrorLogMap : EntityTypeConfiguration<ImportErrorLog>
    {
        public ImportErrorLogMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.ErrorColumn)
                .HasMaxLength(150);

            // Table & Column Mappings
            this.ToTable("ImportErrorLog");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.ImportId).HasColumnName("ImportId");
            this.Property(t => t.Flat_File_Source_Error_Output_Column).HasColumnName("Flat File Source Error Output Column");
            this.Property(t => t.ErrorCode).HasColumnName("ErrorCode");
            this.Property(t => t.ErrorColumn).HasColumnName("ErrorColumn");
            this.Property(t => t.ErrorDescription).HasColumnName("ErrorDescription");
        }
    }
}
