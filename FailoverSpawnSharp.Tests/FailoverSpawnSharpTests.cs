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

            if (Environment.MachineName.Equals("NUC"))
            {
                Assert.IsNotNull(Environment.GetEnvironmentVariable("ciusername", EnvironmentVariableTarget.User));
                Assert.IsNotNull(Environment.GetEnvironmentVariable("cipassword", EnvironmentVariableTarget.User));
            }
            else
            {
                Assert.IsNotNull(Environment.GetEnvironmentVariable("spawnusername", EnvironmentVariableTarget.Machine));
                Assert.IsNotNull(Environment.GetEnvironmentVariable("spawnpassword", EnvironmentVariableTarget.Machine));
            }
           
        }

        [TestMethod]
        public void TestDBConnection()
        {
            var connectionString = $"Data Source=SPAWN; User Id = {Environment.GetEnvironmentVariable("spawnusername", EnvironmentVariableTarget.Machine)}; Password = {Environment.GetEnvironmentVariable("spawnpassword", EnvironmentVariableTarget.Machine)};";

            if (Environment.MachineName.Equals("NUC"))
            {
                connectionString = $"Data Source=CI; User Id = {Environment.GetEnvironmentVariable("ciusername", EnvironmentVariableTarget.User)}; Password = {Environment.GetEnvironmentVariable("cipassword", EnvironmentVariableTarget.User)};";
            }

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
