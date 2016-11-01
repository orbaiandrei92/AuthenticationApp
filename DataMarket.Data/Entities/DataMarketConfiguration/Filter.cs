using System;
using System.Collections.Generic;

namespace DataMarket.Data
{
    public class Filter
    {
        public int FilterId { get; set; }

        public int GroupId { get; set; }

        public string FilterName { get; set; }

        public string DisplayName { get; set; }

        public DateTime AddedDate { get; set; }

        public string AddedBy { get; set; }

        public DateTime UpdatedDate { get; set; }

        public string UpdatedBy { get; set; }

        public virtual Group Group { get; set; }

        public virtual ICollection<FilterValue> FilterValues { get; set; }
    }
}