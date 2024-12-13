using System.ComponentModel.DataAnnotations;

namespace PromillePartner_BackEnd.Models
{
    /// <summary>
    /// Drink plan used by the user to keep track of what to drink and when
    /// </summary>
    public class DrinkPlan
    {
        [Key]
        public string Identifier { get; set; } = string.Empty;      // key used to find drink plan
        public long? TimeStamp { get; set; }                        // when to drink
        public List<UpdateDrinkPlanData>? DrinkPlanen { get; set; } // what to drink

        /// <summary>
        /// Equals override, takes into account that a list is stored
        /// </summary>
        /// <param name="obj">the drink plan it is to be compared to</param>
        /// <returns>whether it is the same as the other obj or not</returns>
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
