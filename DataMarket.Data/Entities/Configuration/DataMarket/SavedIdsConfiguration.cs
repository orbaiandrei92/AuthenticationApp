using System.Data.Entity.ModelConfiguration;

namespace DataMarket.Data
{
    public class SavedIdsConfiguration : EntityTypeConfiguration<SavedIds>
    {
        public SavedIdsConfiguration()
        {
            HasKey<int>(si => si.SavedId).ToTable("SavedIds");
            Property(si => si.FilterValueId).IsRequired();
            Property(si => si.FilterId).IsRequired();
            Property(si => si.GroupId).IsRequired();
            Property(si => si.Count).IsRequired();
            Property(si => si.SavedFilterId).IsRequired();
        }
    }
}
