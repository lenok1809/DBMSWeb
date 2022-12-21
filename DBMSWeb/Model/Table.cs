using System;
using System.Collections.Generic;
using System.Linq;

namespace DBMSWeb.Model
{
    public class Table
    {
        public string name { get; set; }
        public List<Column> columnsList { get; set; }
        public List<Row> rowsList { get; set; }

        public override bool Equals(object obj)
        {
            var otherTable = obj as Table;
            if (otherTable == null)
            {
                return false;
            }
            return Enumerable.SequenceEqual(columnsList, otherTable.columnsList) 
                    && Enumerable.SequenceEqual(rowsList, otherTable.rowsList)
                    && name == otherTable.name;
        }

        public override int GetHashCode()
        {
            return (base.GetHashCode() << 2) ^ (columnsList.Count + rowsList.Count);
        }

        public Table()
        {
            name = "";
            columnsList = new List<Column>();
            rowsList = new List<Row>();
        }

        public Table(string _name)
        {
            name = _name;
            columnsList = new List<Column>();
            rowsList = new List<Row>();
        }

        public Table(string _name, 
                        List<Column> _columnsList,
                        List<Row> _rowsList)
        {
            name = _name;
            columnsList = _columnsList;
            rowsList = _rowsList;
        }
        
        public bool ChangeColumnName(int index, string newName)
        {
            if (columnsList.Count < index)
            {
                return false;
            }
            columnsList[index].SetName(newName);
            return true;
        }

        public bool ChangeValue(string newValue, int columnIndex, int rowIndex)
        {
            if (!columnsList[columnIndex].EvaluateType(newValue))
            {
                return false;
            }
            rowsList[rowIndex].GetValuesList()[columnIndex] = newValue;
            return true;
        }

        public bool AddColumn(string columnName, string customTypeName)
        {
            foreach (Column column in columnsList)
            {
                if (column.GetName() == columnName)
                {
                    return false;
                }
            }
            columnsList.Add(new Column(columnName, customTypeName));
            foreach (Row row in rowsList)
            {
                row.GetValuesList().Add("");
            }
            return true;
        }

        public bool AddRow()
        {
            if (columnsList.Count <= 0)
            {
                return false;
            }

            if (rowsList.Count < 0)
            {
                return false;
            }

            rowsList.Add(new Row());
            var columnsCount = columnsList.Count;
            for (int columnIndex = 0; columnIndex < columnsCount; ++columnIndex)
            {
                rowsList[rowsList.Count - 1].GetValuesList().Add("");
            }
            return true;
        }

        public bool DeleteRow(int rowIndex)
        {
            if (rowIndex < 0 || rowIndex >= rowsList.Count)
            {
                return false;
            }
            rowsList.RemoveAt(rowIndex);
            return true;
        }

        public bool DeleteColumn(int columnIndex)
        {
            if (columnIndex < 0 || columnIndex >= columnsList.Count)
            {
                return false;
            }
            columnsList.RemoveAt(columnIndex);
            foreach (Row row in rowsList)
            {
                row.GetValuesList().RemoveAt(columnIndex);
            }
            return false;
        }

        public List<Column> Columns()
        {
            return columnsList;
        }


        public List<Row> Rows()
        {
            return rowsList;
        }

        public void SetColumns(List<Column> columns)
        {
            columnsList = columns;
        }

        public void SetRows(List<Row> rows)
        {
            rowsList = rows;
        }



        public string GetName()
        {
            return name;
        }
    }
}
