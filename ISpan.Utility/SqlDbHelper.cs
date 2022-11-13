using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISpan.Utility
{
	public class SqlDbHelper
	{
		private string connString;
		public SqlDbHelper(string keyOfConnString)
		{
			connString = System.Configuration.ConfigurationManager.ConnectionStrings[keyOfConnString].ConnectionString; 
		}
		public void ExecuteNonQuery(string sql, SqlParameter[] parameters)
		{
			using (var conn = new SqlConnection(connString))
			{
				SqlCommand command = new SqlCommand(sql, conn);
				conn.Open();

				command.Parameters.AddRange(parameters);
				command.ExecuteNonQuery();
			}
		}
	
	}
}
