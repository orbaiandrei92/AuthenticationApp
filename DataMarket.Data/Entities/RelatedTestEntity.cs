using System.Collections.Generic;

namespace DataMarket.Data
{
    public class RelatedTestEntity
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<TestEntity> TestEntities { get; set; }

    }
}
