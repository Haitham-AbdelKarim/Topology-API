using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Topology_API.Models
{
    public class undir2comp : component
    {
        public Specs specs { get; set; }
        
        public undir2netlist netlist { get; set; }

        //static readonly string nameofattr;

        public override bool hasnode(string node)
        {
            if (netlist.t1 == node || netlist.t2 == node)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public undir2comp()
        {
            netlist = new undir2netlist();
            specs = new Specs();
        }


        public override void set_component(dynamic json)
        {
            id = json.id;
            type = json.type;
            netlist.t1 = json.netlist.t1;
            netlist.t2 = json.netlist.t2;
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