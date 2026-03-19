using System.Security.Claims;
using Arrow.Data;
using Arrow.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch.SystemTextJson;
using Microsoft.AspNetCore.Mvc;

namespace Arrow.Api.Controllers;

[Route("api/v1/[controller]/{id?}")]
[ApiController]
[Authorize]
public class BookingController : ControllerBase
{
    private readonly ArrowContext _context;


    public BookingController(ArrowContext context)
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
                var businessLocations = _context.Businesses.FirstOrDefault(bs => bs.OwnerId == userId)
                    .Locations
                    .ToList();
                
                var bookings = _context.Bookings
                    .Where(b => businessLocations.Contains(b.Location))
                    .Take(50)
                    .OrderByDescending(b => b.Appointment);

                return Ok(bookings);
                
            }
            else
            {
                var booking = _context.Bookings.FirstOrDefault(b => b.Id == id);

                if (booking != null)
                {
                    return Ok(booking);
                }
                
            }

            return NotFound();
        }
        catch (Exception)
        {
            return NotFound("Unable to find bookings for logged-in user");
        }

    }



    [HttpPut]
    public IActionResult Put(Booking booking)
    {
        _context.Bookings.Attach(booking);
        _context.SaveChanges();

        return NoContent();
    }

    [HttpPatch]
    public IActionResult Patch(string id, JsonPatchDocument<Booking> patchDoc)
    {
        var booking = _context.Bookings.FirstOrDefault(b => b.Id == Guid.Parse(id));

        if (booking == null)
        {
            return NotFound();
        }

        patchDoc.ApplyTo(booking, jsonPatchError =>
        {
            var key = jsonPatchError.AffectedObject.GetType().Name;
            ModelState.AddModelError(key, jsonPatchError.ErrorMessage);
        });
        

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        _context.SaveChanges();

        return Ok(booking);
    }

    [HttpPost]
    public IActionResult Post(Booking booking)
    {
        
        try
        {
            if (!_context.Businesses.Any(x => x.OwnerId == Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier))))
            {
                _context.Bookings.Add(booking);
                _context.SaveChanges(); 
                return Ok(booking);
            }

            return BadRequest();
            
        }
        catch (Exception)
        {
            var customResponse = new
            {
                Code = 500,
                Message = "Business or owner not found",
            };

            return StatusCode(StatusCodes.Status500InternalServerError);
            
        }
        // Only make a booking for a service provided by the business

    }

}