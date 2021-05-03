using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobilityTest.Models
{
    public abstract class Node
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ParentId { get; set; }
        public List<Node> listOfNodes { get; set; } = new List<Node>();

    }
}
