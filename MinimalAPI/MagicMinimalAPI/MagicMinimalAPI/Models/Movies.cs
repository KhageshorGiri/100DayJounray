namespace MagicMinimalAPI.Models
{
    public class Movies
    {
        public int Id { get; set; }
        public string MovieName { get; set; }
        public DateTime? ReleaseYear { get; set; }
        public string Description { get; set; }
    }
}
