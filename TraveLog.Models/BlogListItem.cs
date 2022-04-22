using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TraveLog.Models
{
    public class BlogListItem
    {
        public int BlogId { get; set; }
        public DateTime DateVisited { get; set; }
        public string InitialThoughts { get; set; }
        public string Cities { get; set; }
        public string CountryName { get; set; }
        public string Thoughts { get; set; }
    }
}
