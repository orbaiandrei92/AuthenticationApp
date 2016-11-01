using System;
using System.Collections.Generic;

namespace DataMarket.Data
{
    public class SavedFilter
    {
        public int SavedFilterId { get; set; }

        public string ListName { get; set; }

        public int Count { get; set; }      

        public string Query { get; set; }

        public DateTime CreatedDate { get; set; }

        public int UserId { get; set; }

        public ListStatus StatusId { get; set; }

        public Nullable<int> Datamart { get; set; }

        public virtual User User { get; set; }

        public virtual Status Status { get; set; }

        public virtual ICollection<SavedIds> SavedIds { get; set; }
    }
}