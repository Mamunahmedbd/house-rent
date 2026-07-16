using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace DataAccess
{
    public class SQLHelper
    {
        public static string connStr = GetConnectionString();

        private static string GetConnectionString()
        {
            try
            {
                string configVal = ConfigurationManager.AppSettings["DB_CONNECTION_STRING"];
                if (!string.IsNullOrEmpty(configVal))
                {
                    return configVal;
                }
            }
            catch { }
            return "Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=HouseRental;Data Source=.\\SQLEXPRESS";
        }

        public static void InitializeDatabase()
        {
            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(connStr);
                string dbName = builder.InitialCatalog;
                if (string.IsNullOrEmpty(dbName))
                {
                    dbName = "HouseRental";
                }

                // Connect to master database first to check/create target database
                builder.InitialCatalog = "master";
                string masterConnStr = builder.ConnectionString;

                using (SqlConnection conn = new SqlConnection(masterConnStr))
                {
                    conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = string.Format("SELECT database_id FROM sys.databases WHERE name = '{0}'", dbName.Replace("'", "''"));
                        object dbId = cmd.ExecuteScalar();
                        if (dbId == null || dbId == DBNull.Value)
                        {
                            cmd.CommandText = string.Format("CREATE DATABASE [{0}]", dbName);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }

                // Connect to target database and initialize schema/seeds
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

                                INSERT INTO [dbo].[Users] ([Username], [Password], [Role], [Email], [Phone]) 
                                VALUES ('manager1', '1234', 'Manager', 'manager1@rental.com', '0987654321');

                                INSERT INTO [dbo].[Users] ([Username], [Password], [Role], [Email], [Phone]) 
                                VALUES ('user1', '1234', 'User', 'user1@rental.com', '1122334455');
                            END";
                        cmd.ExecuteNonQuery();

                        // Create HouseInfo table
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

                    // Create Tenants table
                    cmd.CommandText = @"
                        IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Tenants]') AND type in (N'U'))
                        BEGIN
                            CREATE TABLE [dbo].[Tenants] (
                                [TenantID] INT IDENTITY(1,1) PRIMARY KEY,
                                [FullName] NVARCHAR(100) NOT NULL,
                                [Gender] NVARCHAR(20) NULL,
                                [Phone] NVARCHAR(30) NOT NULL,
                                [Email] NVARCHAR(100) NULL,
                                [IDNumber] NVARCHAR(50) NULL,
                                [Occupation] NVARCHAR(100) NULL,
                                [Address] NVARCHAR(MAX) NULL,
                                [EmergencyContact] NVARCHAR(30) NULL,
                                [Status] NVARCHAR(30) NULL,
                                [RegisteredDate] DATETIME NULL
                            );
                        END";
                    cmd.ExecuteNonQuery();

                    // Enforce unique ID Number on Tenants (filtered to allow multiple NULLs)
                    cmd.CommandText = @"
                        IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'UX_Tenants_IDNumber' AND object_id = OBJECT_ID(N'[dbo].[Tenants]'))
                        BEGIN
                            CREATE UNIQUE INDEX UX_Tenants_IDNumber ON [dbo].[Tenants]([IDNumber]) WHERE [IDNumber] IS NOT NULL;
                        END";
                    cmd.ExecuteNonQuery();

                    // Create RentalProperties table
                    cmd.CommandText = @"
                        IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RentalProperties]') AND type in (N'U'))
                        BEGIN
                            CREATE TABLE [dbo].[RentalProperties] (
                                [RentalID] INT IDENTITY(1,1) PRIMARY KEY,
                                [Address] NVARCHAR(255) NOT NULL,
                                [RentValue] NVARCHAR(100) NOT NULL,
                                [Status] NVARCHAR(50) NOT NULL
                            );
                        END";
                    cmd.ExecuteNonQuery();
                }
            }
        }
            catch (Exception ex)
            {
                throw new Exception("Database initialization failed.\n\nConnection String: " + connStr + "\n\nError: " + ex.Message, ex);
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
