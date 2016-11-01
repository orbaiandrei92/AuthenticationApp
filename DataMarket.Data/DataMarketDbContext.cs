using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace DataMarket.Data
{
    public class DataMarketDbContext:DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<SavedFilter> SavedFilters { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<SavedIds> SavedIds { get; set; }

        public DataMarketDbContext()
            : base("name=DataMarketConnectionString")
        { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            modelBuilder.Configurations.Add(new UserConfiguration());
            modelBuilder.Configurations.Add(new SavedFilterConfiguration());
            modelBuilder.Configurations.Add(new StatusConfiguration());
            modelBuilder.Configurations.Add(new SavedIdsConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}