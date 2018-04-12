using System;

namespace SqlReflect.Attributes
{
    public class PKAttribute : Attribute
    {
        public bool AutoIncrement { get; set; }

        public PKAttribute() : this(true)
        {

        }
        public PKAttribute(bool autoIncrement)
        {
            this.AutoIncrement = autoIncrement;
        }
    }
}
