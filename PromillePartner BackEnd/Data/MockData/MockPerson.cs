using PromillePartner_BackEnd.Models;
using PromillePartner_BackEnd.Repositories;

namespace PromillePartner_BackEnd.Data.MockData;

public static class MockPerson
{
    public static List<Person> GetMockPersons()
    {
        return
        [
            new Person(1, true, 70.5, 25),
            new Person(2, false, 55.0, 30),
            new Person(3, true, 80.0, 40),
            new Person(4, false, 60.0, 35),
            new Person(5, true, 90.0, 50),
            new Person(6, false, 65.0, 45),
            new Person(7, true, 75.0, 20),
            new Person(8, false, 50.0, 28),
            new Person(9, true, 85.0, 33),
            new Person(10, false, 58.0, 22)
        ];
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
