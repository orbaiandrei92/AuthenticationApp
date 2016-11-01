using DataMarket.Data;
using System;

namespace DataMarket.DTOs
{
    public class SavedFilterDto
    {
        public int SavedFilterId { get; set; }

        public string ListName { get; set; }

        public int Count { get; set; }

        public DateTime CreatedDate { get; set; }

        public int UserId { get; set; }

        public string Query { get; set; }

        public ListStatus StatusId { get; set; }

        public Nullable<int> Datamart { get; set; }
    }
}