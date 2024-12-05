using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PromillePartner_BackEnd.Data;
using PromillePartner_BackEnd.Models;
using PromillePartner_BackEnd.Repositories;
using System.Collections.Concurrent;
using System.Linq;

namespace PromillePartner_BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PromillePartnerPiController : ControllerBase
    {

        private readonly PromillePartnerPiRepository _repo;

        public PromillePartnerPiController(PromillePartnerPiRepository repo)
        {
            _repo = repo;
        }


        /// <summary>
        /// This Method is only supposed to be called by Rpies, and is called regularly to update the ip of the Rpi, to make
        /// sure it can still be connected to even if the Rpi.Ip changes.
        /// </summary>
        /// <param name="identifier">Used to get a unique Rpi</param>
        /// <param name="apiKey">Makes sure that the Rpi Ip doesn't get corrupted by webclient ips, or other Rpies</param>
        /// <returns></returns>
        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Register([FromBody] string identifier, [FromHeader(Name = "X-Api-Key")] string apiKey)
        {
            if (string.IsNullOrWhiteSpace(identifier) || string.IsNullOrWhiteSpace(apiKey))
            {
                return BadRequest("Identifier and API key are required.");
            }

            // Verify the API key
            var isValid = await _repo.IsValidApiKey(identifier, apiKey);
            if (!isValid)
            {
                return Unauthorized("Invalid API key.");
            }

            // Update IP logic
            var clientIp = HttpContext.Connection.RemoteIpAddress?.ToString();
            var success = await _repo.UpdateIp(identifier, clientIp);

            if (!success)
            {
                return NotFound($"No Raspberry Pi found with the identifier '{identifier}'.");
            }

            return Ok($"IP address {clientIp} is updated for identifier '{identifier}'.");
        }


        // what needs to be done:
        // - a web client method to send data to the Rpi, by extracting the id using the identifier,
        // the idetifier will then have to be send by the webclient to access the Rpi
        // - a class called 'DrukPlan', which should contain all the data that is supposed to be sent to the Rpi by the webclient.
        // - repo test
        // - controller/integration test

        

        // what should not be done:
        // - Drukplan should(maybe) not get a repository, as the data gets handled in the pie
        // - When a Drukplan is received it should not be saved in the database
       

    }
}
