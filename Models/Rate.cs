namespace Project_Skynetz.Models
{
    public class Rate
    {
        public int Id { get; set; }
        public string OriginCode { get; set; }
        public string DestinationCode { get; set; }
        public decimal PricePerMinute { get; set; }
    }
}