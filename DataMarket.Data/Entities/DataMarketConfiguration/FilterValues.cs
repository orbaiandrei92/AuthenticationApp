using System;

namespace DataMarket.Data
{
    public class FilterValue
    {

        public int FilterValueId { get; set;}

        public int FilterId { get; set; }

        public string FilterValueName { get; set; }

        public string DisplayName { get; set; }

        public Nullable<Byte> Value { get; set; }

        public DateTime AddedDate { get; set; }

        public string AddedBy { get; set; }

        public DateTime UpdatedDate { get; set; }

        public string UpdatedBy { get; set; }

        public virtual Filter Filters { get; set; }

    }
}