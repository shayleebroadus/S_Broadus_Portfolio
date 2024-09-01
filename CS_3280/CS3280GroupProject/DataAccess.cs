using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Assignment_6;
/// <summary>
/// Data access class, that allows easier access to SQL data
/// </summary>
internal class DataAccess
{
    /// <summary>
    /// Connection string
    /// </summary>
    private static readonly string ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data source= " + Directory.GetCurrentDirectory() + "\\Invoice.mdb";
    /// <summary>
    /// Execute a SQL statement
    /// </summary>
    /// <param name="SQLStatement"></param>
    /// <param name="returnValue"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static DataSet ExecuteSQLStatement(string SQLStatement, ref int returnValue)
    {
        try
        {
            DataSet ds = new DataSet();
            using (OleDbConnection conn = new OleDbConnection(ConnectionString))
            {
                using (OleDbDataAdapter adapter = new OleDbDataAdapter())
                {
                    conn.Open();
                    adapter.SelectCommand = new OleDbCommand(SQLStatement, conn);
                    adapter.SelectCommand.CommandTimeout = 0;
                    adapter.Fill(ds);
                }
            }
            returnValue = ds.Tables[0].Rows.Count;
            return ds;
        }
        catch (Exception e) { throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + e.Message); }
    }
    /// <summary>
    /// Execute a scalar request
    /// </summary>
    /// <param name="SQLStatement"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static string ExecuteScalarSQL(string SQLStatement)
    {
        try
        {
            object obj;
            using (OleDbConnection conn = new OleDbConnection(ConnectionString))
            {
                using (OleDbDataAdapter adapter = new OleDbDataAdapter())
                {
                    conn.Open();
                    adapter.SelectCommand = new OleDbCommand(SQLStatement, conn);
                    adapter.SelectCommand.CommandTimeout = 0;
                    obj = adapter.SelectCommand.ExecuteScalar();
                }
            }
            if (obj == null) { return ""; }
            else return obj.ToString();
        }
        catch (Exception e) { throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + e.Message); }
    }
    /// <summary>
    /// Execute a non-query
    /// </summary>
    /// <param name="SQLStatement"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static int ExecuteNonQuery(string SQLStatement)
    {
        try
        {
            int AffectedRowCount;

            using (OleDbConnection conn = new OleDbConnection(ConnectionString))
            {
                conn.Open();
                OleDbCommand cmd = new OleDbCommand(SQLStatement, conn);
                cmd.CommandTimeout = 0;
                AffectedRowCount = cmd.ExecuteNonQuery();
            }
            return AffectedRowCount;
        }
        catch (Exception e) { throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + e.Message); }
    }
}
