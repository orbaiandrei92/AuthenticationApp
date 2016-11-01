using System.Data.Entity.ModelConfiguration;

namespace DataMarket.Data
{
    class UserConfiguration: EntityTypeConfiguration<User>
    {
       public UserConfiguration()
       {
            HasKey<int>(u => u.UserId).ToTable("Users");
            Property(u => u.UserName).IsRequired().HasMaxLength(25);
            Property(u => u.FirstName).IsOptional().HasMaxLength(25);
            Property(u => u.LastName).IsOptional().HasMaxLength(25);
            Property(u => u.Password).IsRequired().HasMaxLength(300);
            Property(u => u.Email).IsOptional().HasMaxLength(50);
            Property(u => u.IsAdmin).IsOptional();
            Property(u => u.IsEnabled).IsOptional();
            HasMany(u => u.SavedFilters);
        }
    }
}