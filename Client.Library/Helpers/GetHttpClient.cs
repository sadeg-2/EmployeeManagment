﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseLibrary.DTOs;

namespace Client.Library.Helpers
{
    public class GetHttpClient(IHttpClientFactory httpClientFactory,LocalStorageService localStorageService)
    {
        private const string HeaderKey = "Authorization";
        public async Task<HttpClient> GetPrivateHttpClient()
        {
            var client = httpClientFactory.CreateClient("SystemApiClient");
            var stringToken = await localStorageService.GetToken();
            if (stringToken == null) return client;

            var deserializeToken = Serializations.DeSerializeJsonString<UserSession>(stringToken);

            if (deserializeToken == null) return client;

            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer",deserializeToken.Token);
            return client;
        }
        public HttpClient GetPublicHttpClient()
        {
            var client = httpClientFactory.CreateClient("SystemApiClient");
            client.DefaultRequestHeaders.Remove(HeaderKey);
            return client;
        }











    }
}
