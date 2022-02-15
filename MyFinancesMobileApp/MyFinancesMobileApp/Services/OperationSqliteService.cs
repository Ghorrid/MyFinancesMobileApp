using MyFinancesMobileApp.Models;
using MyFinancesMobileApp.Models.Converters;
using MyFinancesMobileApp.Models.Domains;
using MyFinancesMobileApp.Models.Dtos;
using MyFinancesMobileApp.Models.Response;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace MyFinancesMobileApp.Services
{
    public class OperationSqliteService : IOperationService
    {
        private static UnitOfWork _unitOfWork;

        public static UnitOfWork UnitOfWork
        {
            get
            {
                if (_unitOfWork == null)
                {
                    _unitOfWork = new UnitOfWork (Path.Combine
                        (Environment.GetFolderPath (Environment.SpecialFolder.LocalApplicationData),
                        "MyFinancesSqlite.db3"));
                }
                return _unitOfWork;
            }
            
        }

        public async Task<DataResponse<int>> AddAsync(OperationDto operation)
        {
            var response = new DataResponse<int> ();
            try
            {
                response.Data = await UnitOfWork.Operation.AddAsync(operation.ToDao());
            }
            catch (Exception ex)
            {
                response.Errors.Add (new Error (ex.Source, ex.Message));
            }
            return response;
        }

        public async Task<Response> DeleteItemAsync(int id)
        {
            var response = new Response();
            try
            {
                await UnitOfWork.Operation.DeleteAsync(new Operation { Id = id} );
            }
            catch (Exception ex)
            {
                response.Errors.Add(new Error(ex.Source, ex.Message));
            }
            return response;
        }

        public async Task<DataResponse<OperationDto>> GetAsync(int id)
        {
            var response = new DataResponse<OperationDto>();
            try
            {
                response.Data = (await UnitOfWork.Operation.GetAsync(id)).ToDto();
            }
            catch (Exception ex)
            {
                response.Errors.Add(new Error(ex.Source, ex.Message));
            }
            return response;
        }

        public async Task<DataResponse<IEnumerable<OperationDto>>> GetAsync()
        {
            var response = new DataResponse<IEnumerable<OperationDto>>();
            try
            {
                response.Data = (await UnitOfWork.Operation.GetAsync()).ToDtos();
            }
            catch (Exception ex)
            {
                response.Errors.Add(new Error(ex.Source, ex.Message));
            }
            return response;
        }

        public async Task<Response> UpdateItemAsync(OperationDto operation)
        {
            var response = new Response();
            try
            {
                await UnitOfWork.Operation.UpdateAsync(operation.ToDao());
            }
            catch (Exception ex)
            {
                response.Errors.Add(new Error(ex.Source, ex.Message));
            }
            return response;
        }
    }
}
