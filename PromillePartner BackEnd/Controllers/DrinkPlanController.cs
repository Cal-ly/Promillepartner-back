using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using PromillePartner_BackEnd.Models;
using PromillePartner_BackEnd.Repositories;

namespace PromillePartner_BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DrinkPlanController : ControllerBase
    {
        private readonly DrinkPlanRepository _drinkPlanRepository;

        public DrinkPlanController(DrinkPlanRepository drinkPlanRepository)
        {
            _drinkPlanRepository = drinkPlanRepository;
        }

        /// <summary>
        /// Retrieves all drink plans
        /// </summary>
        /// <returns>A list of all drink plans</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<DrinkPlan>>> GetAll()
        {
            var drinkPlans = await _drinkPlanRepository.Get();

            if (drinkPlans == null || drinkPlans.Count == 0)
            {
                return NotFound("No drink plans found");
            }

            return Ok(drinkPlans);
        }

        /// <summary>
        /// Retrieves a specific drink plan by identifier
        /// </summary>
        /// <param name="identifier">The unique identifier for the drink plan</param>
        /// <returns>A specific drink plan</returns>
        [HttpGet("get_drink_plan")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<DrinkPlan>> GetDrinkPlan([FromQuery] string identifier)
        {
            // Input validation for identifier
            if (string.IsNullOrWhiteSpace(identifier))
            {
                return BadRequest("Identifier was null or empty");
            }

            // Retrieve drink plan by identifier
            var drinkPlan = await _drinkPlanRepository.GetDrinkPlan(identifier);

            // Check if drink plan exists
            if (drinkPlan == null)
            {
                return NotFound("Drink plan not found");
            }

            return Ok(drinkPlan.DrinkPlanen);
        }

        /// <summary>
        /// Creates a new drink plan
        /// </summary>
        /// <param name="drinkPlan">The drink plan to create</param>
        /// <returns>The created drink plan</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<DrinkPlan>> CreateDrinkPlan([FromBody] DrinkPlan drinkPlan)
        {
            // Input validation
            if (drinkPlan == null)
            {
                return BadRequest("Drink plan cannot be null");
            }

            // Validate identifier
            if (string.IsNullOrWhiteSpace(drinkPlan.Identifier))
            {
                return BadRequest("Drink plan must have a valid identifier");
            }

            if(drinkPlan.DrinkPlanen == null)
            {
                return BadRequest("Drukplan is null");
            }


            var existingDrinkPlan = await _drinkPlanRepository.GetDrinkPlanByIdentifier(drinkPlan.Identifier);
            if (existingDrinkPlan != null)
            {
                return Conflict("A drink plan with this identifier already exists.");
            }

            // Add drink plan
            var createdDrinkPlan = await _drinkPlanRepository.AddDrinkPlan(drinkPlan);

            // Return created drink plan with 201 Created status
            return CreatedAtAction(nameof(GetDrinkPlan), new { identifier = createdDrinkPlan.Identifier }, createdDrinkPlan);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="drinkPlan"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<DrinkPlan>> UpdateDrinkPlan([FromBody] DrinkPlan drinkPlan)
        {
            // Input validation
            if (drinkPlan == null)
            {
                return BadRequest("Drink plan cannot be null");
            }

            // Validate identifier
            if (string.IsNullOrWhiteSpace(drinkPlan.Identifier))
            {
                return BadRequest("Drink plan must have a valid identifier");
            }

            if (drinkPlan.DrinkPlanen == null)
            {
                return BadRequest("Drukplan is null");
            }

            // Add drink plan
            var createdDrinkPlan = await _drinkPlanRepository.UpdateDrinkPlan(drinkPlan);

            // Return created drink plan with 201 Created status
            return Ok(createdDrinkPlan);
        }
    }

}