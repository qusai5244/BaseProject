namespace BaseProject.Models
{
    public class Movie : BaseModel
    {

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
       
        public MovieType Type { get; set; }

        public DateTime ReleaseDate { get; set; }

        public int Duration { get; set; }



    }

    public enum MovieType
    {
        Action=1,
        Comedy=2,
        Drama=3
    }
}
