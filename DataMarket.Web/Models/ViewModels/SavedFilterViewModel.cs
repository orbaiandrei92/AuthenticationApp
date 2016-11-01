using System;

namespace DataMarket.Web.Models
{
    public class SavedFilterViewModel
    {
        public int SavedFilterId { get; set; }

        public string ListName { get; set; }

        public int Count { get; set; }

        public DateTime CreatedDate { get; set; }

        public Nullable<int> Datamart { get; set; }
    }
}