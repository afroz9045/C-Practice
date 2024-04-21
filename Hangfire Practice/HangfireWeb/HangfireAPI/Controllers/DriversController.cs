using Hangfire;
using HangfireAPI.Models;
using HangfireAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace HangfireAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class DriversController : ControllerBase
{
    private static List<Driver> drivers = new();
    private readonly ILogger<DriversController> _logger;

    public DriversController(ILogger<DriversController> logger)
    {
        _logger = logger;
    }

    [HttpPost]
    public IActionResult AddDriver(Driver driver)
    {
        if(ModelState.IsValid)
        {
            drivers.Add(driver);
            var jobId = BackgroundJob.Enqueue<IServiceManagement>(x=>x.SendEmail());
            return CreatedAtAction("GetDriver",new {driver.Id},driver);
        }
        return BadRequest();
    }

    [HttpGet]
    public IActionResult GetDriver(Guid id)
    {
        var driver = drivers.FirstOrDefault(x=>x.Id == id);
        if(driver is null)
            return NotFound();
        return Ok(driver);
    }

    [HttpDelete]
    public IActionResult DeleteDriver(Guid id)
    {
        var driver = drivers.FirstOrDefault(x=>x.Id == id);
        if(driver is null)
            return NotFound();
        driver.Status = 0;
        return NoContent();
    }


}
