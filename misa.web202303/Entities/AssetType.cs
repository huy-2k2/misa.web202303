namespace misa.web202303.Entities
{
    public class AssetType
    {
        public Guid AssetTypeId { get; set; }

        public string AssetTypeCode { get; set; } 

        public string AssetTypeName { get; set;}

        public int UseDuration { get; set; }

        public double LoseRate { get; set; }
    }
}
