using System;
using System.Data.Entity.ModelConfiguration;

namespace DataMarket.Data
{
    public class TestEntityConfiguration : EntityTypeConfiguration<TestEntity>
    {

        public TestEntityConfiguration()
        {

            HasKey<Guid>(a => a.AccountId).ToTable("Test");
            Property(a => a.Name).IsRequired().HasMaxLength(200);
            Property(a => a.Active).IsOptional();
            Property(a => a.Closed).IsOptional();

        }

    }
}