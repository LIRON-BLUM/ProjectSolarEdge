using Dapper;
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


        public bool ExecuteAll(string query, object param)
        {
            try
            {
                OpenConnection();
                int results = _db.Execute(query, param, commandType: CommandType.Text);
                CloseConnection();

                return results > 0;
            }
            catch (Exception)
            {
                CloseConnection();
                throw;
            }
        }

        public int InsertAndreturnInt(string query, object param)
        {
            try
            {
                OpenConnection();
                int results = _db.Query<int>(query, param, commandType: CommandType.Text).FirstOrDefault();
                CloseConnection();

                return results;
            }
            catch (Exception)
            {
                CloseConnection();
                throw;
            }
        }
    }
}
