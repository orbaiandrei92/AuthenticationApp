using System.Data.Entity.ModelConfiguration;

namespace DataMarket.Data
{
    public class ListInProgressItemConfiguration: EntityTypeConfiguration<ListInProgressItem>
    {
        public ListInProgressItemConfiguration(string tableName)
        {
            HasKey<int>(l => l.ListItemId).ToTable(tableName);
            Property(l => l.GroupId).IsRequired();
            Property(l => l.FilterId).IsRequired();
            Property(l => l.FilterValueId).IsRequired();
            Property(l => l.AddedDate).IsRequired();
        }
    }
}
