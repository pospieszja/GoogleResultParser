using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Google.Apis.Customsearch.v1;
using Google.Apis.Customsearch.v1.Data;
using Google.Apis.Services;
using GoogleResultParser.Web.Models;

namespace GoogleResultParser.Web.Services
{
    public class GoogleResultService
    {
        public List<GoogleResult> GetResult(string q)
        {
            var googleResults = new List<GoogleResult>();
            IList<Result> rawResult;

            //Run call to Google API two times, result limited to 10 entries per request
            rawResult = CallGoogleAPI(10, 1, q);
            if (rawResult != null)
            {
                foreach (var item in rawResult)
                {
                    var googleResult = new GoogleResult();
                    googleResult.Title = item.Title;
                    googleResult.Link = item.Link;
                    googleResults.Add(googleResult);
                }
            }

            rawResult = CallGoogleAPI(2, 11, q);
            if (rawResult != null)
            {
                foreach (var item in CallGoogleAPI(2, 11, q))
                {
                    var googleResult = new GoogleResult();
                    googleResult.Title = item.Title;
                    googleResult.Link = item.Link;
                    googleResults.Add(googleResult);
                }
            }

            return googleResults;
        }

        IList<Result> CallGoogleAPI(int num, int start, string q)
        {
            string apiKey = "AIzaSyBgr7AZC68PiGb4n689qHzv3Cf10SBHGQY";
            string searchEngineId = "007599586031768366314:dgvmqpthlam";
            string query = q;

            var customSearchService = new CustomsearchService(new BaseClientService.Initializer { ApiKey = apiKey });
            var listRequest = customSearchService.Cse.List(q);
            listRequest.Cx = searchEngineId;


            listRequest.Num = num;
            listRequest.Start = start;
            var b = listRequest.Execute();

            return listRequest.Execute().Items;
        }
    }
}