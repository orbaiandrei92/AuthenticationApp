using System.Collections.Generic;

namespace DataMarket.Web.Models
{
    public class B2BFiltersByGroupViewModel
    {
        public int GroupId { get; set; }

        public string GroupName { get; set; }

        public IEnumerable<B2BFiltersByFilterViewModel> ListItemsByFilter { get; set; }
    }
}