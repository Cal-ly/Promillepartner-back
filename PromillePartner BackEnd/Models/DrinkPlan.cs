using System.ComponentModel.DataAnnotations;

namespace PromillePartner_BackEnd.Models
{
    public class DrinkPlan
    {
        [Key]
        public string Identifier { get; set; } = string.Empty;
        public List<UpdateDrinkPlanData>? DrinkPlanen { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is DrinkPlan plan &&
                   Identifier == plan.Identifier &&
                   EqualityComparer<List<UpdateDrinkPlanData>?>.Default.Equals(DrinkPlanen, plan.DrinkPlanen);
        }
    }
}
