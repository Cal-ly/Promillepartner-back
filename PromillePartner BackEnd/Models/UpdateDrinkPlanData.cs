using System.ComponentModel.DataAnnotations;

namespace PromillePartner_BackEnd.Models
{
    /// <summary>
    /// A class containing name, and time until you're supposed to drink again
    /// </summary>
    public class UpdateDrinkPlanData
    {
        [Key]
        public int ID { get; set; }
        public double TimeDifference { get; set; }          // time until you should drink again, calculated in webside

        public string DrinkName { get; set; }               // name of the drink to drink


        public override bool Equals(object? obj)
        {
            return obj is UpdateDrinkPlanData data &&
                   ID == data.ID &&
                   TimeDifference == data.TimeDifference &&
                   DrinkName == data.DrinkName;
        }
    }
}
