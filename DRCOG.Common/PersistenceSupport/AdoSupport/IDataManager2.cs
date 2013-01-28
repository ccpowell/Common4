using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace DRCOG.Common.PersistenceSupport.AdoSupport
{
    /// <summary>
    /// This interface serves as a gateway to data in a database with 
    /// ADO.Net.
    /// </summary>
    public interface IDataManager2
    {
        /// <summary>
        /// Executes an update command against a database and returns the number of records affected by the query.
        /// </summary>
        /// <param name="sql">The update query to execute.</param>
        /// <param name="commandType">The type of command to execute.</param>
        /// <param name="parameters">The parameters for the update.</param>
        /// <returns><see cref="System.Int32"/> of records affected.</returns>
        Int32 ExecuteNonQuery(String sql, CommandType commandType, IDictionary<String, Object> parameters = null);

        /// <summary>
        /// Executes a query that returns one piece of data.
        /// </summary>
        /// <param name="sql">The update query to execute.</param>
        /// <param name="commandType">The type of command to execute.</param>
        /// <param name="parameters">The parameters for the update.</param>
        /// <returns>The data expected.</returns>
        Object ExecuteScalar(String sql, CommandType commandType, IDictionary<String, Object> parameters = null);

        /// <summary>
        /// Gets a data reader given a sql query and parameters.
        /// </summary>
        /// <param name="sql">The sql query.</param>
        /// <param name="commandType">indicates if the query is text or a stored procedure.</param>
        /// <param name="parameters">The parameters for the query.  The String represents the name of the 
        /// parameter and the Object is the value for the parameter.</param>
        /// <returns>An <see cref="System.Data.IDataReader"/> containg the data from the query.</returns>
        IDataReader GetDataReader(String sql, CommandType commandType, IDictionary<String, Object> parameters = null);

        /// <summary>
        /// Get rid of this...
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="commandType"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        DataTable GetDataTable(String sql, CommandType commandType, IDictionary<String, Object> parameters = null);

        //IDataParameterCollection ExecuteNonQuery(String sql, CommandType commandType, IDictionary<String, Object> parameters);
        //Int32 ExecuteUpdate(String sql, CommandType commandType, IDictionary<String, Object> parameters);
        //Int32 ExecuteDelete(String sql, CommandType commandType, IDictionary<String, Object> parameters);
        //Object ExecuteInsert(String sql, CommandType commandType, IDictionary<String, Object> parameters);
        //DataSet GetDataSet(String sql, CommandType commandType);
        //DataSet GetDataSet(String sql, CommandType commandType, IDictionary<String, Object> parameters);
        //DataTable GetDataTable(String sql, CommandType commandType);
        //object GetLookupSingle(string sprocName, string keyFieldName, IDictionary<String, Object> parameters);
        //object GetLookupSingle(string sql, CommandType commandType, string keyFieldName, IDictionary<String, Object> parameters);
        //IDictionary<int, string> GetLookupCollection(string sprocName, string keyFieldName, string valueFieldName);
        //IDictionary<int, string> GetLookupCollection(string sprocName, string keyFieldName, string valueFieldName, IDictionary<String, Object> parameters);
        //IDictionary<int, string> GetLookupCollection(string sql, CommandType commandType, string keyFieldName, string valueFieldName, IDictionary<String, Object> parameters);
        //IList<string> GetLookupCollection(string sprocName, string valueFieldName, IDictionary<String, Object> parameters);
        //IList<string> GetLookupCollection(string sql, CommandType commandType, string valueFieldName, IDictionary<String, Object> parameters);
        //IDictionary<int, string> GetLookupCollection(string sprocName, string keyFieldName, string valueFieldName, IDictionary<String, Object> parameters, IDictionary<string, string> valueAddonValueSeparator);
        //IDictionary<int, string> GetLookupCollection(string sql, CommandType commandType, string keyFieldName, string valueFieldName, IDictionary<String, Object> parameters, IDictionary<string, string> valueAddonValueSeparator);
    }
}
