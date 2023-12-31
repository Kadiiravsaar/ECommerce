﻿using Core.Utilities.Response;
using Entitites.Dtos.Auth;
using Entitites.Dtos.User;
using Newtonsoft.Json;
using WebAPIWithCoreMvc.ApiServices.Interfaces;

namespace WebAPIWithCoreMvc.ApiServices
{
    public class AuthApiService : IAuthApiService
    {
        HttpClient _httpClient;

        public AuthApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ApiDataResponse<UserDto>> LoginAsync(LoginDto loginDto)
        {// apiye istek atıyor
            HttpResponseMessage httpResponseMessage = await _httpClient.PostAsJsonAsync("Auth/Login", loginDto);
            if (httpResponseMessage.IsSuccessStatusCode) 
            {
                var data = await httpResponseMessage.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<ApiDataResponse<UserDto>>(data);
                return await Task.FromResult(result);
            }
            return null;

        }
    }
}
