using Better_Call_Saul.Models;
using Better_Call_Saul.Services;
using Microsoft.AspNetCore.Mvc;

namespace BCS.Controllers;

[ApiController]
[Route("[controller]")]
public class FavoriteEpisodeController : ControllerBase
{
    public FavoriteEpisodeController()
    {
    }

    // GET all action
    [HttpGet]
    public ActionResult<List<FavoriteEpisode>> GetAll() => FavoriteEpisodesService.GetAll();

    // GET by Id action
    [HttpGet("{id}")]
    public ActionResult<FavoriteEpisode> Get(int id)
    {
        var c = FavoriteEpisodesService.Get(id);

        if (c == null)
            return NotFound();

        return c;
    }

    // POST action
    [HttpPost]
    public IActionResult Create(FavoriteEpisode c)
    {
        FavoriteEpisodesService.Add(c);
        return CreatedAtAction(nameof(Create), new { id = c.Id }, c);
    }

    // PUT action
    [HttpPut("{id}")]
    public IActionResult Update(int id, FavoriteEpisode c)
    {
        if (id != c.Id)
            return BadRequest();

        var existingChar = FavoriteEpisodesService.Get(id);
        if (existingChar is null)
            return NotFound();

        FavoriteEpisodesService.Update(c);

        return NoContent();
    }

    // DELETE action
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var c = FavoriteEpisodesService.Get(id);

        if (c is null)
            return NotFound();

        FavoriteEpisodesService.Delete(id);

        return NoContent();
    }   
}
