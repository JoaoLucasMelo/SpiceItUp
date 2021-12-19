using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SpiceItUp.Models;
using SpiceItUp.Services;

namespace SpiceItUp.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class FavoritesController : ControllerBase
  {
    private readonly FavoritesService _fs;

    public FavoritesController(FavoritesService fs)
    {
      _fs = fs;
    }

    [HttpPost]
    [Authorize]
    public ActionResult<Favorite> Create([FromBody] Favorite newFavorite)
    {
      try
      {
        Favorite favorite = _fs.Create(newFavorite);
        return Ok(favorite);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [HttpDelete("{id}")]
    public ActionResult<String> Remove(int id)
    {
      try
      {
        _fs.Remove(id);
        return Ok("Favorite Removed");
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }
}