using Microsoft.EntityFrameworkCore;
using PromillePartner_BackEnd.Data;
using PromillePartner_BackEnd.Models;
using System;

namespace PromillePartner_BackEnd.Repositories;

/// <summary>
/// This is the repository for our person class. It contains methods to add, get, update, and delete persons.
/// </summary>
public class DrinkPlanRepository(VoresDbContext context)
{
    private readonly VoresDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

    /// <summary>
    /// Adds a person to the repository.
    /// </summary>
    /// <param name="drinkPlan">The person to add.</param>
    /// <returns>The added person.</returns>
    /// <exception cref="ArgumentNullException">Thrown if the person is null.</exception>
    public async Task<DrinkPlan> AddDrinkPlan(DrinkPlan drinkPlan)
    {
        if (drinkPlan == null)
        {
            throw new ArgumentNullException(nameof(drinkPlan), "Person cannot be null");
        }
        await _context.AddAsync(drinkPlan);
        await _context.SaveChangesAsync();
        return drinkPlan;
    }

    /// <summary>
    /// Gets a person by Id.
    /// </summary>
    /// <param name="id">The Id of the person to get.</param>
    /// <returns>The person with the specified Id, or null if not found.</returns>
    public async Task<DrinkPlan?> GetDrinkPlan(string identifier)
    {
        return await _context.Set<DrinkPlan>().FirstAsync(p => p.Identifier == identifier);
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
    public async Task<DrinkPlan> UpdateDrinkPlan(string id, DrinkPlan person)
    {
        if (person == null)
        {
            throw new ArgumentNullException(nameof(person), "Person cannot be null");
        }

        var foundDrinkPlan = await _context.Set<DrinkPlan>().Include(nameof(person.DrinkPlanen)).FirstOrDefaultAsync(p => p.Identifier == id);

        foundDrinkPlan.DrinkPlanen = new();
        foreach (var drink in person.DrinkPlanen)
        {
            foundDrinkPlan.DrinkPlanen.Add(drink);
        }
        await _context.SaveChangesAsync();
        return foundDrinkPlan;
    }

    
}
