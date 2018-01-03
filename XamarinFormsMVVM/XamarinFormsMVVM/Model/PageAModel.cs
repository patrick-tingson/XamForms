using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using XamarinFormsMVVM.Class;

namespace XamarinFormsMVVM.Model
{
    public class RequestProfile
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }

    public class PageAModel
    {
        public PageAModel()
        {

        }

        public async Task<string> GetDataAsync(string id)
        {
            if (id.Length == 0)
                throw new ArgumentException("Invalid ID");
                
            var result = ""; 

            try
            {
                var tranUrl = string.Format("{0}/CheckProfile/{1}", InternalVariable.Endpoint, id);

                var restService = new RestService();
                var response = await restService.Transact(HttpVerb.GET, tranUrl);

                if (response != null)
                    result = (response.StatusCode == "OK") ? response.Content : "";
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public async Task<string> PostDataAsync(RequestProfile requestProfile)
        {
            if (requestProfile == null)
                throw new ArgumentException("Invalid ID");

            if (requestProfile.Id.Length == 0)
                throw new ArgumentException("Invalid ID");

            if (requestProfile.Name.Length == 0)
                throw new ArgumentException("Invalid Name");

            var result = "";
            var param = JsonConvert.SerializeObject(requestProfile);

            try
            {
                var tranUrl = string.Format("{0}/AddProfile/", InternalVariable.Endpoint);

                var restService = new RestService();
                var response = await restService.Transact(HttpVerb.POST, tranUrl, param);

                if (response != null)
                    result = (response.StatusCode == "OK") ? response.Content : "";
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        } 
    }
}
