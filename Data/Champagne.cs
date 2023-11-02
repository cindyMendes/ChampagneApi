using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChampagneApi.Data
{
    public class Champagne
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        
        [Required]
        public string Description { get; set; }

        [Required]
        [Range(0, 20)]
        public float AlcoholLevel { get; set; }

        [Required]
        public DateTime BottlingDate { get; set; }

        [Required]
        public string Aging { get; set; }

    }
}
