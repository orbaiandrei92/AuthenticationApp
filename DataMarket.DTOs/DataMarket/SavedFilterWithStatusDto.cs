using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMarket.DTOs
{
    public class SavedFilterWithStatusDto
    {
        public int SavedFilterId { get; set; }

        public string ListName { get; set; }

        public int Datamart { get; set; }

        public int Count { get; set; }

        public DateTime CreatedDate { get; set; }

        public int UserId { get; set; }

        public string Query { get; set; }

        public int StatusId { get; set; }

        public string StatusName { get; set; }
    }
}
