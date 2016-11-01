using System.Collections.Generic;

namespace DataMarket.Web.Models
{
    public class ConsumerFiltersViewModel
    {
        public IEnumerable<GroupViewModel> GroupModelObj { get; set; }

        public IEnumerable<FilterViewModel> FilterModelObj { get; set; }

        public IEnumerable<FilterValuesViewModel> FilterValueModelObj { get; set; }

        public string Datamart { get; set; }

    }
}