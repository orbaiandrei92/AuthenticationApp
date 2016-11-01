using System;

namespace DataMarket.DTOs
{
    public class DatamartDto
    {
        public int DatamartId { get; set; }

        public string DatamartName { get; set; }

        public string DisplayName { get; set; }

        public DateTime AddedDate { get; set; }

        public string AddedBy { get; set; }

        public DateTime UpdatedDate { get; set; }

        public string UpdatedBy { get; set; }

        public string DisplayViewName { get; set; }

        public string FactName { get; set; }

        public string FlatName { get; set; }
    }
}