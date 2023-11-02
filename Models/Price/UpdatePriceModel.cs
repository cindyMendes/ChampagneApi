namespace ChampagneApi.Models.Price
{
    public class UpdatePriceModel
    {
        public int Id { get; set; }

        public int ChampagneId { get; set; }

        public int SizeId { get; set; }

        public float SellingPrice { get; set; }

        public string Currency { get; set; }
    }
}
