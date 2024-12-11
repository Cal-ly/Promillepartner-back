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
            if(obj is not DrinkPlan plan)
            {
                return false;
            }
            if(plan.DrinkPlanen.Count != DrinkPlanen.Count)
            {
                return false;
            }
            for(int i = 0; i<DrinkPlanen.Count; i++) 
            {
                if(DrinkPlanen.ElementAt(0).Equals(plan.DrinkPlanen.ElementAt(0)) == false)
                {
                    return false;
                }
            }
            return Identifier == plan.Identifier;
        }
    }
}
