using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FailoverSpawnSharp.Tests
{
    [TestClass]
    public class FailoverSpawnSharpTests
    {
        [TestMethod]
        public void TestThatPasses()
        {
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void TestThatAlsoPasses()
        {
            Assert.IsFalse(false);
        }

        [TestMethod]
        public void TestVariables()
        {
            Assert.IsNotNull(Environment.GetEnvironmentVariable("spawnusername"));
            Assert.IsNotNull(Environment.GetEnvironmentVariable("spawnpassword"));
        }
    }
}
