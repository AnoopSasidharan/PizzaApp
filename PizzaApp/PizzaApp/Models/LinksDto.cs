using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaApp.Models
{
    public class LinksDto
    {
        public string Href { get; set; }
        public string Rel { get; set; }
        public string Method { get; set; }
        public LinksDto()
        {

        }
        public LinksDto(string href , string rel , string method)
        {
            this.Href = href;
            this.Method = method;
            this.Rel = rel;
        }
    }
}
