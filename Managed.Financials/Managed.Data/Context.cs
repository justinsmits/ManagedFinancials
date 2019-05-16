using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;

namespace Managed.Data
{
	public class Context
	{

		private SqlConnection _connection;
		private SqlTransaction _transaction;
		private SqlCommand _currentCommand;

		#region " Constructors "

		public Context()
		{
			_connection = new SqlConnection(
                "data source=208.78.30.73,14333;Database=SQL2008DataService;Uid=DAL;Password=06Smarog!;");
		}

		#endregion

		#region " Connection "

		public string Database {
			get { return _connection.Database; }
		}

		public SqlConnection GetConnection()
		{
			return this.GetConnection(true);
		}

		public SqlConnection GetConnection(bool openConnectionIfClosed)
		{
			if (openConnectionIfClosed && _connection.State == System.Data.ConnectionState.Closed) {
				_connection.Open();
			}
			return _connection;
		}

		public SqlTransaction GetTransaction()
		{
			return _transaction;
		}

		public void BeginTransaction()
		{
			_connection.Open();
			_transaction = _connection.BeginTransaction();
		}

		public void CommitTransaction()
		{
			_transaction.Commit();
			_transaction = null;
			_connection.Close();
		}

		public void RollbackTransaction()
		{
			_transaction.Rollback();
			_transaction = null;
			_connection.Close();
		}

		public void ReleaseConnection()
		{
			if (_transaction == null) {
				_connection.Close();
			}
			_currentCommand = null;
		}

		public void Cancel()
		{
			if ((_currentCommand != null)) {
				try {
					_currentCommand.Cancel();
					_currentCommand = null;
				} catch {
				}
			}
		}

		#endregion

		#region " Execution "

		public Int32 ExecuteNonQuerySQLStatement(string sqlStatement, Int32 timeoutValue = -1)
		{
			Int32 retValue = default(Int32);
			SqlCommand command = null;
			try {
				command = this.GetConnection().CreateCommand();
				_currentCommand = command;
				command.Transaction = this.GetTransaction();
				command.CommandText = sqlStatement;
				if (timeoutValue > 0) {
					command.CommandTimeout = timeoutValue;
				} else {
					command.CommandTimeout = Managed.Data.Config.CommandTimeout;
				}
				try {
					retValue = command.ExecuteNonQuery();
				} catch (System.Data.SqlClient.SqlException) {
					throw;
				}
			} catch (Exception) {
				throw;
			} finally {
				this.ReleaseConnection();
			}
			return retValue;
		}

		public Int32 ExecuteParameterizedNonQuerySQLStatement(string sqlStatement, System.Collections.IEnumerable parameters, Int32 timeoutValue = -1)
		{
			Int32 retValue = default(Int32);
			SqlCommand command = null;

			try {
				command = this.GetConnection().CreateCommand();
				command.Transaction = this.GetTransaction();
				_currentCommand = command;
				if ((parameters != null)) {
					foreach (SqlParameter parameter in parameters) {
						command.Parameters.Add(parameter);
					}
				}
				command.CommandText = sqlStatement;
				if (timeoutValue > 0) {
					command.CommandTimeout = timeoutValue;
				} else {
					command.CommandTimeout = Managed.Data.Config.CommandTimeout;
				}
				retValue = command.ExecuteNonQuery();

			} catch (Exception) {
				throw;
			} finally {
				this.ReleaseConnection();
			}
			return retValue;
		}

		public System.Data.DataTable ExecuteSQLStatementAsDataTable(string sqlStatement, Int32 timeoutValue = -1)
		{
			SqlCommand command = null;
			System.Data.SqlClient.SqlDataAdapter dataAdapter = null;
			System.Data.DataTable dataTable = null;

			try {
				command = this.GetConnection().CreateCommand();
				command.Transaction = this.GetTransaction();
				_currentCommand = command;
				command.CommandText = sqlStatement;
				if (timeoutValue > 0) {
					command.CommandTimeout = timeoutValue;
				} else {
					command.CommandTimeout = Managed.Data.Config.CommandTimeout;
				}
				dataAdapter = new System.Data.SqlClient.SqlDataAdapter(command);
				dataTable = new System.Data.DataTable();
				dataAdapter.Fill(dataTable);
			} catch (Exception) {
				throw;
			} finally {
				this.ReleaseConnection();
			}

			return dataTable;
		}

		public System.Data.DataTable ExecuteParameterizedSqlStatementAsDataTable(string sqlStatement, System.Data.SqlClient.SqlParameter parameter, Int32 timeoutValue = -1)
		{
			return ExecuteParameterizedSqlStatementAsDataTable(sqlStatement, new System.Data.SqlClient.SqlParameter[] { parameter }, timeoutValue);
		}

