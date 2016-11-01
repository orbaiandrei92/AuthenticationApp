using System;

namespace DataMarket.Data
{
    public class ListInProgressItem
    {
        public int ListItemId { get; set; }
        public int GroupId { get; set; }
        public int FilterId { get; set; }
        public int FilterValueId { get; set; }
        public DateTime AddedDate { get; set; }
        public string GroupName { get; set; }
        public string FilterName { get; set; }
        public string FilterValueName { get; set; }
    }
}
