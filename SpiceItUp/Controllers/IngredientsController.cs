using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SpiceItUp.Models;
using SpiceItUp.Services;

namespace SpiceItUp.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class IngredientsController : ControllerBase
  {
    private readonly IngredientsService _igs;

    public IngredientsController(IngredientsService igs)
    {
      _igs = igs;
    }
    [HttpGet]
    public ActionResult<IEnumerable<Ingredient>> Get()
    {
      try
      {
        List<Ingredient> ingredients = _igs.Get();
        return Ok(ingredients);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [HttpGet("{id}")]
    public ActionResult<Ingredient> Get(int id)
    {
      try
      {
        Ingredient ingredient = _igs.Get(id);
        return Ok(ingredient);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [HttpPost]
    [Authorize]
    public ActionResult<Ingredient> Create([FromBody] Ingredient newIngredient)
    {
      try
      {
        Ingredient ingredient = _igs.Create(newIngredient);
        return Ok(ingredient);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [HttpPut("{id}")]
    [Authorize]
    public ActionResult<Ingredient> Edit([FromBody] Ingredient updateIngredient, int id)
    {
      try
      {
        updateIngredient.Id = id;
        Ingredient ingredient = _igs.Edit(updateIngredient);
        return Ok(ingredient);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [HttpDelete("{id}")]
    [Authorize]
    public ActionResult<String> Remove(int id)
    {
      try
      {
        _igs.Remove(id);
        return Ok("Ingredient Removed!");
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }
}