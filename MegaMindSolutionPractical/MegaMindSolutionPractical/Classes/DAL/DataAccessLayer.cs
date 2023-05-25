using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MegaMindSolutionPractical.Classes.DAL
{
    
    public class DataAccessLayer
    {
        DataSet DS;
        string ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        public void InsertUpdateUserData(string Query,string Id=null,string Name=null,string Phone=null,string Email = null,string StateID = null,string CityID=null,string Address = null)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("SP_UserRegistration", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Query", Query);
                        cmd.Parameters.AddWithValue("@Id", Id);
                        cmd.Parameters.AddWithValue("@Name", Name);
                        cmd.Parameters.AddWithValue("@Phone", Phone);
                        cmd.Parameters.AddWithValue("@Email", Email);
                        cmd.Parameters.AddWithValue("@StateID", StateID);
                        cmd.Parameters.AddWithValue("@CityID", CityID);
                        cmd.Parameters.AddWithValue("@Address", Address);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        
                    }
                }
            }
            catch (Exception ex)
            {
                
            }
        }
        public DataSet GetUserData(string Query, string Id = null,string StateID = null,string CityID = null)
        {
            DS = new DataSet();
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("SP_UserRegistration", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Query", Query);
                        cmd.Parameters.AddWithValue("@Id", Id);
                        cmd.Parameters.AddWithValue("@StateID", StateID);
                        cmd.Parameters.AddWithValue("@CityID", CityID);
                        con.Open();
                        SqlDataAdapter da = new SqlDataAdapter();
                        da.SelectCommand = cmd;
                        da.Fill(DS);
                        con.Close();
                        if (DS != null && DS.Tables.Count > 0 && DS.Tables[0].Rows.Count > 0)
                        {
                            return DS;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
               
            }
            return DS;
        }

        public void DeleteUserData(string Query, string Id = null)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("SP_UserRegistration", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Query", Query);
                        cmd.Parameters.AddWithValue("@Id", Id);
                        con.Open();
                        cmd.ExecuteNonQuery();

                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        public DataSet GetStateData(string Query, string StateID = null)
        {
            DS = new DataSet();
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("SP_State", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Query", Query);
                        cmd.Parameters.AddWithValue("@StateID", StateID);
                        con.Open();
                        SqlDataAdapter da = new SqlDataAdapter();
                        da.SelectCommand = cmd;
                        da.Fill(DS);
                        con.Close();
                        if (DS != null && DS.Tables.Count > 0 && DS.Tables[0].Rows.Count > 0)
                        {
                            return DS;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // ClsCommon.WriteExceptionLog(ex);
            }
            return DS;
        }

        public DataSet GetCityData(string Query,string CityID = null, string StateID = null)
        {
            DS = new DataSet();
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("SP_City", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Query", Query);
                        cmd.Parameters.AddWithValue("@CityID", CityID);
                        cmd.Parameters.AddWithValue("@StateID", StateID);
                        con.Open();
                        SqlDataAdapter da = new SqlDataAdapter();
                        da.SelectCommand = cmd;
                        da.Fill(DS);
                        con.Close();
                        if (DS != null && DS.Tables.Count > 0 && DS.Tables[0].Rows.Count > 0)
                        {
                            return DS;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // ClsCommon.WriteExceptionLog(ex);
            }
            return DS;
        }
    }
}