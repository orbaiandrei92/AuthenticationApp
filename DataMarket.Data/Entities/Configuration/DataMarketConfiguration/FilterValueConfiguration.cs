using System.Data.Entity.ModelConfiguration;

namespace DataMarket.Data
{
    class FilterValueConfiguration: EntityTypeConfiguration<FilterValue>
    {
        public FilterValueConfiguration()
        {
            HasKey<int>(d => d.FilterValueId).ToTable("FilterValues");
            Property(d => d.FilterId).IsRequired();
            Property(d => d.FilterValueName).IsRequired().HasMaxLength(50);
            Property(d => d.DisplayName).IsRequired().HasMaxLength(50);
            Property(d => d.Value).IsOptional();
            Property(d => d.AddedDate).IsRequired();
            Property(d => d.AddedBy).IsRequired().HasMaxLength(50);
            Property(d => d.UpdatedDate).IsRequired();
            Property(d => d.UpdatedBy).IsRequired().HasMaxLength(50);
        }
    }
}