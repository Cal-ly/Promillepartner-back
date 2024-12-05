using Microsoft.IdentityModel.Tokens;
using System.Text.Json;

namespace PromillePartner_BackEnd.Models
{
    public class DrinkPlan
    {
        public List<Drink> Drinks { get; set; }
    }
}
