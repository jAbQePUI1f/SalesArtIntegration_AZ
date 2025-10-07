using SalesArtIntegration_AZ.Manager.Config;
using SalesArtIntegration_AZ.Models.Base;
using System.Net;
using System.Text;

namespace SalesArtIntegration_AZ.Manager.Login
{
    #region Entity
    public class LoginResultModel : CommonResultModel
    {
        public string Token { get; set; }
    }

    #endregion
    public class LoginManager
    {
        public static LoginResultModel Login(string userName, string password)
        {
            LoginResultModel model = new LoginResultModel();

            #region Parameter Control
            if (string.IsNullOrEmpty(userName)
                || string.IsNullOrEmpty(password))
            {
                model.State = false;
                model.Messages.Add("Kullanıcı adı veya şifre boş olamaz!");

                return model;
            }

            userName = userName.Trim();
            password = password.Trim();

            if (string.IsNullOrEmpty(userName)
                || string.IsNullOrEmpty(password))
            {
                model.State = false;
                model.Messages.Add("Kullanıcı adı veya şifre boş olamaz!");

                return model;
            }
            #endregion

            var tokenResult = GetToken(userName, password);

            if (tokenResult == null)
            {
                model.State = false;
                model.Messages.Add("User token information could not be obtained");
                return model;
            }

            if (string.IsNullOrEmpty(tokenResult.Result.Token))
            {
                model.State = false;
                model.Messages.Add("User token information could not be obtained x2");
                return model;
            }

            model.State = true;
            model.Token = tokenResult.Result.Token;

            return model;
        }
        public static Task<LoginResultModel> LoginAsync(string userName, string password)
        {

            var task = Task.Run(() =>
            {
                return Login(userName, password);
            });

            return task;
        }
        private static async Task<LoginResultModel> GetToken(string email, string password)
        {
            LoginResultModel result = new LoginResultModel();
            string loginPage = "MANAGEMENT";

            try
            {
                using (var client = new HttpClient())
                {
                    var requestBody = new
                    {
                        email,
                        password,
                        loginPage
                    };

                    var jsonRequest = Newtonsoft.Json.JsonConvert.SerializeObject(requestBody);
                    var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
                    string url = Configuration.GetUrl() + "authenticate";
                    var response = await client.PostAsync(Configuration.GetUrl() + "authenticate", content);

                    if (response.IsSuccessStatusCode)
                    {
                        var jsonResponse = await response.Content.ReadAsStringAsync();
                        dynamic responseData = Newtonsoft.Json.JsonConvert.DeserializeObject(jsonResponse);
                        if (responseData.responseStatus == HttpStatusCode.BadRequest)
                        {
                            result.Token = "";
                            result.State = false;
                            result.Messages.Add(response.RequestMessage.ToString());

                            return result;
                        }
                        else
                        {
                            result.Token = responseData.data.jwt;
                        }

                    }
                    else
                    {
                        result.Token = "";
                        result.State = false;
                        result.Messages.Add(response.RequestMessage.ToString());

                        return result;
                    }

                }
            }
            catch (Exception ex)
            {
                result.Token = "";
                result.State = false;
                result.Messages.Add(ex.Message.ToString());
                return result;
            }
            return result;
        }

    }
}
