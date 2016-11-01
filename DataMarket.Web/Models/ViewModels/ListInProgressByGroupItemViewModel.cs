using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataMarket.Web.Models
{ 
    public class ListInProgressByGroupItemViewModel
    {
        public string GroupName { get; set; }
        public IEnumerable<ListInprogressByFilterItemViewModel> ListItemsGroupedByFilter { get; set; }
    }
}