﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Oracle.ManagedDataAccess.Client;
using System.Data;

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

        [TestMethod]
        public void TestDBConnection()
        {
            var connectionString = $"Data Source=CI; User Id = {Environment.GetEnvironmentVariable("spawnusername")}; Password = {Environment.GetEnvironmentVariable("spawnpassword")};";            

            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                Assert.AreNotEqual(connection.State, ConnectionState.Open);
                connection.Open();               
                Assert.AreEqual(connection.State, ConnectionState.Open);
                connection.Dispose();
                connection.Close();
                OracleConnection.ClearPool(connection);
                OracleConnection.ClearAllPools();
            }
            
        }
    }
}