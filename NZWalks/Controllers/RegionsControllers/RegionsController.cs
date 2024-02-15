using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.Data;
using NZWalks.Models.Domains;
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

        var regionsDto = regionsDomain.Select(region => new RegionDto { Id = region.Id, Code = region.Code, Name = region.Name, RegionImageURL = region.RegionImageURL }).ToList();

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
        return Ok(new RegionDto
        {
            Id = region.Id,
            Code = region.Code,
            Name = region.Name,
            RegionImageURL = region.RegionImageURL
        });
    }
    
    // POST a region
    [HttpPost]
    public IActionResult InsertRegion([FromBody] AddRegionDto region)
    {
        var regionDomainModel = new Region
        {
            Code = region.Code,
            Name = region.Name,
            RegionImageURL = region.RegionImageURL
        };
        dbContext.Regions.Add(regionDomainModel);
        dbContext.SaveChanges();

        return Created("Success", regionDomainModel);
    }
    
    // Delete a Region
    [HttpDelete]
    [Route("{id:guid}")]
    public IActionResult DeleteRegion([FromRoute] Guid id)
    {
        var region = dbContext.Regions.Find(id);
        if (region is null) return BadRequest("Region to delete does not exist");
        dbContext.Regions.Remove(region);
        dbContext.SaveChanges();
        return Ok("Successfully Deleted");
    }
    
    // Update a Region
    [HttpPut]
    [Route("{id:guid}")]
    public IActionResult UpdateRegion([FromRoute] Guid id, [FromBody] AddRegionDto region)
    {
        var regionFromDb = dbContext.Regions.Find(id);
        if (regionFromDb is null) return BadRequest();
        dbContext.Entry(region).State = EntityState.Modified;
        dbContext.SaveChanges();
        return NoContent();
    }
}