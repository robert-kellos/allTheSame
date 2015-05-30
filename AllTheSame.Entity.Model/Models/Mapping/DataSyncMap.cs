using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Accushield.Entity.Model.Models.Mapping
{
    public class DataSyncMap : EntityTypeConfiguration<DataSync>
    {
        public DataSyncMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.RowVersion)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("DataSync");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.KioskId).HasColumnName("KioskId");
            this.Property(t => t.SyncDateTime).HasColumnName("SyncDateTime");
            this.Property(t => t.RowVersion).HasColumnName("RowVersion");
            this.Property(t => t.CreatedOn).HasColumnName("CreatedOn");
            this.Property(t => t.UpdatedOn).HasColumnName("UpdatedOn");

            // Relationships
            this.HasOptional(t => t.Kiosk)
                .WithMany(t => t.DataSyncs)
                .HasForeignKey(d => d.KioskId);

        }
    }
}
