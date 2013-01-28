using System;
using System.Collections.Generic;
using System.Data;

namespace DRCOG.Common.PersistenceSupport.SqlCeSupport
{
    /// <summary>
    /// This interface serves as a gateway to data in a database with 
    /// ADO.Net.
    /// </summary>
    public interface IDataManager
    {
        /// <summary>
        /// Executes an update command against a database and returns the number records affected by the query.
        /// </summary>
        /// <param name="sql">The update query to execute.</param>
        /// <param name="commandType">The type of command to execute.</param>
        /// <param name="parameters">The parameters for the update.</param>
        /// <returns><see cref="System.Int32"/> of records affected.</returns>
        Int32 ExecuteUpdate(String sql, CommandType commandType, IDictionary<String, Object> parameters);

        /// <summary>
        /// Executes a query that returns one piece of data.
        /// </summary>
        /// <param name="sql">The query to execute.</param>
        /// <param name="commandType">The type of command to execute.</param>
        /// <returns>The data expected.</returns>
        Object ExecuteScalar(String sql, CommandType commandType);

        /// <summary>
        /// Executes a query that returns one piece of data.  Should never be used for inserts.  Transaction
        /// checking is not performed.
        /// </summary>
        /// <param name="sql">The update query to execute.</param>
        /// <param name="commandType">The type of command to execute.</param>
        /// <param name="parameters">The parameters for the update.</param>
        /// <returns>The data expected.</returns>
        Object ExecuteScalar(String sql, CommandType commandType, IDictionary<String, Object> parameters);

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
        Object ExecuteInsert(String sql, CommandType commandType, IDictionary<String, Object> parameters);

        /// <summary>
        /// Gets a data reader given a sql query and parameters.
        /// </summary>
        /// <param name="sql">The sql query.</param>
        /// <param name="commandType">indicates if the query is text or a stored procedure.</param>
        /// <param name="parameters">The parameters for the query.  The String represents the name of the 
        /// parameter and the Object is the value for the parameter.</param>
        /// <returns>An <see cref="System.Data.IDataReader"/> containg the data from the query.</returns>
        IDataReader GetDataReader(String sql, CommandType commandType, IDictionary<String, Object> parameters);

        /// <summary>
        /// Gets a data set given a sql query.
        /// </summary>
        /// <param name="sql">The sql query.</param>
        /// <param name="commandType">indicates if the query is text or a stored procedure.</param>
        /// <returns>An <see cref="System.Data.DataSet"/> containg the data from the query.</returns>
        DataSet GetDataSet(String sql, CommandType commandType);

        /// <summary>
        /// Gets a data set given a sql query and parameters.
        /// </summary>
        /// <param name="sql">The sql query.</param>
        /// <param name="commandType">indicates if the query is text or a stored procedure.</param>
        /// <param name="parameters">The parameters for the query.  The String represents the name of the 
        /// parameter and the Object is the value for the parameter.</param>
        /// <returns>An <see cref="System.Data.DataSet"/> containg the data from the query.</returns>
        DataSet GetDataSet(String sql, CommandType commandType, IDictionary<String, Object> parameters);

        /// <summary>
        ///     Allows the programmer to run a query against the Database.
        /// </summary>
        /// <param name="sql">The SQL to run</param>
        /// <param name="commandType">indicates if the query is text or a stored procedure.</param>
        /// <param name="parameters">The parameters for the query.  The String represents the name of the 
        /// parameter and the Object is the value for the parameter.</param>
        /// <returns>A DataTable containing the result set.</returns>
        DataTable GetDataTable(String sql, CommandType commandType, IDictionary<String, Object> parameters);

        /// <summary>
        /// Gets a data set given a sql query and parameters.
        /// </summary>
        /// <param name="sql">The sql query.</param>
        /// <param name="commandType">indicates if the query is text or a stored procedure.</param>
        /// <returns>An <see cref="System.Data.IDataReader"/> containg the data from the query.</returns>
        DataTable GetDataTable(String sql, CommandType commandType);


