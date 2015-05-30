using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Accushield.Entity.Model.Models.Mapping
{
    public class C__RefactorLogMap : EntityTypeConfiguration<C__RefactorLog>
    {
        public C__RefactorLogMap()
        {
            // Primary Key
            this.HasKey(t => t.OperationKey);

            // Properties
            // Table & Column Mappings
            this.ToTable("__RefactorLog");
            this.Property(t => t.OperationKey).HasColumnName("OperationKey");
        }
    }
}
