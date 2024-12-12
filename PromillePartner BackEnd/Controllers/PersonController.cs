using Microsoft.AspNetCore.Mvc;
using PromillePartner_BackEnd.Repositories;
using PromillePartner_BackEnd.Models;

namespace PromillePartner_BackEnd.Controllers;

/// <summary>
/// This is a controller for our person class and repository. 
/// It handles the HTTP requests and responses. 
/// It is used to get, post. Put and delete is not implemented.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class PersonController(PersonRepository repo) : ControllerBase
{
    // GET: api/<PersonController>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet]
    public ActionResult<IEnumerable<Person>> GetAll()
    {
        var persons = repo.GetPersons().Result;
        if (!persons.Any())
        {
            return NotFound("No persons found.");
        }
        return Ok(persons);
    }

    // GET api/<PersonController>/5
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet("{id}")]
    public ActionResult<Person> Get(int id)
    {
        try
        {
            var person = repo.GetPerson(id).Result;
            return Ok(person);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    // POST api/<PersonController>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpPost]
    public async Task<ActionResult<Person>> Post([FromBody] Person value)
    {
        if (value == null)
        {
            return BadRequest("Person data is null.");
        }
        try
        {
            await repo.AddPerson(value);
            return CreatedAtAction(nameof(Get), new { id = value.Id }, value);
        }
        catch (Exception ex)
        {
            return BadRequest($"Validation failed: {ex.Message}");
        }
    }

    // PUT api/<PersonController>/5
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpPut("{id}")]
    public ActionResult<Person> Put(int id, [FromBody] Person value)
    {
        if (value == null)
        {
            return BadRequest("Person data is null.");
        }
        try
        {
            var updatedPerson = repo.UpdatePerson(id, value).Result;
            return Ok(updatedPerson);
        }
        catch (ArgumentOutOfRangeException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return BadRequest($"Validation failed: {ex.Message}");
        }
    }

    // DELETE api/<PersonController>/5
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<Person> Delete(int id)
    {
        try
        {
            Person deletedPerson = repo.DeletePerson(id).Result;
            return Ok(deletedPerson);
        }
        catch (ArgumentOutOfRangeException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
}
