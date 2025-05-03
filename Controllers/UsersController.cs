using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.OpenApi.Validations.Rules;
using UserManagementApi.Models;
using UserManagementApi.Services;

namespace UserManagementApi.Controllers;

[Route("/api/[controller]")]
public class UsersController : Controller
{
    private IUserService _userService;
    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("GetList")]
    public ActionResult<List<User>> GetList()
    {
        return Ok(_userService.GetList());
    }

    [HttpPost]
    public ActionResult<User> Post([FromBody] User newUser)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState); 
        }

        _userService.AddNewUser(newUser);
        return Ok(newUser);
    }

    [HttpPut]
    public ActionResult Put([FromBody] User user)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState); 
        }

        try 
        {
            return Ok(_userService.UpdateUser(user));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message); 
        }
    }

    [HttpDelete("{id:int}")]
    public ActionResult Delete(int id)
    {
        try 
        {
            _userService.DeleteUser(id);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message); 
        }
    }

    [HttpPost("GenerateProxies")]
    public async Task<ActionResult> GenerateProxies()
    {
        await new ClientGenerator.ClientGenerator().GenerateTypescript();
        return Ok();
    }
}