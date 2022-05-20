using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Topology_API.Models
{
    public abstract class component
    {
        public string type { get; set; }
        public string id { get; set; }

        public abstract bool hasnode(string node);

        //public abstract void set_netlist(dynamic jnetlist);

        //public abstract void set_specs(dynamic jnetlist);

        public abstract void set_component(dynamic jnetlist);

    }
}