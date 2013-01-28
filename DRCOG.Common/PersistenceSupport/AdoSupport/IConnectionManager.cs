using System;
using System.Data;

namespace DRCOG.Common.PersistenceSupport.AdoSupport
{
    /// <summary>
    /// This interface is for ConnectionManagers that use ADO.Net providers.
    /// </summary>
    public interface IConnectionManager
    {
        /// <summary>
        /// <para>Creates a command and associates it with either a new unopened connection</para>
        /// <para>or the current active transaction and its open connection.</para>
        /// </summary>
        /// <returns>An <see cref="IDbCommand"/> with a reference to a transaction and or connection</returns>
        IDbCommand GetConnectedCommand();

        /// <summary>
        /// indicates if a transaction is currently open for the manager.
        /// </summary>
        Boolean HasOpenTransaction { get; }

        /// <summary>
        /// To use this method, the connection manager implementation cannot use a singleton 
        /// if multiple connections will be created by different users.
        /// </summary>
        /// <returns>A reference to the connection to be reused.</returns>
        IDbConnection TurnOnConnectionReuse();

        /// <summary>
        /// To use this method, the connection manager implementation cannot use a singleton 
        /// if multiple connections will be created by different users.
        /// </summary>
        void TurnOffConnectionReuse();

        Boolean IsReusingConnections { get; }

        Boolean IsUsingAmbientTransactions { get; }

        Boolean IsUsingStandardTransactions { get; }

        IDataAdapter CreateAdapter(IDbCommand command);
    }
}
