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
        if (drinkPlan == null) // if drinkplan is invalid
        {
            throw new ArgumentNullException(nameof(drinkPlan), "Person cannot be null");
        }
        await _context.AddAsync(drinkPlan); // add
        await _context.SaveChangesAsync(); // save
        return drinkPlan;
    }

    /// <summary>
    /// this method return all drinkplan objects found in the database
    /// </summary>
    /// <returns>All DrinkPlan Repositories</returns>
    public async Task<List<DrinkPlan>> Get()
    {
        return await _context.Set<DrinkPlan?>().Include(d => d.DrinkPlanen).AsNoTracking().ToListAsync();
    }


    /// <summary>
    /// Gets a person by Id.
    /// </summary>
    /// <param name="id">The Id of the person to get.</param>
    /// <returns>The person with the specified Id, or null if not found.</returns>
    public async Task<DrinkPlan?> GetDrinkPlan(string identifier)
    {
        return await _context.Set<DrinkPlan>()
                                    .Include(d => d.DrinkPlanen)                // Important to include all of these
                                    .Where(d => d.Identifier == identifier)     // but only if the identifier matches
                                    .FirstOrDefaultAsync();                     // find the first object with identifier

    }

    /// <summary>
    /// Find a drinkplan with given identifier
    /// </summary>
    /// <param name="identifier">identifier used to find drink plan</param>
    /// <returns>The drink plan or null if it can't find it</returns>
    public async Task<DrinkPlan?> GetDrinkPlanByIdentifier(string identifier)
    {
        return await _context.Set<DrinkPlan>()                          
            .FirstOrDefaultAsync(dp => dp.Identifier == identifier);
    }

    /// <summary>
    /// updates an existing drink plan with a new drink plan
    /// </summary>
    /// <param name="drikkeplan">The new drink plan</param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">if the new drinkplan is not valid</exception>
    /// <exception cref="Exception">if no drink plan in the database contains the same identifier as the new drinkplan</exception>
    public async Task<DrinkPlan?> UpdateDrinkPlan(DrinkPlan drikkeplan)
    {
        if(drikkeplan == null)
        {
            throw new ArgumentNullException();
        }

        // Find a drink plan in the database with the same identifier as the drink plan given as argument
        var foundDrinkPlan = await _context.Set<DrinkPlan>()
                                           .Include(nameof(drikkeplan.DrinkPlanen))
                                           .FirstOrDefaultAsync(p => p.Identifier == drikkeplan.Identifier);

        // if no drinkplan is found throw exception
        if (foundDrinkPlan == null)
        {
            throw new Exception();
        }

        foundDrinkPlan.DrinkPlanen = new(); // clear all data in the existing drink plan

        // Add the values of the new drink plan to the existing drink plan
        foreach (var drink in drikkeplan.DrinkPlanen)
        {
            foundDrinkPlan.DrinkPlanen.Add(drink);
        }
        foundDrinkPlan.TimeStamp = drikkeplan.TimeStamp;    // set timestamp
        await _context.SaveChangesAsync();                  // save
        return drikkeplan;                                  // return the updated drink plan
    }

}
