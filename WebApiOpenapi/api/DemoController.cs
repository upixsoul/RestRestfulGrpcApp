using Microsoft.AspNetCore.Mvc;

namespace WebApiOpenapi.api;

// Controller       REST Principles	    RESTful Principles	        Notes
// DemoController	✅ Uses HTTP GET	❌ URL is action‑based	    Not RESTful
        
[Route("api/[controller]")]
[ApiController]
public class DemoController : ControllerBase
{
    // GET api/demo/hello
    [HttpGet("hello")]
    public ActionResult<string> SayHello()
    {
        return Ok("Hello from DemoController!");
    }

    // GET api/demo/user/5
    [HttpGet("user/{id}")]
    public ActionResult<object> GetUserById(int id)
    {
        if (id <= 0)
            return BadRequest("Invalid ID");

        var user = new
        {
            Id = id,
            Name = "Sample User",
            Active = true
        };

        return Ok(user);
    }

    // POST api/demo/create
    [HttpPost("create")]
    public ActionResult<object> CreateItem([FromBody] string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return BadRequest("Value cannot be empty");

        var created = new
        {
            Id = 1,
            Value = value,
            Status = "Created"
        };

        return CreatedAtAction(nameof(GetUserById), new { id = 1 }, created);
    }
}