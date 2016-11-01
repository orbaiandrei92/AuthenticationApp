using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataMarket.Web.Models
{
    public class ListInprogressByFilterItemViewModel
    {
        public string FilterName { get; set; }
        public IEnumerable<ListInProgressItemViewModel> ListItems { get; set; }
    }
}