


using System.Net;
using BaseLibrary.DTOs;
using Client.Library.Services.Contracts;

namespace Client.Library.Helpers
{
    public class CustomHttpHandler(LocalStorageService localStorage , GetHttpClient httpClient,IUserAccountService accountService) : DelegatingHandler
    {
        protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            bool loginUrl = request.RequestUri!.AbsoluteUri.Contains("login");
            bool registerUrl = request.RequestUri!.AbsoluteUri.Contains("register");
            bool refreshUrl = request.RequestUri!.AbsoluteUri.Contains("refresh-token");
            
            if (loginUrl || registerUrl || refreshUrl) return await base.SendAsync(request, cancellationToken);


            var result = await base.SendAsync(request, cancellationToken);

            if(result.StatusCode == HttpStatusCode.Unauthorized)
            {
                var stringToken = await localStorage.GetToken();
                if (stringToken == null) return result;

                string token = String.Empty;
                try {
                    token = request.Headers.Authorization!.Parameter!;
                }
                catch { }
                var deserializtionToken = Serializations.DeSerializeJsonString<UserSession>(stringToken);
                if (deserializtionToken == null) return result;
                if (string.IsNullOrEmpty(token))
                {
                    request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer",deserializtionToken.Token);
                    return await base.SendAsync(request, cancellationToken); 
                }
                var newJwt = await GetRefreshToken(deserializtionToken.RefreshToken!);
                if (string.IsNullOrEmpty(newJwt)) return result;
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", newJwt);
                return await base.SendAsync(request, cancellationToken);
            }
            return result;
        }

        private async Task<string> GetRefreshToken(string refreshToken)
        {
            var result = await accountService.RefreshTokenAsync(new RefreshToken() { Token = refreshToken});
            string serializedToken = Serializations.SerializeObj(new UserSession() { 
                Token = result.Token,RefreshToken = result.RefreshToken
            });
            await localStorage.SetToken(serializedToken);
            return result.Token;
        }
    }
}
