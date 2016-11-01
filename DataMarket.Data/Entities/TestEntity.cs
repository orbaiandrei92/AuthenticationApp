using System;

namespace DataMarket.Data
{
    public class TestEntity
    {

        public Guid AccountId { get; set; }

        public string Name { get; set; }

        public bool? Active { get; set; }

        public bool? Closed { get; set; }

        public virtual RelatedTestEntity RelatedTestEntity { get; set; }

    }
}
