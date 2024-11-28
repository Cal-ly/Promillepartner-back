using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PromillePartner_BackEnd.Models;

namespace PromillePartner_BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DrinkController : Controller
    {
        // GET: DrinkController
        [ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<Drink>> Index()
        {
            var drinklist = new List<Drink>();
            drinklist.Add(new Drink(1, "Whiskey", 0.7, 40.0, "Strong"));
            drinklist.Add(new Drink(2, "Red Wine", 0.75, 13.5, "Wine"));
            drinklist.Add(new Drink(3, "Beer", 0.5, 5.0, "Beer"));
            drinklist.Add(new Drink(4, "Vodka", 0.7, 37.5, "Strong"));
            drinklist.Add(new Drink(5, "White Wine", 0.75, 12.0, "Wine"));
            drinklist.Add(new Drink(6, "Tequila", 0.7, 38.0, "Strong"));
            drinklist.Add(new Drink(7, "Cider", 0.33, 4.5, "Light Alcohol"));
            drinklist.Add(new Drink(8, "Gin", 0.7, 40.0, "Strong"));
            drinklist.Add(new Drink(9, "Rum", 0.7, 37.5, "Strong"));
            drinklist.Add(new Drink(10, "Champagne", 0.75, 12.0, "Wine"));

            return Ok(drinklist);
        }

    }
}