		//TODO: Use System.Collections.Generic.IEnumerable(Of SqlParameter)
		public System.Data.DataTable ExecuteParameterizedSqlStatementAsDataTable(string sqlStatement, System.Collections.IEnumerable parameters, Int32 timeoutValue = -1)
		{
			SqlCommand command = null;
			System.Data.SqlClient.SqlDataAdapter dataAdapter = null;
			System.Data.DataTable dataTable = null;
			if (parameters == null)
				parameters = new SqlParameter[0];

			try {
				command = this.GetConnection().CreateCommand();
				command.Transaction = this.GetTransaction();
				command.CommandText = sqlStatement;
				_currentCommand = command;
				if (timeoutValue > 0) {
					command.CommandTimeout = timeoutValue;
				} else {
					command.CommandTimeout = Managed.Data.Config.CommandTimeout;
				}
				foreach (SqlParameter parameter in parameters) {
					command.Parameters.Add(parameter);
				}
				dataAdapter = new System.Data.SqlClient.SqlDataAdapter(command);
				dataTable = new System.Data.DataTable();
				dataAdapter.Fill(dataTable);
			} catch (Exception) {
				throw;
			} finally {
				this.ReleaseConnection();
			}

			return dataTable;
		}

		public T ExecuteSqlStatementAsScalar<T>(string sqlStatement)
		{
			return ExecuteSqlStatementAsScalar<T>(sqlStatement, null, -1);
		}

		public T ExecuteSqlStatementAsScalar<T>(string sqlStatement, System.Collections.Generic.IEnumerable<SqlParameter> parameters)
		{
			return ExecuteSqlStatementAsScalar<T>(sqlStatement, parameters, -1);
		}

		public T ExecuteSqlStatementAsScalar<T>(string sqlStatement, Int32 timeoutValue)
		{
			return ExecuteSqlStatementAsScalar<T>(sqlStatement, null, timeoutValue);
		}

		public T ExecuteSqlStatementAsScalar<T>(string sqlStatement, System.Collections.Generic.IEnumerable<SqlParameter> parameters, Int32 timeoutValue)
		{
			return (T)this.ExecuteSqlStatementAsScalar<T>(sqlStatement, parameters, timeoutValue);
		}

		public T ExecuteSqlStatementAsScalar<T>(string sqlStatement, params SqlParameter[] parameterCollection)
		{
			return (T)this.ExecuteSqlStatementAsScalar(sqlStatement, parameterCollection);
		}

		public object ExecuteSqlStatementAsScalar(string sqlStatement, params SqlParameter[] parameterCollection)
		{
			return this.ExecuteSqlStatementAsScalar(sqlStatement, parameterCollection, -1);
		}

		public object ExecuteSqlStatementAsScalar(string sqlStatement, System.Collections.IEnumerable parameters, Int32 timeoutValue = -1)
		{
			SqlCommand command = null;
			object retval = null;
			if (parameters == null)
				parameters = new SqlParameter[0];

			try {
				command = this.GetConnection().CreateCommand();
				command.Transaction = this.GetTransaction();
				command.CommandText = sqlStatement;
				_currentCommand = command;
				if (timeoutValue > 0) {
					command.CommandTimeout = timeoutValue;
				} else {
					command.CommandTimeout = Managed.Data.Config.CommandTimeout;
				}
				foreach (SqlParameter param in parameters) {
					command.Parameters.Add(param);
				}
				retval = command.ExecuteScalar();
			} catch (Exception) {
				throw;
			} finally {
				this.ReleaseConnection();
			}

			return retval;
		}

		public System.Data.SqlClient.SqlDataReader ExecuteSQLStatementAsReader(string sqlStatement, Int32 timeoutValue = -1)
		{
			SqlCommand command = null;
			System.Data.SqlClient.SqlDataReader retValue = null;

			try {
				command = this.GetConnection().CreateCommand();
				command.Transaction = this.GetTransaction();
				command.CommandText = sqlStatement;
				_currentCommand = command;
				if (timeoutValue > 0) {
					command.CommandTimeout = timeoutValue;
				} else {
					command.CommandTimeout = Managed.Data.Config.CommandTimeout;
				}
				retValue = command.ExecuteReader();
			} catch (Exception) {
				throw;
			} finally {
				_currentCommand = null;
			}

			return retValue;
		}

