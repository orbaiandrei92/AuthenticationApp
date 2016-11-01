namespace DataMarket.Data
{
    public class SavedIds
    {
        public int SavedId { get; set; }

        public int FilterValueId { get; set; }

        public int FilterId { get; set; }

        public int GroupId { get; set; }

        public int SavedFilterId { get; set; }

        public int Count { get; set; }

        public virtual SavedFilter SavedFilter { get; set; }
    }
}
