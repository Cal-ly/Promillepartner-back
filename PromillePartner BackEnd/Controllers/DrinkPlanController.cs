using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PromillePartner_BackEnd.Models;

namespace PromillePartner_BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DrinkPlanController : Controller
    {
        // GET: DrinkController
        [ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<DrinkPlan> Get()
        {
            return null;
        }

    }
}
