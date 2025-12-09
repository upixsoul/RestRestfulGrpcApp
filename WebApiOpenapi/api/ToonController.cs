using Microsoft.AspNetCore.Mvc;

namespace WebApiOpenapi.api;

[Route("api/[controller]")]
[ApiController]
public class ToonController : ControllerBase
{
    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        if (id <= 0)
        {
            var errorToon = ToonSerializer.ToToon(new
            {
                Status = "error",
                Message = "Invalid ID"
            });

            return BadRequest(errorToon);
        }
        
        if (id >= 10)
        {
            var users = new List<UserInfo>
            {
                new UserInfo { Name = "Alice", Age = 28, Email = "alice@mail.com", Active = true },
                new UserInfo { Name = "Bob", Age = 35, Email = "bob@mail.com", Active = false },
                new UserInfo { Name = "Charlie", Age = 30, Email = "charlie@mail.com", Active = true }
            };

            var toon = ToonSerializer.ToToon(users, "users");

            return Content(toon, "text/plain");
        }
        
        var user = new UserInfo
        {
            Name = "Sarah Wilson",
            Age = 32,
            Email = "swilson@telco.com",
            Active = true
        };

        var singleToon = ToonSerializer.ToToon(user);

        return Content(singleToon, "text/plain");
    } 

    //How does a modern endpoint with JSON + TOON looks like?
    /*[HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        var user = new UserInfo { ... };

        if (Request.Headers.Accept.Contains("text/toon"))
            return Content(ToonSerializer.ToToon(user), "text/toon");

        return Ok(user); // JSON
    }*/

    
    [HttpPost]
    public IActionResult Post([FromBody] UserInfo user)
    {
        if (user == null)
        {
            var errorToon = ToonSerializer.ToToon(new
            {
                Status = "error",
                Message = "Invalid body"
            });

            return BadRequest(errorToon);
        }

        var toon = ToonSerializer.ToToon(new
        {
            Status = "created",
            user.Name,
            user.Age,
            user.Email,
            user.Active
        });

        return Created("api/toon/1", toon);
    }
}