using System.Data;
using Microsoft.Data.SqlClient;

namespace IntemBB.Web.Data;

public class SqlServerHelper
{
    private readonly IConfiguration _configuration;

    public SqlServerHelper(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GetConnectionString(string serverKey)
    {
        return _configuration.GetConnectionString(serverKey)
            ?? throw new InvalidOperationException($"Connection string '{serverKey}' not found.");
    }

    public string GetMachineConnectionString(string machineCode)
    {
        var serverKey = _configuration.GetSection("MachineConnections")[machineCode];
        if (string.IsNullOrEmpty(serverKey))
            throw new InvalidOperationException($"Machine connection '{machineCode}' not found.");
        return GetConnectionString(serverKey);
    }

    public DataTable ExecuteQuery(string serverKey, string query, Dictionary<string, object>? parameters = null)
    {
        var connectionString = GetConnectionString(serverKey);
        return ExecuteQueryWithConnectionString(connectionString, query, parameters);
    }

    public DataTable ExecuteQueryWithConnectionString(string connectionString, string query,
        Dictionary<string, object>? parameters = null)
    {
        using var conn = new SqlConnection(connectionString);
        var dt = new DataTable();
        try
        {
            conn.Open();
            using var cmd = new SqlCommand(query, conn);
            cmd.CommandTimeout = 6000;
            if (parameters != null)
            {
                foreach (var item in parameters)
                {
                    cmd.Parameters.AddWithValue("@" + item.Key, item.Value ?? DBNull.Value);
                }
            }

            using var adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"SQL Error: {ex.Message}");
        }

        return dt;
    }

    public bool ExecuteNonQuery(string serverKey, string query, Dictionary<string, object>? parameters = null)
    {
        var connectionString = GetConnectionString(serverKey);
        return ExecuteNonQueryWithConnectionString(connectionString, query, parameters);
    }

    public bool ExecuteNonQueryWithConnectionString(string connectionString, string query,
        Dictionary<string, object>? parameters = null)
    {
        using var conn = new SqlConnection(connectionString);
        try
        {
            conn.Open();
            using var cmd = new SqlCommand(query, conn);
            cmd.CommandTimeout = 6000;
            if (parameters != null)
            {
                foreach (var item in parameters)
                {
                    cmd.Parameters.AddWithValue("@" + item.Key, item.Value ?? DBNull.Value);
                }
            }

            return cmd.ExecuteNonQuery() > 0;
        }
        catch (SqlException sqlEx)
        {
            Console.WriteLine($"SQL Error: {sqlEx.Message}");
            return false;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            return false;
        }
    }

    public bool CheckConnection(string serverKey)
    {
        try
        {
            var connectionString = GetConnectionString(serverKey);
            using var conn = new SqlConnection(connectionString);
            conn.Open();
            return true;
        }
        catch
        {
            return false;
        }
    }
}
