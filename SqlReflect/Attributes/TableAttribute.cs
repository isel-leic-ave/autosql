using System;

namespace SqlReflect.Attributes
{
    public class TableAttribute : Attribute
    {
        public string Name { get; set;  }

        public TableAttribute(string name)
        {
            Name = name;
        }
    }
}
