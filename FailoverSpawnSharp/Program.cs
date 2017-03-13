using System;
using System.Data;
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

                switch(connectionInfo.ConnectionState)
                {
                    case ConnectionState.Closed:
                        Console.WriteLine($"[{connectionInfo.ConnectionDateTime}]: {connectionInfo.DatabaseName}");
                        break;
                    default:
                        Console.WriteLine($"Error: ConnectionState should be {ConnectionState.Closed}, but was: {connectionInfo.ConnectionState}.");
                        break;
                }

                Console.WriteLine("");

                // Sleep 5 minutes.
                System.Threading.Thread.Sleep(5 * 60 * 1000);
            }
        }
    }
}
