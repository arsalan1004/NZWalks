using Microsoft.AspNetCore.Mvc;
using NZWalks.Data;

namespace NZWalks.Controllers.RegionsControllers;
[Route("api/[controller]")]
[ApiController]
public class RegionsController : Controller
{
    private readonly NZWalksDbContext dbContext;
    
    // Constructor to initialize the db context for further use
    public RegionsController(NZWalksDbContext dbOptions)
    {
        this.dbContext = dbOptions;
    }
    
    // GET all Regions
    // httpp://localhost:5027/api/Regions
    [HttpGet]
    public IActionResult GetAllRegions()
    {
        var regions = dbContext.Regions.ToList();
        return Ok(regions);
    }
}