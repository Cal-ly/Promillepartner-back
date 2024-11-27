using PromillePartner_BackEnd.Models;
using PromillePartner_BackEnd.Repositories;

namespace PromillePartner_BackEnd.Data.MockData;

public static class MockPerson
{
    public static List<Person> GetMockPersons()
    {
        return new List<Person>
        {
            new Person(1, true, 70.5f, 25),
            new Person(2, false, 55.0f, 30),
            new Person(3, true, 80.0f, 40),
            new Person(4, false, 60.0f, 35),
            new Person(5, true, 90.0f, 50),
            new Person(6, false, 65.0f, 45),
            new Person(7, true, 75.0f, 20),
            new Person(8, false, 50.0f, 28),
            new Person(9, true, 85.0f, 33),
            new Person(10, false, 58.0f, 22)
        };
    }

    public static void AddMockPersonsToRepository(PersonRepository repository)
    {
        var mockPersons = GetMockPersons();
        foreach (var person in mockPersons)
        {
            repository.AddPerson(person);
        }
    }
}
