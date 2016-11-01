using System;

namespace DataMarket.Web.Models
{
    public class ListViewModel
    {
        public string SavedFilterId { get; set; }

        public string ListName { get; set; }

        public int Datamart { get; set; }

        public int Count { get; set; }

        public string CreatedDate { get; set; }

        public string StatusName { get; set; }
    }
}