        /// <summary>
        /// Gets a data reader given a sql query.
        /// </summary>
        /// <param name="sql">The sql query.</param>
        /// <param name="commandType">indicates if the query is text or a stored procedure.</param>
        /// <returns>An <see cref="System.Data.IDataReader"/> containg the data from the query.</returns>
        IDataReader GetDataReader(String sql, CommandType commandType);

        /// <summary>
        /// Executes a delete command against a database and returns the records affected by the query.
        /// </summary>
        /// <param name="sql">The delete query to execute.</param>
        /// <param name="commandType">The type of command to execute.</param>
        /// <param name="parameters">The parameters for the statement.</param>
        /// <returns>The records affected.</returns>
        Int32 ExecuteDelete(String sql, CommandType commandType, IDictionary<String, Object> parameters);

        

        /// <summary>
        /// Get a single item from the database
        /// </summary>
        /// <param name="sprocName"></param>
        /// <param name="keyFieldName"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        object GetLookupSingle(string sprocName, string keyFieldName, IDictionary<String, Object> parameters);

        /// <summary>
        /// Get a single item from the database
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="commandType"></param>
        /// <param name="keyFieldName"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        object GetLookupSingle(string sql, CommandType commandType, string keyFieldName, IDictionary<String, Object> parameters);

        /// <summary>
        /// Get a lookup collection from the database
        /// </summary>
        /// <param name="sprocName"></param>
        /// <param name="keyFieldName"></param>
        /// <param name="valueFieldName"></param>
        /// <returns></returns>
        IDictionary<int, string> GetLookupCollection(string sprocName, string keyFieldName, string valueFieldName);

        /// <summary>
        /// Get a lookup collection from the database
        /// </summary>
        /// <param name="sprocName"></param>
        /// <param name="keyFieldName"></param>
        /// <param name="valueFieldName"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        IDictionary<int, string> GetLookupCollection(string sprocName, string keyFieldName, string valueFieldName, IDictionary<String, Object> parameters);

        /// <summary>
        /// Get a lookup collection from the database
        /// </summary>
        /// <param name="sprocName"></param>
        /// <param name="keyFieldName"></param>
        /// <param name="valueFieldName"></param>
        /// <param name="sqlParameters"></param>
        /// <returns></returns>
        IDictionary<int, string> GetLookupCollection(string sql, CommandType commandType, string keyFieldName, string valueFieldName, IDictionary<String, Object> parameters);

        /// <summary>
        /// Get a lookup collection from the database
        /// </summary>
        /// <param name="sprocName"></param>
        /// <param name="valueFieldName"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        IList<string> GetLookupCollection(string sprocName, string valueFieldName, IDictionary<String, Object> parameters);

        /// <summary>
        /// Get a lookup collection from the database
        /// </summary>
        /// <param name="sprocName"></param>
        /// <param name="keyFieldName"></param>
        /// <param name="valueFieldName"></param>
        /// <param name="sqlParameters"></param>
        /// <returns></returns>
        IList<string> GetLookupCollection(string sql, CommandType commandType, string valueFieldName, IDictionary<String, Object> parameters);
        
        /// <summary>
        /// Get a lookup collection from the database
        /// </summary>
        /// <param name="sprocName"></param>
        /// <param name="keyFieldName"></param>
        /// <param name="valueFieldName"></param>
        /// <param name="parameters"></param>
        /// <param name="valueAddonValueSeparator"></param>
        /// <returns></returns>
        IDictionary<int, string> GetLookupCollection(string sprocName, string keyFieldName, string valueFieldName, IDictionary<String, Object> parameters, IDictionary<string, string> valueAddonValueSeparator);
        
        /// <summary>
        /// Get a lookup collection from the database
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="commandType"></param>
        /// <param name="keyFieldName"></param>
        /// <param name="valueFieldName"></param>
        /// <param name="parameters"></param>
        /// <param name="valueAddonValueSeparator"></param>
        /// <returns></returns>
        IDictionary<int, string> GetLookupCollection(string sql, CommandType commandType, string keyFieldName, string valueFieldName, IDictionary<String, Object> parameters, IDictionary<string, string> valueAddonValueSeparator);

    }
}
