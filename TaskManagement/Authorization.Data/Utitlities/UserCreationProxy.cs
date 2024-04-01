using Authorization.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TaskManagement.IDAM.Entities;
using System.Net.Http;
using Microsoft.AspNetCore.Http;

namespace Authorization.Data.Utitlities
{
    public class UserCreationProxy : IUserCreationProxy
    {
        public async Task CreateUser(User user, HttpContext httpContext)
        {
            string token = httpContext.Request.Headers["Authorization"];
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            // Pass the handler to httpclient(from you are calling api)
            HttpClient _httpClient = new HttpClient(clientHandler);
            _httpClient.DefaultRequestHeaders.Add("Authorization", token);
            var userJson = JsonSerializer.Serialize(user);

            var content = new StringContent(userJson, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("https://localhost:7011/api/user", content);
        }
    }
}
