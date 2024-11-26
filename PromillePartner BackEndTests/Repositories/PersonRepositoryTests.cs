using Microsoft.VisualStudio.TestTools.UnitTesting;
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
    public class PersonRepositoryTests
    {
        private PersonRepository repo = new();

        [TestInitialize()] 
        public void TestReset()
        {
            repo = new();
            Person input1 = new Person(-5, false, 80, 17);
            Person input2 = new Person(-5, true, 60, 19);
            Person input3 = new Person(-5, true, 90, 22);
            repo.AddPerson(input1);
            repo.AddPerson(input2);
            repo.AddPerson(input3);
        }

        [TestMethod()]
        public void AddMultiplePerson()
        {
            //arrange
            Person input4 = new Person(-5, false, 70, 18);
            Person input5 = new Person(-5, true, 100, 18);
            Person expected = new Person(1, false, 80, 17); //expected is the first person added from the testreset method

            //assert
            Assert.AreEqual(3, repo.GetPersons().Count());
            Assert.AreEqual(1, repo.GetPerson(1).Id);

            repo.AddPerson(input4); //act
            Assert.AreEqual(4, repo.GetPersons().Count());
            Assert.AreEqual(2, repo.GetPerson(2).Id);

            repo.AddPerson(input5); //act
            Assert.AreEqual(5, repo.GetPersons().Count());
            Assert.AreEqual(1, repo.GetPerson(1).Id);
            Assert.AreEqual(2, repo.GetPerson(2).Id);
            Assert.AreEqual(3, repo.GetPerson(3).Id);

            Assert.AreEqual(expected, repo.GetPerson(1));
        }

        [TestMethod()]
        public void ThrowExceptionWhenValidateFailsOnAdd()
        {
            //arrange
            Person input = new Person(-5, false, -8, 20);

            //act and assert
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => repo.AddPerson(input));
        }

        [TestMethod()]
        public void GetPersonTest()
        {
            //arrange
            Person expected = new Person(3, true, 90, 22); //expected is the third person added from the testreset method
            //act
            Person result = repo.GetPerson(3);
            //assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod()]
        public void GetPersonsTest()
        {
            //arrange
            Person expected1 = new Person(1, false, 80, 17); //expected is the first person added from the testreset method
            Person expected2 = new Person(2, true, 60, 19);//expected is the second person added from the testreset method
            Person expected3 = new Person(3, true, 90, 22);//expected is the third person added from the testreset method
            //act
            IEnumerable<Person> resultList = repo.GetPersons();
            resultList = resultList.OrderBy(p => p.Id);//Sorterer resultlvisten så lavest id kommer først
            //assert
            Assert.AreEqual(3, resultList.Count());
            Assert.AreEqual(expected1, resultList.ElementAt(0));
            Assert.AreEqual(expected2, resultList.ElementAt(1));
            Assert.AreEqual(expected3, resultList.ElementAt(2));
        }
    }
}