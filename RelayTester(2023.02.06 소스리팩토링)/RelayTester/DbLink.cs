using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace RelayTester
{
    public class DbLink
    {
        //string servertype = "relaytester";
        string servertype = "relaytester_test_nb";
        public DbLink() { }


        public DataSet AllSelect(string query, DataSet ds)
        {
            //조회 요청
            try
            {
                SqlConnection sqlConnection = new SqlConnection("server =" + Global.globalInOut + ",14233; uid =daeaticost; pwd =daeati1234; database =" + servertype);
                //DB 오픈
                if (sqlConnection != null && sqlConnection.State == ConnectionState.Closed)
                {
                    sqlConnection.Open();
                }
                SqlDataAdapter Ldap = new SqlDataAdapter(query, sqlConnection);
                Ldap.SelectCommand.CommandTimeout = 1800;
                //조회결과 저장
                Ldap.Fill(ds);
                sqlConnection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("조회 도중 에러 발생 : " + ex.Message);
            }
            return ds;
        }
        //공통 Update 로직

        public int ModifyMethod(string query)
        {
            int returnInt = 0;
            SqlCommand sqlComm = new SqlCommand();

            //DB 오픈
            SqlConnection sqlConnection = new SqlConnection("server =" + Global.globalInOut + ",14233; uid =daeaticost; pwd =daeati1234; database =" + servertype);
            sqlComm.Connection = sqlConnection;
            sqlComm.CommandText = query;
            sqlConnection.Open();
            SqlTransaction tran = sqlConnection.BeginTransaction(); //트랜젝션 시작

            try
            {
                sqlComm.Transaction = tran;
                returnInt = sqlComm.ExecuteNonQuery();

                tran.Commit();
                sqlConnection.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine("업데이트 도중 에러 발생 : " + ex.Message);
                tran.Rollback();
            }
            return returnInt;
        }

        public int DBupdate(string query)
        {
            int returnInt = 0;
            SqlCommand sqlComm = new SqlCommand();

            //DB 오픈
            SqlConnection sqlConnection = new SqlConnection("server =" + Global.globalInOut + ",14233; uid =daeaticost; pwd =daeati1234; database =" + servertype);
            sqlComm.Connection = sqlConnection;
            sqlComm.CommandText = query;
            sqlConnection.Open();
            SqlTransaction tran = sqlConnection.BeginTransaction(); //트랜젝션 시작

            try
            {
                sqlComm.Transaction = tran;
                returnInt = sqlComm.ExecuteNonQuery();

                tran.Commit();
                sqlConnection.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine("업데이트 도중 에러 발생 : " + ex.Message);
                tran.Rollback();
            }
            return returnInt;

        }

        public int ImageModifyMethod(SqlCommand sqlComm)
        {
            int returnInt = 0;

            //DB 오픈
            SqlConnection sqlConnection = new SqlConnection("server =" + Global.globalInOut + ",14233; uid =daeaticost; pwd =daeati1234; database =" + servertype);
            sqlComm.Connection = sqlConnection;
            sqlConnection.Open();
            SqlTransaction tran = sqlConnection.BeginTransaction(); //트랜젝션 시작

            try
            {
                sqlComm.Transaction = tran;
                returnInt = sqlComm.ExecuteNonQuery();

                tran.Commit();
                sqlConnection.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine("업데이트 도중 에러 발생 : " + ex.Message);
                tran.Rollback();
            }
            return returnInt;
        }
    }
}
