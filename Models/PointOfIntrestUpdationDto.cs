﻿
using System.ComponentModel.DataAnnotations;

namespace city_info_api.Models
{
    public class PointOfIntrestUpdationDto
    {
        [Required(ErrorMessage = "You should provide a name value")]
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(200)]
        public string Description { get; set; }

    }
}
