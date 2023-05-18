
namespace misa.web202303.Entities
{
    public class Asset
    {
        public Guid AssetId { get; set; }

        public Guid DepartmentId { get; set; }

        public Guid AssetTypeId { get; set; }

        public string AssetCode { get; set; }

        public string AssetName { get; set; }

        public int Quantity { get; set; }

        public double Price { get; set; }

        public int UseDuration { get; set; }

        public int TrackYear { get; set; }

        public DateTime BuyDate { get; set; }

        public DateTime UseDate { get; set; }

        public double LoseRate { get; set; }

        public double LoseRateYear { get; set; }
    }
}
