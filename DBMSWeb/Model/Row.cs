using System;
using System.Collections.Generic;
using System.Linq;

namespace DBMSWeb.Model
{
    public class Row
    {
        public List<string> valuesList { get; set; }

        public Row()
        {
            valuesList = new List<string>();
        }

        public Row(List<string> info)
        {
            valuesList = new List<string>();
            for (int i = 0; i < info.Count; ++i)
            {
                valuesList.Add(info[i]);
            }
        }

        public override bool Equals(object obj)
        {
            var otherRow = obj as Row;
            if (otherRow == null)
            {
                return false;
            }
            return Enumerable.SequenceEqual(valuesList, otherRow.valuesList);
        }

        public override int GetHashCode()
        {
            return (base.GetHashCode() << 2) ^ (valuesList.Count) + 1;
        }

        public List<string> GetValuesList()
        {
            return valuesList;
        }
    }
}
