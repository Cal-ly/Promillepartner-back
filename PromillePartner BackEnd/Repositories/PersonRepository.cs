using Microsoft.EntityFrameworkCore;
using PromillePartner_BackEnd.Data;
using PromillePartner_BackEnd.Models;
using System;

namespace PromillePartner_BackEnd.Repositories;

/// <summary>
/// This is the repository for our person class. It contains methods to add, get, update, and delete persons.
/// </summary>
public class PersonRepository(VoresDbContext context)
{
    private readonly VoresDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

    /// <summary>
    /// Adds a person to the repository.
    /// </summary>
    /// <param name="person">The person to add.</param>
    /// <returns>The added person.</returns>
    /// <exception cref="ArgumentNullException">Thrown if the person is null.</exception>
    public async Task<Person> AddPerson(Person person)
    {
        if (person == null)
        {
            throw new ArgumentNullException(nameof(person), "Person cannot be null");
        }
        person.Validate();
        await _context.AddAsync(person);
        await _context.SaveChangesAsync();
        return person;
    }

    /// <summary>
    /// Gets a person by Id.
    /// </summary>
    /// <param name="id">The Id of the person to get.</param>
    /// <returns>The person with the specified Id, or null if not found.</returns>
    public async Task<Person?> GetPerson(int id)
    {
        return await _context.Set<Person>().FirstOrDefaultAsync(p => p.Id == id);
    }

    /// <summary>
    /// Gets all persons in the repository.
    /// </summary>
    /// <returns>A list of all persons.</returns>
    public async Task<IEnumerable<Person>> GetPersons()
    {
        return await _context.Set<Person>().AsNoTracking().ToListAsync();
    }

    /// <summary>
    /// Updates a person with the given Id.
    /// </summary>
    /// <param name="id">The Id of the person to update.</param>
    /// <param name="person">The updated person data.</param>
    /// <returns>The updated person.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if the Id is less than 1.</exception>
    /// <exception cref="ArgumentNullException">Thrown if the person is null.</exception>
    /// <exception cref="KeyNotFoundException">Thrown if the person with the specified Id is not found.</exception>
    public async Task<Person> UpdatePerson(int id, Person person)
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

        var foundPerson = await _context.Set<Person>().FirstOrDefaultAsync(p => p.Id == id)
            ?? throw new KeyNotFoundException($"Person with Id {id} not found.");

        foundPerson.Man = person.Man;
        foundPerson.Weight = person.Weight;
        foundPerson.Age = person.Age;
        await _context.SaveChangesAsync();
        return foundPerson;
    }

    /// <summary>
    /// Deletes a person with the given Id.
    /// </summary>
    /// <param name="id">The Id of the person to delete.</param>
    /// <returns>The deleted person.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if the Id is less than 1.</exception>
    /// <exception cref="KeyNotFoundException">Thrown if the person with the specified Id is not found.</exception>
    public async Task<Person> DeletePerson(int id)
    {
        if (id < 1)
        {
            throw new ArgumentOutOfRangeException(nameof(id), "Id cannot be less than 1");
        }

        var foundPerson = await _context.Set<Person>().FirstOrDefaultAsync(p => p.Id == id)
            ?? throw new KeyNotFoundException($"Person with Id {id} not found.");

        _context.Set<Person>().Remove(foundPerson);
        await _context.SaveChangesAsync();
        return foundPerson;
    }
}
