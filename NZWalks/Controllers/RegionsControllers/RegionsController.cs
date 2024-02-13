using Microsoft.AspNetCore.Mvc;
using NZWalks.Data;
using NZWalks.Models.DTO;

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
    // http://localhost:5027/api/Regions
    [HttpGet]
    public IActionResult GetAllRegions()
    {
        var regionsDomain = dbContext.Regions.ToList();

        var regionsDto = new List<RegionDto>();

        foreach (var region in regionsDomain)
        {
            regionsDto.Add(new RegionDto
            {
                Id = region.Id,
                Code = region.Code,
                Name = region.Name,
                RegionImageURL = region.RegionImageURL
            });
        }
        return Ok(regionsDto);
    }
    
    // GET REGION BY ID
    [HttpGet]
    [Route("{id:guid}")]
    public IActionResult GetRegById([FromRoute] Guid id)
    {
        var region = dbContext.Regions.Find(id);

        if (region == null)
        {
            return NotFound();
        }

        var regionDto = new RegionDto
        {
            Id = region.Id,
            Code = region.Code,
            Name = region.Name,
            RegionImageURL = region.RegionImageURL
        };
        return Ok(regionDto);
    }
}