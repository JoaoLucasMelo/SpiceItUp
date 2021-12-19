using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SpiceItUp.Models;
using SpiceItUp.Services;

namespace SpiceItUp.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class RecipesController : ControllerBase
  {
    private readonly RecipesService _rs;
    private readonly IngredientsService _igs;
    private readonly StepsService _ss;

    public RecipesController(RecipesService rs, StepsService ss, IngredientsService igs)
    {
      _rs = rs;
      _igs = igs;
      _ss = ss;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Recipe>> Get()
    {
      try
      {
        List<Recipe> recipes = _rs.Get();
        return Ok(recipes);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [HttpGet("{id}")]
    public ActionResult<Recipe> Get(int id)
    {
      try
      {
        Recipe recipe = _rs.Get(id);
        return Ok(recipe);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Recipe>> Create([FromBody] Recipe newRecipe)
    {
      try
      {
        Account user = await HttpContext.GetUserInfoAsync<Account>();
        newRecipe.CreatorId = user?.Id;
        Recipe recipe = _rs.Create(newRecipe);
        recipe.Creator = user;
        return Ok(recipe);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [HttpPut("{id}")]
    public ActionResult<Recipe> Edit([FromBody] Recipe updateRecipe, int id)
    {
      try
      {
        updateRecipe.Id = id;
        Recipe recipe = _rs.Edit(updateRecipe);
        return Ok(recipe);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [HttpDelete("{id}")]
    public async Task<ActionResult<String>> Remove(int id)
    {
      try
      {
        Account user = await HttpContext.GetUserInfoAsync<Account>();
        _rs.Remove(id, user.Id);
        return Ok("Recipe Removed!");
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [HttpGet("{id}/ingredients")]
    public ActionResult<IEnumerable<Ingredient>> GetByRecipe(int id)
    {
      try
      {
        List<Ingredient> ingredients = _igs.GetByRecipe(id);
        return Ok(ingredients);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [HttpGet("{id}/steps")]
    public ActionResult<IEnumerable<Step>> GetByStep(int id)
    {
      try
      {
        List<Step> steps = _ss.GetByStep(id);
        return Ok(steps);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }
}