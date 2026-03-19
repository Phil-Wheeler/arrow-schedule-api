using System.Security.Claims;
using Arrow.Data;
using Arrow.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch.SystemTextJson;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Arrow.Api.Controllers;

[Route("api/v1/[controller]/{id?}")]
[ApiController]
[Authorize]
public class BusinessController : ControllerBase
{
    private readonly ArrowContext _context;


    public BusinessController(ArrowContext context)
    {
        _context = context;
    }


    [HttpGet]
    public IActionResult Get()
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

        try
        {
            var business = _context.Businesses.FirstOrDefault(x => x.OwnerId == userId);

            if (business == null)
            {
                return NotFound();
            }

            return Ok(business);
        }
        catch (Exception ex)
        {
            var customResponse = new
            {
                Code = 500,
                Message = "Business or owner not found",
            };

            return StatusCode(StatusCodes.Status500InternalServerError);
        }

    }

    [HttpPut]
    public IActionResult Put(Business business)
    {
        _context.Businesses.Attach(business);
        _context.SaveChanges();

        return NoContent();
    }

    [HttpPatch]
    public IActionResult Patch(string id, JsonPatchDocument<Business> patchDoc)
    {
        var business = _context.Businesses.FirstOrDefault(b => b.Id == Guid.Parse(id));

        if (business == null)
        {
            return NotFound();
        }

        patchDoc.ApplyTo(business, jsonPatchError =>
        {
            var key = jsonPatchError.AffectedObject.GetType().Name;
            ModelState.AddModelError(key, jsonPatchError.ErrorMessage);
        });
        

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        _context.SaveChanges();

        return Ok(business);
    }

    [HttpPost]
    public IActionResult Post(Business business)
    {
        business.DateCreated = DateTime.UtcNow;

        // Only create a new business if the registered owner has not previously created one
        if (!_context.Businesses.Any(x => x.OwnerId == Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier))))
        {
            _context.Businesses.Add(business);
            _context.SaveChanges(); 
            return Ok(business);
        }

        return BadRequest("Business or owner not found");
    }

    [HttpPost("location")]
    public IActionResult PostLocation(Guid id, Location location)
    {
        var owner = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

        try
        {
            // Only create a new location if the registered owner has defined a valid business
            if (_context.Businesses.Any(x => x.OwnerId == owner))
            {
                _context.Businesses.FirstOrDefault(x => x.Id == id).Locations.Add(location);
                _context.SaveChanges();
                return Ok(location);
            }

            return NotFound("Business or owner not found");
        }
        catch(Exception ex)
        {
            throw;
        }
    }

    [HttpPatch("location/{locationId}")]
    public IActionResult PatchLocation(string id, int locationId, JsonPatchDocument<Location> patchDoc)
    {
        var business = _context.Businesses
            .Include(b => b.Locations)
            .FirstOrDefault(b => b.Id == Guid.Parse(id));


        if (business == null)
        {
            return NotFound("Business not found");
        }

        var location = business.Locations.FirstOrDefault(l => l.Id == locationId);

        patchDoc.ApplyTo(location, jsonPatchError =>
        {
            var key = jsonPatchError.AffectedObject.GetType().Name;
            ModelState.AddModelError(key, jsonPatchError.ErrorMessage);
        });
        

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        _context.SaveChanges();

        return Ok(location);
    }

    [HttpGet("location/{locationId?}")]
    public IActionResult GetLocation(Guid id, int? locationId)
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

        try
        {
            var business = _context.Businesses.Include(b => b.Locations).FirstOrDefault(x => x.OwnerId == userId && x.Id == id);

            if (business == null)
            {
                return NotFound();
            }

            if (business.Locations.Any(l => l.Id == locationId))
            {
                return Ok(business.Locations.FirstOrDefault(l => l.Id == locationId));
            }

            Console.WriteLine($"Business {business.Name} has {business.Locations.Count} locations.");
            return Ok(business.Locations);
        }
        catch (Exception ex)
        {
            throw;
        }

    }
}