using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace IntemBB
{
        class SQL
        {
            public static string ueser;
            //public static string some;
            public static DataTable ExecuteQuery15(string Query, object[] parameter = null)
            {
                string ConnectionString = "Data Source=198.1.8.15;Initial Catalog=CWSS_S7;User ID=kendaKV2;Password=kenda123";

                using (var conn = new SqlConnection(ConnectionString))
                {
                    try
                    {
                        conn.Open();
                        SqlCommand cmd = new SqlCommand(Query, conn);
                        if (parameter != null)
                        {
                            string[] listPara = Query.Split(' ');
                            int i = 0;
                            foreach (string item in listPara)
                            {
                                if (item.Contains('?'))
                                {
                                    cmd.Parameters.AddWithValue(item, parameter[i]);
                                    i++;
                                }
                            }
                        }
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        return dt;
                    }
                    catch (Exception ex)
                    {
                        return new DataTable();
                    }
                    finally
                    {
                        if (conn.State != ConnectionState.Closed)
                            conn.Close();
                    }
                }

            }
            public static DataTable ExecuteQuery16(string Query, object[] parameter = null)
            {
                string ConnectionString = "Data Source=198.1.8.16;Initial Catalog=CWSS_S7;User ID=kendaKV2;Password=kenda123";

                using (var conn = new SqlConnection(ConnectionString))
                {
                    try
                    {
                        conn.Open();
                        SqlCommand cmd = new SqlCommand(Query, conn);
                        if (parameter != null)
                        {
                            string[] listPara = Query.Split(' ');
                            int i = 0;
                            foreach (string item in listPara)
                            {
                                if (item.Contains('?'))
                                {
                                    cmd.Parameters.AddWithValue(item, parameter[i]);
                                    i++;
                                }
                            }
                        }
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        return dt;
                    }
                    catch (Exception ex)
                    {
                        return new DataTable();
                    }
                    finally
                    {
                        if (conn.State != ConnectionState.Closed)
                            conn.Close();
                    }
                }

            }
            public static DataTable ExecuteQuery21(string Query, object[] parameter = null)
            {
                string ConnectionString = "Data Source=198.1.8.21;Initial Catalog=mfns;User ID=kendaKV2;Password=kenda123";

                using (var conn = new SqlConnection(ConnectionString))
                {
                    try
                    {
                        conn.Open();
                        SqlCommand cmd = new SqlCommand(Query, conn);
                        if (parameter != null)
                        {
                            string[] listPara = Query.Split(' ');
                            int i = 0;
                            foreach (string item in listPara)
                            {
                                if (item.Contains('?'))
                                {
                                    cmd.Parameters.AddWithValue(item, parameter[i]);
                                    i++;
                                }
                            }
                        }
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        return dt;
                    }
                    catch (Exception ex)
                    {
                        return new DataTable();
                    }
                    finally
                    {
                        if (conn.State != ConnectionState.Closed)
                            conn.Close();
                    }
                }

            }
            public static DataTable ExecuteQuery34(string Query, object[] parameter = null)
            {
                string ConnectionString = "Data Source=198.1.10.34;Initial Catalog=erp;User ID=kendaKV2;Password=kenda123";

                using (var conn = new SqlConnection(ConnectionString))
                {
                    try
                    {
                        conn.Open();
                        SqlCommand cmd = new SqlCommand(Query, conn);
                        if (parameter != null)
                        {
                            string[] listPara = Query.Split(' ');
                            int i = 0;
                            foreach (string item in listPara)
                            {
                                if (item.Contains('?'))
                                {
                                    cmd.Parameters.AddWithValue(item, parameter[i]);
                                    i++;
                                }
                            }
                        }
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        return dt;
                    }
                    catch (Exception ex)
                    {
                        return new DataTable();
                    }
                    finally
                    {
                        if (conn.State != ConnectionState.Closed)
                            conn.Close();
                    }
                }

            }
            public static DataTable ExecuteQuery33(string Query, object[] parameter = null)
            {
                string ConnectionString = "Data Source=198.1.10.33;Failover Partner=198.1.10.31;Initial Catalog=BB;User ID=kendaKV2;Password=kenda123";

                using (var conn = new SqlConnection(ConnectionString))
                {
                    try
                    {
                        conn.Open();
                        SqlCommand cmd = new SqlCommand(Query, conn);
                        if (parameter != null)
                        {
                            string[] listPara = Query.Split(' ');
                            int i = 0;
                            foreach (string item in listPara)
                            {
                                if (item.Contains('?'))
                                {
                                    cmd.Parameters.AddWithValue(item, parameter[i]);
                                    i++;
                                }
                            }
                        }
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        return dt;
                    }
                    catch (Exception ex)
                    {
                        return new DataTable();
                    }
                    finally
                    {
                        if (conn.State != ConnectionState.Closed)
                            conn.Close();
                    }
                }

            }
            public static DataTable ExecuteQuery35(string Query, object[] parameter = null)
            {
                string ConnectionString = "Data Source=198.1.8.35;Initial Catalog=mfns;User ID=kendaKV2;Password=kenda123";

                using (var conn = new SqlConnection(ConnectionString))
                {
                    try
                    {
                        conn.Open();
                        SqlCommand cmd = new SqlCommand(Query, conn);
                        if (parameter != null)
                        {
                            string[] listPara = Query.Split(' ');
                            int i = 0;
                            foreach (string item in listPara)
                            {
                                if (item.Contains('?'))
                                {
                                    cmd.Parameters.AddWithValue(item, parameter[i]);
                                    i++;
                                }
                            }
                        }
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        return dt;
                    }
                    catch (Exception ex)
                    {
                        return new DataTable();
                    }
                    finally
                    {
                        if (conn.State != ConnectionState.Closed)
                            conn.Close();
                    }
                }

            }
            public static DataTable ExecuteQuery36(string Query, object[] parameter = null)
            {
                string ConnectionString = "Data Source=198.1.8.36;Initial Catalog=mfns;User ID=kendaKV2;Password=kenda123";

                using (var conn = new SqlConnection(ConnectionString))
                {
                    try
                    {
                        conn.Open();
                        SqlCommand cmd = new SqlCommand(Query, conn);
                        if (parameter != null)
                        {
                            string[] listPara = Query.Split(' ');
                            int i = 0;
                            foreach (string item in listPara)
                            {
                                if (item.Contains('?'))
                                {
                                    cmd.Parameters.AddWithValue(item, parameter[i]);
                                    i++;
                                }
                            }
                        }
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        return dt;
                    }
                    catch (Exception ex)
                    {
                        return new DataTable();
                    }
                    finally
                    {
                        if (conn.State != ConnectionState.Closed)
                            conn.Close();
                    }
                }

            }
            public static bool CheckConnectSQL15()
            {
                string ConnectionString = "Data Source=198.1.8.15;Initial Catalog=CWSS_S7;User ID=kendaKV2;Password=kenda123";
                using (var conn = new SqlConnection(ConnectionString))
                {
                    try
                    {
                        conn.Open();
                        return true;
                    }
                    catch (Exception ex)
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
            public static bool CheckConnectSQL21()
            {
                string ConnectionString = "Data Source=198.1.8.21;Initial Catalog=mfns;User ID=kendaKV2;Password=kenda123";
                using (var conn = new SqlConnection(ConnectionString))
                {
                    try
                    {
                        conn.Open();
                        return true;
                    }
                    catch (Exception ex)
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
            public static bool CheckConnectSQL22()
            {
                string ConnectionString = "Data Source=198.1.8.22;Initial Catalog=mfns;User ID=kendaKV2;Password=kenda123";
                using (var conn = new SqlConnection(ConnectionString))
                {
                    try
                    {
                        conn.Open();
                        return true;
                    }
                    catch (Exception ex)
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
            public static bool CheckConnectSQL23()
            {
                string ConnectionString = "Data Source=198.1.8.23;Initial Catalog=mfns;User ID=kendaKV2;Password=kenda123";
                using (var conn = new SqlConnection(ConnectionString))
                {
                    try
                    {
                        conn.Open();
                        return true;
                    }
                    catch (Exception ex)
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
            public static bool CheckConnectSQL24()
            {
                string ConnectionString = "Data Source=198.1.8.24;Initial Catalog=mfns;User ID=kendaKV2;Password=kenda123";
                using (var conn = new SqlConnection(ConnectionString))
                {
                    try
                    {
                        conn.Open();
                        return true;
                    }
                    catch (Exception ex)
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
            public static bool CheckConnectSQL16()
            {
                string ConnectionString = "Data Source=198.1.8.16;Initial Catalog=CWSS_S7;User ID=kendaKV2;Password=kenda123";
                using (var conn = new SqlConnection(ConnectionString))
                {
                    try
                    {
                        conn.Open();
                        return true;
                    }
                    catch (Exception ex)
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
            public static bool CheckConnectSQL35()
            {
                string ConnectionString = "Data Source=198.1.8.35;Initial Catalog=mfns;User ID=kendaKV2;Password=kenda123";
                using (var conn = new SqlConnection(ConnectionString))
                {
                    try
                    {
                        conn.Open();
                        return true;
                    }
                    catch (Exception ex)
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
            public static bool CheckConnectSQL36()
            {
                string ConnectionString = "Data Source=198.1.8.36;Initial Catalog=mfns;User ID=kendaKV2;Password=kenda123";
                using (var conn = new SqlConnection(ConnectionString))
                {
                    try
                    {
                        conn.Open();
                        return true;
                    }
                    catch (Exception ex)
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
            public static bool ExecuteNonQuery21(string query, object[] parameter = null)
            {
                string ConnectionString = "Data Source=198.1.8.21;Initial Catalog=mfns;User ID=kendakv2;Password=kenda123";

                using (var conn = new SqlConnection(ConnectionString))
                {
                    try
                    {
                        conn.Open();

                        SqlCommand cmd = new SqlCommand(query, conn);

                        if (parameter != null)
                        {
                            string[] listPara = query.Split(' ');
                            int i = 0;
                            foreach (string item in listPara)
                            {
                                if (item.Contains('?'))
                                {
                                    cmd.Parameters.AddWithValue(item, parameter[i]);
                                    i++;
                                }
                            }
                        }
                        int effectedRow = cmd.ExecuteNonQuery();
                        return effectedRow > 0;
                    }
                    catch (Exception ex)
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
            public static bool ExecuteNonQuery22(string query, object[] parameter = null)
            {
                string ConnectionString = "Data Source=198.1.8.22;Initial Catalog=mfns;User ID=kendakv2;Password=kenda123";

                using (var conn = new SqlConnection(ConnectionString))
                {
                    try
                    {
                        conn.Open();

                        SqlCommand cmd = new SqlCommand(query, conn);

                        if (parameter != null)
                        {
                            string[] listPara = query.Split(' ');
                            int i = 0;
                            foreach (string item in listPara)
                            {
                                if (item.Contains('?'))
                                {
                                    cmd.Parameters.AddWithValue(item, parameter[i]);
                                    i++;
                                }
                            }
                        }
                        int effectedRow = cmd.ExecuteNonQuery();
                        return effectedRow > 0;
                    }
                    catch (Exception ex)
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
            public static bool ExecuteNonQuery23(string query, object[] parameter = null)
            {
                string ConnectionString = "Data Source=198.1.8.23;Initial Catalog=mfns;User ID=kendakv2;Password=kenda123";

                using (var conn = new SqlConnection(ConnectionString))
                {
                    try
                    {
                        conn.Open();

                        SqlCommand cmd = new SqlCommand(query, conn);

                        if (parameter != null)
                        {
                            string[] listPara = query.Split(' ');
                            int i = 0;
                            foreach (string item in listPara)
                            {
                                if (item.Contains('?'))
                                {
                                    cmd.Parameters.AddWithValue(item, parameter[i]);
                                    i++;
                                }
                            }
                        }
                        int effectedRow = cmd.ExecuteNonQuery();
                        return effectedRow > 0;
                    }
                    catch (Exception ex)
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
            public static bool ExecuteNonQuery24(string query, object[] parameter = null)
            {
                string ConnectionString = "Data Source=198.1.8.24;Initial Catalog=mfns;User ID=kendakv2;Password=kenda123";

                using (var conn = new SqlConnection(ConnectionString))
                {
                    try
                    {
                        conn.Open();

                        SqlCommand cmd = new SqlCommand(query, conn);

                        if (parameter != null)
                        {
                            string[] listPara = query.Split(' ');
                            int i = 0;
                            foreach (string item in listPara)
                            {
                                if (item.Contains('?'))
                                {
                                    cmd.Parameters.AddWithValue(item, parameter[i]);
                                    i++;
                                }
                            }
                        }
                        int effectedRow = cmd.ExecuteNonQuery();
                        return effectedRow > 0;
                    }
                    catch (Exception ex)
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
            public static bool ExecuteNonQuery35(string query, object[] parameter = null)
            {
                string ConnectionString = "Data Source=198.1.8.35;Initial Catalog=mfns;User ID=kendakv2;Password=kenda123";

                using (var conn = new SqlConnection(ConnectionString))
                {
                    try
                    {
                        conn.Open();

                        SqlCommand cmd = new SqlCommand(query, conn);

                        if (parameter != null)
                        {
                            string[] listPara = query.Split(' ');
                            int i = 0;
                            foreach (string item in listPara)
                            {
                                if (item.Contains('?'))
                                {
                                    cmd.Parameters.AddWithValue(item, parameter[i]);
                                    i++;
                                }
                            }
                        }
                        int effectedRow = cmd.ExecuteNonQuery();
                        return effectedRow > 0;
                    }
                    catch (Exception ex)
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
            public static bool ExecuteNonQuery36(string query, object[] parameter = null)
            {
                string ConnectionString = "Data Source=198.1.8.36;Initial Catalog=mfns;User ID=kendakv2;Password=kenda123";

                using (var conn = new SqlConnection(ConnectionString))
                {
                    try
                    {
                        conn.Open();

                        SqlCommand cmd = new SqlCommand(query, conn);

                        if (parameter != null)
                        {
                            string[] listPara = query.Split(' ');
                            int i = 0;
                            foreach (string item in listPara)
                            {
                                if (item.Contains('?'))
                                {
                                    cmd.Parameters.AddWithValue(item, parameter[i]);
                                    i++;
                                }
                            }
                        }
                        int effectedRow = cmd.ExecuteNonQuery();
                        return effectedRow > 0;
                    }
                    catch (Exception ex)
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

            public static bool ExecuteNonQuery186(string query, object[] parameter = null)
            {
                string ConnectionString = "Data Source=198.1.9.186;Initial Catalog=InTem;User ID=kendakv2;Password=kenda123";

                using (var conn = new SqlConnection(ConnectionString))
                {
                    try
                    {
                        conn.Open();

                        SqlCommand cmd = new SqlCommand(query, conn);

                        if (parameter != null)
                        {
                            string[] listPara = query.Split(' ');
                            int i = 0;
                            foreach (string item in listPara)
                            {
                                if (item.Contains('?'))
                                {
                                    cmd.Parameters.AddWithValue(item, parameter[i]);
                                    i++;
                                }
                            }
                        }
                        int effectedRow = cmd.ExecuteNonQuery();
                        return effectedRow > 0;
                    }
                    catch (Exception ex)
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

            public static DataTable ExecuteQuery23(string Query, object[] parameter = null)
            {
                string ConnectionString = "Data Source=198.1.8.23;Initial Catalog=mfns;User ID=kendaKV2;Password=kenda123";

                using (var conn = new SqlConnection(ConnectionString))
                {
                    try
                    {
                        conn.Open();
                        SqlCommand cmd = new SqlCommand(Query, conn);
                        if (parameter != null)
                        {
                            string[] listPara = Query.Split(' ');
                            int i = 0;
                            foreach (string item in listPara)
                            {
                                if (item.Contains('?'))
                                {
                                    cmd.Parameters.AddWithValue(item, parameter[i]);
                                    i++;
                                }
                            }
                        }
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        return dt;
                    }
                    catch (Exception ex)
                    {
                        return new DataTable();
                    }
                    finally
                    {
                        if (conn.State != ConnectionState.Closed)
                            conn.Close();
                    }
                }

            }

            public static DataTable ExecuteQuery24(string Query, object[] parameter = null)
            {
                string ConnectionString = "Data Source=198.1.8.24;Initial Catalog=mfns;User ID=kendaKV2;Password=kenda123";

                using (var conn = new SqlConnection(ConnectionString))
                {
                    try
                    {
                        conn.Open();
                        SqlCommand cmd = new SqlCommand(Query, conn);
                        if (parameter != null)
                        {
                            string[] listPara = Query.Split(' ');
                            int i = 0;
                            foreach (string item in listPara)
                            {
                                if (item.Contains('?'))
                                {
                                    cmd.Parameters.AddWithValue(item, parameter[i]);
                                    i++;
                                }
                            }
                        }
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        return dt;
                    }
                    catch (Exception ex)
                    {
                        return new DataTable();
                    }
                    finally
                    {
                        if (conn.State != ConnectionState.Closed)
                            conn.Close();
                    }
                }

            }

            public static bool ExecuteNonQuery33BB(string query, object[] parameter = null)
            {
                string ConnectionString = "Data Source=198.1.10.33;Failover Partner=198.1.10.31;Initial Catalog=BB;User ID=kendakv2;Password=kenda123";

                using (var conn = new SqlConnection(ConnectionString))
                {
                    try
                    {
                        conn.Open();

                        SqlCommand cmd = new SqlCommand(query, conn);

                        if (parameter != null)
                        {
                            string[] listPara = query.Split(' ');
                            int i = 0;
                            foreach (string item in listPara)
                            {
                                if (item.Contains('?'))
                                {
                                    cmd.Parameters.AddWithValue(item, parameter[i]);
                                    i++;
                                }
                            }
                        }
                        int effectedRow = cmd.ExecuteNonQuery();
                        return effectedRow > 0;
                    }
                    catch (Exception ex)
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
            public static DataTable ExecuteQuery33BB(string Query, object[] parameter = null)
            {
                string ConnectionString = "Data Source=198.1.10.33;Failover Partner=198.1.10.31;Initial Catalog=BB;User ID=kendakv2;Password=kenda123";

                using (var conn = new SqlConnection(ConnectionString))
                {
                    try
                    {
                        conn.Open();
                        SqlCommand cmd = new SqlCommand(Query, conn);
                        if (parameter != null)
                        {
                            string[] listPara = Query.Split(' ');
                            int i = 0;
                            foreach (string item in listPara)
                            {
                                if (item.Contains('?'))
                                {
                                    cmd.Parameters.AddWithValue(item, parameter[i]);
                                    i++;
                                }
                            }
                        }
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        return dt;
                    }
                    catch (Exception ex)
                    {
                        return new DataTable();
                    }
                    finally
                    {
                        if (conn.State != ConnectionState.Closed)
                            conn.Close();
                    }
                }

            }
            public static bool ExecuteNonQuery37(string query, object[] parameter = null)
            {
                string ConnectionString = "Data Source=198.1.8.37;Initial Catalog=mfns;User ID=kendakv2;Password=kenda123";

                using (var conn = new SqlConnection(ConnectionString))
                {
                    try
                    {
                        conn.Open();

                        SqlCommand cmd = new SqlCommand(query, conn);

                        if (parameter != null)
                        {
                            string[] listPara = query.Split(' ');
                            int i = 0;
                            foreach (string item in listPara)
                            {
                                if (item.Contains('?'))
                                {
                                    cmd.Parameters.AddWithValue(item, parameter[i]);
                                    i++;
                                }
                            }
                        }
                        int effectedRow = cmd.ExecuteNonQuery();
                        return effectedRow > 0;
                    }
                    catch (Exception ex)
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
            public static DataTable ExecuteQuery37(string Query, object[] parameter = null)
            {
                string ConnectionString = "Data Source=198.1.8.37;Initial Catalog=mfns;User ID=kendaKV2;Password=kenda123";

                using (var conn = new SqlConnection(ConnectionString))
                {
                    try
                    {
                        conn.Open();
                        SqlCommand cmd = new SqlCommand(Query, conn);
                        if (parameter != null)
                        {
                            string[] listPara = Query.Split(' ');
                            int i = 0;
                            foreach (string item in listPara)
                            {
                                if (item.Contains('?'))
                                {
                                    cmd.Parameters.AddWithValue(item, parameter[i]);
                                    i++;
                                }
                            }
                        }
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        return dt;
                    }
                    catch (Exception ex)
                    {
                        return new DataTable();
                    }
                    finally
                    {
                        if (conn.State != ConnectionState.Closed)
                            conn.Close();
                    }
                }

            }

            public static bool ExecuteNonQuery38(string query, object[] parameter = null)
            {
                string ConnectionString = "Data Source=198.1.8.38;Initial Catalog=mfns;User ID=kendakv2;Password=kenda123";

                using (var conn = new SqlConnection(ConnectionString))
                {
                    try
                    {
                        conn.Open();

                        SqlCommand cmd = new SqlCommand(query, conn);

                        if (parameter != null)
                        {
                            string[] listPara = query.Split(' ');
                            int i = 0;
                            foreach (string item in listPara)
                            {
                                if (item.Contains('?'))
                                {
                                    cmd.Parameters.AddWithValue(item, parameter[i]);
                                    i++;
                                }
                            }
                        }
                        int effectedRow = cmd.ExecuteNonQuery();
                        return effectedRow > 0;
                    }
                    catch (Exception ex)
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
            public static DataTable ExecuteQuery38(string Query, object[] parameter = null)
            {
                string ConnectionString = "Data Source=198.1.8.38;Initial Catalog=mfns;User ID=kendaKV2;Password=kenda123";

                using (var conn = new SqlConnection(ConnectionString))
                {
                    try
                    {
                        conn.Open();
                        SqlCommand cmd = new SqlCommand(Query, conn);
                        if (parameter != null)
                        {
                            string[] listPara = Query.Split(' ');
                            int i = 0;
                            foreach (string item in listPara)
                            {
                                if (item.Contains('?'))
                                {
                                    cmd.Parameters.AddWithValue(item, parameter[i]);
                                    i++;
                                }
                            }
                        }
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        return dt;
                    }
                    catch (Exception ex)
                    {
                        return new DataTable();
                    }
                    finally
                    {
                        if (conn.State != ConnectionState.Closed)
                            conn.Close();
                    }
                }

            }
            public static DataTable ExecuteQuery17(string Query, object[] parameter = null)
            {
                string ConnectionString = "Data Source=198.1.8.17;Initial Catalog=CWSS_S7;User ID=kendaKV2;Password=kenda123";

                using (var conn = new SqlConnection(ConnectionString))
                {
                    try
                    {
                        conn.Open();
                        SqlCommand cmd = new SqlCommand(Query, conn);
                        if (parameter != null)
                        {
                            string[] listPara = Query.Split(' ');
                            int i = 0;
                            foreach (string item in listPara)
                            {
                                if (item.Contains('?'))
                                {
                                    cmd.Parameters.AddWithValue(item, parameter[i]);
                                    i++;
                                }
                            }
                        }
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        return dt;
                    }
                    catch (Exception ex)
                    {
                        return new DataTable();
                    }
                    finally
                    {
                        if (conn.State != ConnectionState.Closed)
                            conn.Close();
                    }
                }

            }
            public static DataTable ExecuteQuery18(string Query, object[] parameter = null)
            {
                string ConnectionString = "Data Source=198.1.8.18;Initial Catalog=CWSS_S7;User ID=kendaKV2;Password=kenda123";

                using (var conn = new SqlConnection(ConnectionString))
                {
                    try
                    {
                        conn.Open();
                        SqlCommand cmd = new SqlCommand(Query, conn);
                        if (parameter != null)
                        {
                            string[] listPara = Query.Split(' ');
                            int i = 0;
                            foreach (string item in listPara)
                            {
                                if (item.Contains('?'))
                                {
                                    cmd.Parameters.AddWithValue(item, parameter[i]);
                                    i++;
                                }
                            }
                        }
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        return dt;
                    }
                    catch (Exception ex)
                    {
                        return new DataTable();
                    }
                    finally
                    {
                        if (conn.State != ConnectionState.Closed)
                            conn.Close();
                    }
                }

            }
        }
}