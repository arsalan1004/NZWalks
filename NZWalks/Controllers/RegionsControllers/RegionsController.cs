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
    private readonly NZWalksDbContext _dbContext;
    
    // Constructor to initialize the db context for further use
    public RegionsController(NZWalksDbContext dbOptions)
    {
        this._dbContext = dbOptions;
    }
    
    // GET all Regions
    // http://localhost:5027/api/Regions
    [HttpGet]
    public IActionResult GetAllRegions()
    {
        var regionsDomain = _dbContext.Regions.ToList();

        var regionsDto = regionsDomain.Select(region => new RegionDto { Id = region.Id, Code = region.Code, Name = region.Name, RegionImageURL = region.RegionImageURL }).ToList();

        return Ok(regionsDto);
    }
    
    // GET REGION BY ID
    [HttpGet]
    [Route("{id:guid}")]
    public IActionResult GetRegById([FromRoute] Guid id)
    {
        var region = _dbContext.Regions.Find(id);

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
        _dbContext.Regions.Add(regionDomainModel);
        _dbContext.SaveChanges();

        return CreatedAtAction(nameof(GetRegById), new {id = regionDomainModel.Id}, region);
    }
    
    // Delete a Region
    [HttpDelete]
    [Route("{id:guid}")]
    public IActionResult DeleteRegion([FromRoute] Guid id)
    {
        var region = _dbContext.Regions.Find(id);
        if (region is null) return BadRequest("Region to delete does not exist");
        _dbContext.Regions.Remove(region);
        _dbContext.SaveChanges();
        return Ok("Successfully Deleted");
    }
    
    // Update a Region
    [HttpPut]
    [Route("{id:guid}")]
    public IActionResult UpdateRegion([FromRoute] Guid id, [FromBody] AddRegionDto region)
    {
        var regionFromDb = _dbContext.Regions.Find(id);
        if (regionFromDb is null) return BadRequest();
        _dbContext.Entry(region).State = EntityState.Modified;
        _dbContext.SaveChanges();
        return NoContent();
    }
}