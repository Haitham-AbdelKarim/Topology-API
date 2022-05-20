using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Topology_API.Models
{
    public class undir2netlist
    {
        [Required]
        public string t1 { get; set; }

        [Required]
        public string t2 { get; set; }

    }
}