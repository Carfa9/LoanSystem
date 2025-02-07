using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("users")] // URI börjar med /users
public class UserController : ControllerBase
{
    LoanDbContext db;

    public UserController(LoanDbContext db)
    {
        this.db = db;
    }

    // Här används [HttpGet] för att explicit säga att en GET request till "/users/oneuser" ska routas hit
    [HttpGet("oneuser")]
    public User ReturnFirstUser()
    {
        return db.Users.FirstOrDefault() ?? new User();
        // En user returneras, eller en ny User om ingen finns i databasen
        // Inga statuskoder hanteras i detta meddelande dock! Inte helt bra.
    }


    // Den här metoden skickar tillbaka en lista på users när HTTP Requesten "GET /users" hittar fram hit.
    // Naming conventions gör att just GET routas hit pga metodens namn börjar med "Get"
    public IActionResult GetUsers(string name = "")
    {
        return string.IsNullOrWhiteSpace(name) ? Ok(db.Users) : Ok(db.Users.Where(u => u.Name.Contains(name)));

        // Genom att låta metoden returnera IActionResult kan vi använda Ok() för att skicka tillbaka en 200 OK statuskod
        // Ok() är en av flera inbyggda metoder i ControllerBase som skickar tillbaka en statuskod och ett meddelande
        // samt den data som skickas med som argument. Andra metoder är bla NotFound(), BadRequest() och CreatedAtAction()
        // Dessa metoder är typ samma som Result.Ok() och Result.NotFound() som används i MinimalAPI
    }

    [HttpGet("{id}")] // {id} är en "route parameter" som används som input till metoden (int id)
    public async Task<ActionResult<User>> GetUserById(int id)
    {
        User? user = await db.Users.FindAsync(id);

        if (user is null)
        {
            return NotFound();
        }

        return Ok(user);

        // I och med att metoden returnerar IActionResult kan vi använda NotFound() för att skicka tillbaka en 404 statuskod
        // eller Ok() för att skicka tillbaka en 200 OK statuskod. De båda metoderna returnerar olika objekt
        // ( NotFound() returnerar en NotFoundResult, Ok() returnerar en OkObjectResult ) men båda implementerar IActionResult
    }
}