using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SpiceItUp.Models;
using SpiceItUp.Services;
using CodeWorks.Auth0Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SpiceItUp.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class AccountController : ControllerBase
  {
    private readonly AccountService _accountService;
    private readonly FavoritesService _fs;

    public AccountController(AccountService accountService, FavoritesService fs)
    {
      _accountService = accountService;
      _fs = fs;
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<Account>> Get()
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        return Ok(_accountService.GetOrCreateProfile(userInfo));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [HttpGet("{id}/favorites")]
    public ActionResult<IEnumerable<Favorite>> Get(string id)
    {
      try
      {
        List<Favorite> favorites = _fs.Get(id);
        return Ok(favorites);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [HttpPut]
    [Authorize]
    public async Task<ActionResult<Account>> Edit([FromBody] Account editData)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        Account newAccount = _accountService.Edit(editData, userInfo.Email);
        return Ok(newAccount);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }


}