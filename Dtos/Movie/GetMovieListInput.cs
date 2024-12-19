using BaseProject.Models;

namespace BaseProject.Dtos.Movie
{
    public class GetMovieListInput : GlobalFilterDto
    {
        public MovieSortByTypes? SortBy { get; set; }
        public MovieType? Type { get; set; }

    }

    public enum MovieSortByTypes
    {
        Type,
        ReleaseDate

    }
}
