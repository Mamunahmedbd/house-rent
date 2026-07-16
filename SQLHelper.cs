using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess
{
    //百度： C#  数据库访问类
    //帮助我们进行数据库访问--简化
    public class SQLHelper
    {
        //1、Create Database 建立数据库连接串curity Info=Fa
        static string connStr = "Integrated Security=SSPI;Persist Selse;Initial Catalog=HouseDB;Data Source=.\\SQLEXPRESS";
        //Method to query data from HouseDB
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
