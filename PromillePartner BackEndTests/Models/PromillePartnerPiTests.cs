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
    public class PromillePartnerPiTests
    {

        public PromillePartnerPi pi;

        [TestInitialize]
        public void _Init_()
        {
            pi = new() { Identifier = "uniqueIdentifier", Ip = "127.0.0.1" };
        }

        [TestMethod()]
        public void ValidateIndentifierTest()
        {
            pi.Validate();
            pi.Identifier = null;
            Assert.ThrowsException<ArgumentNullException>(() => pi.ValidateIndentifier());
        }

        [TestMethod()]
        public void ValidateIpTest()
        {
            pi.Validate();
            pi.Ip = null;
            Assert.ThrowsException<ArgumentNullException>(() => pi.ValidateIp());
        }
    }
}