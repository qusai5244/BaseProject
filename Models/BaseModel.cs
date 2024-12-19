﻿
using System.ComponentModel.DataAnnotations;

namespace BaseProject.Models
{
    public class BaseModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } 
        public DateTime? UpdatedAt { get; set; }
    }
}
