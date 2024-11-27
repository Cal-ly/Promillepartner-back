using PromillePartner_BackEnd.Models;

namespace PromillePartner_BackEnd.Repositories
{
    public class PiReadingRepository
    {
        private List<PiReading> _PiReadingList = new List<PiReading>();

        public PiReading AddPiReading(PiReading piReading)
        {
            if(piReading == null)
            {
                throw new ArgumentNullException($"{piReading} can not be null");
            }
            piReading.Validate();
            piReading.Id = _PiReadingList.Count() + 1;
            _PiReadingList.Add(piReading);
            return piReading;
        }

        public PiReading GetPiReading(int id)
        {
            return _PiReadingList.FirstOrDefault(p => p.Id == id) ?? throw new InvalidOperationException($"PiReading with id {id} not found.");
        }

        public IEnumerable<PiReading> GetPiReadings()
        {
            return _PiReadingList;
        }

    }
}
