using Microsoft.VisualStudio.TestTools.UnitTesting;
using PromillePartner_BackEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromillePartner_BackEnd.Models.Tests
{
    [TestClass()]
    public class PiReadingTests
    {
        [TestMethod()]
        public void ValidateTimeStampTest()
        {
            PiReading good = new() { TimeStampMiliseconds = 1 };
            PiReading okay = new() { TimeStampMiliseconds = 0 };
            PiReading bad = new() { TimeStampMiliseconds = -1 };

            good.ValidateTimeStamp();
            okay.ValidateTimeStamp();
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => bad.ValidateTimeStamp());
        }

        [TestMethod()]
        public void ValidatePromilleTest()
        {
            PiReading good = new() { Promille = 1 };
            PiReading okay = new() { Promille = 0 };
            PiReading bad = new() { Promille = -1 };

            good.ValidatePromille();
            okay.ValidatePromille();
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => bad.ValidatePromille());
        }

        [TestMethod()]
        public void ValidateTest()
        {
            PiReading good = new() { Promille = 1, TimeStampMiliseconds = 1 };
            PiReading okay = new() { Promille = 0, TimeStampMiliseconds = 0 };
            PiReading badPromille = new() { Promille = -1, TimeStampMiliseconds = 1 };
            PiReading badTimeStamp = new() { Promille = 1, TimeStampMiliseconds = -1 };

            good.Validate();
            okay.Validate();
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => badPromille.Validate());
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => badTimeStamp.Validate());
        }
    }
}