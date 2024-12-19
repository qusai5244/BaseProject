using BaseProject.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BaseProject.Dtos.Movie
{
    public class getMovieListDto
    {

        public int Id { get; set; }
        public string MName { get; set; } = string.Empty;

        public string Title { get; set; } = string.Empty;
 
        public string Description { get; set; } = string.Empty;

        public MType Mtype { get; set; }
        [Required]

        public DateTime release_date { get; set; }

        public int duration { get; set; }
        public string language { get; set; } = string.Empty;

        public bool isDeleted { get; set; }



    }
}
