namespace BaseProject.Models
{
    public class Cinema : BaseModel
    {
        public string Name { get; set; }
        public string Location { get; set; }

        // Navigation properties
        public ICollection<Hall> Halls { get; set; }
        public ICollection<Movie> Movies { get; set; }
    }
}
