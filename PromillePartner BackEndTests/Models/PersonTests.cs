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
    public class PersonTests
    {

        [TestMethod()]
        public void ValidateWeightTest()
        {
            //arrange
            Person good = new() { Weight = 30 };
            Person maybe = new() { Weight = 29 };
            Person bad = new() { Weight = 28 };

            good.ValidateWeight();
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => maybe.ValidateWeight());
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => bad.ValidateWeight());
        }

        [TestMethod()]
        public void ValidateAgeTest()
        {
            //arrange
            Person badAge14 = new() { Age = 14, Weight = 30 };
            Person badAge15 = new() { Age = 15, Weight = 30 };
            Person goodAge16 = new() { Age = 16, Weight = 30 };
            Person goodAge199 = new() { Age = 199, Weight = 30 };
            Person badAge200 = new() { Age = 200, Weight = 30 };
            Person badAge201 = new() { Age = 201, Weight = 30 };

            goodAge16.Validate();
            goodAge199.Validate();
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => badAge14.Validate());
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => badAge15.Validate());
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => badAge200.Validate());
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => badAge201.Validate());
        }

        [TestMethod()]
        public void ValidateTest()
        {

            Person goodWeight30 = new() { Weight = 30, Age = 16 };
            Person badWeight29 = new() { Weight = 29, Age = 16 };
            Person badWeight28 = new() { Weight = 28, Age = 16 };
            //arrange
            Person badAge14 = new() { Age = 14, Weight = 30 };
            Person badAge15 = new() { Age = 15, Weight = 30 };
            Person goodAge16 = new() { Age = 16, Weight = 30 };
            Person goodAge199 = new() { Age = 199, Weight = 30 };
            Person badAge200 = new() { Age = 200, Weight = 30 };
            Person badAge201 = new() { Age = 201, Weight = 30 };

            goodWeight30.Validate();
            goodAge16.Validate();
            goodAge199.Validate();

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => badWeight28.Validate());
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => badWeight29.Validate());
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => badAge14.Validate());
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => badAge15.Validate());
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => badAge200.Validate());
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => badAge201.Validate());


        }

    }
}