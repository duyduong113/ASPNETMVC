﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Core
{
    public class SqlHelper
    {
        string connectionString { get; set; }
        public SqlHelper()
        {
            connectionString = ConfigurationManager.AppSettings["connectionString"].ToString();
        }

        public SqlHelper(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public SqlConnection Connection()
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            return con;
        }

        public SqlConnection Connection(string connectionString)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            return con;
        }

        public DataTable SelectQuery(string strSQL)
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand(strSQL, con);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return dt;
        }

        public DataTable ExecuteQuery(string spName, List<SqlParameter> listpara)
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = Connection(connectionString))
            {
                try
                {
                    SqlCommand command = con.CreateCommand();
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = spName;
                    if (spName != null)
                    {
                        foreach (SqlParameter para in listpara)
                        {
                            command.Parameters.Add(para);
                        }
                    }
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(dt);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return dt;
        }

        public int ExecuteNoneQuery(string spName, List<SqlParameter> listpara)
        {
            int n = -1;
            using (SqlConnection con = Connection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(spName, con);
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandTimeout = 0;
                    if (listpara != null)
                    {
                        foreach (SqlParameter para in listpara)
                            command.Parameters.Add(para);
                    }
                    n = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return n;
        }

        public int ExecuteNoneQuery(string spName, List<SqlParameter> listpara, CommandType t)
        {
            int n = -1;
            using (SqlConnection con = Connection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(spName, con);
                    command.CommandType = t;
                    command.CommandTimeout = 0;
                    if (listpara != null)
                    {
                        foreach (SqlParameter para in listpara)
                            command.Parameters.Add(para);
                    }
                    n = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    con.Close();
                    throw ex;
                }
            }
            return n;
        }

        public DataTable ExecuteString(string sql, List<SqlParameter> listpara)
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = con.CreateCommand();
                    command.CommandType = CommandType.Text;
                    command.CommandText = sql;
                    if (sql != null)
                    {
                        foreach (SqlParameter para in listpara)
                        {
                            command.Parameters.Add(para);
                        }
                    }
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(dt);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return dt;
        }

        public DataSet ExcuteQueryDataSet(string sp, List<SqlParameter> listpara)
        {
            DataSet dts = new DataSet();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand(sp, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 0;
                    if (listpara != null)
                    {
                        foreach (SqlParameter para in listpara)
                            cmd.Parameters.Add(para);
                    }
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dts);
                }
                catch (System.Exception ex)
                {
                    throw ex;
                }
            }
            return dts;
        }

        public object getValueProcWithParameter(string NameStoreProcedure, SqlParameter[] param, SqlConnection connect)
        {
            using (connect)
            {
                object obj = null;
                try
                {
                    SqlCommand sqlCmd = new SqlCommand();
                    sqlCmd.CommandTimeout = 2000;
                    sqlCmd.Connection = connect;
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.CommandText = NameStoreProcedure;
                    obj = sqlCmd.ExecuteScalar();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return obj;
            }
        }
   
    }
}
