namespace PromillePartner_BackEnd.Models
{
    public class PiReading
    {

        public int Id { get; set; }
        public long TimeStampMiliseconds { get; set; }
        public double Promille { get; set; }

        public PiReading(int id, long timeStampMiliseconds, double promille)
        {
            Id = id;
            TimeStampMiliseconds = timeStampMiliseconds;
            Promille = promille;
        }

        public PiReading()
        {

        }

        public void ValidateTimeStamp()
        {
            if(TimeStampMiliseconds < 0)
            {
                throw new ArgumentOutOfRangeException($"{nameof(TimeStampMiliseconds)} can not be less than 0");
            }
        }

        public void ValidatePromille()
        {
            if (Promille < 0)
            {
                throw new ArgumentOutOfRangeException($"{nameof(Promille)} can not be less than 0");
            }
        }

        public void Validate()
        {
            ValidateTimeStamp();
            ValidatePromille();
        }
    }
}
