using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Q1
{
	internal class Program
	{
		static void Main(string[] args)
		{
			string connString = System.Configuration.ConfigurationManager.ConnectionStrings["default"].ConnectionString;
			string sql = @"insert into Users(Name,Account,Password,DateOfBirth,Height)
                          values(@Name,@Account,@Password,@DateOfBirth,@Height)";

			using (var conn = new SqlConnection(connString))
			{
				try
				{
					SqlCommand command = new SqlCommand(sql,conn);
					conn.Open();
					SqlParameter NameParam = new SqlParameter("@Name", SqlDbType.NVarChar, 50)
					{ Value = "Lin" };
				}
				catch (Exception ex)
				{
					Console.WriteLine($"連線失敗，原因:{ex.Message}");
				}
			}
		}
	}
}
