using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChampagneApi.Data
{
    public class Size
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string NameFR { get; set; }

        [Required]
        [MaxLength(50)]
        public string NameEN { get; set; }

        [Required]
        public string DescriptionFR { get; set; }

        [Required]
        public string DescriptionEN { get; set; }
    }

}
