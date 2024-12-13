using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PromillePartner_BackEnd.Models;

namespace PromillePartner_BackEnd.Controllers
{
    /// <summary>
    /// Drink controller class
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class DrinkController : Controller
    {
        /// <summary>
        /// Controller til at returnere en liste med Drinks som brugeren kan vælge imellem
        /// Kaldes med HttpGet
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<Drink>> Index()
        {
            var drinklist = new List<Drink>();
            drinklist.Add(new Drink(7, "Cider", 0.33, 4.5, "Light Alcohol"));            // 11.72 grams
            drinklist.Add(new Drink(3, "Øl", 0.33, 5.0, "Beer"));                      // 13.01 grams
            drinklist.Add(new Drink(24, "Prosecco", 0.125, 11.0, "Wine"));               // 10.84 grams
            drinklist.Add(new Drink(5, "Hvid vin", 0.15, 12.0, "Wine"));               // 14.19 grams
            drinklist.Add(new Drink(2, "Rød vin", 0.15, 13.5, "Wine"));                 // 15.98 grams
            drinklist.Add(new Drink(10, "Champagne", 0.125, 12.0, "Wine"));              // 11.84 grams
            drinklist.Add(new Drink(15, "Sangria", 0.15, 12.0, "Wine"));                 // 14.19 grams
            drinklist.Add(new Drink(12, "Pina Colada", 0.2, 13.0, "Cocktail"));          // 20.49 grams
            drinklist.Add(new Drink(18, "Whiskey Sour", 0.2, 16.0, "Cocktail"));         // 25.25 grams
            drinklist.Add(new Drink(13, "Margarita", 0.2, 15.0, "Cocktail"));            // 23.67 grams
            drinklist.Add(new Drink(19, "Aperol Spritz", 0.25, 11.0, "Cocktail"));       // 21.71 grams
            drinklist.Add(new Drink(25, "Jägerbomb", 0.2, 15.0, "Cocktail"));            // 23.67 grams
            drinklist.Add(new Drink(11, "Mojito", 0.2, 10.0, "Cocktail"));               // 15.78 grams
            drinklist.Add(new Drink(21, "Baileys", 0.05, 17.0, "Liqueur"));              // 6.71 grams
            drinklist.Add(new Drink(8, "Gin Shot", 0.03, 40.0, "Strong"));               // 9.47 grams
            drinklist.Add(new Drink(6, "Tequila Shot", 0.04, 38.0, "Strong"));           // 11.99 grams
            drinklist.Add(new Drink(4, "Vodka Shot", 0.04, 37.5, "Strong"));             // 11.84 grams
            drinklist.Add(new Drink(1, "Whiskey", 0.03, 40.0, "Strong"));                // 9.47 grams
            drinklist.Add(new Drink(9, "Rom Shot", 0.03, 37.5, "Strong"));               // 8.86 grams
            drinklist.Add(new Drink(14, "Long Island Iced Tea", 0.25, 22.0, "Cocktail"));// 43.34 grams
            drinklist.Add(new Drink(20, "Espresso Martini", 0.2, 18.0, "Cocktail"));     // 28.40 grams
            drinklist.Add(new Drink(20, "Cocktail (low alcohol)", 0.2, 8.0, "Cocktail"));     // 12,62 grams
            drinklist.Add(new Drink(20, "Cocktail (medium alcohol)", 0.3, 9.0, "Cocktail"));     // 21,30 grams
            drinklist.Add(new Drink(20, "Cocktail (high alcohol)", 0.15, 26.0, "Cocktail"));     // 30,77 grams
            drinklist.Add(new Drink(20, "Cocktail (very high alcohol)", 0.36, 18.0, "Cocktail"));     // 51,13 grams

            return Ok(drinklist);
        }

    }
}
