using Better_Call_Saul.Models;
using Better_Call_Saul.Services;
using Microsoft.AspNetCore.Mvc;

namespace BCS.Controllers;

[ApiController]
[Route("[controller]")]
public class FavoriteCharacterController : ControllerBase
{
    public FavoriteCharacterController()
    {
    }

    // GET all action
    [HttpGet]
    public ActionResult<List<FavoriteCharacter>> GetAll() => FavoriteCharactersService.GetAll();

    // GET by Id action
    [HttpGet("{id}")]
    public ActionResult<FavoriteCharacter> Get(int id)
    {
        var c = FavoriteCharactersService.Get(id);

        if (c == null)
            return NotFound();

        return c;
    }

    // POST action
    [HttpPost]
    public IActionResult Create(FavoriteCharacter c)
    {
        FavoriteCharactersService.Add(c);
        return CreatedAtAction(nameof(Create), new { id = c.Id }, c);
    }

    // PUT action
    [HttpPut("{id}")]
    public IActionResult Update(int id, FavoriteCharacter c)
    {
        if (id != c.Id)
            return BadRequest();

        var existingChar = FavoriteCharactersService.Get(id);
        if (existingChar is null)
            return NotFound();

        FavoriteCharactersService.Update(c);

        return NoContent();
    }

    // DELETE action
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var c = FavoriteCharactersService.Get(id);

        if (c is null)
            return NotFound();

        FavoriteCharactersService.Delete(id);

        return NoContent();
    } 
}