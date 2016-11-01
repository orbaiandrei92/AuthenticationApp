using DataMarket.Data;
using System;

namespace DataMarket.DTOs
{
    public class StatusDto
    {
        public ListStatus StatusId { get; set; }

        public string DisplayName { get; set; }

        public DateTime CreatedDate { get; set; }

        public string CreatedBy { get; set; }

    }
}
