using Entities.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace TestAPI_DDD_2022
{
    [TestClass]
    public class UnitTest1
    {
        public static string Token { get; set; }    
        
        [TestMethod]
        public void TestMethod1()
        {
            var result = CallApiPost("https://localhost:7162/api/List").Result;
            var listMessage = JsonConvert.DeserializeObject<Message[]>(result).ToList();
            Assert.IsTrue(listMessage.Any());
        }

        public void GetToken()
        {
            string urlApiTokenGenerate = "https://localhost:7162/api/CreateTokenIdentity";

            using (var client = new HttpClient())
            {
                string login = "welin@gmail.com";
                string password = "@Welin7123";
                string cpf = "11122233396";

                var data = new
                {
                    email = login,
                    password = password,
                    cpf = cpf,
                };

                string jsonObject = JsonConvert.SerializeObject(data);
                var content = new StringContent(jsonObject, Encoding.UTF8, "application/json");

                var response = client.PostAsync(urlApiTokenGenerate, content);
                response.Wait();

                if(response.Result.IsSuccessStatusCode)
                {
                    var tokenJson = response.Result.Content.ReadAsStringAsync();
                    Token = JsonConvert.DeserializeObject(tokenJson.Result).ToString();
                }
            }
        }

        public string CallApiGet(string url)
        {
            GetToken();
            if(!string.IsNullOrWhiteSpace(Token))
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",Token);
                    var response = client.GetStringAsync(url);
                    response.Wait();
                    return response.Result;
                }
            }

            return null;
        }

        public async Task<string> CallApiPost(string url, object data = null)
        {
            string jsonObject = data != null ? JsonConvert.SerializeObject(data) : "";
            var content = new StringContent(jsonObject, Encoding.UTF8, "application/json");

            GetToken();
            if(!string.IsNullOrWhiteSpace(Token))
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                    var response = client.PostAsync(url, content);
                    response.Wait();

                    if(response.Result.IsSuccessStatusCode)
                    {
                        var result = await response.Result.Content.ReadAsStringAsync();
                        return result;
                    }
                }
            }

            return null;
        }
    }
}