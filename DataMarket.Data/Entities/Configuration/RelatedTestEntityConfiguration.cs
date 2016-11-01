using System.Data.Entity.ModelConfiguration;

namespace DataMarket.Data
{
    public class RelatedTestEntityConfiguration : EntityTypeConfiguration<RelatedTestEntity>
    {

        public RelatedTestEntityConfiguration()
        {

            Property(ac => ac.Name).IsOptional().HasMaxLength(50);
            HasKey<int>(ac => ac.Id).ToTable("RelatedToTest");
            Property(ac => ac.Name).IsOptional().HasMaxLength(50);
            HasMany(m => m.TestEntities);

        }

    }
}