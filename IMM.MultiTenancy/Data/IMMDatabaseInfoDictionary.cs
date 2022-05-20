using IMM.MultiTenancy.Exceptions;
using IMM.MultiTenancy.Extentions.Collections;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMM.MultiTenancy.Data
{
    public class IMMDatabaseInfoDictionary : Dictionary<string, IMMDatabaseInfo>
    {
        private Dictionary<string, IMMDatabaseInfo> ConnectionIndex { get; set; }

        public IMMDatabaseInfoDictionary()
        {
            ConnectionIndex = new Dictionary<string, IMMDatabaseInfo>();
        }

        [CanBeNull]
        public IMMDatabaseInfo GetMappedDatabaseOrNull(string connectionStringName)
        {
            return ConnectionIndex.GetOrDefault(connectionStringName);
        }

        public IMMDatabaseInfoDictionary Configure(string databaseName, Action<IMMDatabaseInfo> configureAction)
        {
            var databaseInfo = this.GetOrAdd(
                databaseName,
                () => new IMMDatabaseInfo(databaseName)
            );

            configureAction(databaseInfo);

            return this;
        }

        /// <summary>
        /// This method should be called if this dictionary changes.
        /// It refreshes indexes for quick access to the connection informations.
        /// </summary>
        public void RefreshIndexes()
        {
            ConnectionIndex = new Dictionary<string, IMMDatabaseInfo>();

            foreach (var databaseInfo in Values)
            {
                foreach (var mappedConnection in databaseInfo.MappedConnections)
                {
                    if (ConnectionIndex.ContainsKey(mappedConnection))
                    {
                        throw new IMMException(
                            $"A connection name can not map to multiple databases: {mappedConnection}."
                        );
                    }

                    ConnectionIndex[mappedConnection] = databaseInfo;
                }
            }
        }
    }
}
