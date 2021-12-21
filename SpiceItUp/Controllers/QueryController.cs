using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SpiceItUp.Models;
using SpiceItUp.Services;

namespace SpiceItUp.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class QueryController : ControllerBase
  {
    private RecipesService _rs;

    public QueryController(RecipesService rs)
    {
      _rs = rs;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Recipe>> Get(string search)
    {
      try
      {
        List<Recipe> recipes = _rs.Search(search);
        return Ok(recipes);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }
}