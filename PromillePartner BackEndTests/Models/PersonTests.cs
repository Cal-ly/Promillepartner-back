using Microsoft.VisualStudio.TestTools.UnitTesting;
using PromillePartner_BackEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromillePartner_BackEnd.Models.Tests;

[TestClass()]
public class PersonTests
{
    [TestMethod()]
    public void ValidateWeightTest()
    {
        // Arrange
        Person succes = new() { Weight = 29 };
        Person fail = new() { Weight = 28 };

        // Act & Assert
        succes.ValidateWeight();
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => fail.ValidateWeight());
    }

    [TestMethod()]
    public void ValidateAgeTest()
    {
        // Arrange
        Person badAge14 = new() { Age = 14, Weight = 30 };
        Person goodAge15 = new() { Age = 15, Weight = 30 };
        Person goodAge199 = new() { Age = 199, Weight = 30 };
        Person badAge201 = new() { Age = 201, Weight = 30 };

        // Act & Assert
        goodAge15.Validate();
        goodAge199.Validate();
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => badAge14.Validate());
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => badAge201.Validate());
    }

    [TestMethod()]
    public void ValidateTest()
    {
        // Arrange
        Person goodWeight29 = new() { Weight = 29, Age = 16 };
        Person badWeight28 = new() { Weight = 28, Age = 16 };
        Person badAge14 = new() { Age = 14, Weight = 30 };
        Person goodAge15 = new() { Age = 15, Weight = 30 };
        Person goodAge199 = new() { Age = 199, Weight = 30 };
        Person badAge201 = new() { Age = 201, Weight = 30 };

        // Act & Assert
        goodWeight29.Validate();
        goodAge15.Validate();
        goodAge199.Validate();

        Assert.ThrowsException<ArgumentOutOfRangeException>(() => badWeight28.Validate());
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => badAge14.Validate());
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => badAge201.Validate());
    }

    [TestMethod()]
    public void EqualsTest()
    {
        // Arrange
        Person person1 = new(1, true, 70, 25);
        Person person2 = new(1, true, 70, 25);
        Person person3 = new(2, false, 60, 30);

        // Act & Assert
        Assert.IsTrue(person1.Equals(person2));
        Assert.IsFalse(person1.Equals(person3));
    }

    [TestMethod()]
    public void GetHashCodeTest()
    {
        // Arrange
        Person person1 = new(1, true, 70, 25);
        Person person2 = new(1, true, 70, 25);
        Person person3 = new(2, false, 60, 30);

        // Act & Assert
        Assert.AreEqual(person1.GetHashCode(), person2.GetHashCode());
        Assert.AreNotEqual(person1.GetHashCode(), person3.GetHashCode());
    }
}
