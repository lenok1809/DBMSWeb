using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using DBMSWeb.Model;

namespace DBMSWeb.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DatabaseController : ControllerBase
    {
        private readonly IDBManager _dbManager;
        private readonly ILogger<DatabaseController> _logger;

        public DatabaseController(IDBManager dbManager, ILogger<DatabaseController> logger)
        {
            _dbManager = dbManager;
            _logger = logger;
        }

        [HttpGet]
        public Database GetDatabase()
        {
            return new Database();
        }

        [HttpGet("current")]
        public ActionResult<Database> GetCurrentDatabase()
        {
            var database = _dbManager.GetCurrentDatabase();
            if (database == null)
            {
                return NotFound();
            }
            return database;
        }

        [HttpGet("tables/names")]
        public ActionResult<List<string>> GetTablesList()
        {
            try
            {
                return _dbManager.GetTablesNameList();
            } 
            catch (Exception ex)
            {
                return NotFound();
            }
        }

        [HttpGet("tables")]
        public ActionResult<List<Table>> GetTables()
        {
            try 
            {
                var tablesList = _dbManager.GetCurrentDatabase().tables;
                return tablesList;
            } 
            catch (Exception ex)
            {
                return NotFound();
            }
        }

        [HttpGet("table/{tableIndex}")]
        public ActionResult<Table> GetTable(int tableIndex)
        {
            var table = _dbManager.GetTable(tableIndex);
            if (table == null)
            {
                return NotFound();
            }
            return table;
        }

        [HttpGet("load/{databaseName}")]
        public IActionResult LoadDatabase(string databaseName)
        {
            if (!_dbManager.LoadDatabase(databaseName))
            {
                return NotFound();
            }
            return Ok();
        }


        [HttpPost("create")]
        public IActionResult CreateDatabase(string databaseName)
        {
            if (!_dbManager.CreateDatabase(databaseName))
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpPost("table")]
        public IActionResult CreateTableInCurrentDatabase(string tableName)
        {
            if (!_dbManager.AddTable(tableName))
            {
                return BadRequest();
            }
            return CreatedAtAction(nameof(CreateTableInCurrentDatabase), new { tableName = tableName }, tableName);
        }


        [HttpPost("save")]
        public IActionResult SaveCurrentDatabase()
        {
            if (!_dbManager.SaveCurrentDatabase())
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpPost("table/{tableIndex}/row")]
        public IActionResult AddRow(int tableIndex)
        {
            var table = _dbManager.GetTable(tableIndex);
            if (table == null)
            {
                return NotFound();
            }
            if (!_dbManager.AddRow(tableIndex))
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpPost("table/{tableIndex}/column")]
        public IActionResult AddColumn(int tableIndex, string name, string type)
        {
            var table = _dbManager.GetTable(tableIndex);
            if (table == null)
            {
                return NotFound();
            }
            if (!_dbManager.AddColumn(tableIndex, name, type))
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpPut("duplicates/table/{tableIndex}")]
        public ActionResult<Table> SwapColumns(int tableIndex, int firstColumnIndex, int secondColumnIndex)
        {
            var table = _dbManager.GetTable(tableIndex);
            if (table == null)
            {
                return BadRequest();
            }
            if (!_dbManager.DeleteDuplicates(table))
            {
                return BadRequest();
            }
            return Ok();
        }


        [HttpPut("table/{tableIndex}/column/{columnIndex}/row/{rowIndex}")]
        public IActionResult UpdateCellValue(int tableIndex, int columnIndex, int rowIndex, string value)
        {
            var table = _dbManager.GetTable(tableIndex);
            if (table == null)
            {
                return NotFound();
            }
            if (!_dbManager.ChangeValue(value, tableIndex, columnIndex, rowIndex))
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpDelete("table/{tableIndex}")]
        public IActionResult RemoveTable(int tableIndex)
        {
            if (!_dbManager.DeleteTable(tableIndex))
            {
                return BadRequest();
            }
            return NoContent();
        }

        [HttpDelete("table/{tableIndex}/column/{columnIndex}")]
        public IActionResult RemoveColumn(int tableIndex, int columnIndex)
        {
            if (!_dbManager.DeleteColumn(tableIndex, columnIndex))
            {
                return BadRequest();
            }
            return NoContent();
        }

        [HttpDelete("table/{tableIndex}/row/{rowIndex}")]
        public IActionResult RemoveColum(int tableIndex, int rowIndex)
        {
            if (!_dbManager.DeleteRow(tableIndex, rowIndex))
            {
                return BadRequest();
            }
            return NoContent();
        }
    }
}
