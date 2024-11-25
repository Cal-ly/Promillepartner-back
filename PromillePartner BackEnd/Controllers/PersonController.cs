using Microsoft.AspNetCore.Mvc;
using PromillePartner_BackEnd.Repositories;
using PromillePartner_BackEnd.Models;

namespace PromillePartner_BackEnd.Controllers
{
    /// <summary>
    /// This is a controller for our person class and repository. 
    /// It handles the HTTP requests and responses. 
    /// It is used to get, post. Put and delete is not implemented.
    /// 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly PersonRepository _repo;

        public PersonController(PersonRepository repo) //dependency injection
        {
            _repo = repo;
        }

        // GET: api/<PersonController>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet]
        public ActionResult<IEnumerable<Person>> GetAll()
        {
            return Ok(_repo.GetPersons());
        }

        // GET api/<PersonController>/5
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public ActionResult<Person> Get(int id)
        {
            Person person = _repo.GetPerson(id);
            if (person == null)
            {
                return NotFound("No such item, id " + id);
            }
            return Ok(person);
        }

        // POST api/<PersonController>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPost]
        public ActionResult<Person> Post(Person value)
        {
            if (value == null)
            {
                return BadRequest("Person data is null."); // Return 400 if data is missing or invalid
            }

            try
            {
                // Validate the Person object
                _repo.AddPerson(value);

            }
            catch (Exception ex)
            {
                return BadRequest($"Validation failed: {ex.Message}");  // Return 400 with validation error message
            }
           


            // If person is successfully added, return 201 and the created object
            return Created("Success", value);
        }

        // PUT api/<PersonController>/5
        [ProducesResponseType(StatusCodes.Status200OK)]  // For successful update
        [ProducesResponseType(StatusCodes.Status400BadRequest)]  // For invalid input
        [ProducesResponseType(StatusCodes.Status404NotFound)]  // When Person is not found
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //[HttpPut("{id}")]
        public ActionResult<Person> Put(int id, [FromBody] Person value)
        {
            return null;

            //if (value == null)
            //{
            //    return BadRequest("Person data is null.");  // Return 400 if no data is provided
            //}

            //try
            //{
            //    // Validate the Person object
            //    value.Validate();
            //}
            //catch (Exception ex)
            //{
            //    return BadRequest($"Validation failed: {ex.Message}");  // Return 400 with validation error message
            //}

            //var existingPerson = _repo.GetPerson(id);

            //if (existingPerson == null)
            //{
            //    return NotFound($"Person with ID {id} not found."); // Return 404 if the Person doesn't exist
            //}

            //// Update the person details
            //existingPerson.Man = value.Man;
            //existingPerson.Weight = value.Weight;

            //return Ok(existingPerson); // Return 200 with the updated person object
        }

        // DELETE api/<PersonController>/5
       // [HttpDelete("{id}")]
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}