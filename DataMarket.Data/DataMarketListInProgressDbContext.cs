using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace DataMarket.Data
{
    class DataMarketListInProgressDbContext: DbContext
    {
        public DbSet<ListInProgressItem> ListInProgress { get; set; }
        private string _tableName;

        public DataMarketListInProgressDbContext(string tableName)
            : base("name=DataMarketListInProgressConnectionString")
        {
            _tableName = tableName;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Configurations.Add(new ListInProgressItemConfiguration(_tableName));

            base.OnModelCreating(modelBuilder);
        }
    }
}
