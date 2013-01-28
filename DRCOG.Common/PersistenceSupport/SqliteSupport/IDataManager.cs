using System;
using System.Collections.Generic;
using System.Data;

namespace DRCOG.Common.PersistenceSupport.SqliteSupport
{
    /// <summary>
    /// This interface serves as a gateway to data in a database with 
    /// ADO.Net.
    /// </summary>
    public interface IDataManager
    {
        DataTable GetDataTable(string sql);

        int ExecuteNonQuery(string sql);
        
        ///// <summary>
        ///// Executes an update command against a database and returns the number records affected by the query.
        ///// </summary>
        ///// <param name="sql">The update query to execute.</param>
        ///// <param name="commandType">The type of command to execute.</param>
        ///// <param name="parameters">The parameters for the update.</param>
        ///// <returns><see cref="System.Int32"/> of records affected.</returns>
        bool ExecuteUpdate(String tableName, Dictionary<String, Object> data, String where);

        /// <summary>
        /// Executes a query that returns one piece of data.
        /// </summary>
        /// <param name="sql">The query to execute.</param>
        /// <param name="commandType">The type of command to execute.</param>
        /// <returns>The data expected.</returns>
        string ExecuteScalar(string sql);

        /// <summary>
        /// Executes an insert command against a database and returns the id created by the database.
        /// </summary>
        /// <example>
        /// <paramref name="sql"/> set to 
        /// INSERT INTO table (column1) VALUES (@value); SELECT @@IDENTITY;
        /// 
        /// <paramref name="commandType"/> set to 
        /// <see cref="CommandType.Text"/>
        /// 
        /// <paramref name="parameters"/> contains 
        /// key "@value" value "value"
        /// </example>
        /// <param name="sql">The insert query to execute.</param>
        /// <param name="commandType">The type of command to execute.</param>
        /// <param name="parameters">The parameters for the insert.</param>
        /// <returns>The id if one exists.  A SELECT @@IDENTITY statement must be appended for anything to be returned</returns>
        bool ExecuteInsert(String tableName, Dictionary<String, Object> data);

        /// <summary>
        /// Executes a delete command against a database and returns the records affected by the query.
        /// </summary>
        /// <param name="sql">The delete query to execute.</param>
        /// <param name="commandType">The type of command to execute.</param>
        /// <param name="parameters">The parameters for the statement.</param>
        /// <returns>The records affected.</returns>
        bool ExecuteDelete(String tableName, String where);

        bool ClearDB();

        bool ClearTable(String table);

        bool CreateTableIfNotExists(String table, Dictionary<String, String> columns);

    }
}
