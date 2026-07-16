using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess
{
    public class SQLHelper
    {
        public static string connStr = "Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=HouseRental;Data Source=.\\SQLEXPRESS";

        public static void InitializeDatabase()
        {
            string masterConnStr = connStr.Replace("Initial Catalog=HouseRental", "Initial Catalog=master");
            using (SqlConnection conn = new SqlConnection(masterConnStr))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT database_id FROM sys.databases WHERE name = 'HouseRental'";
                    object dbId = cmd.ExecuteScalar();
                    if (dbId == null || dbId == DBNull.Value)
                    {
                        cmd.CommandText = "CREATE DATABASE HouseRental";
                        cmd.ExecuteNonQuery();
                    }
                }
            }

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    // Create Users table
                    cmd.CommandText = @"
                        IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Users]') AND type in (N'U'))
                        BEGIN
                            CREATE TABLE [dbo].[Users] (
                                [UserID] INT IDENTITY(1,1) PRIMARY KEY,
                                [Username] NVARCHAR(50) NOT NULL UNIQUE,
                                [Password] NVARCHAR(50) NOT NULL,
                                [Role] NVARCHAR(50) NOT NULL,
                                [Email] NVARCHAR(100) NULL,
                                [Phone] NVARCHAR(50) NULL
                            );
                            
                            INSERT INTO [dbo].[Users] ([Username], [Password], [Role], [Email], [Phone]) 
                            VALUES ('admin', '1234', 'Admin', 'admin@rental.com', '1234567890');
                        END";
                    cmd.ExecuteNonQuery();

                    // Create HouseInfo table compatible with both schemas
                    cmd.CommandText = @"
                        IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[HouseInfo]') AND type in (N'U'))
                        BEGIN
                            CREATE TABLE [dbo].[HouseInfo] (
                                [HouseID] NVARCHAR(50) PRIMARY KEY,
                                [CategoryID] NVARCHAR(50) NULL,
                                [Address] NVARCHAR(255) NULL,
                                [HouseAddress] NVARCHAR(255) NULL,
                                [Area] NVARCHAR(100) NULL,
                                [HouseArea] NVARCHAR(100) NULL,
                                [RentPrice] NVARCHAR(100) NULL,
                                [Deposit] NVARCHAR(100) NULL,
                                [IsVacant] NVARCHAR(50) NULL,
                                [Status] NVARCHAR(50) NULL,
                                [Introduction] NVARCHAR(MAX) NULL
                            );
                        END";
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public static DataSet GetData(string sqlStr)
        {
            //1、建立数据库连接串
            //string connStr = "Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=Students;Data Source=JSZXTEACHER406-\\SQLEXPRESS";
            //2、建立数据库连接对象
            SqlConnection sqlConn = new SqlConnection(connStr);
            //3、打开连接（建立了C#程序he数据库之间的连接通道）
            sqlConn.Open();

            //4、数据查询--SqlDataAdapter（数据适配器）
            //4.1、形成SQL语句-查询全部数据
            //Console.WriteLine("a={0}",x);
            //string sqlStr = string.Format("SELECT *  FROM StudList where SName = '{0}'", this.tbSName.Text);
            //4.2、建立SqlDataAdapter对象（大卡车）
            SqlDataAdapter sqlAdpter = new SqlDataAdapter(sqlStr, sqlConn);//取哪些数据，走哪一个通道
            //4.3、取数据(表格)、卸货（ds1--表格）
            DataSet ds1 = new DataSet();
            sqlAdpter.Fill(ds1);//索引为0
            //5、关闭连接（C#程序he数据库之间的连接通道被关闭）
            sqlConn.Close();

            return ds1;
        }
        public static int ExecuteCmd(string sqlStr)
        {
            //1、建立数据库连接串
            //string connStr = "Integrated Security=SSPI;Initial Catalog=Students;Data Source=JSZXTEACHER406-\\SQLEXPRESS";
            //2、建立数据库连接对象
            SqlConnection sqlConn = new SqlConnection(connStr);
            //3、打开连接（建立了C#程序he数据库之间的连接通道）
            sqlConn.Open();

            //4、数据删除--SqlCommand
            //4.1、形成SQL语句-Delete语句        
            //string sqlStr = string.Format("delete from StudList where SNo ='{0}'", this.tbSNo.Text);
            //4.2、建立SqlCommand对象
            SqlCommand sqlCmd = new SqlCommand(sqlStr, sqlConn);//执行哪一个指令，走哪一个通道

            //4.3、执行指令--SqlCommand
            int n = sqlCmd.ExecuteNonQuery();
            //5、关闭连接（C#程序he数据库之间的连接通道被关闭）
            sqlConn.Close();

            return n;
        }
    }
}
