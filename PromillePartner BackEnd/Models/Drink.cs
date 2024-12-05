using System.Text.Json;

namespace PromillePartner_BackEnd.Models
{
    public class Drink
    {
        public int Id { get; set; }
        //Name of the drink
        public string Name { get; set; } = string.Empty;
        //Volume of the drink in liter
        public double Volume { get; set; }
        //Alcohol percent
        public double AlcoholPercentOfVolume { get; set; }
        // Categories like strong, beer etc
        public string Category { get; set; } = string.Empty;

        public Drink(int id, string name, double volume, double alcoholPercentOfVolume, string category)
        {
            Id = id;
            Name = name;
            Volume = volume;
            AlcoholPercentOfVolume = alcoholPercentOfVolume;
            Category = category;
        }

        public Drink()
        {
        }

    }
}
