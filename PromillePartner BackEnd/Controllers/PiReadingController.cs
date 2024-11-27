using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PromillePartner_BackEnd.Repositories;
using PromillePartner_BackEnd.Models;

namespace PromillePartner_BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PiReadingController : ControllerBase
    {
        private readonly PiReadingRepository _repo;

        public PiReadingController(PiReadingRepository repo) //dependency injection
        {
            _repo = repo;
        }

        // GET: api/<PiReadingController>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet]
        public ActionResult<IEnumerable<PiReading>> GetAll()
        {
            return Ok(_repo.GetPiReadings());
        }

        // GET api/<PiReadingController>/5
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public ActionResult<PiReading> Get(int id)
        {
            PiReading piReading = _repo.GetPiReading(id);
            if (piReading == null)
            {
                return NotFound("No such item, id " + id);
            }
            return Ok(piReading);
        }

        // POST api/<PiReadingController>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPost]
        public ActionResult<PiReading> Post(PiReading value)
        {
            if (value == null)
            {
                return BadRequest("PiReading data is null."); // Return 400 if data is missing or invalid
            }

            try
            {
                // Validate the PiReading object
                _repo.AddPiReading(value);
            }
            catch (Exception ex)
            {
                return BadRequest($"Validation failed: {ex.Message}");  // Return 400 with validation error message
            }

            // If PiReading is successfully added, return 201 and the created object
            return Created("Success", value);
        }

        // PUT api/<PiReadingController>/5
        [ProducesResponseType(StatusCodes.Status200OK)]  // For successful update
        [ProducesResponseType(StatusCodes.Status400BadRequest)]  // For invalid input
        [ProducesResponseType(StatusCodes.Status404NotFound)]  // When PiReading is not found
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //[HttpPut("{id}")]
        public ActionResult<PiReading> Put(int id, [FromBody] PiReading value)
        {
            return null;
            //if (value == null)
            //{
            //    return BadRequest("PiReading data is null.");  // Return 400 if no data is provided
            //}

            //PiReading existingPiReading;
            //try
            //{
            //    // Update the PiReading object
            //    existingPiReading = _repo.UpdatePiReading(id, value);
            //}
            //catch (Exception ex)
            //{
            //    return NotFound($"PiReading with id {id} not found: {ex.Message}");  // Return 404 if PiReading is not found
            //}

            //return Ok(existingPiReading); // Return 200 with the updated PiReading object
        }

        // DELETE api/<PiReadingController>/5
        //[HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<PiReading> Delete(int id)
        {
            return null;
            //PiReading deletedPiReading;
            //try
            //{
            //    deletedPiReading = _repo.DeletePiReading(id);
            //}
            //catch (ArgumentOutOfRangeException e)
            //{
            //    return BadRequest($"{e.Message}");
            //}
            //catch (Exception e)
            //{
            //    return NotFound($"{e.Message}");
            //}
            //return Ok(deletedPiReading);
        }
    }
}
