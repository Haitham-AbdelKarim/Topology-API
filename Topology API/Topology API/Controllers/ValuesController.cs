using Grpc.Core;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Topology_API.Models;

namespace Topology_API.Controllers

    
{

    public class ValuesController : ApiController
    {
        // GET api/values

        int knowtype(string type)
        {
            HashSet<string> undir2 = new HashSet<string>() { "resistor", "capacitor", "inductor" };
            HashSet<string> dir2 = new HashSet<string>() { "battery" , "diode" };
            HashSet<string> dir3 = new HashSet<string>() { "nmos", "pmos" };

            if (undir2.Contains(type))
                return 1;
            else if (dir2.Contains(type))
                return 2;
            else if (dir3.Contains(type))
                return 3;
            else return -1;
        }

        public List<Topology> Get()
        {
            return memory.memo;
        }

        [HttpPost]
        public dynamic WriteJSON (string topologyId)
        {
            for (int i = 0; i < memory.memo.Count; i++)
            {
                if (memory.memo[i].id == topologyId)
                {
                    string filename = memory.memo[i].id + ".json";
                    string fullpath = @"D:\H\Master Micro Tasks\Topology API\" + filename;
                    string json = JsonConvert.SerializeObject(memory.memo[i], Formatting.Indented);
                    File.WriteAllText(fullpath, json);
                    return memory.memo[i];

                }
            }
            return "Topology Not Exist";
        }


        [Route("api/queryDevices")]
        [HttpGet]
        public List<string> queryDevices(string topologyId)
        {
            // check if no component
            List<string> devices = new List<string>();
            if (memory.topologiesId.Contains(topologyId))
            {
                for (int i = 0; i < memory.memo.Count; i++)
                {
                    if (memory.memo[i].id == topologyId)
                    {
                        for(int j=0; j< memory.memo[i].components.Count; j++)
                        {
                            devices.Add(memory.memo[i].components[j].id);
                        }
                        return devices;


                    }
                }
            }
            return devices;
        }


        [Route("api/queryDevicesWithNetlistNode")]
        [HttpGet]
        public List<string> queryDevicesWithNetlistNode(string topologyId, string nodeId)
        {
            List<string> devices = new List<string>();
            if (memory.topologiesId.Contains(topologyId))
            {
                for (int i = 0; i < memory.memo.Count; i++)
                {
                    if (memory.memo[i].id == topologyId)
                    {
                        for (int j = 0; j < memory.memo[i].components.Count; j++)
                        {
                            if (memory.memo[i].components[j].hasnode(nodeId))
                            {
                                devices.Add(memory.memo[i].components[j].id);
                            }
                        }
                        return devices;


                    }
                }
            }
            return devices;
        }


        // GET api/values/5

        public dynamic Get(string filename)
        {
            string json = new StreamReader(filename).ReadToEnd();
            Topology topology = new Topology();
            dynamic stuff = JsonConvert.DeserializeObject(System.IO.File.ReadAllText(filename));
            topology.id = stuff.id;
            if (memory.topologiesId.Contains(topology.id))
                return "Topology already Exist";
            int numofcomponents = stuff.components.Count;
            for (int i = 0; i < numofcomponents; i++)
            {
                int type = knowtype((string)stuff.components[i].type);
                component comp = null;
                if (type == 1)
                    comp = new undir2comp();
                else if (type == 2)
                    comp = new dir2comp();
                else if (type == 3)
                    comp = new dir3comp();
                comp.set_component(stuff.components[i]);
                topology.components.Add(comp);
            }
            memory.memo.Add(topology);
            memory.topologiesId.Add(topology.id);
            return topology ;
        }
        //public string Get(string filename)
        //{
        //    //string filePath = Server.MapPath("~/JSON/VideoFeeder.json");
        //    string json = new StreamReader(filename).ReadToEnd();
        //    var model = JsonConvert.DeserializeObject<dynamic>(System.IO.File.ReadAllText(filename));

        //    Topology topology = new Topology();
        //    //topology.components = new List<component>();

        //    dynamic stuff = JsonConvert.DeserializeObject(System.IO.File.ReadAllText(filename));
        //    topology.id = stuff.id;
        //    if (memory.topologiesId.Contains(topology.id))
        //        return "Topology already Exist";
        //    int resmin = stuff.components.Count;
        //    for (int i = 0; i < resmin; i++)
        //    {
        //        //var prop = ((JObject)stuff.components[i]).Properties();

        //        foreach (var prop in (JObject)stuff.components[i])
        //        {
        //            string nmofprop = (prop.Value.);
        //            if (nmofprop != "type" && nmofprop != "id" && nmofprop != "netlist")
        //            {
        //                int s = stuff.components[i][nmofprop].min;
        //            }

        //        }
        //        if (stuff.components[i].type == "resistor")
        //        {
        //            component comp = new resistor();
        //            comp.id = stuff.components[i].id;
        //            comp.type = stuff.components[i].type;

        //            ((resistor)comp).netlist = new undir2();
        //            ((resistor)comp).netlist.t1 = stuff.components[i].netlist.t1;
        //            ((resistor)comp).netlist.t2 = stuff.components[i].netlist.t2;

        //            ((resistor)comp).resistance = new specs();
        //            ((resistor)comp).resistance.min = stuff.components[i].resistance.min;
        //            ((resistor)comp).resistance.max = stuff.components[i].resistance.max;
        //            ((resistor)comp).resistance.defualt = stuff.components[i].resistance["default"];

        //            topology.components.Add(comp);
        //        }
        //        else if (stuff.components[i].type == "nmos")
        //        {
        //            component comp = new nmostransistor();
        //            comp.id = stuff.components[i].id;
        //            comp.type = stuff.components[i].type;

        //            //((nmostransistor)comp).netlist = new dir3();

        //            //var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        //            //var jsonabc = serializer.Serialize(stuff.components[i].netlist);
        //            //var c = serializer.Deserialize<dir3>(jsonabc);


        //            ((nmostransistor)comp).netlist.source = stuff.components[i].netlist.source;
        //            ((nmostransistor)comp).netlist.drain = stuff.components[i].netlist.drain;
        //            ((nmostransistor)comp).netlist.gate = stuff.components[i].netlist.gate;

        //            //((nmostransistor)comp).m1 = new specs();
        //            ((nmostransistor)comp).m1.max = stuff.components[i]["m(l)"].max;
        //            ((nmostransistor)comp).m1.min = stuff.components[i]["m(l)"].min;
        //            ((nmostransistor)comp).m1.defualt = stuff.components[i]["m(l)"]["default"];

        //            topology.components.Add(comp);
        //        }
        //    }
        //    memory.memo.Add(topology);
        //    memory.topologiesId.Add(topology.id);
        //    //int val = model.components[0].resistance[1];
        //    return JsonConvert.SerializeObject(topology);
        //    //return "value";
        //}

        // POST api/values
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        public string Delete(string id)
        {
            if (memory.topologiesId.Contains(id))
            {
                for(int i = 0; i < memory.topologiesId.Count; i++)
                {
                    if (memory.memo[i].id == id)
                    {
                        memory.memo.RemoveAt(i);
                        memory.topologiesId.Remove(id);
                        return "Topology has been deleted";
                    }
                }
            }
            return "Topology Not Exist";
        }
    }
}
