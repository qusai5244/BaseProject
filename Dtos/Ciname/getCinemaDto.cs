using System.ComponentModel.DataAnnotations;

namespace BaseProject.Dtos.Ciname
{
    public class getCinemaDto
    {
        public int Id { get; set; }

        public string CName { get; set; } = string.Empty;

        public string Clocation { get; set; } = string.Empty;
        public string Cphone { get; set; } = string.Empty;
  
        public string CEmail { get; set; } = string.Empty;
        public string BulidingName { get; set; } = string.Empty;

    }
}
