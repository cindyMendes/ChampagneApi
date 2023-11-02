namespace ChampagneApi.Models.Champagne
{
    public class UpdateChampagneModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public float AlcoholLevel { get; set; }

        public DateTime BottlingDate { get; set; }
    }
}