		public System.Data.SqlClient.SqlDataReader ExecuteProcedureAsReader(string procedureName, System.Collections.Generic.IEnumerable<SqlParameter> parameters, Int32 timeoutValue = -1)
		{
			SqlCommand command = null;
			System.Data.SqlClient.SqlDataReader retValue = null;

			try {
				command = this.GetConnection().CreateCommand();
				command.Transaction = this.GetTransaction();
				command.CommandText = procedureName;
				command.CommandType = System.Data.CommandType.StoredProcedure;

				foreach (System.Data.SqlClient.SqlParameter item in parameters) {
					command.Parameters.Add(item);
				}

				_currentCommand = command;
				if (timeoutValue > 0) {
					command.CommandTimeout = timeoutValue;
				} else {
					command.CommandTimeout = Managed.Data.Config.CommandTimeout;
				}

				retValue = command.ExecuteReader();
			} catch (Exception) {
				throw;
			} finally {
				_currentCommand = null;
			}

			return retValue;
		}

		public Int32 ExecuteProcedureNonQuery(string procedureName, System.Collections.Generic.IEnumerable<SqlParameter> parameters, Int32 timeoutValue = -1)
		{
			SqlCommand command = null;
			Int32 retVal = default(Int32);

			try {
				command = this.GetConnection().CreateCommand();
				command.Transaction = this.GetTransaction();
				command.CommandText = procedureName;
				command.CommandType = System.Data.CommandType.StoredProcedure;

				foreach (System.Data.SqlClient.SqlParameter item in parameters) {
					command.Parameters.Add(item);
				}

				_currentCommand = command;
				if (timeoutValue > 0) {
					command.CommandTimeout = timeoutValue;
				} else {
					command.CommandTimeout = Managed.Data.Config.CommandTimeout;
				}

				retVal = command.ExecuteNonQuery();
			} catch (Exception) {
				throw;
			} finally {
				_currentCommand = null;
			}

			return retVal;
		}

		public System.Data.SqlClient.SqlDataReader ExecuteParameterizedSQLStatementAsReader(string sqlStatement, System.Collections.IEnumerable parameters, Int32 timeoutValue = -1)
		{
			SqlCommand command = null;
			System.Data.SqlClient.SqlDataReader retValue = null;

			try {
				command = this.GetConnection().CreateCommand();
				command.Transaction = this.GetTransaction();
				command.CommandText = sqlStatement;
				_currentCommand = command;
				if (timeoutValue > 0) {
					command.CommandTimeout = timeoutValue;
				} else {
					command.CommandTimeout = Managed.Data.Config.CommandTimeout;
				}
				foreach (SqlParameter parameter in parameters) {
					command.Parameters.Add(parameter);
				}
				retValue = command.ExecuteReader();
			} catch (Exception) {
				throw;
			} finally {
				_currentCommand = null;
			}

			return retValue;
		}

		#region "ExecuteSqlStatementAsDataSet"

		public System.Data.DataSet ExecuteSqlStatementAsDataSet(string statement)
		{
			return ExecuteSqlStatementAsDataSet(statement, null, -1);
		}

		public System.Data.DataSet ExecuteSqlStatementAsDataSet(string statement, System.Collections.Generic.IEnumerable<SqlParameter> parameters)
		{
			return ExecuteSqlStatementAsDataSet(statement, parameters, -1);
		}

		public System.Data.DataSet ExecuteSqlStatementAsDataSet(string statement, Int32 timeoutValue)
		{
			return ExecuteSqlStatementAsDataSet(statement, null, timeoutValue);
		}

		public System.Data.DataSet ExecuteSqlStatementAsDataSet(string sqlStatement, System.Collections.Generic.IEnumerable<SqlParameter> parameters, Int32 timeoutValue)
		{
			System.Data.SqlClient.SqlCommand command = null;
			System.Data.SqlClient.SqlDataAdapter dataAdapter = null;
			System.Data.DataSet dataSet = null;
			if (parameters == null)
				parameters = new SqlParameter[0];

			try {
				command = this.GetConnection().CreateCommand();
				command.Transaction = this.GetTransaction();
				command.CommandText = sqlStatement;
				_currentCommand = command;
				if (timeoutValue > 0) {
					command.CommandTimeout = timeoutValue;
				} else {
					command.CommandTimeout = Managed.Data.Config.CommandTimeout;
				}
				foreach (SqlParameter param in parameters) {
					command.Parameters.Add(param);
				}
				dataAdapter = new System.Data.SqlClient.SqlDataAdapter(command);
				dataSet = new System.Data.DataSet();
				dataAdapter.Fill(dataSet);
			} catch (Exception) {
				throw;
			} finally {
				this.ReleaseConnection();
			}
			return dataSet;
		}

		#endregion

		#endregion

	}
}
