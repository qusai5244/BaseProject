using BaseProject.Models;
using System.ComponentModel.DataAnnotations;

namespace BaseProject.Dtos.Movie
{
    public class GetMovieListOutput
    {
        public int Id { get; set; }
        public string Title { get; set; }

      
        public string Description { get; set; }

       
        public string Type { get; set; }

       
        public DateTime ReleaseDate { get; set; }

       
        public int Duration { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
