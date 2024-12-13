using Microsoft.EntityFrameworkCore;
using PromillePartner_BackEnd.Data;
using PromillePartner_BackEnd.Models;

namespace PromillePartner_BackEnd.Repositories
{
    /// <summary>
    /// Functionality for manipulating pi-readings from the database 
    /// </summary>
    /// <param name="context">dependency injection on the class to avoid using a constructor</param>
    public class PiReadingRepository
    {
        private readonly VoresDbContext _context;

        public PiReadingRepository(VoresDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <summary>
        /// Adds a pi reading to the database
        /// </summary>
        /// <param name="piReading">pi reading to be added</param>
        /// <returns>the newly added pi reading</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<PiReading> AddPiReading(PiReading piReading)
        {
            if(piReading == null)
            {
                throw new ArgumentNullException($"{piReading} can not be null");
            }
            piReading.Validate(); // model class validation, to make sure pireading doesnt over step constraints

            await _context.AddAsync(piReading); // add  
            await _context.SaveChangesAsync();  // save
            return piReading;
        }

        /// <summary>
        /// Searches for a pi reading that matches the given id
        /// </summary>
        /// <param name="id">id of pi reading to find</param>
        /// <returns>the pi reading</returns>
        /// <exception cref="InvalidOperationException">if the pi reading cant be found</exception>
        public PiReading GetPiReading(int id)
        {
            return _context.Set<PiReading>()
                           .FirstOrDefault(p => p.Id == id) ?? throw new InvalidOperationException($"PiReading with id {id} not found.");
        }

        /// <summary>
        /// Get all pi readings from the database
        /// </summary>
        /// <returns>A list of all pi readings</returns>
        public async Task<IEnumerable<PiReading>> GetPiReadings()
        {
            return await _context.Set<PiReading>().AsNoTracking().ToListAsync(); ;
        }

    }
}
