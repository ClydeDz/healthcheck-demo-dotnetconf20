using System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Data.SqlClient;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace Demo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PingController : ControllerBase
    {
        private readonly ILogger<PingController> _logger;
        private readonly IConfiguration _config;

        public PingController(ILogger<PingController> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var connectionString = _config.GetConnectionString("dbConnectionString");
            _logger.LogInformation($"Ping {nameof(Get)} called and connection string fetched.");
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var dbCheck = db.Query<dynamic>("SELECT 1");

                if (dbCheck != null)
                {
                    return Ok("Healthy");
                }

                return BadRequest();
            }
        }
    }
}
