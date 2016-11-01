using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace DataMarket.Data
{
    public class DataMarketConfigurationDbContext: DbContext
    {
        public DbSet<Group> Groups { get; set; }
        public DbSet<Filter> Filters { get; set; }
        public DbSet<FilterValue> FilterValues { get; set; }
        public DbSet<Datamart> Datamarts { get; set; }

        public DataMarketConfigurationDbContext()
            : base("name=DataMarketConfiguratinConnectionString")
        { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            modelBuilder.Configurations.Add(new FilterConfiguration());
            modelBuilder.Configurations.Add(new GroupConfiguration());
            modelBuilder.Configurations.Add(new FilterValueConfiguration());
            modelBuilder.Configurations.Add(new DatamartConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}