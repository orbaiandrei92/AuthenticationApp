using System;
using System.Collections.Generic;

namespace DataMarket.Web.Models
{
    public class SeeDetailsViewModel
    {
        public int SavedFilterId { get; set; }

        public Nullable<int> Datamart { get; set; }

        public string ListName { get; set; }

        public int EntireCount { get; set; }

        public DateTime CreatedDate { get; set; }

        public IEnumerable<MyListDetailsByGroupViewModel> ListItemsByGroup { get; set; }
    }
}