using System.Data.Entity.ModelConfiguration;

namespace DataMarket.Data
{
    class FilterConfiguration: EntityTypeConfiguration<Filter>
    {
        public FilterConfiguration()
        {
            HasKey<int>(d => d.FilterId).ToTable("Filters");
            Property(d => d.GroupId).IsRequired();
            Property(d => d.FilterName).IsRequired().HasMaxLength(50);
            Property(d => d.DisplayName).IsRequired().HasMaxLength(50);           
            Property(d => d.AddedDate).IsRequired();
            Property(d => d.AddedBy).IsRequired().HasMaxLength(50);
            Property(d => d.UpdatedDate).IsRequired();
            Property(d => d.UpdatedBy).IsRequired().HasMaxLength(50);
            HasMany(d => d.FilterValues);
        }
    }
}