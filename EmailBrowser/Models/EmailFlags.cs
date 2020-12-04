using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmailBrowser.Models
{
    public class EmailFlags
    {
        public bool? Seen { get; set; }
        public bool? Answered { get; set; }
        public bool? Draft { get; set; }
        public bool? Recent { get; set; }
        public bool? Flagged { get; set; }
        public bool? Deleted { get; set; }
    }
}