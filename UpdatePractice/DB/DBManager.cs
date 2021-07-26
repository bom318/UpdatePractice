using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;
using UpdatePractice.Models;

namespace UpdatePractice.DB
{
    public class DBManager
    {
        public const string _connectionString = "Server=192.168.0.112,1434; Database=UPDATE_PRACTICE; uid=sa; pwd=qwe123!@#";

        public User GetUserInfo(string userId, string userPW)
        {
            User result = null;

            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("USP_GET_USER_INFO", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@USER_ID", userId);
                        cmd.Parameters.AddWithValue("@USER_PWD", userPW);

                        var reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            result = new User();
                            result.userId = reader["USER_ID"].ToString();
                        }

                    }
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("GetUserInfo : " + e.Message);
            }


            return result;

        }
        public bool InsertApp(Package package)
        {
            bool result = false;

            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("USP_INSERT_APP", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@appName", package.appName);
                        cmd.Parameters.AddWithValue("@appCmd", package.appCmd);
                        cmd.Parameters.AddWithValue("@protocol", package.protocol);
                        cmd.Parameters.AddWithValue("@server", package.server);
                        cmd.Parameters.AddWithValue("@path", package.path);
                        cmd.Parameters.AddWithValue("@memo", package.memo);

                        var excuteResult = cmd.ExecuteNonQuery();

                        if (excuteResult > 0)
                        {
                            result = true;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("InsertApp : " + ex.Message);
            }

            return result;
        }

        public List<Package> GetAppList()
        {
            List<Package> result = new List<Package>();

            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("USP_GET_APP_LIST", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;


                        var reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            Package package = new Package();
                            package.seq = int.Parse(reader["SEQ"].ToString());
                            package.appName = reader["APP_NAME"].ToString();
                            package.appCmd = reader["APP_CMD"].ToString();
                            package.version = reader["VERSION"].ToString();
                            package.protocol = reader["PROTOCOL"].ToString();
                            package.server = reader["SERVER"].ToString();
                            package.path = reader["PATH"].ToString();
                            package.state = reader["STATE"].ToString();
                            package.memo = reader["MEMO"].ToString();


                            result.Add(package);
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("GetAppList : " + ex.Message);
            }

            return result;

        }
        public bool UpdateApp(Package package)
        {
            bool result = false;

            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("USP_UPDATE_APP", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@SEQ", package.seq);
                        cmd.Parameters.AddWithValue("@APP_NAME", package.appName);
                        cmd.Parameters.AddWithValue("@APP_CMD", package.appCmd);
                        cmd.Parameters.AddWithValue("@VERSION", package.version);
                        cmd.Parameters.AddWithValue("@PROTOCOL", package.protocol);
                        cmd.Parameters.AddWithValue("@SERVER", package.server);
                        cmd.Parameters.AddWithValue("@PATH", package.path);
                        cmd.Parameters.AddWithValue("@MEMO", package.memo);

                        var excuteResult = cmd.ExecuteNonQuery();

                        if (excuteResult > 0)
                        {
                            result = true;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("UpdateApp : " + ex.Message);
            }

            return result;
        }
        public bool StopState(int seq)
        {
            bool result = false;

            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("USP_STOP_STATE", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@SEQ", seq);
                        var excuteResult = cmd.ExecuteNonQuery();

                        if (excuteResult > 0)
                        {
                            result = true;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("StopState : " + ex.Message);
            }

            return result;
        }
        public bool UpdateState(int seq)
        {
            bool result = false;

            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("USP_UPDATE_STATE", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@SEQ", seq);
                        var excuteResult = cmd.ExecuteNonQuery();

                        if (excuteResult > 0)
                        {
                            result = true;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("UpdateState : " + ex.Message);
            }

            return result;
        }
        public bool DeleteApp(int seq)
        {
            bool result = false;

            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("USP_DELETE_APP", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@SEQ", seq);
                        var excuteResult = cmd.ExecuteNonQuery();

                        if (excuteResult > 0)
                        {
                            result = true;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("DeleteState : " + ex.Message);
            }

            return result;
        }
        public List<FileInfo> GetFileList(int seq)
        {
            List<FileInfo> result = new List<FileInfo>();

            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("USP_GET_FILE_LIST", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("SEQ", seq);

                        var reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            FileInfo file = new FileInfo();
                            file.seq = int.Parse(reader["SEQ"].ToString());
                            file.name = reader["NAME"].ToString();
                            file.local = reader["LOCAL"].ToString();
                            file.tagName = reader["TAG_NAME"].ToString();
                            file.version = reader["VERSION"].ToString();
                            file.type = reader["TYPE"].ToString();
                            file.reg = reader["REG"].ToString();
                            file.size = reader["SIZE"].ToString();
                            file.path = reader["PATH"].ToString();


                            result.Add(file);
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("GetAppList : " + ex.Message);
            }

            return result;

        }
        public bool InsertFile(FileInfo file)
        {
            bool result = false;

            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("USP_INSERT_FILE", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@SEQ", file.appSeq);
                        cmd.Parameters.AddWithValue("@NAME", file.name);
                        cmd.Parameters.AddWithValue("@LOCAL", file.local);
                        cmd.Parameters.AddWithValue("@TAG_NAME", file.tagName);
                        cmd.Parameters.AddWithValue("@TYPE", file.type);
                        cmd.Parameters.AddWithValue("@SIZE", file.size);
                        cmd.Parameters.AddWithValue("@PATH", file.path);
                        cmd.Parameters.AddWithValue("@VERSION", file.version);

                        var excuteResult = cmd.ExecuteNonQuery();

                        if (excuteResult > 0)
                        {
                            result = true;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("InsertFile : " + ex.Message);
            }

            return result;
        }
        public bool DeleteFile(int seq)
        {
            bool result = false;

            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("USP_DELETE_FILE", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@SEQ", seq);
                        var excuteResult = cmd.ExecuteNonQuery();

                        if (excuteResult > 0)
                        {
                            result = true;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("DeleteFile : " + ex.Message);
            }

            return result;
        }
        public bool UpdateFile(FileInfo file)
        {
            bool result = false;

            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("USP_UPDATE_FILE", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@LOCAL", file.local);
                        cmd.Parameters.AddWithValue("@TAG_NAME", file.tagName);
                        cmd.Parameters.AddWithValue("@VERSION", file.version);
                        cmd.Parameters.AddWithValue("@TYPE", file.type);
                        cmd.Parameters.AddWithValue("@REG", file.reg);
                        cmd.Parameters.AddWithValue("@NAME", file.name);
                        cmd.Parameters.AddWithValue("@PATH", file.path);

                        var excuteResult = cmd.ExecuteNonQuery();

                        if (excuteResult > 0)
                        {
                            result = true;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("UpdateFile : " + ex.Message);
            }

            return result;
        }
        public bool UpdateNewFile(FileInfo file)
        {
            bool result = false;

            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("USP_UPDATE_NEW_FILE", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@NAME", file.name);
                        cmd.Parameters.AddWithValue("@LOCAL", file.local);
                        cmd.Parameters.AddWithValue("@TAG_NAME", file.tagName);
                        cmd.Parameters.AddWithValue("@TYPE", file.type);
                        cmd.Parameters.AddWithValue("@SIZE", file.size);
                        cmd.Parameters.AddWithValue("@VERSION", file.version);
                        cmd.Parameters.AddWithValue("@PATH", file.path);

                        var excuteResult = cmd.ExecuteNonQuery();

                        if (excuteResult > 0)
                        {
                            result = true;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("UpdateNewFile : " + ex.Message);
            }

            return result;
        }
    }
}