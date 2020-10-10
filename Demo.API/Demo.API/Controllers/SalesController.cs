using System.Collections.Generic;
using System.Data;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Data.SqlClient;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace Demo.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SalesController : ControllerBase
    {

        private readonly ILogger<SalesController> _logger;
        private readonly IConfiguration _config;

        public SalesController(ILogger<SalesController> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }

        [HttpGet]
        public IEnumerable<dynamic> Get()
        {
            var connectionString = _config.GetConnectionString("dbConnectionString");
            _logger.LogInformation($"Sales {nameof(Get)} called and connection string fetched.");
            using (IDbConnection db = new SqlConnection(connectionString)) 
            {
                return db.Query<dynamic>
                ("SELECT TOP (2) FirstName, LastName, EmailAddress FROM [SalesLT].[Customer]").ToList();
            }
        }
    }
}
