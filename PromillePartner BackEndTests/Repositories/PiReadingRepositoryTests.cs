using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MockQueryable.EntityFrameworkCore;
using Moq;
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
        private Mock<VoresDbContext>? _mockContext;
        private Mock<DbSet<PiReading>>? _mockDbSet;
        PiReadingRepository _repo;
        [TestInitialize()]
        public void TestReset()
        {
            _mockDbSet = new();
            _mockContext = new();

            // Configure the DbSet to return mock data
            var persons = new List<PiReading>().AsQueryable();
            _mockDbSet.As<IQueryable<PiReading>>().Setup(m => m.Provider).Returns(persons.Provider);
            _mockDbSet.As<IQueryable<PiReading>>().Setup(m => m.Expression).Returns(persons.Expression);
            _mockDbSet.As<IQueryable<PiReading>>().Setup(m => m.ElementType).Returns(persons.ElementType);
            _mockDbSet.As<IQueryable<PiReading>>().Setup(m => m.GetEnumerator()).Returns(persons.GetEnumerator());

            _mockContext.Setup(m => m.Set<PiReading>()).Returns(_mockDbSet.Object);

            // Setup AddAsync for AddPerson test
            _mockDbSet.Setup(m => m.AddAsync(It.IsAny<PiReading>(), default))
                      .ReturnsAsync((Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<PiReading>?)null!);

            _repo = new(_mockContext.Object);


            _mockDbSet = new Mock<DbSet<PiReading>>();
            _mockDbSet.As<IAsyncEnumerable<PiReading>>()
                .Setup(m => m.GetAsyncEnumerator(It.IsAny<CancellationToken>()))
                .Returns(new TestAsyncEnumerator<PiReading>(persons.GetEnumerator()));

            _mockDbSet.As<IQueryable<PiReading>>()
                .Setup(m => m.Provider)
                .Returns(new List<PiReading>(persons.Provider));

            _mockContext = new Mock<VoresDbContext>();
            _mockContext.Setup(c => c.PiReadings).Returns(_mockDbSet.Object);
            //_repo = new(new Data.VoresDbContext());

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