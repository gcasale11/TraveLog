﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TraveLog.Models
{
    public class BlogListItem
    {
        public int BlogId { get; set; }
        public int VisitedId { get; set; }
        public string ProfileName { get; set; }
        public string Thoughts { get; set; }
    }
}
