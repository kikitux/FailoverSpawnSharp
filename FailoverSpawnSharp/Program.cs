using System;
using System.Diagnostics;
using Oracle.ManagedDataAccess.Client;

namespace FailoverSpawnSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            var failoverSpawnConnector = new FailoverSpawnConnector();

            while (true)
            {
                var connectionInfo = failoverSpawnConnector.Connect();
                if (!string.IsNullOrEmpty(connectionInfo.DatabaseName))
                    Console.WriteLine($"[{connectionInfo.ConnectionDateTime}]: {connectionInfo.DatabaseName}");
                Console.WriteLine("");
                // Sleep 5 minutes.
                System.Threading.Thread.Sleep(5 * 60 * 1000);
            }
        }
    }
}
