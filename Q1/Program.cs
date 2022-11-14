using ISpan.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Q1
{
	internal class Program
	{
		static void Main(string[] args)
		{
			try
			{
				Program program = new Program();
				program.Insert("王士允", "Terry", "Wang", new DateTime(1996, 07, 29), 172);
				program.Update("士允", "Terry", "Wang", new DateTime(1996, 07, 29), 170, 2);
				program.Delete(4);
				program.Select(3);
			}
			catch(Exception ex)
			{
				Console.WriteLine($"連線失敗，原因:{ex.Message}");
			}
			
			
		}

		
		public void Insert(string inputName,string inputAccount,string inputPassword,DateTime inputDateOfBirth,int inputHeight)
		{
			string sql = @"insert into Users(Name,Account,Password,DateOfBirth,Height)
                          values(@Name,@Account,@Password,@DateOfBirth,@Height)";
			var dpHelper = new SqlDbHelper("default");
			var parameters = new SqlParameterBuilder()
							   .AddNVarchar("@Name", 50, inputName)
							   .AddNVarchar("@Account", 50, inputAccount)
							   .AddNVarchar("@Password", 50, inputPassword)
							   .AddDateTime("@DateOfBirth", inputDateOfBirth)
							   .AddInt("@Height", inputHeight)
							   .Build();
			dpHelper.ExecuteNonQuery(sql, parameters);
			Console.WriteLine("紀錄已新增");
			
		}
		public void Update(string inputName,string inputAccount,string inputPassword,DateTime inputDateOfBirth,int inputHeight,int inputId)
		{
			string sql = @"update users
                           set name=@Name,Account=@Account,Password=@Password,DateOfBirth=@DateOfBirth,Height=@Height
                           where Id=@Id";
			var dpHelper = new SqlDbHelper("default");
			var parameters = new SqlParameterBuilder()
							   .AddNVarchar("@Name", 50, inputName)
							   .AddNVarchar("@Account", 50, inputAccount)
							   .AddNVarchar("@Password", 50, inputPassword)
							   .AddDateTime("@DateOfBirth", inputDateOfBirth)
							   .AddInt("@Height", inputHeight)
							   .AddInt("@Id",inputId)
							   .Build();
			dpHelper.ExecuteNonQuery(sql, parameters);
			Console.WriteLine("紀錄已更新");
			
			
		}
		public void Delete(int inputId)
		{
			string sql = @"DELETE FROM Users
                           WHERE 
                           Id=@Id";
			var dbHelper = new SqlDbHelper("default");
			var parameters = new SqlParameterBuilder()
                               .AddInt("@Id", inputId)
					           .Build();

			dbHelper.ExecuteNonQuery(sql, parameters);
			Console.WriteLine("記錄刪除");
		}
		public void Select(int inputId)
		{   
			
			string connString = System.Configuration.ConfigurationManager.ConnectionStrings["default"].ConnectionString;
			string sql = "SELECT Id,Name from Users WHERE Id>@Id ORDER BY Id DESC";
			using (var conn = new SqlConnection(connString))
			{
				var command = new SqlCommand(sql, conn);
				var parameters = new SqlParameter[] { new SqlParameter("@Id", SqlDbType.Int) { Value = inputId } };
				command.Parameters.AddRange(parameters);
				SqlDataAdapter adapter = new SqlDataAdapter(command);

				DataSet dataset = new DataSet();
				adapter.Fill(dataset, "users");

				DataTable users = dataset.Tables["users"];
				foreach (DataRow row in users.Rows)
				{
					int id = row.Field<int>("Id");
					string Name = row.Field<string>("Name");
					Console.WriteLine($"Id={id},Name={Name}");
				}
			}
			
		}

    }	
}
