using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TraveLog.Models
{
    public class CountryCreate
    {
        
        [Required]
        public string CountryName { get; set; }
        public string Continent { get; set; }

    }
}
