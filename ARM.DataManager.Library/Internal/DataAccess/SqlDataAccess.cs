﻿using Dapper;
using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARM.DataManager.Library
{
    public class SqlDataAccess : IDisposable
    {
        private IDbConnection dbConnection;

        private IDbTransaction dbTransaction;

        private bool isClosed = false;

        public string GetConnectionString(string name)
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }

        public List<T> LoadData<T, U>(string storedProcedure, U parameters, string connectionStringName)
        {
            string connectionString = GetConnectionString(connectionStringName);

            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                List<T> rows = connection.Query<T>(storedProcedure, parameters, commandType: CommandType.StoredProcedure).ToList();

                return rows;
            }
        }

        public void SaveData<T>(string storedProcedure, T parameters, string connectionStringName)
        {
            string connectionString = GetConnectionString(connectionStringName);

            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                connection.Execute(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
            }
        }

        public List<T> SaveAndLoadData<T, U>(string storedProcedure, U parameters, string connectionStringName)
        {
            string connectionString = GetConnectionString(connectionStringName);

            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                List<T> rows = connection.Query<T>(storedProcedure, parameters, commandType: CommandType.StoredProcedure).ToList();

                return rows;
            }
        }

        public void StartTransaction(string connectionStringName)
        {
            string connectionString = GetConnectionString(connectionStringName);

            dbConnection = new SqlConnection(connectionString);

            dbConnection.Open();

            dbTransaction = dbConnection.BeginTransaction();

            isClosed = false;
        }

        public void SaveDataInTransaction<T>(string storedProcedure, T parameters)
        {
            dbConnection.Execute(storedProcedure, parameters, commandType: CommandType.StoredProcedure, transaction: dbTransaction);
        }

        public List<T> LoadDataInTransaction<T, U>(string storedProcedure, U parameters)
        {
            List<T> rows = dbConnection.Query<T>(storedProcedure, parameters, 
                commandType: CommandType.StoredProcedure, transaction: dbTransaction).ToList();

            return rows;
        }

        public List<T> SaveAndLoadDataInTransaction<T, U>(string storedProcedure, U parameters)
        {
            List<T> rows = dbConnection.Query<T>(storedProcedure, parameters,
                commandType: CommandType.StoredProcedure, transaction: dbTransaction).ToList();

            return rows;
        }

        public void CommitTransaction()
        {
            dbTransaction?.Commit();
            dbConnection?.Close();

            isClosed = true;
        }

        public void RollbackTransaction()
        {
            dbTransaction?.Rollback();
            dbConnection?.Close();

            isClosed = true;
        }

        public void Dispose()
        {
            if (isClosed == false)
            {
                try
                {
                    CommitTransaction();
                }
                catch
                {
                    Debug.WriteLine("Fail");
                }
            }

            dbTransaction = null;
            dbConnection = null;
        }
    }
}
