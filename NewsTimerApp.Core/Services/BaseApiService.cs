using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using NewsTimerApp.Core.Models.Security;
using Newtonsoft.Json;

namespace NewsTimerApp.Core.Services
{
    public class BaseApiService
    {
        private static readonly HttpClient _httpClient = new HttpClient();
        private readonly JsonSerializer _serializer = new JsonSerializer();
        private readonly ISecureStorageService _secureStorageService;
        public BaseApiService(ISecureStorageService secureStorageService)
        {
            _secureStorageService = secureStorageService;
        }

        protected async Task<T> GetAsync<T>(string url) where T : class, new()
        {
            T result = default;
            try
            {
                HttpRequestMessage request = ConstructGetRequest(url);
                
                using (HttpResponseMessage httpResponse = await _httpClient.SendAsync(request).ConfigureAwait(false))
                {
                    if (httpResponse.IsSuccessStatusCode)
                    {
                        result = await DeserializeContentStream(result, httpResponse);
                    }
                    else if (httpResponse.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        result = await RefreshTokenAndRetryGet(result, url);
                    }
                    else
                    {
                        result = new T();
                    }
                }
            }
            catch
            {
                result = new T();
            }

            return result;
        }

        protected async Task<T> PostAsync<T>(string url, object data = null) where T : new()
        {
            T result = default;
            
            if (data == null)
            {
                data = new { };
            }

            try
            {

                var jsonData = JsonConvert.SerializeObject(data);
                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                HttpRequestMessage request = ConstructPostRequest(url, content);
                using (HttpResponseMessage httpResponse = await _httpClient.SendAsync(request).ConfigureAwait(false))
                {
                    if (httpResponse.IsSuccessStatusCode)
                    {
                        result = await DeserializeContentStream(result, httpResponse);
                    }
                    else if (httpResponse.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        //result = await RefreshTokenAndRetryPost(result, url, content);
                    }
                    else
                    {
                        result = new T();
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("BaseRepository.PostAsync Failed: Exception=" + ex.Message);
                result = new T();
            }

            return result;
        }

        protected async Task<T> PostFormUrlEncodedContentAsync<T>(string url, Dictionary<string, string> formData) where T : new()
        {
            T result = default;
            try
            {
                var content = new FormUrlEncodedContent(formData);
                HttpRequestMessage request = ConstructPostRequest(url, content);
                using (HttpResponseMessage httpResponse = await _httpClient.SendAsync(request).ConfigureAwait(false))
                {
                    result = await DeserializeContentStream(result, httpResponse);
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
                result = new T();
            }

            return result;
        }

        private async Task<T> RefreshTokenAndRetryGet<T>(T result, string url) where T : new()
        {
            var accessToken = _secureStorageService.GetAsync(Constants.AccessToken).Result;
            var refreshToken = _secureStorageService.GetAsync(Constants.RefreshToken).Result;
            bool hasBothTokens = string.IsNullOrEmpty(accessToken) == false && string.IsNullOrEmpty(refreshToken) == false;
            
            if (hasBothTokens)
            {
                var tokens = await RefreshToken(new RefreshTokenModel
                {
                    AccessToken = accessToken,
                    RefreshToken = refreshToken
                });
                bool hasBothNewTokens = string.IsNullOrEmpty(tokens.AccessToken) == false && string.IsNullOrEmpty(tokens.RefreshToken) == false;
                if (hasBothNewTokens)
                {
                    await _secureStorageService.SetAsync(Constants.AccessToken, tokens.AccessToken);
                    await _secureStorageService.SetAsync(Constants.RefreshToken, tokens.RefreshToken);

                    //try again
                    HttpRequestMessage request = ConstructGetRequest(url);
                    
                    using (HttpResponseMessage httpResponse = await _httpClient.SendAsync(request).ConfigureAwait(false))
                    {
                        result = await DeserializeContentStream(result, httpResponse);
                    }
                }
            }

            return result;
        }

        private async Task<T> RefreshTokenAndRetryPost<T>(T result, string url, StringContent content) where T : new()
        {
            var accessToken = _secureStorageService.GetAsync(Constants.AccessToken).Result;
            var refreshToken = _secureStorageService.GetAsync(Constants.RefreshToken).Result;
            bool hasBothTokens = string.IsNullOrEmpty(accessToken) == false && string.IsNullOrEmpty(refreshToken) == false;
            
            if (hasBothTokens)
            {
                var tokens = await RefreshToken(new RefreshTokenModel { AccessToken = accessToken, RefreshToken = refreshToken });
                if (string.IsNullOrEmpty(tokens.AccessToken) == false && string.IsNullOrEmpty(tokens.RefreshToken) == false)
                {
                    await _secureStorageService.SetAsync(Constants.AccessToken, tokens.AccessToken);
                    await _secureStorageService.SetAsync(Constants.RefreshToken, tokens.RefreshToken);
                    
                    //https://github.com/xamarin/xamarin-macios/issues/4380
                    HttpRequestMessage request = ConstructPostRequest(url, content);
                    
                    using (HttpResponseMessage httpResponse = await _httpClient.SendAsync(request).ConfigureAwait(false))
                    {
                        result = await DeserializeContentStream(result, httpResponse);
                    }
                }
            }

            return result;
        }

        private async Task<RefreshTokenModel> RefreshToken(RefreshTokenModel tokens)
        {
            var formData = new Dictionary<string, string>
            {
                {"accessToken", tokens.AccessToken},
                {"refreshToken", tokens.RefreshToken},
            };

            var result = await PostFormUrlEncodedContentAsync<RefreshTokenModel>("hobbyConnect/refreshToken", formData);
            return result;
        }

        private async Task<T> DeserializeContentStream<T>(T result, HttpResponseMessage httpResponse) where T : new()
        {
            using (var stream = await httpResponse.Content.ReadAsStreamAsync())
            using (var reader = new StreamReader(stream))
            using (var json = new JsonTextReader(reader))
            {
                result = _serializer.Deserialize<T>(json);
            }

            return result;
        }

        private HttpRequestMessage ConstructGetRequest(string url)
        {
            var accessToken = _secureStorageService.GetAsync(Constants.AccessToken).Result;

            return new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(Constants.WebApiBaseUrl + url),
                Headers = {
                    { HttpRequestHeader.Authorization.ToString(), "Bearer " + accessToken },
                    { HttpRequestHeader.Accept.ToString(), "application/json" },
                }
            };
        }

        private HttpRequestMessage ConstructPostRequest(string url, StringContent content)
        {
            var accessToken = _secureStorageService.GetAsync(Constants.AccessToken).Result;

            return new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(Constants.WebApiBaseUrl + url),
                Headers = {
                    { HttpRequestHeader.Authorization.ToString(), "Bearer " + accessToken },
                    { HttpRequestHeader.Accept.ToString(), "application/json" },
                },
                Content = content
            };
        }

        private HttpRequestMessage ConstructPostRequest(string url, FormUrlEncodedContent content)
        {
            var accessToken = _secureStorageService.GetAsync(Constants.AccessToken).Result;
            
            return new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(Constants.WebApiBaseUrl + url),
                Headers = {
                    { HttpRequestHeader.Authorization.ToString(), "Bearer " + accessToken },
                    { HttpRequestHeader.Accept.ToString(), "application/json" },
                },
                Content = content
            };
        }
    }
}
