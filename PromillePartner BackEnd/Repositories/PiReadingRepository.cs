using Microsoft.EntityFrameworkCore;
using PromillePartner_BackEnd.Data;
using PromillePartner_BackEnd.Models;

namespace PromillePartner_BackEnd.Repositories
{
    public class PiReadingRepository(VoresDbContext context)
    {
        private List<PiReading> _PiReadingList = new List<PiReading>();

        private readonly VoresDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

        public async Task<PiReading> AddPiReading(PiReading piReading)
        {
            if(piReading == null)
            {
                throw new ArgumentNullException($"{piReading} can not be null");
            }
            piReading.Validate();

            //piReading.Id = _PiReadingList.Count() + 1; // database generates id now
            //_PiReadingList.Add(piReading);

            await _context.AddAsync(piReading);
            await _context.SaveChangesAsync();
            return piReading;
        }

        public PiReading GetPiReading(int id)
        {
            return _PiReadingList.FirstOrDefault(p => p.Id == id) ?? throw new InvalidOperationException($"PiReading with id {id} not found.");
        }

        public async Task<IEnumerable<PiReading>> GetPiReadings()
        {
            return await _context.Set<PiReading>().AsNoTracking().ToListAsync(); ;
        }

    }
}
