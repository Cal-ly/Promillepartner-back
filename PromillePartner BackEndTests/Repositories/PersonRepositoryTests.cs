using PromillePartner_BackEnd.Models;

namespace PromillePartner_BackEnd.Repositories.Tests
{
    [TestClass]
    public class PersonRepositoryTests
    {
        private PersonRepository repo = new PersonRepository();

        [TestInitialize]
        public void TestInitialize()
        {
            repo = new PersonRepository();
        }

        [TestMethod]
        public void AddPerson_ValidPerson_AddsPerson()
        {
            // Arrange
            var person = new Person { Age = 25, Weight = 70, Man = true };

            // Act
            repo.AddPerson(person);

            // Assert
            Assert.AreEqual(person.Id, repo.GetPersons().Result.Count());
            Assert.AreEqual(person, repo.GetPerson(person.Id).Result);
        }

        [TestMethod]
        public void AddPerson_NullPerson_ThrowsArgumentNullException()
        {
            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => repo.AddPerson(null!));
        }

        [TestMethod]
        public void GetPerson_ValidId_ReturnsPerson()
        {
            // Arrange
            var person = new Person { Age = 25, Weight = 70, Man = true };
            repo.AddPerson(person);

            // Act
            var result = repo.GetPerson(person.Id).Result;

            // Assert
            Assert.AreEqual(person, result);
        }

        [TestMethod]
        public void GetPerson_InvalidId_ThrowsKeyNotFoundException()
        {
            // Arrange
            int initialCount = repo.GetPersons().Result.Count();

            // Act & Assert
            Assert.ThrowsException<KeyNotFoundException>(() => repo.GetPerson(initialCount + 1));
        }

        [TestMethod]
        public void GetPersons_ReturnsAllPersons()
        {
            // Arrange
            int initialCount = repo.GetPersons().Result.Count();
            var person1 = new Person { Age = 25, Weight = 70, Man = true };
            var person2 = new Person { Age = 30, Weight = 80, Man = false };
            repo.AddPerson(person1);
            repo.AddPerson(person2);

            // Act
            var result = repo.GetPersons().Result;

            // Assert
            Assert.AreEqual(initialCount + 2, result.Count());
            CollectionAssert.Contains(result.ToList(), person1);
            CollectionAssert.Contains(result.ToList(), person2);
        }

        [TestMethod]
        public void UpdatePerson_ValidId_UpdatesPerson()
        {
            // Arrange
            var person = new Person { Age = 25, Weight = 70, Man = true };
            repo.AddPerson(person);
            var updatedPerson = new Person { Age = 30, Weight = 75, Man = false };

            // Act
            var result = repo.UpdatePerson(person.Id, updatedPerson).Result;

            // Assert
            Assert.AreEqual(updatedPerson.Age, result.Age);
            Assert.AreEqual(updatedPerson.Weight, result.Weight);
            Assert.AreEqual(updatedPerson.Man, result.Man);
        }

        [TestMethod]
        public void UpdatePerson_InvalidId_ThrowsKeyNotFoundException()
        {
            // Act & Assert
            Assert.ThrowsException<KeyNotFoundException>(() => repo.UpdatePerson(1000000, new Person { Age = 30, Weight = 75, Man = false } ));
        }

        [TestMethod]
        public void UpdatePerson_NullPerson_ThrowsArgumentNullException()
        {
            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => repo.UpdatePerson(1, null!));
        }

        [TestMethod]
        public void DeletePerson_ValidId_DeletesPerson()
        {
            // Arrange
            int initialCount = repo.GetPersons().Result.Count();
            var person = new Person { Age = 25, Weight = 70, Man = true };
            repo.AddPerson(person);

            // Act
            var result = repo.DeletePerson(person.Id).Result;

            // Assert
            Assert.AreEqual(person, result);
            Assert.AreEqual(initialCount, repo.GetPersons().Result.Count());
        }

        [TestMethod]
        public void DeletePerson_InvalidId_ThrowsKeyNotFoundException()
        {
            // Act & Assert
            Assert.ThrowsException<KeyNotFoundException>(() => repo.DeletePerson(1000000));
        }

        [TestMethod]
        public void DeletePerson_IdLessThan1_ThrowsArgumentOutOfRangeException()
        {
            // Act & Assert
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => repo.DeletePerson(0));
        }
    }
}