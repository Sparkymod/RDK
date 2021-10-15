namespace RDK.Core.Models
{
    public class NodeAttribute
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public string Type { get; set; }

        public NodeAttribute()
        {
            Name = "attribute";
            Value = "value";
            Type = "type";
        }

        public NodeAttribute(string name, string value)
        {
            Name = name;
            Value = value;
            Type = "type";
        }

        public override string ToString() => $"Name: {Name}, Value: {Value}";
    }
}
