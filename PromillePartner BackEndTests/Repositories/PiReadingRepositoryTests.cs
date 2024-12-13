using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PromillePartner_BackEnd.Data;
using PromillePartner_BackEnd.Models;
using PromillePartner_BackEnd.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromillePartner_BackEnd.Repositories.Tests
{
    [TestClass()]
    public class PiReadingRepositoryTests
    {
        PiReadingRepository _repo;
        [TestInitialize()]
        public void TestReset(VoresDbContext context)
        {
            _repo = new(context);
           
        }

     

        [TestMethod()]
        public void AddAndGetPiReadingTest()
        {
            //arrange
            PiReading input1 = new PiReading(1, 1, 0.1);
            PiReading input2 = new PiReading(2, 2, 0.2);
            PiReading input3 = new PiReading(3, 3, 0.3);
            //act
            _repo.AddPiReading(input1);
            _repo.AddPiReading(input2);
            _repo.AddPiReading(input3);
            //assert
            Assert.AreEqual(3, _repo.GetPiReadings().Result.Count());
            Assert.AreEqual(1, _repo.GetPiReading(1).Id);
            Assert.AreEqual(2, _repo.GetPiReading(2).Id);
            Assert.AreEqual(3, _repo.GetPiReading(3).Id);
        }
    }
}