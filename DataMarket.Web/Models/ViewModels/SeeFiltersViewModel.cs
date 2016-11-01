using System.Collections.Generic;

namespace DataMarket.Web.Models
{
    public class SeeFiltersViewModel
    {
        public string Datamart { get; set; }   
        
        public string BackToScreen { get; set; } 

        public IEnumerable<ListInProgressByGroupItemViewModel> ListItemsGrouped { get; set; }
    }
}