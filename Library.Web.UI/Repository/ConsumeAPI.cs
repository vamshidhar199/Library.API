using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using System.Net.Http.Headers;
using Library.Web.UI.Models;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Library.Web.UI.Repository
{
    public class ConsumeAPI
    {
        public async Task<IEnumerable<BookDetail>> GetBooks()
        {
            string Baseurl = "http://localhost:56181/";
            List<BookDetail> books = new List<BookDetail>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage Res = await client.GetAsync("api/BookDisplay");

                if (Res.IsSuccessStatusCode)
                {
                    var res = Res.Content.ReadAsStringAsync().Result;
                    books = JsonConvert.DeserializeObject<List<BookDetail>>(res);
                    return books;
                }
                else
                    return null;

            }
        }
    }
}

        