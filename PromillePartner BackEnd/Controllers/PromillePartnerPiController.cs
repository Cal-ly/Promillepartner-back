﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PromillePartner_BackEnd.Data;
using PromillePartner_BackEnd.Models;
using PromillePartner_BackEnd.Repositories;
using System.Collections.Concurrent;
using System.Linq;
using System.Text.Json;

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
        /// 
        /// in theory it can be called by everyone sending a valid X-Api-Key in the header + the corresponding identifier
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

        /// <summary>
        /// This Method is called by the webclient, and is used to send data to the pi
        /// </summary>
        /// <returns>If Successful, a Response from the Pi</returns>
        [HttpPost("send_to_pi")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status502BadGateway)]
        public async Task<IActionResult> UpdateDrinkPlan([FromBody] UpdateDrinkPlanRequest data)
        {
            if (string.IsNullOrWhiteSpace(data.Identifier))
            {
                return BadRequest("Identifier is required");
            }
            if (data.DrinkPlan.Drinks == null || data.DrinkPlan == null)
            {
                return BadRequest("DrinkPlan can't be null");
            }

            PromillePartnerPi? pi = await _repo.GetByIdAsync(data.Identifier);

            if(pi == null)
            {
                return NotFound($"Could not find a pi with the given identifier: {data.Identifier}");
            }

            // Send to the Pi, if a response message is received it was succesfull, getting a response message
            // doesn't mean that the data was sent or retrieved succesfully
            string? connected = await _repo.SendToPie(drinkPlan: data.DrinkPlan, pi: pi);


            if (string.IsNullOrWhiteSpace(connected))
            {
                return StatusCode(502, "Was unable to connect to the PromillePartner");
            }

            return Ok(connected); // returns the response from raspberry pi

        }


        // what needs to be done:
        // - Update the database to contain a table for PromillePartnerPi
        // - repo test () 
        // - controller/integration test (postman)

        // lige nu sendes en drinkplan, som indeholder drinks, men den skal ikke nødvendigvis indeholde drinks.

        // next steps:
        // pi script
        // web-app


        // what should not be done:
        // - Drukplan should(maybe) not get a repository, as the data gets handled in the pie
        // - When a Drukplan is received it should not be saved in the database


    }
}
