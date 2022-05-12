using IMM.MultiTenancy.Extentions;
using IMM.MultiTenancy.Extentions.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMM.MultiTenancy.Data
{
    public class AbpDbConnectionOptions
    {
        public ConnectionStrings ConnectionStrings { get; set; }

        public AbpDatabaseInfoDictionary Databases { get; set; }

        public AbpDbConnectionOptions()
        {
            ConnectionStrings = new ConnectionStrings();
            Databases = new AbpDatabaseInfoDictionary();
        }

        public string GetConnectionStringOrNull(
            string connectionStringName,
            bool fallbackToDatabaseMappings = true,
            bool fallbackToDefault = true)
        {
            var connectionString = ConnectionStrings.GetOrDefault(connectionStringName);
            if (!connectionString.IsNullOrEmpty())
            {
                return connectionString;
            }

            if (fallbackToDatabaseMappings)
            {
                var database = Databases.GetMappedDatabaseOrNull(connectionStringName);
                if (database != null)
                {
                    connectionString = ConnectionStrings.GetOrDefault(database.DatabaseName);
                    if (!connectionString.IsNullOrEmpty())
                    {
                        return connectionString;
                    }
                }
            }

            if (fallbackToDefault)
            {
                connectionString = ConnectionStrings.Default;
                if (!connectionString.IsNullOrWhiteSpace())
                {
                    return connectionString;
                }
            }

            return null;
        }
    }
}
