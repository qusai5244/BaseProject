﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using BaseProject.Models;

namespace BaseProject.Dtos.Movie
{
    public class AddNewMovieDto
    {
        [Required]
        [MaxLength(100)]
        public string MName { get; set; } = string.Empty;
        [Required]
        [MaxLength(100)]
        public string Title { get; set; } = string.Empty;
        [Required]
        [MaxLength(500)]
        public string Description { get; set; } = string.Empty;

        [Required]

        public MType Mtype { get; set; }
        [Required]

        public DateTime release_date { get; set; }
        [Required]

        public int duration { get; set; }
        [Required]
        public string language { get; set; } = string.Empty;




    }
}
