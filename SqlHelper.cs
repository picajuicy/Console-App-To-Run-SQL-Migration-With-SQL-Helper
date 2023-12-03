using System;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Summary description for SqlHelper.
/// </summary>
public class SQLHelper
{
    SqlConnection sqlConn;
    SqlCommand sqlCmd;
    //SqlDataReader sqlDr;
    SqlDataAdapter sqlDa;

    public SQLHelper()
    {
        string strConn = "server=serverName;database=DB_Name;user id=yourID;password=YourPassword";
        sqlConn = new SqlConnection(strConn);
        sqlConn.Open();
        sqlCmd = new SqlCommand();
        sqlCmd.Connection = sqlConn;
        sqlDa = new SqlDataAdapter(sqlCmd);
        sqlCmd.CommandText = SqlText;
    }

    public SQLHelper(string strConn)
    {
        //
        // TODO: Add constructor logic here
        //
        sqlConn = new SqlConnection(strConn);
        sqlConn.Open();
        sqlCmd = new SqlCommand();
        sqlCmd.Connection = sqlConn;
        sqlDa = new SqlDataAdapter(sqlCmd);

    }

    public SQLHelper(String Server, String Database)
    {
        string strConn = "server=" + Server + ";database=" + Database + ";user id=yourID;password=yourID";
        sqlConn = new SqlConnection(strConn);
        sqlConn.Open();
        sqlCmd = new SqlCommand();
        sqlCmd.Connection = sqlConn;
        sqlDa = new SqlDataAdapter(sqlCmd);
        sqlCmd.CommandText = SqlText;
    }

    public DataTable getDataTable(bool IsProcedure)
    {
        DataTable dt = new DataTable();
        if (IsProcedure)
            sqlCmd.CommandType = CommandType.StoredProcedure;
        else
            sqlCmd.CommandType = CommandType.Text;

        sqlDa.Fill(dt);
        return dt;
    }

    public void FillDataSet(DataSet ds, string TableName, bool IsProcedure)
    {
        if (IsProcedure)
        {
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlDa.Fill(ds, TableName);
        }
        else
        {
            sqlCmd.CommandType = CommandType.Text;
            sqlDa.Fill(ds, TableName);
        }
    }

    public DataTable getDataTable()
    {
        DataTable dt = new DataTable();
        sqlCmd.CommandType = CommandType.Text;

        sqlDa.Fill(dt);
        return dt;
    }

    public SqlDataReader getSqlDataReader(bool IsProcedure)
    {
        if (IsProcedure)
            sqlCmd.CommandType = CommandType.StoredProcedure;
        else
            sqlCmd.CommandType = CommandType.Text;

        return sqlCmd.ExecuteReader();
    }

    public string SqlText
    {
        set { sqlCmd.CommandText = value; }
        get { return sqlCmd.CommandText; }
    }

    public void ExecuteSql(bool IsProcedure)
    {

        sqlCmd.Connection = sqlConn;
        if (IsProcedure)
        {
            sqlCmd.CommandType = CommandType.StoredProcedure;
        }
        else
        {
            sqlCmd.CommandType = CommandType.Text;
        }
        sqlCmd.ExecuteNonQuery();
    }

    public object ExecuteScalar(bool IsProcedure)
    {

        sqlCmd.Connection = sqlConn;
        if (IsProcedure)
        {
            sqlCmd.CommandType = CommandType.StoredProcedure;
        }
        else
        {
            sqlCmd.CommandType = CommandType.Text;
        }
        return sqlCmd.ExecuteScalar();
    }

    public object GetIdentityValue(string tablName)
    {
        sqlCmd.CommandText = "SELECT IDENT_CURRENT('" + tablName + "')";
        return sqlCmd.ExecuteScalar();
    }

    public SqlParameter AddParameter(string strParam, object value)
    {
        return sqlCmd.Parameters.AddWithValue("@" + strParam, value);
    }
    public void SetValueForParameter(string strParam, object value)
    {
        sqlCmd.Parameters["@" + strParam].Value = value;
    }
    public void ClearAllParameters()
    {
        sqlCmd.Parameters.Clear();
    }
    public void Close()
    {
        sqlCmd.Dispose();
        sqlDa.Dispose();
        //sqlConn.Close();
    }

    public void Dispose()
    {
        sqlCmd.Dispose();
        sqlDa.Dispose();
        sqlConn.Close();
        sqlConn.Dispose();

    }

}

