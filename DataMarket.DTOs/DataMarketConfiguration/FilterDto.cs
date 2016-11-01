using System;

namespace DataMarket.DTOs
{
    public class FilterDto
    {
        public int FilterId { get; set; }

        public int GroupId { get; set; }

        public string FilterName { get; set; }

        public string DisplayName { get; set; }

        public DateTime AddedDate { get; set; }

        public string AddedBy { get; set; }

        public DateTime UpdatedDate { get; set; }

        public string UpdatedBy { get; set; }
    }
}