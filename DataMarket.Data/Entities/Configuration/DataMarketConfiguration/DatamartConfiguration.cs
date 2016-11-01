using System.Data.Entity.ModelConfiguration;

namespace DataMarket.Data
{
    class DatamartConfiguration : EntityTypeConfiguration<Datamart>
    {
        public DatamartConfiguration()
        {
            HasKey<int>(d => d.DatamartId).ToTable("Datamarts");
            Property(d => d.DatamartName).IsRequired().HasMaxLength(50);
            Property(d => d.DisplayName).IsRequired().HasMaxLength(50);
            Property(d => d.AddedDate).IsRequired();
            Property(d => d.AddedBy).IsRequired().HasMaxLength(50);
            Property(d => d.UpdatedDate).IsRequired();
            Property(d => d.UpdatedBy).IsRequired().HasMaxLength(50);
            Property(d => d.DisplayViewName).IsOptional().HasMaxLength(50);
            Property(d => d.FactName).IsOptional().HasMaxLength(50); ;
            Property(d => d.FlatName).IsOptional().HasMaxLength(50);
            HasMany(d => d.Groups);
        }
    }
}