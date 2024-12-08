namespace PromillePartner_BackEnd.Models
{
    public class UpdateDrinkPlanRequest
    {
        public string Identifier { get; set; } = string.Empty;
        public UpdateDrinkPlanData? DrinkPlan { get; set; }
    }

}
