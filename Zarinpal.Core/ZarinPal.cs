using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;

namespace Zarinpal.Core
{
  public class Zarinpal
    {
        private string merchantId;
        private RestClient _client;
        public Zarinpal(string merchantId)
        {
            this.merchantId = merchantId;
        }

        private RestClient GetClient()
        {
            if (_client == null)
            {
                _client= new RestClient("https://api.zarinpal.com/pg/v4/payment/");
            }

            return _client;
        }
        public async Task<string> SendRequest(int amount, string description, string callback,ZarinMetaData metaData=null)
        {
            var client = GetClient();
            var request = new RestRequest("request.json", Method.POST);
            request.AddParameter("merchant_id", merchantId);
            request.AddParameter("amount", amount);
            request.AddParameter("description", description);
            request.AddParameter("callback_url", callback);
           // if(metaData!=null)
            //    request.AddParameter("metadata", JsonConvert.SerializeObject(metaData));

            var response = await client.ExecuteAsync<ZarinResponse>(request);
            if (response.Data.data.code == 100)
                return response.Data.data.authority;
            return null;
        }

        public string CreateGatewayLink(string authority)
        {
            return ("https://www.zarinpal.com/pg/StartPay/" + authority);
        }
        
        public async Task<ZarinVerify> Verify(int amount, string authority)
        {
            var client = GetClient();
            var request = new RestRequest("verify.json", Method.POST);
            request.AddParameter("merchant_id", merchantId);
            request.AddParameter("amount", amount);
            request.AddParameter("authority", authority);

            var response =await client.ExecuteAsync(request);
            if (response.StatusCode==HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<ZarinVerify>(response.Content);
            }

            return null;

        }
        
    }
}