using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ChampagneApi.Data
{
    public class Composition
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(ChampagneId))]
        public int ChampagneId { get; set; }

        [JsonIgnore]
        public Champagne Champagne { get; set; }

        [ForeignKey(nameof(GrapeVarietyId))]
        public int GrapeVarietyId { get; set; }

        [JsonIgnore]
        public GrapeVariety GrapeVariety { get; set; }

        [Required]
        [Range(1, 100, ErrorMessage = "The value must be between 1 and 100")]
        public int Percentage { get; set; }
    }
}
