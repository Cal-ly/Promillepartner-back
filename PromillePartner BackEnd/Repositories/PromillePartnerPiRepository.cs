using PromillePartner_BackEnd.Data;
using PromillePartner_BackEnd.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using PromillePartner_BackEnd.Data.MockData;


namespace PromillePartner_BackEnd.Repositories
{
    /// <summary>
    /// A repository to keep track of indiviual promille partners, to allow multiple pis to be on at the same time.
    /// </summary>
    /// <param name="context">Our Database Context</param>
    public class PromillePartnerPiRepository(VoresDbContext context)
    {
        private readonly VoresDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

        // private List<PromillePartnerPi> _pies = MockPromillePartnerPi.GetMockPies(); // for mock data

        /// <summary>
        /// Used to get all PromillePartnerPies From the Database
        /// </summary>
        /// <returns>All pies</returns>
        public async Task<IEnumerable<PromillePartnerPi>> GetAsync()
        {
            // Database
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

        public async Task<string?> SendToPie(List<UpdateDrinkPlanData> drinkPlan, PromillePartnerPi pi)
        {
            if (drinkPlan == null)
            {
                throw new ArgumentNullException(nameof(drinkPlan), "DrinkPlan or Drinks list was null");
            }
            if (pi == null)
            {
                throw new ArgumentNullException(nameof(pi), "Pi was null");
            }
            if (string.IsNullOrWhiteSpace(pi.Ip))
            {
                throw new ArgumentException("Pi does not have a valid IP address", nameof(pi));
            }

            string? response = null;

            // Extract the TimeDifference values and create a List<double>
            List<double> data = new List<double>();
            foreach (var val in drinkPlan)
            {
                data.Add(val.TimeDifference);
            }

            // Create an object with a "data" property
            var dataObject = new { data };

            // Serialize the object to JSON (this will include the "data" key)
            string jsonData = JsonSerializer.Serialize(dataObject);

            // Define the endpoint for the Raspberry Pi
            string raspberryPiIp = pi.Ip;
            int raspberryPiPort = 13000;

            try
            {
                using (TcpClient tcpClient = new TcpClient())
                {
                    // Set the connection timeout (e.g., 5 seconds)
                    var connectTask = tcpClient.ConnectAsync(raspberryPiIp, raspberryPiPort);
                    if (await Task.WhenAny(connectTask, Task.Delay(5000)) != connectTask)
                    {
                        throw new TimeoutException("Connection to Raspberry Pi timed out.");
                    }

                    using (NetworkStream networkStream = tcpClient.GetStream())
                    {
                        // Set a read timeout (e.g., 5 seconds)
                        tcpClient.ReceiveTimeout = 5000;

                        // Convert the JSON data to a byte array
                        byte[] dataToSend = Encoding.UTF8.GetBytes(jsonData);

                        // Send the data over the network
                        await networkStream.WriteAsync(dataToSend, 0, dataToSend.Length);

                        // Optionally, you can wait for a response from the server
                        byte[] buffer = new byte[1024];
                        int bytesRead = await networkStream.ReadAsync(buffer, 0, buffer.Length);

                        // Handle no response scenario
                        if (bytesRead == 0)
                        {
                            Console.WriteLine("No response from Raspberry Pi.");
                            return null;
                        }

                        // Convert the response to a string
                        response = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                        Console.WriteLine($"Response from Raspberry Pi: {response}");
                        return response;
                    }
                }
            }
            catch (TimeoutException ex)
            {
                Console.WriteLine($"Timeout error: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Log the detailed error
                Console.WriteLine($"An error occurred while sending data to the Raspberry Pi: {ex.Message}");
            }

            return null;
        }

    }
}
