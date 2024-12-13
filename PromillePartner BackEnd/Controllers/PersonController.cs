using Microsoft.AspNetCore.Mvc;
using PromillePartner_BackEnd.Models;
using PromillePartner_BackEnd.Repositories;

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
    /// <summary>
    /// Get http method which returns all persons
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<IEnumerable<Person>> GetAll()
    {
        var persons = repo.GetPersons().Result;
        if (!persons.Any())
        {
            return NotFound("No persons found.");
        }
        return Ok(persons);
    }

    /// <summary>
    /// Get method which returns a specific person with a id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
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

    /// <summary>
    /// Post method requires json person in body
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
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

    /// <summary>
    /// Put method which updates person with a specified id, person json values must be from body
    /// </summary>
    /// <param name="id"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
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
            return NotFound(ex.Message);
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

    /// <summary>
    /// Delete method removes person with specified id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status405MethodNotAllowed)]
    public ActionResult<Person> Delete(int id)
    {
        if (id < 1)
        {
            return BadRequest("Id must be greater than 0.");
        }
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
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
