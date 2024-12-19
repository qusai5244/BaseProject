

using System.ComponentModel.DataAnnotations;

namespace BaseProject.Models
{
    
        public class Cinema : BaseModel
        {

        [Required]
        [StringLength(500)]
        public string Name { get; set; }


        [Required]
        [StringLength(500)]
        public string Location { get; set; }

            
          public ICollection<Hall> Halls { get; set; }
          public ICollection<MovieSchedule> MovieSchedules { get; set; }
        }
    }


