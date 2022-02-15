using MyFinancesMobileApp.Models;
using MyFinancesMobileApp.Models.Dtos;
using MyFinancesMobileApp.Models.Response;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MyFinancesMobileApp.Services
{
    public class OperationService : IOperationService
    {

        private static readonly HttpClient _httpClient = new HttpClient { BaseAddress = new Uri("local") };

        public async Task<DataResponse<int>> AddAsync(OperationDto operation)
        {
           var stringContent = new StringContent(JsonConvert.SerializeObject(operation),
               Encoding.UTF8, "application/json");

            using (var response = await _httpClient.PostAsync("operation", stringContent))
            {
                var responseContent = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<DataResponse<int>>(responseContent);
            }
            
        }

        public async Task<Response> UpdateItemAsync(OperationDto operation)
        {
            var stringContent = new StringContent(JsonConvert.SerializeObject(operation),
           Encoding.UTF8, "application/json");

            using (var response = await _httpClient.PutAsync("operation", stringContent))
            {
                var responseContent = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<Response>(responseContent);
            }
        }


            public async Task<Response> DeleteItemAsync(int id)
        {
           
            using (var response = await _httpClient.DeleteAsync ($"operation/{id}"))
            {
                var responseContent = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<Response>(responseContent);
            }
        }

        public async Task<DataResponse<OperationDto>> GetAsync(int id)
        {
            var json = await _httpClient.GetStringAsync($"operation/{id}");

            return JsonConvert.DeserializeObject<DataResponse<OperationDto>>(json);
        }

        public async Task<DataResponse<IEnumerable<OperationDto>>> GetAsync()
        {
            var json = await _httpClient.GetStringAsync("operation");

            return JsonConvert.DeserializeObject<DataResponse<IEnumerable<OperationDto>>>(json);
        }
    }
}