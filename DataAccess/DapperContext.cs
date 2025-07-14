using System;
using Dapper;
using Microsoft.Data.SqlClient;

namespace Rota.DataAccess
{
	public class DapperContext
	{
		public static string connectionString = "Server=localhost,1433;Database=rotadb;User Id=SA;Password=reallyStrongPwd123;TrustServerCertificate=True;";

		//ekleme, silme, güncelleme
		public static void ExecuteReturn(string procAdi, DynamicParameters param = null)
		{
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				connection.Open();
				connection.Execute(procAdi, param, commandType: System.Data.CommandType.StoredProcedure);
			}
		}









	}
}

