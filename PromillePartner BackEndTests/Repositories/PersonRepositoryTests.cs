using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Moq;
using PromillePartner_BackEnd.Data;
using PromillePartner_BackEnd.Models;
using PromillePartner_BackEnd.Repositories;

namespace PromillePartner_BackEndTests.Repositories;

[TestClass]
public class PersonRepositoryTests
{
    private Mock<VoresDbContext>? _mockContext;
    private Mock<DbSet<Person>>? _mockDbSet;
    private PersonRepository _repository = null!;

    [TestInitialize]
    public void TestInitialize()
    {
        _mockDbSet = new Mock<DbSet<Person>>();
        _mockContext = new Mock<VoresDbContext>();

        // Configure the DbSet to return mock data
        var persons = new List<Person>().AsQueryable();
        _mockDbSet.As<IQueryable<Person>>().Setup(m => m.Provider).Returns(persons.Provider);
        _mockDbSet.As<IQueryable<Person>>().Setup(m => m.Expression).Returns(persons.Expression);
        _mockDbSet.As<IQueryable<Person>>().Setup(m => m.ElementType).Returns(persons.ElementType);
        _mockDbSet.As<IQueryable<Person>>().Setup(m => m.GetEnumerator()).Returns(persons.GetEnumerator());

        _mockContext.Setup(m => m.Set<Person>()).Returns(_mockDbSet.Object);

        // Setup AddAsync for AddPerson test
        _mockDbSet.Setup(m => m.AddAsync(It.IsAny<Person>(), default))
                  .ReturnsAsync((Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<Person>?)null!);

        _repository = new PersonRepository(_mockContext.Object);
    }

    [TestMethod]
    public async Task AddPerson_ShouldAddPerson_WhenPersonIsValid()
    {
        // Arrange
        Person person = new() { Id = 100, Man = true, Weight = 70, Age = 30 };

        // Act
        var result = await _repository.AddPerson(person);

        // Assert
        Assert.AreEqual(person, result);
        // _mockDbSet!.Verify(m => m.Add(person), Times.Once);
        // _mockContext!.Verify(m => m.SaveChangesAsync(default), Times.Once);
    }

    [TestMethod]
    public async Task GetPerson_ShouldReturnPerson_WhenPersonExists()
    {
        // Arrange
        var person = new Person { Id = 1, Man = true, Weight = 70, Age = 30 };
        var persons = new List<Person> { person };
        var mockSet = persons.AsQueryable().BuildMockDbSet();

        _mockContext!.Setup(m => m.Set<Person>()).Returns(mockSet.Object);

        // Act
        var result = await _repository.GetPerson(1);

        // Assert
        Assert.AreEqual(person, result);
    }

    [TestMethod]
    public async Task GetPersons_ShouldReturnAllPersons()
    {
        // Arrange
        var persons = new List<Person>
        {
            new Person { Id = 1, Man = true, Weight = 70, Age = 30 },
            new Person { Id = 2, Man = false, Weight = 60, Age = 25 }
        };
        var mockSet = persons.AsQueryable().BuildMockDbSet();

        _mockContext!.Setup(m => m.Set<Person>()).Returns(mockSet.Object);

        // Act
        var result = await _repository.GetPersons();

        // Assert
        Assert.AreEqual(2, result.Count());
        CollectionAssert.AreEquivalent(persons, result.ToList());
    }

    [TestMethod]
    public async Task UpdatePerson_ShouldUpdatePerson_WhenPersonExists()
    {
        // Arrange
        var person = new Person { Id = 1, Man = true, Weight = 70, Age = 30 };
        var updatedPerson = new Person { Id = 1, Man = false, Weight = 75, Age = 35 };

        var persons = new List<Person> { person };
        var mockSet = persons.AsQueryable().BuildMockDbSet();

        _mockContext!.Setup(m => m.Set<Person>()).Returns(mockSet.Object);

        // Act
        var result = await _repository.UpdatePerson(1, updatedPerson);

        // Assert
        Assert.AreEqual(updatedPerson.Weight, result.Weight);
        Assert.AreEqual(updatedPerson.Age, result.Age);
        Assert.AreEqual(updatedPerson.Man, result.Man);
        _mockContext.Verify(m => m.SaveChangesAsync(default), Times.Once);
    }

    [TestMethod]
    public async Task DeletePerson_ShouldRemovePerson_WhenPersonExists()
    {
        // Arrange
        var person = new Person { Id = 1, Man = true, Weight = 70, Age = 30 };
        var persons = new List<Person> { person };
        var mockSet = persons.AsQueryable().BuildMockDbSet();

        _mockContext!.Setup(m => m.Set<Person>()).Returns(mockSet.Object);

        // Act
        var result = await _repository.DeletePerson(1);

        // Assert
        Assert.AreEqual(person, result);
        mockSet.Verify(m => m.Remove(person), Times.Once);
        _mockContext.Verify(m => m.SaveChangesAsync(default), Times.Once);
    }
}
