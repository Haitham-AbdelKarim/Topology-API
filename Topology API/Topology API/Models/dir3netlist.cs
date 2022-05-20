using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Topology_API.Models
{
    public class dir3netlist
    {
        [Required]
        public string drain { get; set; }

        [Required]
        public string source { get; set; }

        [Required]
        public string gate { get; set; }

    }
}