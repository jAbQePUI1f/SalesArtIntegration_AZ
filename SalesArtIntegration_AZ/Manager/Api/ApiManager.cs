using Newtonsoft.Json;
using SalesArtIntegration_AZ.Manager.Login;
using SalesArtIntegration_AZ.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SalesArtIntegration_AZ.Manager.Api
{
    public class ApiManager
    {
        public static async Task<TResponse> PutAsync<TRequest, TResponse>(TRequest request, string apiUrl)
        {
            int maxRetryCount = 3; // Tekrar deneme limiti
            string jwtToken = UserSharedInfo.GetToken();

            for (int retryCount = 0; retryCount < maxRetryCount; retryCount++)
            {
                // Serialize request body to JSON
                string jsonRequestBody = Newtonsoft.Json.JsonConvert.SerializeObject(request);
                var settings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                };
                using (HttpClient client = new HttpClient())
                {
                    // Add JWT token to request headers
                    client.DefaultRequestHeaders.Add("x-auth-token", $"Bearer {jwtToken}");

                    // Create HttpContent from JSON
                    HttpContent content = new StringContent(jsonRequestBody, System.Text.Encoding.UTF8, "application/json");

                    // Make POST request
                    HttpResponseMessage response = await client.PutAsync(apiUrl, content);

                    // Check if request is successful
                    if (response.IsSuccessStatusCode)
                    {
                        // Read response content as string
                        string responseContent = await response.Content.ReadAsStringAsync();
                        TResponse responseObject = JsonConvert.DeserializeObject<TResponse>(responseContent, settings);
                        return responseObject;
                    }
                    else if (response.StatusCode == HttpStatusCode.Unauthorized) // Unauthorized hatası alındığında
                    {
                        // Burada kullanıcı kimlik doğrulama işlemlerini gerçekleştirmeniz gerekecek
                        // Bu örnekte varsayılan bir sınıf kullanılıyor, gerçek duruma göre uyarlayın
                        var newToken = await AuthenticateAndGetToken();

                        // Eğer yeni bir token alındıysa, yeni token ile tekrar deneme yapın
                        if (!string.IsNullOrEmpty(newToken))
                        {
                            jwtToken = newToken;
                            continue; // Retry
                        }
                    }
                    // Diğer hatalar için uyarı veya log işlemleri eklenebilir
                    MessageBox.Show($"Token Error: {response.StatusCode} - {response.ReasonPhrase}");  //Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                    return default; // veya isteğe bağlı olarak hata işleme stratejisi
                }
            }
            // Retry limitine ulaşıldığında
            Console.WriteLine($"Error: Maximum retry limit reached");
            return default; // veya isteğe bağlı olarak hata işleme stratejisi
        }
        public static async Task<TResponse> SendRequestAsync<TRequest, TResponse>(TRequest request, string apiUrl)
        {
            int maxRetryCount = 3; // Tekrar deneme limiti
            string jwtToken = UserSharedInfo.GetToken();

            for (int retryCount = 0; retryCount < maxRetryCount; retryCount++)
            {
                // Serialize request body to JSON
                string jsonRequestBody = JsonConvert.SerializeObject(request);

                using (HttpClient client = new HttpClient())
                {
                    // Add JWT token to request headers
                    client.DefaultRequestHeaders.Add("x-auth-token", $"Bearer {jwtToken}");

                    // Create HttpContent from JSON
                    HttpContent content = new StringContent(jsonRequestBody, Encoding.UTF8, "application/json");
                    try
                    {
                        // Make PUT request
                        HttpResponseMessage response = await client.PostAsync(apiUrl, content);

                        // Check if request is successful
                        if (response.IsSuccessStatusCode)
                        {
                            // Read response content as string
                            string responseContent = await response.Content.ReadAsStringAsync();

                            // Deserialize response JSON to the specified type
                            TResponse responseObject = JsonConvert.DeserializeObject<TResponse>(responseContent);

                            return responseObject;
                        }
                        else if (response.StatusCode == HttpStatusCode.Unauthorized) // Unauthorized hatası alındığında
                        {
                            // Burada kullanıcı kimlik doğrulama işlemlerini gerçekleştirmeniz gerekecek
                            // Bu örnekte varsayılan bir sınıf kullanılıyor, gerçek duruma göre uyarlayın
                            var newToken = await AuthenticateAndGetToken();

                            // Eğer yeni bir token alındıysa, yeni token ile tekrar deneme yapın
                            if (!string.IsNullOrEmpty(newToken))
                            {
                                jwtToken = newToken;
                                continue; // Retry
                            }
                        }
                        // Diğer hatalar için uyarı veya log işlemleri eklenebilir
                        MessageBox.Show($"Token Error: {response.StatusCode} - {response.ReasonPhrase}");
                        return default(TResponse);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
            }
            // Retry limitine ulaşıldığında
            Console.WriteLine($"Error: Maximum retry limit reached");
            return default(TResponse);
        }
        public static async Task<TResponse> PostAsync<TRequest, TResponse>(string apiUrl, TRequest requestBody)
        {
            int maxRetryCount = 3; // Tekrar deneme limiti
            string jwtToken = UserSharedInfo.GetToken();

            for (int retryCount = 0; retryCount < maxRetryCount; retryCount++)
            {
                // Serialize request body to JSON
                string jsonRequestBody = Newtonsoft.Json.JsonConvert.SerializeObject(requestBody);
                var settings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                };
                using (HttpClient client = new HttpClient())
                {
                    // Add JWT token to request headers
                    client.DefaultRequestHeaders.Add("x-auth-token", $"Bearer {jwtToken}");

                    // Create HttpContent from JSON
                    HttpContent content = new StringContent(jsonRequestBody, System.Text.Encoding.UTF8, "application/json");

                    // Make POST request
                    HttpResponseMessage response = await client.PostAsync(apiUrl, content);

                    // Check if request is successful
                    if (response.IsSuccessStatusCode)
                    {
                        // Read response content as string
                        string responseContent = await response.Content.ReadAsStringAsync();
                        TResponse responseObject = JsonConvert.DeserializeObject<TResponse>(responseContent, settings);
                        return responseObject;
                    }
                    else if (response.StatusCode == HttpStatusCode.Unauthorized) // Unauthorized hatası alındığında
                    {
                        // Burada kullanıcı kimlik doğrulama işlemlerini gerçekleştirmeniz gerekecek
                        // Bu örnekte varsayılan bir sınıf kullanılıyor, gerçek duruma göre uyarlayın
                        var newToken = await AuthenticateAndGetToken();

                        // Eğer yeni bir token alındıysa, yeni token ile tekrar deneme yapın
                        if (!string.IsNullOrEmpty(newToken))
                        {
                            jwtToken = newToken;
                            continue; // Retry
                        }
                    }

                    // Diğer hatalar için uyarı veya log işlemleri eklenebilir
                    MessageBox.Show($"Token Error: {response.StatusCode} - {response.ReasonPhrase}");  //Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                    return default; // veya isteğe bağlı olarak hata işleme stratejisi
                }
            }

            // Retry limitine ulaşıldığında
            Console.WriteLine($"Error: Maximum retry limit reached");
            return default; // veya isteğe bağlı olarak hata işleme stratejisi
        }
        private static async Task<string> AuthenticateAndGetToken()
        {
            var response = await LoginManager.LoginAsync(UserSharedInfo.UserInfo.UserName, UserSharedInfo.UserInfo.Password);

            return response.Token;
        }
    }
}
