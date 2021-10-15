using System.Collections.Generic;

namespace RDK.Core.Models
{
    public class Node
    {
        public string Name { get; set; }
        public List<NodeAttribute> Attributes { get; set; }
        public List<Node> ChildNodes { get; set; }

        public Node()
        {
            Name = "node";
            Attributes = new();
            ChildNodes = new();
        }

        public Node(string name, List<NodeAttribute> attributes)
        {
            Name = name;
            Attributes = attributes;
            ChildNodes = new();
        }

        public Node(string name, List<NodeAttribute> attributes, List<Node> childNodes)
        {
            Name = name;
            Attributes = attributes;
            ChildNodes = childNodes;
        }

        public override string ToString() => $"Node: {Name}, Attributes: {Attributes}, ChildNodes: {ChildNodes}";
    }
}
