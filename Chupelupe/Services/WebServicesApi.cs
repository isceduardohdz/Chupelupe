using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Chupelupe.Models;
using Chupelupe.Services;
using FireSharp;
using FireSharp.Config;
using FireSharp.Extensions;
using FireSharp.Interfaces;
using FireSharp.Response;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xamarin.Forms;

[assembly: Dependency(typeof(WebServicesApi))]
namespace Chupelupe.Services
{
    public class WebServicesApi : IWebServiceApi
    {
        const string _baseUrl = "https://chupelupe-68b4c.firebaseio.com/";
        const string _authSecret = "gmK8kFtQ7hHkJmL5ltGJmZHi2X8nI8ImHNFM8gMJ";

        public IFirebaseClient Client { get; set; }

        public WebServicesApi()
        {
            if (Client == null)
            {
                IFirebaseConfig config = new FirebaseConfig
                {
                    AuthSecret = _authSecret,
                    BasePath = _baseUrl
                };
                Client = new FirebaseClient(config);
            }
        }

        public async Task<List<Promotion>> GetPromotions()
        {
            List<Promotion> promotions = new List<Promotion>();
            try
            {

                FirebaseResponse response = await Client.GetAsync("promotions");

                if (response == null)
                {
                   return promotions;
                }

                if (string.IsNullOrEmpty(response.Body))
                {
                    return promotions;
                }

                var JsonResponse = response.Body;
                var JsonObjetc = JObject.Parse(JsonResponse);

                foreach (var item in JsonObjetc)
                {
                    var promotion = await Task.Run(() =>
                        JsonConvert.DeserializeObject<Promotion>(item.Value.ToJson()));
                    if (promotion == null)
                    {
                        continue;
                    }
                    promotions.Add(promotion);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return promotions;
        }
    }
}
