using System.ComponentModel.DataAnnotations;

namespace ChampagneApi.Data
{
    public class GrapeVariety
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string Name { get; set; }
    }
}
