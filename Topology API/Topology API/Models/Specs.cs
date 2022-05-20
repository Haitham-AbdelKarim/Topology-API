using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Topology_API.Models
{
    public class Specs
    {
        [JsonProperty("default")]

        public float defualt { get; set; }
        public float min { get; set; }
        public float max { get; set; }

    }
}