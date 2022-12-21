using System.Collections.Generic;

namespace DBMSWeb.Model
{
    public interface IDBManager
    {
        public Database? GetCurrentDatabase();
        public bool CreateDatabase(string databaseName);
        public bool AddTable(string newTableName);
        public bool AddTable(Table newTable);
        public Table? GetTable(int index);
        public bool AddColumn(int tableIndex, string columnName, string customTypeName);
        public bool AddRow(int tableIndex);
        public bool ChangeValue(string newValue, int tableIndex, int columnIndex, int rowIndex);
        public bool DeleteRow(int tableIndex, int rowIndex);
        public bool DeleteColumn(int tableIndex, int columnIndex);
        public bool DeleteTable(int tableIndex);
        public bool SaveCurrentDatabase();
        public bool LoadDatabase(string databaseName);
        public List<string> GetTablesNameList();
        public bool DeleteDuplicates(Table Table);
    }
}
