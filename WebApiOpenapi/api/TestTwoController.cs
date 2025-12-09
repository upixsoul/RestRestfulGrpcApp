using Microsoft.AspNetCore.Mvc;

namespace WebApiOpenapi.api;

/*
 * Ok(), CreatedAtAction(), BadRequest(), etc. → siempre serializan a JSON.
 * Los objetos anónimos (new { ... }) se serializan automáticamente.
 */

[Route("api/[controller]")]
[ApiController]
public class TestTwoController : ControllerBase
{
    // GET api/test
    [HttpGet]
    public ActionResult<IEnumerable<object>> Get()
    {
        var result = new[]
        {
            new { Id = 1, Value = "value1" },
            new { Id = 2, Value = "value2" }
        };

        return Ok(result); // <-- JSON
    }

    // GET api/test/5
    [HttpGet("{id}")]
    public ActionResult<object> Get(int id)
    {
        var result = new { Id = id, Value = "value" };

        return Ok(result); // <-- JSON
    }

    // POST api/test
    [HttpPost]
    public ActionResult<object> Post([FromBody] string value)
    {
        var created = new { Id = 1, Value = value };

        return CreatedAtAction(nameof(Get), new { id = 1 }, created); // <-- JSON
    }

    // PUT api/test/5
    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] string value)
    {
        var updated = new { Id = id, Value = value };

        return Ok(updated); // <-- JSON
    }

    // DELETE api/test/5
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        return Ok(new { Message = $"Item {id} deleted" }); // <-- JSON
    } 
}
