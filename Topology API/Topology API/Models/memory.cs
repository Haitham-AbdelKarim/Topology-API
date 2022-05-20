using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Topology_API.Models
{
    public static class memory
    {
        public static List<Topology> memo = new List<Topology>();
        public static HashSet<string> topologiesId = new HashSet<string>();

    }
}