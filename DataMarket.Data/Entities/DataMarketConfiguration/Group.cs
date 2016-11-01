using System;
using System.Collections.Generic;

namespace DataMarket.Data
{
    public class Group
    {
        public int GroupId { get; set; }

        public int DatamartId { get; set; }

        public string GroupName { get; set; }

        public string DisplayName { get; set; }

        public DateTime AddedDate { get; set; }

        public string AddedBy { get; set; }

        public DateTime UpdatedDate { get; set; }

        public string UpdatedBy { get; set; }

        public byte? ParentGroup { get; set; }

        public string Operator { get; set; } 

        public string FactKey { get; set; }
        public virtual Datamart Datamart { get; set; }

        public virtual ICollection<Filter> Filters { get; set; }
    }
}