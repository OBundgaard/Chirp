using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserProfileService.Models;

namespace API.Controllers;

public class UserController : Controller
{
    private readonly HttpClient _httpClient;

    public UserController(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    [HttpGet("users/{id}")]
    public async Task<IActionResult> GetUserById(int id)
    {
        var response = await _httpClient.GetAsync($"http://localhost:5002/api/userservice/users/{id}");
        return Ok(response);
    }


    [HttpPost("users")]
    public async Task<IActionResult> CreateUser([FromBody] User user)
    {
        var response = await _httpClient.PostAsJsonAsync("http://localhost:5002/api/userservice/users", user);
        return Ok(response);
    }
}
