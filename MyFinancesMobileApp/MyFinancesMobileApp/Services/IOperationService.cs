using MyFinancesMobileApp.Models.Dtos;
using MyFinancesMobileApp.Models.Response;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyFinancesMobileApp.Services
{
    public interface IOperationService
    {
        Task<DataResponse<int>> AddAsync(OperationDto operation);
        Task<Response> UpdateItemAsync(OperationDto operation);
        Task<Response> DeleteItemAsync(int id);
        Task<DataResponse<OperationDto>> GetAsync(int id);
        Task<DataResponse<IEnumerable<OperationDto>>> GetAsync();
    }
}
