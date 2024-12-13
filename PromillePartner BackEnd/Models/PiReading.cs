using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PromillePartner_BackEnd.Models
{
    /// <summary>
    /// a promille reading with a timestamp of when it was taken
    /// </summary>
    public class PiReading
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public long TimeStampMiliseconds { get; set; }          // total time since 01/01/1970 -> now in miliseconds.
        public double Promille { get; set; }                    // takes a value which is assumed to be promille


        public PiReading(int id, long timeStampMiliseconds, double promille)
        {
            Id = id;
            TimeStampMiliseconds = timeStampMiliseconds;
            Promille = promille;
        }

        public PiReading()
        {

        }

        /// <summary>
        /// validate that timestamp has a value
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public void ValidateTimeStamp()
        {
            if(TimeStampMiliseconds < 0)
            {
                throw new ArgumentOutOfRangeException($"{nameof(TimeStampMiliseconds)} can not be less than 0");
            }
        }

        /// <summary>
        /// validate that promille has a value
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public void ValidatePromille()
        {
            if (Promille < 0)
            {
                throw new ArgumentOutOfRangeException($"{nameof(Promille)} can not be less than 0");
            }
        }

        /// <summary>
        /// helper method to easier use other validate methods
        /// </summary>
        public void Validate()
        {
            ValidateTimeStamp();
            ValidatePromille();
        }
    }
}
