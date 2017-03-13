using System;
using System.Diagnostics;
using Oracle.ManagedDataAccess.Client;

namespace FailoverSpawnSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                // call DoWork()
                Console.WriteLine(DoWork());
                Console.WriteLine("");
                // Sleep 5 minutes.
                System.Threading.Thread.Sleep(5 * 60 * 1000);
            }
        }

        private static object ExecuteQuery(OracleConnection connection, string query)
        {
            var command = connection.CreateCommand();
            command.CommandText = query;
            var reader = command.ExecuteReader();
            try
            {
                reader.Read();
                return reader.GetValue(0);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                reader.Close();
                return 1;
            }
        }

        private static string DoWork()
        {
            var connectionString = $"Data Source=SPAWN; User Id = {Environment.GetEnvironmentVariable("spawnusername", EnvironmentVariableTarget.Machine)}; Password = {Environment.GetEnvironmentVariable("spawnpassword", EnvironmentVariableTarget.Machine)};";

            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                // Create new stopwatch.
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();

                connection.Open();         
                Console.WriteLine("Milliseconds elapsed open connection\t\t: {0}", stopwatch.ElapsedMilliseconds);

                var dbName = ExecuteQuery(connection, @"select lower(sys_context('userenv','db_name')) as db_name from dual");
                var sysdate = ExecuteQuery(connection, @"select sysdate from dual");

                Console.WriteLine("Milliseconds elapsed run staments\t\t: {0}", stopwatch.ElapsedMilliseconds);
                
                connection.Dispose();
                connection.Close();
                OracleConnection.ClearPool(connection);
                OracleConnection.ClearAllPools();
                Console.WriteLine("Milliseconds elapsed close connection/pool\t: {0}", stopwatch.ElapsedMilliseconds);
                stopwatch.Stop();

                Console.WriteLine("Connection state\t\t\t\t: " + connection.State);

                return $"[{sysdate}]: {dbName}";
            }
        }
    }
}
