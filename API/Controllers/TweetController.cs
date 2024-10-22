using Microsoft.AspNetCore.Mvc;
using TweetPostingService.Models;

namespace API.Controllers;

public class TweetController : Controller
{
    private readonly HttpClient _httpClient;

    public TweetController(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    [HttpGet("tweets/{id}")]
    public async Task<IActionResult> GetTweetById(int id)
    {
        var response = await _httpClient.GetAsync($"http://localhost:5001/api/tweetposting/tweets/{id}");
        return Ok(response);
    }


    [HttpPost("tweets")]
    public async Task<IActionResult> CreateTweet([FromBody] Tweet tweet)
    {
        var response = await _httpClient.PostAsJsonAsync("http://localhost:5001/api/tweetposting/tweets", tweet);
        return Ok(response);
    }
}
