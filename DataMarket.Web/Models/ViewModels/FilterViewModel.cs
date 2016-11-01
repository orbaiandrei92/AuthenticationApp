namespace DataMarket.Web
{
    public class FilterViewModel 
    {
        public int FilterId { get; set; }

        public int FilterValueId { get; set; }
    
        public int GroupId { get; set; }

        public string DisplayName { get; set; }

        public string With { get { return "with "; } }
    }
}