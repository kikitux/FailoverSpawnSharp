using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FailoverSpawnSharp
{
    public class ConnectionStringBuilder
    {
        public class ConnectionStringInfo
        {
            public string UserId { get; set; }
            public string Password { get; set; }
            public string DataSource { get; set; }
        }

        public ConnectionStringInfo GetConnectionStringInfo()
        {
            var userId = Environment.GetEnvironmentVariable("spawnusername", EnvironmentVariableTarget.Machine);
            var password = Environment.GetEnvironmentVariable("spawnpassword", EnvironmentVariableTarget.Machine);
            var dataSource = "SPAWN";

            var connectionStringInfo = new ConnectionStringInfo() { DataSource = dataSource, Password = password, UserId = userId};
            return connectionStringInfo;
        }

        public string Build()
        {
            var connectionStringInfo = GetConnectionStringInfo();
            var connectionString = $"Data Source={connectionStringInfo.DataSource}; User Id = {connectionStringInfo.UserId}; Password = {connectionStringInfo.Password};";
            return connectionString;
        }
    }
}
