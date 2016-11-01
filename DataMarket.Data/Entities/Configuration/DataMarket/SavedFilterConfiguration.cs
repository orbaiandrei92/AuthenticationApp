using System.Data.Entity.ModelConfiguration;

namespace DataMarket.Data
{
    class SavedFilterConfiguration : EntityTypeConfiguration<SavedFilter>
    {
        public SavedFilterConfiguration()
        {
            HasKey<int>(sf => sf.SavedFilterId).ToTable("SavedFilters");
            Property(sf => sf.Query).IsOptional();
            Property(sf => sf.ListName).IsRequired().HasMaxLength(50);
            Property(sf => sf.Count).IsOptional();
            Property(sf => sf.CreatedDate).IsRequired();
            Property(sf => sf.UserId).IsRequired();
            Property(sf => sf.StatusId).IsOptional();
            Property(sf => sf.Datamart).IsOptional();
            HasMany(sf => sf.SavedIds);   
        }
    }
}