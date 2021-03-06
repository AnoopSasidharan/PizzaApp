using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaApp.Models
{
    public class PizzaDto
    {
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        [Required]
        [Range(1,100)]
        public double Price { get; set; }
        public IEnumerable<LinksDto> Links { get; set; } = new List<LinksDto>();
    }
}
