using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ChampagneApi.Data
{
    public class Price
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(ChampagneId))]
        public int ChampagneId { get; set; }

        [JsonIgnore] // Add this attribute to avoid circular reference
        public Champagne Champagne { get; set; }

        [ForeignKey(nameof(SizeId))]
        public int SizeId { get; set; }

        [JsonIgnore] // So it doesn't show
        public Size Size { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "The value must be greater than 0")]
        public float SellingPrice { get; set; }

        [Required]
        public string Currency { get; set; }
    }
}
