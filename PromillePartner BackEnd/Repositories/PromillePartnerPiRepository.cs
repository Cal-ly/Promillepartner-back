using PromillePartner_BackEnd.Data;
using PromillePartner_BackEnd.Models;
using Microsoft.EntityFrameworkCore;


namespace PromillePartner_BackEnd.Repositories
{
    /// <summary>
    /// A repository to keep track of indiviual promille partners, to allow multiple pis to be on at the same time.
    /// </summary>
    /// <param name="context">Our Database Context</param>
    public class PromillePartnerPiRepository(VoresDbContext context)
    {
        private readonly VoresDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

        /// <summary>
        /// Used to get all PromillePartnerPies From the Database
        /// </summary>
        /// <returns>All pies</returns>
        public async Task<IEnumerable<PromillePartnerPi>> GetAsync()
        {
            IEnumerable<PromillePartnerPi> pies = await _context.Set<PromillePartnerPi>().AsNoTracking().ToListAsync();

            return pies;
        }

        /// <summary>
        /// Used to get a single specific PromillePartnerPi with an identifier corresponding to the argument given
        /// </summary>
        /// <param name="identifier">The input identifier used to search for a PromillePartnerPi in the database</param>
        /// <returns></returns>
        public async Task<PromillePartnerPi?> GetByIdAsync(string identifier)
        {
            if (string.IsNullOrWhiteSpace(identifier))
            {
                throw new ArgumentNullException($"{nameof(identifier)} can't be null");
            }

            try
            {
                PromillePartnerPi? pi = await _context.Set<PromillePartnerPi>().FirstOrDefaultAsync(p => p.Identifier == identifier);
                return pi;
            }
            catch (Exception e)
            {
                throw new Exception("An error occurred while Looking for the pi.", e);
            }
        }

        /// <summary>
        /// Used to manually add a pi to the database
        /// </summary>
        /// <param name="pie">The pi that is to be added to the database</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<PromillePartnerPi> AddAsync(PromillePartnerPi pie)
        {
            if (pie == null)
            {
                throw new ArgumentNullException($"{nameof(pie)} can't be null");
            }
            pie.Validate();
            await _context.AddAsync(pie);
            await _context.SaveChangesAsync();
            return pie;
        }

        /// <summary>
        /// Delete a pie from the database
        /// </summary>
        /// <param name="identifer">the identifier used to find the pi that should be deleted</param>
        /// <returns></returns>
        public async Task<PromillePartnerPi?> DeleteAsync(string identifer)
        {
            if (string.IsNullOrEmpty(identifer))
            {
                throw new ArgumentNullException("The pi identifier can't be null, when trying to look-up a pi");
            }

            var piToBeDeleted = await _context.Set<PromillePartnerPi>().FirstOrDefaultAsync(p => p.Identifier == identifer);

            if(piToBeDeleted != null)
            {
                _context.Remove(piToBeDeleted);
                await _context.SaveChangesAsync();
            }
            return piToBeDeleted;

        }


        /// <summary>
        /// This method updates the IP of a Raspberry Pi
        /// </summary>
        public async Task<bool> UpdateIp(string identifier, string ip)
        {
            if (string.IsNullOrWhiteSpace(identifier))
            {
                throw new ArgumentNullException($"{nameof(identifier)} can't be null or empty.");
            }

            if (string.IsNullOrWhiteSpace(ip))
            {
                throw new ArgumentNullException($"{nameof(ip)} can't be null or empty.");
            }

            // Validate the IP format
            if (!System.Net.IPAddress.TryParse(ip, out _))
            {
                throw new ArgumentException($"{nameof(ip)} is not a valid IP address.");
            }

            // Find the Raspberry Pi by identifier
            var pi = await _context.Set<PromillePartnerPi>().FirstOrDefaultAsync(p => p.Identifier == identifier);

            if (pi == null)
            {
                return false; // No matching Raspberry Pi found
            }

            // Check if the IP needs updating
            if (pi.Ip != ip)
            {
                pi.Ip = ip;
                _context.Update(pi);
                await _context.SaveChangesAsync();
            }

            return true; // IP is now up-to-date
        }


        /// <summary>
        /// This method validates if the provided API key corresponds to the given identifier
        /// </summary>
        public async Task<bool> IsValidApiKey(string identifier, string apiKey)
        {
            if (string.IsNullOrWhiteSpace(identifier) || string.IsNullOrWhiteSpace(apiKey))
            {
                return false; // If identifier or API key is missing, return false
            }

            // Find the Raspberry Pi by identifier
            var pi = await _context.Set<PromillePartnerPi>().FirstOrDefaultAsync(p => p.Identifier == identifier);

            if (pi == null)
            {
                return false; // No matching Raspberry Pi was found
            }

            // Returns whether or not the Api-Key is valid
            return pi.ApiKey == apiKey; 
        }


    }
}
