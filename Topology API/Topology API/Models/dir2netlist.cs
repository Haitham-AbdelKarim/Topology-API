using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Topology_API.Models
{
    public class dir2netlist
    {
        [Required]
        public string pterm { get; set; }

        [Required]
        public string nterm { get; set; }

    }
}