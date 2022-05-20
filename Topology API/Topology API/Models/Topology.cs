using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Topology_API.Models
{
    public class Topology
    {
        public string id { get; set; }
        [JsonProperty("components")]
        //public List<resistor> resistance { get; set; }
        public List<component> components { get; set; }

        public Topology ()
        {
            components = new List<component> ();
        }
    }
}