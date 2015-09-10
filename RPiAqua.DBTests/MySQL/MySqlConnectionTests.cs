using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPiAqua.DB.MySQL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace RPiAqua.DB.MySQL.Tests
{
	[TestClass()]
	public class MySqlConnectionTests
	{
		public const string Host = "localhost";
		public const string Dbname = "RPiAqua";
		public const string Username = "root";
		public const string Passwd = "Runr4646";

		[TestMethod]
		public void MysqlConnectionHostTest()
		{
			MySqlConnector mySql = new MySqlConnector(Host);
			Assert.AreEqual(mySql.Host, Host);
		}

		[TestMethod]
		public void MysqlConnectionHostDbNameTest()
		{
			MySqlConnector mySql = new MySqlConnector(Host, Dbname);
			Assert.AreEqual(mySql.Host, Host);
			Assert.AreEqual(mySql.DbName, Dbname);
		}

		[TestMethod]
		public void MysqlConnectionHostDbNameUserTest()
		{
			MySqlConnector mySql = new MySqlConnector(Host, Dbname,Username);
			Assert.AreEqual(mySql.Host, Host);
			Assert.AreEqual(mySql.DbName, Dbname);
			Assert.AreEqual(mySql.Username, Username);
		}

		[TestMethod]
		public void MysqlConnectionHostDbNameUserPwTest()
		{
			MySqlConnector mySql = new MySqlConnector(Host, Dbname, Username, Passwd);
			Assert.AreEqual(mySql.Host, Host);
			Assert.AreEqual(mySql.DbName, Dbname);
			Assert.AreEqual(mySql.Username, Username);
			Assert.AreEqual(mySql.Password, Passwd);
		}

		[TestMethod]
		public void MySqlConnectionInitSuccessTest()
		{
			MySqlConnector mySql = new MySqlConnector(Host, Dbname, Username, Passwd);
			Assert.IsTrue(mySql.Initialize());
		}

		[TestMethod]
		public void MySqlConnectionInitMissingPasswdTest()
		{
			MySqlConnector mySql = new MySqlConnector(Host, Dbname, Username);
			Assert.IsFalse(mySql.Initialize());
		}

		[TestMethod]
		public void MySqlConnectionInitMissingPasswdUserTest()
		{
			MySqlConnector mySql = new MySqlConnector(Host, Dbname);
			Assert.IsFalse(mySql.Initialize());
		}

		[TestMethod]
		public void MySqlConnectionInitMissingPasswdUserDbNameTest()
		{
			MySqlConnector mySql = new MySqlConnector(Host);
			Assert.IsFalse(mySql.Initialize());
		}

		[TestMethod]
		public void MySqlConnectionConnectSuccess()
		{
			MySqlConnector mySql = new MySqlConnector(Host, Dbname, Username, Passwd);
			mySql.Initialize();

			Assert.IsTrue(mySql.Connect());
		}

		[TestMethod]
		public void MySqlConnectionConnectWrongPw()
		{
			MySqlConnector mySql = new MySqlConnector(Host, Dbname, Username, "test");
			mySql.Initialize();

			Assert.IsFalse(mySql.Connect());
		}

		[TestMethod]
		public void MySqlConnectionConnectWrongUser()
		{
			MySqlConnector mySql = new MySqlConnector(Host, Dbname, "Testuser", Passwd);
			mySql.Initialize();

			Assert.IsFalse(mySql.Connect());
		}

		[TestMethod]
		public void MySqlConnectionConnectWrongDbName()
		{
			MySqlConnector mySql = new MySqlConnector(Host, "Foobar", Username, Passwd);
			mySql.Initialize();

			Assert.IsFalse(mySql.Connect());
		}

		[TestMethod]
		public void MySqlConnectionConnectWrongHost()
		{
			MySqlConnector mySql = new MySqlConnector("local", Dbname, Username, Passwd);
			mySql.Initialize();

			Assert.IsFalse(mySql.Connect());
		}

		[TestMethod]
		public void MySqlConnectionDisconnect()
		{
			MySqlConnector mySql = new MySqlConnector(Host, Dbname, Username, Passwd);
			mySql.Initialize();

			mySql.Connect();

			Assert.IsTrue(mySql.Disconnect());
		}

		[TestMethod]
		public void MySqlConnectionDisconnectWithoutConnect()
		{
			MySqlConnector mySql = new MySqlConnector(Host, Dbname, Username, Passwd);
			mySql.Initialize();

			Assert.IsTrue(mySql.Disconnect());
		}
	}
}
