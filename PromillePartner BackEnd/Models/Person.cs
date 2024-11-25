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
        public bool Man { get; set; } //True is man, false is woman

        /// <summary>
        /// Weight is in kilograms
        /// </summary>
        public float Weight { get; set; } //Weight in kg

        /// <summary>
        /// A constructor for person class
        /// </summary>
        /// <param name="id"></param>
        /// <param name="man"></param>
        /// <param name="weight"></param>
        
        public Person(int id, bool man, float weight) //constructor for person
        {
            Id = id;
            Man = man;
            Weight = weight;
        }

        /// <summary>
        /// Default constructor
        /// </summary>

        public Person() //empty constructor
        {
        }

        /// <summary>
        /// Validates the weight of each person
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if weight is less than or equal to 29</exception>
        public void ValidateWeight()
        {
            if (Weight <= 29)
            {
                throw new ArgumentOutOfRangeException($"{nameof(Weight)} cannot be less than 29");
            }
        }


        /// <summary>
        /// Main validate method calls every other validate method this class has
        /// used to validate the integrity of a person object
        /// </summary>
        public void Validate()
        {
            ValidateWeight();
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
                   Weight == person.Weight;
        }
    }
}
