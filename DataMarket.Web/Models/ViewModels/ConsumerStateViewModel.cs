using System.Collections.Generic;

namespace DataMarket.Web.Models
{
    public class ConsumerStateViewModel
    {
        public IEnumerable<FilterValuesViewModel> FilterModelObj { get; set; }
 
        public string Datamart { get; set; }       
    }

}