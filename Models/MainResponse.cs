namespace ChampagneApi.Models
{
    public class MainResponse
    {
        public bool IsSuccess { get; set; } = true;

        public string? Message { get; set; }

        public object? Content { get; set; }
    }
}
