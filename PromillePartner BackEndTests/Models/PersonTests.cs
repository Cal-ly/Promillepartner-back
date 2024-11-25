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
            good.Validate();

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => maybe.ValidateWeight());
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => maybe.Validate());

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => bad.ValidateWeight());
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => bad.Validate());
        }

    }
}