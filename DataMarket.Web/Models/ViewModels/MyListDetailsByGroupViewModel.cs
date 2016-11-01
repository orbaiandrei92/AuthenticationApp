using System.Collections.Generic;

namespace DataMarket.Web.Models
{
    public class MyListDetailsByGroupViewModel
    {
        public string GroupName { get; set; }

        public IEnumerable<MyListDetailsByFilterViewModel> ListItemsByFilter { get; set; }
    }
}