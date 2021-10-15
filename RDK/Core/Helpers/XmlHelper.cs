using RDK.Core.Models;
using System.Collections.Generic;
using System.Xml;

namespace RDK.Core.Helpers
{
    public static class XmlHelper
    {
        public static List<NodeAttribute> GetAttributesFromNode(XmlNode currentNode)
        {
            if (currentNode == null || currentNode.Attributes.Count == 0)
            {
                return new();
            }

            List<NodeAttribute> attributes = new();

            foreach (XmlAttribute xmlAttribute in currentNode.Attributes)
            {
                attributes.Add(new NodeAttribute(xmlAttribute.Name, xmlAttribute.Value));
            }

            return attributes;
        }

        public static List<Node> GetNodesFromChildNodes(XmlNodeList currentNodes)
        {
            if (currentNodes == null || currentNodes.Count == 0)
            {
                return new();
            }

            List<Node> nodes = new();
            foreach (XmlNode child in currentNodes)
            {
                Node node;

                if (child.ChildNodes.Count >= 1)
                {
                    node = new Node(child.Name, GetAttributesFromNode(child), GetNodesFromChildNodes(child.ChildNodes));
                }
                else
                {
                    node = new Node(child.Name, GetAttributesFromNode(child));
                }

                nodes.Add(node);
            }

            return nodes;
        }
    }
}
