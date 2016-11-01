namespace DataMarket.Web.Models
{
    public class FilterValuesViewModel
    {
        public int FilterValueId { get; set; }

        public int FilterId { get; set; }

        public string DisplayName { get; set; }

        public string With { get { return "with "; } }
    }
}