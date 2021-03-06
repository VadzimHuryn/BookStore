﻿using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace BookStore.Repositories
{
    public abstract class BaseRepository
    {
        private string _connectionString;

        public BaseRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<T> Query<T>(string sql)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var result = connection.Query<T>(sql).ToList();
                    connection.Close();
                    return result;
                }
            }
            catch(Exception e)
            {
                return null;
            }
        }

        public int QuerySingle<T>(string sql, T model)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var result = connection.QuerySingle<int>(sql, model);
                    return result;
                }
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        public T First<T>(string sql)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var result = connection.Query<T>(sql).ToList().FirstOrDefault();
                return result;
            }
        }

        public T GetById<T>(string sql, int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var result = connection.QueryFirst<T>(sql, new { Id = id });
                return result;
            }
        }

        public int Execute<T>(string sql, T model)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.Execute(sql, model);
            }
        }
    }
}
