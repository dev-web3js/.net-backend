using System;
using Microsoft.AspNetCore.Mvc;

namespace Houseiana.Controllers;

[ApiController]
[Route("api")]
public class HomeController : ControllerBase
{
    /// <summary>
    /// Get API health status
    /// </summary>
    [HttpGet]
    public ActionResult<object> GetHello()
    {
        return Ok(new
        {
            message = "🚀 Houseiana API is running!",
            version = "1.0.0",
            timestamp = DateTime.UtcNow,
            environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"
        });
    }

    /// <summary>
    /// Get API health status
    /// </summary>
    [HttpGet("health")]
    public ActionResult<object> GetHealth()
    {
        return Ok(new
        {
            status = "healthy",
            timestamp = DateTime.UtcNow,
            uptime = Environment.TickCount64
        });
    }
}