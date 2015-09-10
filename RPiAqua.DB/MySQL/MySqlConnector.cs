using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;

namespace RPiAqua.DB.MySQL
{
	public class MySqlConnector
	{
		private MySqlConnection _connection;

		/// <summary>
		/// Hoster of the URL
		/// </summary>
		public string Host { get; set; }
		/// <summary>
		/// Name of Database
		/// </summary>
		public string DbName { get; set; }
		/// <summary>
		/// Username to Connect
		/// </summary>
		public string Username { get; set; }
		/// <summary>
		/// Password to Connect
		/// </summary>
		public string Password { get; set; }

		public MySqlConnector(string host)
		{
			Host = host;
		}

		public MySqlConnector(string host, string dbName)
			: this(host)
		{
			DbName = dbName;
		}

		public MySqlConnector(string host, string dbName, string userName)
			: this(host, dbName)
		{
			Username = userName;
		}

		public MySqlConnector(string host, string dbName, string userName, string passwd) :
			this(host, dbName, userName)
		{
			Password = passwd;
		}

		/// <summary>
		/// <para>Init the Connection to the Database</para>
		/// <remarks>All Connection Settings has to be set</remarks>
		/// </summary>
		/// <returns>(bool) true when success</returns>
		public bool Initialize()
		{
			bool bRet = false;

			if (!string.IsNullOrEmpty(Host) && !string.IsNullOrEmpty(DbName) && !string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password))
			{

				string connectionString = "SERVER=" + Host + ";" + "DATABASE=" + DbName + ";" + "UID=" + Username + ";" + "PASSWORD=" + Password + ";";

				_connection = new MySqlConnection(connectionString);

				if (_connection != null)
					bRet = true;
			}

			return bRet;
		}

		/// <summary>
		/// Open the Database Connection
		/// </summary>
		/// <returns>(true) If success</returns>
		public bool Connect()
		{
			bool bRet = false;

			try
			{
				if (_connection.State == ConnectionState.Closed)
					_connection.Open();

				if (_connection.State == ConnectionState.Open)
					bRet = true;
			}
			catch (MySqlException ex)
			{
				bRet = false;
			}

			return bRet;
		}

		/// <summary>
		/// Close the Database Connection
		/// </summary>
		/// <returns>(true) If success</returns>
		public bool Disconnect()
		{
			bool bRet = false;

			if (_connection.State == ConnectionState.Open)
				_connection.Close();

			if (_connection.State == ConnectionState.Closed)
				bRet = true;

			return bRet;
		}
	}
}
