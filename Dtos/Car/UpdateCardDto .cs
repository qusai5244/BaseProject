﻿using BaseProject.Models;
using System.ComponentModel.DataAnnotations;

namespace BaseProject.Dtos.Car
{
    public class UpdateCardDto
    {
        [Required]
        public int Year { get; set; }

        [Range(0, 500000)]
        public int Mileage { get; set; }

        [MaxLength(255)]
        public string Color { get; set; }

        [Required]
        public int Price { get; set; }
        [Required]
        public FuelType FuelType { get; set; }

        [Required]
        public TransmissionType TransmissionType { get; set; }
    }
}
