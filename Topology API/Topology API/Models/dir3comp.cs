using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Topology_API.Models
{
    public class dir3comp : component
    {
        public Specs specs { get; set; }

        public dir3netlist netlist { get; set; }

        public override bool hasnode(string node)
        {
            if(netlist.drain == node || netlist.source == node || netlist.gate == node)
            {
                return true; 
            }
            else
            {
                return false;
            }
        }

        public dir3comp()
        {
            netlist = new dir3netlist();
            specs = new Specs();
        }


        public override void set_component(dynamic json)
        {
            id = json.id;
            type = json.type;
            netlist.source = json.netlist.source;
            netlist.gate = json.netlist.gate;
            netlist.drain = json.netlist.drain;
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