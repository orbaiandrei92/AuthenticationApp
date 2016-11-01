using System;
using System.Collections.Generic;

namespace DataMarket.Data
{
    public class Status
    {
        public ListStatus StatusId { get; set; }

        public string DisplayName { get; set; }

        public DateTime CreatedDate { get; set; }

        public string CreatedBy { get; set; }

        public virtual ICollection<SavedFilter> SavedFilters { get; set; }

    }
}
