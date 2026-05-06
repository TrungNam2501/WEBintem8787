using System.Data;
using Npgsql;

namespace IntemBB.Web.Data;

public class PostgresHelper
{
    private readonly string _connectionString;

    public PostgresHelper(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("Postgres")
            ?? throw new InvalidOperationException("Postgres connection string not found.");
    }

    public bool ExecuteNonQuery(string query, Dictionary<string, object>? parameters = null)
    {
        using var conn = new NpgsqlConnection(_connectionString);
        try
        {
            conn.Open();
            using var cmd = new NpgsqlCommand(query, conn);
            cmd.CommandTimeout = 6000;
            if (parameters != null)
            {
                foreach (var item in parameters)
                {
                    cmd.Parameters.AddWithValue(item.Key, item.Value ?? DBNull.Value);
                }
            }

            return cmd.ExecuteNonQuery() > 0;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Postgres Error: {ex.Message}");
            return false;
        }
    }

    public DataTable ExecuteQuery(string query, Dictionary<string, object>? parameters = null)
    {
        using var conn = new NpgsqlConnection(_connectionString);
        var dt = new DataTable();
        try
        {
            conn.Open();
            using var cmd = new NpgsqlCommand(query, conn);
            cmd.CommandTimeout = 6000;
            if (parameters != null)
            {
                foreach (var item in parameters)
                {
                    cmd.Parameters.AddWithValue(item.Key, item.Value ?? DBNull.Value);
                }
            }

            using var adapter = new NpgsqlDataAdapter(cmd);
            adapter.Fill(dt);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Postgres Error: {ex.Message}");
        }

        return dt;
    }
}
