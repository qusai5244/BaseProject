using System.ComponentModel.DataAnnotations;

namespace BaseProject.Dtos.Movies
{
    public class GetMoviesInput : GlobalFilterDto
    {
        [Range(1,int.MaxValue)]
        public int? CinemaId { get; set; }
    }
}
