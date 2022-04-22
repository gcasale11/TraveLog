using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TraveLog.Data
{
    public class Visited
    {
        [Key]
        public int VisitedId { get; set; }

        public DateTime DateVisited {get; set;}

        public string InitialThoughts { get; set; }
        
        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }


        

     


    }
}
