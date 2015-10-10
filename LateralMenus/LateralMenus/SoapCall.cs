using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
using System.Net.Http.Headers;


namespace LateralMenus
{
    class SoapCall
    {
        
            public  async void  Login(string url, string data)
        {
                try
                {
                     HttpClient httpClient = new HttpClient();

                    httpClient.DefaultRequestHeaders.Accept.TryParseAdd("text/xml");
                    HttpContent content = new StringContent(data, Encoding.UTF8, "text/xml");

                    var Response = await httpClient.GetAsync(new Uri(url));
                        //(new Uri(url), content);
                    var statusCode = Response.StatusCode;

                    Response.EnsureSuccessStatusCode();
                }
                catch
                {
                    Console.WriteLine("SALUT");
                }
                
        }
    }
}
