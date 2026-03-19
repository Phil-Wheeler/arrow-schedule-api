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
public class ServiceController : ControllerBase
{
    private readonly ArrowContext _context;


    public ServiceController(ArrowContext context)
    {
        _context = context;
    }


    [HttpGet]
    public IActionResult Get(Guid? id)
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

        try
        {
            if (id == null)
            {
                // Ensure we're reading from the authorised business
                var business = _context.Businesses.Include(b => b.Locations).ThenInclude(s => s.Services).FirstOrDefault(b => b.OwnerId == userId);

                if (business != null)
                {
                    var locations = business.Locations.Select(l => l.Services);

                    return Ok(locations);
                }

                return NotFound();
                
            }
            else
            {
                var service = _context.Services.Include(s => s.Locations).FirstOrDefault(s => s.Id == id);

                if (service != null)
                {
                    return Ok(service);
                }
                
            }

            return NotFound("Unable to find services for logged-in user");
        }
        catch (Exception)
        {
            throw;
        }

    }



    [HttpPut]
    public IActionResult Put(Service service)
    {
        _context.Services.Attach(service);
        _context.SaveChanges();

        return NoContent();
    }

    [HttpPatch]
    public IActionResult Patch(string id, JsonPatchDocument<Service> patchDoc)
    {
        var service = _context.Services.FirstOrDefault(s => s.Id == Guid.Parse(id));

        if (service == null)
        {
            return NotFound();
        }

        patchDoc.ApplyTo(service, jsonPatchError =>
        {
            var key = jsonPatchError.AffectedObject.GetType().Name;
            ModelState.AddModelError(key, jsonPatchError.ErrorMessage);
        });
        

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        _context.SaveChanges();

        return Ok(service);
    }

    [HttpPost]
    public IActionResult Post(Service service, int? locationId)
    {
        Console.WriteLine($"Location ID is {locationId}.");
        
        try
        {
            if (_context.Businesses.Any(x => x.OwnerId == Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier))))
            {
                _context.Services.Add(service);
                _context.SaveChanges(); 
                return Ok(service);
            }

            return BadRequest();
            
        }
        catch (Exception)
        {
            throw;
            
        }
        // Only make a booking for a service provided by the business

    }

}