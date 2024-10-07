using Application.Interfaces.IHttpServices;
using Application.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases
{
    public class TransferHttpService : ITransferHttpService
    {
        private readonly HttpClient _httpClient;

        public TransferHttpService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public /*async Task<List<TransferResponse>>*/ List<TransferResponse> GetAllTransfersByAccount(Guid accountId)
        {
            /* 
            var response = await _httpClient.GetAsync($"http://localhost/api/Transfer/{accountId}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<TransferResponse>>();
            }

            return null; //Manejar de forma correcta
            */

            //Datos ficticios de prueba
            var transferResponse = new List<TransferResponse>
            {
                new TransferResponse
                {
                    Id = 1,
                    Amount = 1500,
                    Date = DateTime.Now,
                    Status = "Done"
                },
                new TransferResponse
                {
                    Id = 2,
                    Amount = 10000,
                    Date = DateTime.Today,
                    Status = "Done"
                },
                new TransferResponse
                {
                    Id = 3,
                    Amount = 2156,
                    Date = new DateTime(2024, 10, 10, 0, 0, 0, DateTimeKind.Utc),
                    Status = "Cancel"
                }
            };

            return transferResponse;
        }
    }
}
