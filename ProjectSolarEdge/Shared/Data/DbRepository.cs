﻿using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSolarEdge.Shared.Data
{
    public class DbRepository
    {
        public IDbConnection _db;
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public DbRepository(string connectionString)
        {
            _db = new SqlConnection(connectionString);
        }

        public void OpenConnection()
        {
            _db.Open();
        }

        public void CloseConnection()
        {
            _db.Close();
        }

        public List<T> GetRecords<T>(string query, object param)
        {
            try
            {
                OpenConnection();
                List<T> data =
                    _db.Query<T>(
                        query, param).ToList();
                CloseConnection();
                return data;
            }
            catch (Exception ex)
            {
                CloseConnection();
                throw;
            }
        }
    }
}
