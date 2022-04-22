using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TraveLog.Models
{
    public class BlogCreate
    {
        [Required]
        public string Thoughts { get; set; }

        public int LocationId { get; set; }
        public int CountryId { get; set; }
        public int VisitedId { get; set; }
    }
}
