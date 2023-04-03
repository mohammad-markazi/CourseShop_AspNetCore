using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnHub.Application.Common.Interfaces
{
    public interface IDapperService
    {
        Task<IList<T>> GetAllAsync<T>(string query, CommandType type = CommandType.Text, object? parameter = null) where T : class;

        Task<IList<FirstType>> GetAllAsync<FirstType,SecodType>(string query, Func<FirstType, SecodType, FirstType> func, CommandType type = CommandType.Text, string SplitOn = "Id", object? parameter = null) where FirstType : class;
        Task<IList<FirstType>> GetAllAsync<FirstType, SecodType, ThirdType>(string query, Func<FirstType, SecodType, ThirdType, FirstType> func, CommandType type = CommandType.Text, string SplitOn = "Id", object? parameter = null) where FirstType : class;
        Task<IList<FirstType>> GetAllAsync<FirstType, SecodType, ThirdType, FifthType>(string query, Func<FirstType, SecodType, ThirdType, FifthType, FirstType> func, CommandType type = CommandType.Text, string SplitOn = "Id", object? parameter = null) where FirstType : class;

        Task<T> GetAsync<T>(string query) where T :class;


    }
}
