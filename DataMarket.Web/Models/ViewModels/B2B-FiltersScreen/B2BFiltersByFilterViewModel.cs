using System.Collections.Generic;

namespace DataMarket.Web.Models
{
    public class B2BFiltersByFilterViewModel
    {
        public int FilterId { get; set; }

        public string FilterName { get; set; }

        public IEnumerable<B2BFiltersViewModel> ListItems { get; set; }
    }
}