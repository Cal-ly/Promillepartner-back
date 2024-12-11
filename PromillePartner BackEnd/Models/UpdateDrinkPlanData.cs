using System.ComponentModel.DataAnnotations;

namespace PromillePartner_BackEnd.Models
{
    public class UpdateDrinkPlanData
    {
        [Key]
        public int ID { get; set; }
        public double TimeDifference { get; set; }

        public string DrinkName { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is UpdateDrinkPlanData data &&
                   ID == data.ID &&
                   TimeDifference == data.TimeDifference &&
                   DrinkName == data.DrinkName;
        }
    }
}
