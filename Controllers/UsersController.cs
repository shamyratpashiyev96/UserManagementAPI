using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
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
        _userService.AddNewUser(newUser);
        return Ok(newUser);
    }

    [HttpPut]
    public ActionResult Put([FromBody] User user)
    {
        var result = _userService.UpdateUser(user);
        return Ok(result);
    }

    [HttpDelete("{id:int}")]
    public ActionResult Delete(int id)
    {
        _userService.DeleteUser(id);
        return Ok();
    }
}