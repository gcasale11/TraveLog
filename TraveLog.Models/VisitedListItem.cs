using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TraveLog.Models
{
    public class VisitedListItem
    {
        public int VisitedId { get; set; }

        [Display(Name="Date Visited")]
        public DateTime DateVisited { get; set; }
        public string CountryName { get; set; }
        public string Cities { get; set; }
        public string InitialThoughts { get; set; }

    }
}
