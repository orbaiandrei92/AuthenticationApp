using System.Data.Entity.ModelConfiguration;

namespace DataMarket.Data
{
    class GroupConfiguration: EntityTypeConfiguration<Group>
    {
        public GroupConfiguration()
        {
            HasKey<int>(d => d.GroupId).ToTable("Groups");
            Property(d => d.DatamartId).IsRequired();
            Property(d => d.GroupName).IsRequired().HasMaxLength(50); ; ; ; ; ; ;
            Property(d => d.DisplayName).IsRequired().HasMaxLength(50);
            Property(d => d.AddedDate).IsRequired();
            Property(d => d.AddedBy).IsRequired().HasMaxLength(50);
            Property(d => d.UpdatedDate).IsRequired(); ;
            Property(d => d.UpdatedBy).IsRequired().HasMaxLength(50);
            Property(d => d.ParentGroup).IsOptional();
            Property(d => d.Operator).IsOptional().HasMaxLength(50); ;
            HasMany(d => d.Filters);
        }
    }
}