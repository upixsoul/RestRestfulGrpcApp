using Microsoft.AspNetCore.Mvc;
using ToonSharp;


namespace WebApiOpenapi.api;

[Route("api/[controller]")]
[ApiController]
public class ToonTwoController : ControllerBase
{
    private ToonSharp.ToonSerializer serializer = new ToonSharp.ToonSerializer();
    
    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        
        if (id <= 0)
        {
            var errorToon = serializer.Dumps(new
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

            var toon = serializer.Dumps(users);

            return Content(toon, "text/plain");
        }
        
        var singleToon = serializer.Dumps(new UserInfo { Name = "Alice", Age = 28, Email = "alice@mail.com", Active = true }); 

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
            var errorToon = serializer.Dumps(new
            {
                Status = "error",
                Message = "Invalid body"
            });

            return BadRequest(errorToon);
        }

        var toon = serializer.Dumps(new
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