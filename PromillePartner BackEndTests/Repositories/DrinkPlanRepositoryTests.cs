using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Moq;
using PromillePartner_BackEnd.Data;
using PromillePartner_BackEnd.Models;
using PromillePartner_BackEnd.Repositories;
using System;

namespace PromillePartner_BackEndTests.Repositories;

[TestClass]
public class DrinkPlanRepositoryTests
{
    private Mock<VoresDbContext>? _mockContext;
    private Mock<DbSet<DrinkPlan>>? _mockDbSet;
    private DrinkPlanRepository _repository = null!;

    [TestInitialize]
    public void TestInitialize()
    {
        _mockDbSet = new();
        _mockContext = new();

        // Configure the DbSet to return mock data
        var persons = new List<DrinkPlan>().AsQueryable();
        _mockDbSet.As<IQueryable<DrinkPlan>>().Setup(m => m.Provider).Returns(persons.Provider);
        _mockDbSet.As<IQueryable<DrinkPlan>>().Setup(m => m.Expression).Returns(persons.Expression);
        _mockDbSet.As<IQueryable<DrinkPlan>>().Setup(m => m.ElementType).Returns(persons.ElementType);
        _mockDbSet.As<IQueryable<DrinkPlan>>().Setup(m => m.GetEnumerator()).Returns(persons.GetEnumerator());

        _mockContext.Setup(m => m.Set<DrinkPlan>()).Returns(_mockDbSet.Object);

        // Setup AddAsync for AddPerson test
        _mockDbSet.Setup(m => m.AddAsync(It.IsAny<DrinkPlan>(), default))
                  .ReturnsAsync((Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<DrinkPlan>?)null!);

        _repository = new(_mockContext.Object);
    }

    [TestMethod]
    public async Task AddDrukplan()
    {
        // Arrange
        DrinkPlan drinkPlan = new() { Identifier = "bob", DrinkPlanen = new() { new() { ID = 1, DrinkName = "Florida", TimeDifference = 123.123 } } };

        // Act
        var result = await _repository.AddDrinkPlan(drinkPlan);

        // Assert
        Assert.AreEqual(drinkPlan, result);
        // _mockDbSet!.Verify(m => m.Add(person), Times.Once);
        // _mockContext!.Verify(m => m.SaveChangesAsync(default), Times.Once);
    }

    [TestMethod]
    public async Task GetDrukPlanById()
    {
        // Arran
        // 
        DrinkPlan drinkPlan = new() { Identifier = "bob", DrinkPlanen = new() { new() { ID = 1, DrinkName = "Florida", TimeDifference = 123.123 }, new() { ID = 1, DrinkName = "Florida", TimeDifference = 123.123 }, new() { ID = 1, DrinkName = "Florida", TimeDifference = 123.123 } } };
        var persons = new List<DrinkPlan> { drinkPlan };

        var mockSet = persons.AsQueryable().BuildMockDbSet();

        _mockContext!.Setup(m => m.Set<DrinkPlan>()).Returns(mockSet.Object);


        // Act
        var result = await _repository.GetDrinkPlan("bob");


        // Assert
        //Assert.AreEqual(1, result.Count());
        Assert.AreEqual(drinkPlan, result);
    }

    [TestMethod]
    public async Task UpdateDrukplanTest()
    {
        // Arrange

        DrinkPlan addedDrinkPlan = new() { Identifier = "bob", DrinkPlanen = new() { new() { ID = 1, DrinkName = "Mandor", TimeDifference = 123.123 }, new() { ID = 1, DrinkName = "Florida", TimeDifference = 123.123 }, new() { ID = 1, DrinkName = "Florida", TimeDifference = 123.123 } } };
        DrinkPlan newValuesDrinkPlan = new() { Identifier = "bob", DrinkPlanen = new() { new() { ID = 1, DrinkName = "Florida", TimeDifference = 123.123 }, new() { ID = 1, DrinkName = "Florida", TimeDifference = 123.123 }, new() { ID = 1, DrinkName = "Florida", TimeDifference = 123.123 } } };
        DrinkPlan expectedDrinkPlan = new() { Identifier = "bob", DrinkPlanen = new() { new() { ID = 1, DrinkName = "Florida", TimeDifference = 123.123 }, new() { ID = 1, DrinkName = "Florida", TimeDifference = 123.123 }, new() { ID = 1, DrinkName = "Florida", TimeDifference = 123.123 } } };

        var persons = new List<DrinkPlan> { addedDrinkPlan };
        var mockSet = persons.AsQueryable().BuildMockDbSet();
        _mockContext!.Setup(m => m.Set<DrinkPlan>()).Returns(mockSet.Object);

        // Act
        //await _repository.AddDrinkPlan(addedDrinkPlan);
        var result = await _repository.UpdateDrinkPlan(newValuesDrinkPlan);


        // Assert
        //Assert.AreEqual(1, result.Count());
        Assert.AreEqual(expectedDrinkPlan, result);
        Assert.AreEqual(expectedDrinkPlan.DrinkPlanen.ElementAt(0), result.DrinkPlanen.ElementAt(0));
    }
}
