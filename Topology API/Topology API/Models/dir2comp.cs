using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Topology_API.Models
{
    public class dir2comp : component
    {
        public Specs specs { get; set; }

        public dir2netlist netlist { get; set; }

        public override bool hasnode(string node)
        {
            if (netlist.pterm == node || netlist.nterm == node)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public dir2comp()
        {
            netlist = new dir2netlist();
            specs = new Specs();
        }


        public override void set_component(dynamic json)
        {
            id = json.id;
            type = json.type;
            netlist.pterm = json.netlist.pterm;
            netlist.nterm = json.netlist.nterm;
            string attrname = "";
            foreach (var prop in (JObject)json)
            {
                string nmofprop = (prop.Key);
                if (nmofprop != "type" && nmofprop != "id" && nmofprop != "netlist")
                {
                    attrname = nmofprop;
                }

            }
            specs.min = json[attrname].min;
            specs.max = json[attrname].max;
            specs.defualt = json[attrname]["default"];


        }
    }
}