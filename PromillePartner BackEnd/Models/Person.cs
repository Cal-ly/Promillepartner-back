namespace PromillePartner_BackEnd.Models
{
    /// <summary>
    /// This is a class for a person. Id is handled by the repository. Gender is a boolean where man is true.
    /// Weight needs to be at least 29 kg.
    /// </summary>
    public class Person
    {
        /// <summary>
        /// This is Id for the person. It is handled by the repository.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// This is a boolean which represents whether this person is a man or not
        /// </summary>
        public bool Man { get; set; } // True is man, false is woman

        /// <summary>
        /// Weight is in kilograms and needs to be at least 29 kg
        /// </summary>
        public double Weight { get; set; } // Weight in kg

        /// <summary>
        /// Age as an int between 15 and 200
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        /// A constructor for person class
        /// </summary>
        /// <param name="id"></param>
        /// <param name="man"></param>
        /// <param name="weight"></param>
        /// <param name="age"></param>
        public Person(int id, bool man, double weight, int age) // Needs id, gender(bool), weight(double), age(int)
        {
            Id = id;
            Man = man;
            Weight = weight;
            Age = age;
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        public Person() // Empty constructor
        {
        }

        /// <summary>
        /// Validates the weight of each person
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if weight is less than or equal to 29</exception>
        public void ValidateWeight()
        {
            if (Weight < 29)
            {
                throw new ArgumentOutOfRangeException(nameof(Weight), "Weight cannot be less than 29 kg");
            }
        }

        /// <summary>
        /// Age has to be between 15 and 200
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public void ValidateAge()
        {
            if (Age < 15)
            {
                throw new ArgumentOutOfRangeException(nameof(Age), "Age cannot be less than 15");
            }
            if (Age > 200)
            {
                throw new ArgumentOutOfRangeException(nameof(Age), "Age cannot be more than 200");
            }
        }

        /// <summary>
        /// Main validate method calls every other validate method this class has
        /// used to validate the integrity of a person object
        /// </summary>
        public void Validate()
        {
            ValidateWeight();
            ValidateAge();
        }

        /// <summary>
        /// Default override of equals method
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object? obj)
        {
            return obj is Person person &&
                   Id == person.Id &&
                   Man == person.Man &&
                   Weight == person.Weight &&
                   Age == person.Age;
        }

        /// <summary>
        /// Generates a hash code for the current person object.
        /// </summary>
        /// <returns>A hash code for the current person object.</returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Man, Weight, Age);
        }
    }
}
