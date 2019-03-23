using Microsoft.Extensions.Caching.Memory;
using Microsoft.Graph;
using Newtonsoft.Json;
using PreOrclFrontEnd.Utilidades;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace PreOrclFrontEnd.Helpers
{
    public class GraphAuthCustom {
        IMemoryCache _memoryCache;
        MSGraphConfiguration _msGraphConfiguration;
        public GraphAuthCustom(IMemoryCache memoryCache, MSGraphConfiguration msGraphConfiguration) {
            _memoryCache = memoryCache;
            _msGraphConfiguration = msGraphConfiguration;
        }
        public string GetMe(string token =null) {
            string entity ="";

            try
            {
                HttpClient client = new HttpClient();
                string uri = "https://graph.microsoft.com/v1.0/me";
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                HttpResponseMessage response = client.GetAsync(uri).Result;

                if (response.IsSuccessStatusCode)
                    entity = response.Content.ReadAsStringAsync().Result;

            }
            catch (Exception ex)
            {
             //   ExceptionUtility.LogException(ex);
            }

            return entity;
        }

        public string GetToken(string _code)
        {
            
            TokenT t = new TokenT();
            Peticion p =
                new Peticion
                { client_id = "212a93ce-c93e-43b8-adaf-cc32d3606e75",
                    client_secret = "ysCRFE4_}fenfmVKW2574${",
                    scope = "offline_access calendars.read user.read user.readbasic.all mail.read",
                    //scope = "https://graph.microsoft.com/.default",
                    grant_type = "authorization_code",
                    code = _code,
                    redirect_uri = "http://localhost:50222/Auth/LoginMicrosoft/"
                };
            try
            {
                HttpClient client = new HttpClient();

                var parames= new Dictionary<string, string>();
                string uri = "https://login.microsoftonline.com/common/oauth2/v2.0/token";

                parames.Add("client_id", p.client_id);
                parames.Add("client_secret", p.client_secret);
                parames.Add("scope", p.scope);
                parames.Add("grant_type", p.grant_type);
                parames.Add("code", p.code);
                parames.Add("redirect_uri", p.redirect_uri);

                // client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "EwCIA8l6BAAURSN/FHlDW5xN74t6GzbtsBBeBUYAAc6BnbUNV9Ul2QBvqM4NqjxDYRNLTlQOierQyQE7q1MXDn141wYU2h5o8e8EYVLSAcWk3BtTPt3mAqloE7sH8q2DLC0X00NVnQOCf6j89n1QMHBnBY/eZGE4CkzEKRAqPyDeHkdDAmxa4g53nS8wuaExrLPEttS4hFAxq4Su6a2KTFSWFphPqB+d0m5bOvRuS6XVBOYD9U657wwDcSH5T4vc6ZZUxUcXTQ53xPYYnLWsk8BYWzUVYClcCPZylvIudL6wVClf8s8TAx5qo+8nWWZvLh7l54Qwj/P0b/EhLPwuWsrJlem0mNpN5IJYO8noZn/hDiInpL5SLE0fOVgPuksDZgAACGcxIOebsKW0WAKT6HlLsNJwSze9N9LM3MqViDqcopnz4Ek2TrHeBCSAubCOb9xi1nNB+XqexhHvpNXYGcqox/sRS6a7DIKfEcBC8WNcRz5p3WbETefYmHTTY433HGHDz1VDOU8Bx2csbfvCD+gRM8Jtg5+CLZqFahWgW8hW3TaJ3kCXQ2KX4o2dtiuGZe18skvUe/aq92D3h3W2WU8tsIoFod/p12lDQGAGGHLm6CSZmmKYpuSJPQ6sMaB3hMkERu0jtwT65JhshxrE1li3ypiI0/qWZeLXMVt/7uf0Wep990Qgp7zBmqbHSxx8ncvBIYG6LRQ1kEMjC2LYLLXLro8zc0a6C3Ns+3VwkC3pY53atPo+0V7bRTD46363gJ/DG6HFpU8wvFoMOgaHKe3VryAj9bYABLjKS+DUdhaquz1IDv8RNUrF5VKkyhvwzcGlQXxj2bnxGKu0pB/L5PJjmdd0Yv0V6AJfykWiFI+yMZX9eQ6lXN4b59ezcuyfAb5xPb14NNEPA0SAe0usbk6cf0KgLLQ19X/Tg6M+UlvXFotDR1TiwjO3Mg+CphaAv0UkPN0jT/5RMHkVDW39PyIesH134q3QFnU0w2KL0ZChy/njvFSN7gd+q0Z767fSsSIhwy7fOU8LAdPfd472rUNPPoySwlUJBDr4IpdY2v3eJbGKR9y452ItNZomKQ/Hk4RLZwCbqoN+MnYisJTsNwQCEKyXgzRduLdV/AjZH4E4cHtgiOpA2uiHAe39Zz2UOx7djHPWkWdVlSaMgSdOR0LTXA3LybpHU+7uEhMcIiImButU4HChAg==");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = client.PostAsync(uri, new FormUrlEncodedContent(parames)).Result;

                if (response.IsSuccessStatusCode)
                    t = response.Content.ReadAsAsync<TokenT>().Result;

            }
            catch (Exception ex)
            {
                   ExceptionUtility.LogException(ex);
            }

            return t.access_token;
        }

        public TokenT GetTokenT(string _code)
        {

            TokenT t = new TokenT();
            Peticion p =
                new Peticion
                {
                    client_id = _msGraphConfiguration.ClientId,
                    client_secret = _msGraphConfiguration.ClientSecret,
                    scope = _msGraphConfiguration.Scope,
                    //scope = "https://graph.microsoft.com/.default",
                    grant_type = "authorization_code",
                    code = _code,
                    redirect_uri = _msGraphConfiguration.RedirectUrl
                };
            try
            {
                HttpClient client = new HttpClient();

                var parames = new Dictionary<string, string>();
                string uri = "https://login.microsoftonline.com/common/oauth2/v2.0/token";

                parames.Add("client_id", p.client_id);
                parames.Add("client_secret", p.client_secret);
                parames.Add("scope", p.scope);
                parames.Add("grant_type", p.grant_type);
                parames.Add("code", p.code);
                parames.Add("redirect_uri", p.redirect_uri);

                // client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "EwCIA8l6BAAURSN/FHlDW5xN74t6GzbtsBBeBUYAAc6BnbUNV9Ul2QBvqM4NqjxDYRNLTlQOierQyQE7q1MXDn141wYU2h5o8e8EYVLSAcWk3BtTPt3mAqloE7sH8q2DLC0X00NVnQOCf6j89n1QMHBnBY/eZGE4CkzEKRAqPyDeHkdDAmxa4g53nS8wuaExrLPEttS4hFAxq4Su6a2KTFSWFphPqB+d0m5bOvRuS6XVBOYD9U657wwDcSH5T4vc6ZZUxUcXTQ53xPYYnLWsk8BYWzUVYClcCPZylvIudL6wVClf8s8TAx5qo+8nWWZvLh7l54Qwj/P0b/EhLPwuWsrJlem0mNpN5IJYO8noZn/hDiInpL5SLE0fOVgPuksDZgAACGcxIOebsKW0WAKT6HlLsNJwSze9N9LM3MqViDqcopnz4Ek2TrHeBCSAubCOb9xi1nNB+XqexhHvpNXYGcqox/sRS6a7DIKfEcBC8WNcRz5p3WbETefYmHTTY433HGHDz1VDOU8Bx2csbfvCD+gRM8Jtg5+CLZqFahWgW8hW3TaJ3kCXQ2KX4o2dtiuGZe18skvUe/aq92D3h3W2WU8tsIoFod/p12lDQGAGGHLm6CSZmmKYpuSJPQ6sMaB3hMkERu0jtwT65JhshxrE1li3ypiI0/qWZeLXMVt/7uf0Wep990Qgp7zBmqbHSxx8ncvBIYG6LRQ1kEMjC2LYLLXLro8zc0a6C3Ns+3VwkC3pY53atPo+0V7bRTD46363gJ/DG6HFpU8wvFoMOgaHKe3VryAj9bYABLjKS+DUdhaquz1IDv8RNUrF5VKkyhvwzcGlQXxj2bnxGKu0pB/L5PJjmdd0Yv0V6AJfykWiFI+yMZX9eQ6lXN4b59ezcuyfAb5xPb14NNEPA0SAe0usbk6cf0KgLLQ19X/Tg6M+UlvXFotDR1TiwjO3Mg+CphaAv0UkPN0jT/5RMHkVDW39PyIesH134q3QFnU0w2KL0ZChy/njvFSN7gd+q0Z767fSsSIhwy7fOU8LAdPfd472rUNPPoySwlUJBDr4IpdY2v3eJbGKR9y452ItNZomKQ/Hk4RLZwCbqoN+MnYisJTsNwQCEKyXgzRduLdV/AjZH4E4cHtgiOpA2uiHAe39Zz2UOx7djHPWkWdVlSaMgSdOR0LTXA3LybpHU+7uEhMcIiImButU4HChAg==");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = client.PostAsync(uri, new FormUrlEncodedContent(parames)).Result;

                if (response.IsSuccessStatusCode)
                    t = response.Content.ReadAsAsync<TokenT>().Result;

            }
            catch (Exception ex)
            {
                ExceptionUtility.LogException(ex);
            }

            return t;
        }
        public bool CreateToken(string _code)
        {
            TokenT token = GetTokenT(_code);
            _memoryCache.Set("TokenT", token);
            bool resultado = token==null;
            return !resultado;
        }

        public GraphServiceClient GetAuthenticatedClient(string token)
        {
            string c = "";
            GraphServiceClient _graphClient = new GraphServiceClient(new DelegateAuthenticationProvider(
                async requestMessage =>
                {
                    // Passing tenant ID to the sample auth provider to use as a cache key
                    var accessToken = token;

                    // Append the access token to the request
                    requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                    // This header identifies the sample in the Microsoft Graph service. If extracting this code for your project please remove.
                  //  requestMessage.Headers.Add("SampleID", "aspnetcore-connect-sample");
                }));
            // c = _authProvider.GetUserAccessTokenAsync(userId).Result;
            return _graphClient;
        }


    }

    public class Peticion {
        public string client_id { get; set; }
        public string client_secret { get; set; }
        public string scope { get; set; }
        public string grant_type { get; set; }
        public string code { get; set; }
        public string redirect_uri { get; set; }
       

    }

    public class TokenT
    {
    public string token_type { get; set; }
    public string scope { get; set; }
    public string expires_in { get; set; }
    public string ext_expires_in { get; set; }
    public string access_token { get; set; }
    public string refresh_token { get; set; }
    }

}
