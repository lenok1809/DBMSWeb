using System;
using System.Collections.Generic;
using System.Text;

namespace DBMSWeb.Model
{
    public class Column
    {
        public string name { get; set; }
        public string customTypeName { get; set; }

        public override bool Equals(object obj)
        {
            var otherColumn = obj as Column;
            if (obj == null)
            {
                return false;
            }
            else
            {
                return name == otherColumn.name && customTypeName == otherColumn.customTypeName;
            }
        }
        
        public void SetName(string newName)
        {
            name = newName;
        }

        public override int GetHashCode()
        {
            return (base.GetHashCode() << 2) ^ name.Length;
        }

        public Column()
        {
            name = "";
            customTypeName = "Integer";
        }
        
        public Column(string _name, string _customTypeName)
        {
            name = _name;
            customTypeName = _customTypeName;
        }
        
        public bool EvaluateType(string value)
        {
            return TypesValidator.IsValidValue(customTypeName, value); 
        }

        public string GetName()
        {
            return name;
        }

    }
}
