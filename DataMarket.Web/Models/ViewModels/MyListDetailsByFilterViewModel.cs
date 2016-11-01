using System.Collections.Generic;

namespace DataMarket.Web.Models
{
    public class MyListDetailsByFilterViewModel
    {
        public string FilterName { get; set; }

        public IEnumerable<MyListDetailsViewModel> ListItems { get; set; }
    }
}