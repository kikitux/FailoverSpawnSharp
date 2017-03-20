using System;
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
            Assert.IsNotNull(Environment.GetEnvironmentVariable("dbinstance", EnvironmentVariableTarget.Machine));
            Assert.IsNotNull(Environment.GetEnvironmentVariable("dbusername",EnvironmentVariableTarget.Machine));
            Assert.IsNotNull(Environment.GetEnvironmentVariable("dbpassword", EnvironmentVariableTarget.Machine));
        }

        [TestMethod]
        public void TestDbConnection()
        {
            var connectionString = $"Data Source = {Environment.GetEnvironmentVariable("dbinstance", EnvironmentVariableTarget.Machine)};" +
                                   $"User Id = {Environment.GetEnvironmentVariable("dbusername", EnvironmentVariableTarget.Machine)}; " +
                                   $"Password = {Environment.GetEnvironmentVariable("dbpassword", EnvironmentVariableTarget.Machine)};" +
                                   $"Pooling = False;";            

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
