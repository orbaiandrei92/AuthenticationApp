using System.Data.Entity.ModelConfiguration;

namespace DataMarket.Data
{
    class StatusConfiguration : EntityTypeConfiguration<Status>
    {
        public StatusConfiguration()
        {
            HasKey<ListStatus>(s => s.StatusId).ToTable("Statuses");
            Property(s => s.DisplayName).IsRequired();
            Property(s => s.CreatedDate).IsOptional();
            Property(s => s.CreatedBy).IsOptional().HasMaxLength(50);
            HasMany(s => s.SavedFilters);
        }
    }
}
