namespace ChampagneApi.Models.Composition
{
    public class UpdateCompositionModel
    {
        public int Id { get; set; }

        public int ChampagneId { get; set; }

        public int GrapeVarietyId { get; set; }

        public int Percentage { get; set; }
    }
}
