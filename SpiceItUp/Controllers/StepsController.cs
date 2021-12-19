using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SpiceItUp.Models;

namespace SpiceItUp.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class StepsController : ControllerBase
  {
    private readonly StepsService _ss;

    public StepsController(StepsService ss)
    {
      _ss = ss;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Step>> Get()
    {
      try
      {
        List<Step> steps = _ss.Get();
        return Ok(steps);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [HttpGet("{id}")]
    public ActionResult<Step> Get(int id)
    {
      try
      {
        Step step = _ss.Get(id);
        return Ok(step);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [HttpPost]
    [Authorize]
    public ActionResult<Step> Create([FromBody] Step newStep)
    {
      try
      {
        Step step = _ss.Create(newStep);
        return Ok(newStep);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpPut("{id}")]
    public ActionResult<Step> Edit([FromBody] Step updateStep, int id)
    {
      try
      {
        updateStep.Id = id;
        Step step = _ss.Edit(updateStep);
        return Ok(updateStep);
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
        _ss.Remove(id);
        return Ok("Step Removed");
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }
}