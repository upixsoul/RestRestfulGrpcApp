using Microsoft.AspNetCore.Mvc;

namespace WebApiOpenapi.api;

/*
 * Ok(), CreatedAtAction(), BadRequest(), etc. → siempre serializan a JSON.
 * Nota: Los objetos anónimos (new { ... }) se serializan automáticamente.
 */

/*
 * ✅ Is it REST‑like? It goes beyond REST‑like. REST‑like means “looks like REST but doesn’t follow the rules strictly.”
                Your controller does follow the rules strictly. So it is fully RESTful, not just REST‑like.
                
 * ✅ Is this controller REST? Yes.
 * REST (the architectural style) only requires:
 * HTTP verbs   Each HTTP verb maps to the correct operation
 *              Uses proper HTTP status codes
                Ok() → 200
                CreatedAtAction() → 201
                Ok() for PUT and DELETE (acceptable, though 204 is also common)
 * Statelessness
 * Resource representations (JSON, XML, TOON, etc.)
 * URLs that identify resources
 * Uses DTOs for resource representation
                ItemDto is a clean, immutable record — perfect for RESTful APIs.
 */

public record ItemDto(int Id, string Value);

[Route("api/[controller]")]
[ApiController]
public class ItemsController : ControllerBase
{
    // GET api/Items
    [HttpGet]
    public ActionResult<IEnumerable<ItemDto>> Get()
    {
        var result = new List<ItemDto>
        {
            new ItemDto(1, "value1" ),
            new ItemDto(2, "value2" ), 
        };

        return Ok(result); // <-- JSON
    }

    // GET api/Items/5
    [HttpGet("{id}")]
    public ActionResult<ItemDto> Get(int id)
    {
        var result = new ItemDto( id, "value" );
        return Ok(result); // <-- JSON
    }

    // POST api/Items
    [HttpPost]
    public ActionResult<ItemDto> Post([FromBody] string value)
    {
        var created = new { Id = 1, Value = value };
        return CreatedAtAction(nameof(Get), new { id = 1 }, created); // <-- JSON
    }

    // PUT api/Items/5
    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] string value)
    {
        var updated = new ItemDto(id, value);
        return Ok(updated); // <-- JSON
    }

    // DELETE api/Items/5
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        return Ok(new { Message = $"Item {id} deleted" }); // <-- JSON
    } 
}
