using System;
using System.Collections.Generic;
using System.Data;
using Npgsql;

namespace IntemBB
{
    public class SQLproPg
    {
        private static readonly string connStr = "Host=198.1.10.85;Port=5432;Username=postgres;Password=kenda;Database=kverp;";

        public static bool ExecuteNonQueryPg(string query, CommandType commandType = CommandType.Text, Dictionary<string, object> param = null)
        {
            using (var conn = new NpgsqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.CommandType = commandType;
                        cmd.CommandTimeout = 6000;

                        if (param != null)
                        {
                            foreach (var item in param)
                            {
                                cmd.Parameters.AddWithValue(item.Key, item.Value ?? DBNull.Value);
                            }
                        }

                        int affectedRows = cmd.ExecuteNonQuery();
                        return affectedRows > 0;
                    }
                }
                catch
                {
                    return false;
                }
                finally
                {
                    if (conn.State != ConnectionState.Closed)
                        conn.Close();
                }
            }
        }

        public static DataTable ExecuteQueryPg(string query, CommandType commandType = CommandType.Text, Dictionary<string, object> param = null)
        {
            using (var conn = new NpgsqlConnection(connStr))
            {
                DataTable dt = new DataTable();
                try
                {
                    conn.Open();
                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.CommandType = commandType;
                        cmd.CommandTimeout = 6000;

                        if (param != null)
                        {
                            foreach (var item in param)
                            {
                                cmd.Parameters.AddWithValue(item.Key, item.Value ?? DBNull.Value);
                            }
                        }

                        using (var adapter = new NpgsqlDataAdapter(cmd))
                        {
                            adapter.Fill(dt);
                        }
                    }
                }
                catch
                {
                    // Trường hợp lỗi, trả về DataTable rỗng
                }
                finally
                {
                    if (conn.State != ConnectionState.Closed)
                        conn.Close();
                }

                return dt;
            }
        }
    }
}
