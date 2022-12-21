using System;
using System.Collections.Generic;
using System.Text;

namespace DBMSWeb.Model
{ 
    public class Database
    {
        public string name { get; set; }
        public List<Table> tables { get; set; }

        public Database()
        {
            name = "";
            tables = new List<Table>();
        }

        public Database(string _name)
        {
            name = _name;
            tables = new List<Table>();
        }
        
        public bool AddTable(string newTableName)
        {
            if (tables.Count > 0)
            {
                foreach (Table table in tables)
                {
                    if (table.GetName() == newTableName)
                    {
                        return false;
                    }
                }
            }
            tables.Add(new Table(newTableName));
            return true;
        }

        public bool AddTable(Table newTable)
        {
            if (tables.Count > 0)
            {
                foreach (Table table in tables)
                {
                    if (newTable.GetName() == table.GetName())
                    {
                        return false;
                    }
                }
            }
            tables.Add(newTable);
            return true;
        }


        public Table? GetTable(int index)
        {
            if (!isValidIndex(index))
            {
                return null;
            }
            return tables[index];
        }

        public bool ChangeValue(string newValue, int tableIndex, int columnIndex, int rowIndex)
        {
            if (!isValidIndex(tableIndex))
            {
                return false;
            }
            var table = tables[tableIndex];
            return table.ChangeValue(newValue, columnIndex, rowIndex);
        }

        public bool AddColumn(int tableIndex, string columnName, string customTypeName)
        {
            if (!isValidIndex(tableIndex))
            {
                return false;
            }
            var table = tables[tableIndex];
            return table.AddColumn(columnName, customTypeName);
        }

        public bool AddRowToTable(int tableIndex)
        {
            if (!isValidIndex(tableIndex))
            {
                return false;
            }
            tables[tableIndex].AddRow();
            return true;
        }

        public bool DeleteRow(int tableIndex, int rowIndex)
        {
            if (!isValidIndex(tableIndex))
            {
                return false;
            }
            var table = tables[tableIndex];
            table.DeleteRow(rowIndex);
            return true;
        }

        public bool DeleteColumn(int tableIndex, int columnIndex)
        {
            if (!isValidIndex(tableIndex))
            {
                return false;
            }
            var table = tables[tableIndex];
            table.DeleteColumn(columnIndex);
            return true;
        }

        public bool DeleteTable(int tableIndex)
        {
            if (!isValidIndex(tableIndex))
            {
                return false;
            }
            tables.RemoveAt(tableIndex);
            return true;
        }

        public List<string> GetTablesNamesList()
        {
            List<string> tablesNames = new List<string>();
            foreach (Table table in tables)
            {
                tablesNames.Add(table.GetName());
            }
            return tablesNames;
        }

        public string GetName()
        {
            return name;
        }

        private bool isValidIndex(int index)
        {
            return index >= 0 || index < tables.Count;
        }
    }
}
