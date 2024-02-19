using MVC_Master.Models;
using Newtonsoft.Json;

namespace MVC_Master.Services
{
    public class NetworkServices
    {
        public static async Task<LoginModel> CheckLoginMasterCCP(string user, string pass)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    const string baseUrl = "http://ccp-info2.com";
                    string function = "/APIMasterCCP/api/GetData/CheckLoginMasterCCP" +
                        $"?username={user}&password={pass}&programId=WB2022-091";

                    client.BaseAddress = new Uri(baseUrl);
                    var response = await client.GetAsync(function);  // Get http method
                    response.EnsureSuccessStatusCode();


                    var stringResponse = await response.Content.ReadAsStringAsync();
                    LoginModel result = JsonConvert.DeserializeObject<LoginModel>(stringResponse);
                    return result;
                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine($"Request exception: {e.Message}");
                }
            }
            return null;
        }

		public static async Task<string> LoginToken(string user, string password)
		{
			using (var client = new HttpClient())
			{
				try
				{
					const string baseUrl = "http://192.168.0.81";
					string function = "/APItbshop/api/Account/Login" +
					$"?userName={user}&password={password}";

					client.BaseAddress = new Uri(baseUrl);
					var response = await client.GetAsync(function);
					response.EnsureSuccessStatusCode();

					var strResponse = await response.Content.ReadAsStringAsync();
					
					return strResponse;

				}
				catch (Exception ex)
				{
					return null;
				}
			}
			return null;
		}

	}
}
