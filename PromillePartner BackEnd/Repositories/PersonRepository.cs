using PromillePartner_BackEnd.Models;

namespace PromillePartner_BackEnd.Repositories;

/// <summary>
/// This is the repository for our person class. It contains a list of all persons.
/// And has the following methods: AddPerson, GetPerson, GetPersons(getall)
/// </summary>
public class PersonRepository
{
    private static readonly List<Person> _persons = Data.MockData.MockPerson.GetMockPersons();

    /// <summary>
    /// This method adds a person to the list of persons in the repository
    /// It runs validate from the person class before adding.
    /// </summary>
    /// <param name="person"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public void AddPerson(Person person)
    {
        if (person == null)
        {
            throw new ArgumentNullException(nameof(person), "Person cannot be null");
        }
        person.Validate();
        person.Id = _persons.Any() ? _persons.Max(p => p.Id) + 1 : 1;
        _persons.Add(person);
    }

    /// <summary>
    /// Get person by Id. If no person is found, an exception is thrown.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="KeyNotFoundException"></exception>
    public Person GetPerson(int id)
    {
        return _persons.FirstOrDefault(p => p.Id == id) ?? throw new KeyNotFoundException($"Person with Id {id} not found.");
    }

    /// <summary>
    /// Returns the list of all persons in the repository.
    /// </summary>
    /// <returns></returns>
    public IEnumerable<Person> GetPersons()
    {
        return new List<Person>(_persons);
    }

    /// <summary>
    /// Updates a person with the given id. Id must be greater than 0.
    /// Returns the updated person if successful.
    /// Throws exception if id is less than 1 or person is null.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="person"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    /// <exception cref="ArgumentNullException"></exception>
    public Person UpdatePerson(int id, Person person)
    {
        if (id < 1)
        {
            throw new ArgumentOutOfRangeException(nameof(id), "Id cannot be less than 1");
        }
        if (person == null)
        {
            throw new ArgumentNullException(nameof(person), "Person cannot be null");
        }
        person.Validate();

        var foundPerson = _persons.FirstOrDefault(p => p.Id == id) ?? throw new KeyNotFoundException($"Person with Id {id} not found.");

        foundPerson.Age = person.Age;
        foundPerson.Weight = person.Weight;
        foundPerson.Man = person.Man;

        return foundPerson;
    }

    /// <summary>
    /// Deletes a person with the given id. Id must be greater than 0.
    /// Returns the deleted person if successful.
    /// Throws exception if person is not found.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    /// <exception cref="KeyNotFoundException"></exception>
    public Person DeletePerson(int id)
    {
        if (id < 1)
        {
            throw new ArgumentOutOfRangeException(nameof(id), "Id cannot be less than 1");
        }
        var personToBeDeleted = _persons.FirstOrDefault(p => p.Id == id) ?? throw new KeyNotFoundException($"Person with Id {id} not found.");
        _persons.Remove(personToBeDeleted);
        return personToBeDeleted;
    }
}
