using PromillePartner_BackEnd.Models;

namespace PromillePartner_BackEnd.Repositories
{
    /// <summary>
    /// This is the repository for our person class. It contains a list of all persons.
    /// And has the following methods: AddPerson, GetPerson, GetPersons(getall)
    /// </summary>
    public class PersonRepository
    {
        //List of all persons
        private List<Person> persons = new List<Person>();
        /// <summary>
        /// This method adds a person to the list of persons in the repository
        /// It runs validate from the person class before adding.
        /// </summary>
        /// <param name="person"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public void AddPerson(Person person)
        {
            if(person == null)
            {
                throw new ArgumentNullException($"{nameof(person)} cannot be null");
            }
            person.Validate();
            person.Id = persons.Count() + 1;
            persons.Add(person);
        }
        /// <summary>
        /// Get person by Id. If no person is found, null is returned.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        //Get a person by id
        public Person GetPerson(int id)
        {
            return persons.FirstOrDefault(p => p.Id == id) ?? throw new KeyNotFoundException($"Person with Id {id} not found."); ;
        }
        /// <summary>
        /// Returns the list of all persons in the repository.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Person> GetPersons()
        {
            return persons;
        }
        /// <summary>
        /// Empty constructor for the repository.
        /// </summary>
        public PersonRepository()
        {
        }
    }
}
