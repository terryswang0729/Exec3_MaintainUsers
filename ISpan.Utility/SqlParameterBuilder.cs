﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ISpan.Utility
{
	public class SqlParameterBuilder
	{
		private List<SqlParameter> parameters = new List<SqlParameter>();
		public SqlParameterBuilder AddNVarchar(string name, int length, string value)
		{
			var param = new SqlParameter(name, SqlDbType.NVarChar, length) { Value = value };

			parameters.Add(param);
			return this;
		}
		public SqlParameterBuilder AddInt(string name, int value)
		{
			var param = new SqlParameter(name, SqlDbType.Int) { Value = value };
			parameters.Add((param));
			return this;
		}
		public SqlParameterBuilder AddDateTime(string name, DateTime value)
		{
			var param = new SqlParameter(name, SqlDbType.DateTime) { Value = value };
			parameters.Add((param));
			return this;
		}
		public SqlParameter[] Build()
		{
			return parameters.ToArray();
		}
	}
	
	
}
