using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using LearnHub.Application.Common.Interfaces;
using LearnHub.Application.Course.Query;
using Learnhub.Domain.Entities.Course;
using Learnhub.Domain.ValueObjects;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Npgsql;
using Dapper.FluentMap;

namespace LearnHub.Infrastructure.Persistence
{
    public class DapperService:IDapperService
    {
        private readonly IConfiguration _configuration;
        private string _connectionString;
        public DapperService(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = configuration.GetConnectionString("Defaultconnection")!;
        }
        public async Task<IList<T>> GetAllAsync<T>(string query, CommandType type = CommandType.Text, object? parameter=null) where T : class
        {
            using IDbConnection connection=new NpgsqlConnection(_connectionString);
            

          var result= await connection.QueryAsync<T>(query,parameter,null,null,type);

          return result.ToList();
        }

        public async Task<IList<FirstType>> GetAllAsync<FirstType, SecodType>(string query, Func<FirstType, SecodType, FirstType> func, CommandType type = CommandType.Text,string SplitOn="Id", object? parameter = null) where FirstType : class
        {
       
            using IDbConnection connection = new SqlConnection(_connectionString);
          


            var result = await connection.QueryAsync<FirstType,SecodType,FirstType>(query,func,parameter,null,true,SplitOn,null,type);
             

            return result.ToList();

        }

        public async Task<IList<FirstType>> GetAllAsync<FirstType, SecodType, ThirdType>(string query, Func<FirstType, SecodType, ThirdType, FirstType> func, CommandType type = CommandType.Text,
            string SplitOn = "Id", object? parameter = null) where FirstType : class
        {
            using IDbConnection connection = new SqlConnection(_connectionString);



            var result = await connection.QueryAsync<FirstType, SecodType, ThirdType, FirstType>(query, func, parameter, null, true, SplitOn, null, type);


            return result.ToList();

        }

        public async  Task<IList<FirstType>> GetAllAsync<FirstType, SecodType, ThirdType, FifthType>(string query, Func<FirstType, SecodType, ThirdType, FifthType, FirstType> func,
            CommandType type = CommandType.Text, string SplitOn = "Id", object? parameter = null) where FirstType : class
        {
            using IDbConnection connection = new SqlConnection(_connectionString);



            var result = await connection.QueryAsync<FirstType, SecodType, ThirdType, FifthType, FirstType>(query, func, parameter, null, true, SplitOn, null, type);

            return result.ToList();

        }


        public async Task<T> GetAsync<T>(string query) where T : class
        {
            using IDbConnection connection = new SqlConnection(_connectionString);

            var result = await connection.QueryFirstOrDefaultAsync<T>(query);

            return result;
        }
    }
}
