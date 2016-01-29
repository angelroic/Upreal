using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Xml.Linq;
using System.Threading.Tasks;
namespace LateralMenus
{
    class WebService
    {
        public string maison = "http://10.224.9.202//UpReal/services/";
        public string ecole = "http://163.5.84.202//UpReal/services/";
        public XElement value;
        async public Task<int> AskWebService(string web)
        {
            HttpClient httpClient = new HttpClient();
            List<New> lnew = new List<New>();
            httpClient.DefaultRequestHeaders.Accept.TryParseAdd("text/xml");
            HttpContent content = new StringContent("", Encoding.UTF8, "text/xml");
            var Response = await httpClient.GetAsync(new Uri(ecole  + web));
            var statusCode = Response.StatusCode;
            Response.EnsureSuccessStatusCode();
            var ResponseText = await Response.Content.ReadAsStringAsync();
            value = XElement.Parse(ResponseText);
            return 1;
        }
    }
}
