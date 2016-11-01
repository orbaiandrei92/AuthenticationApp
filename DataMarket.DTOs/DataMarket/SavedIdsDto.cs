namespace DataMarket.DTOs
{
    public class SavedIdsDto
    {
        public int SavedId { get; set; }

        public int FilterValueId { get; set; }

        public int FilterId { get; set; }

        public int GroupId { get; set; }

        public int SavedFilterId { get; set; }

        public string GroupName { get; set; }

        public string FilterName { get; set; }

        public string FilterValueName { get; set; }

        public int Count { get; set; }
    }
}
