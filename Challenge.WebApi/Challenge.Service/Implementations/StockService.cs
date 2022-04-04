using Challenge.Domain.DTO;
using Challenge.Domain.Entities;
using Challenge.Service.Interfaces;
using CsvHelper;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.Service.Implementations
{
    public class StockService : IStockService
    {
        private readonly IAuthentication auth;
        public IConfiguration conf { get; }
        public StockService(IAuthentication auth, IConfiguration conf)
        {
            this.auth = auth;
            this.conf = conf;
        }

        

        public async void RetrieveStockMessage(Message command)
        {
            string quote;
            using (var client = new HttpClient())
            {
                var stockcode = command.Content.Split('=')[1];

                using (var result = await client.GetAsync("https://stooq.com/q/l/?s="+stockcode+"&f=sd2t2ohlcv&h&e=csv"))
                {
                    if (result.IsSuccessStatusCode)
                    {
                        using (var reader = new StreamReader(result.Content.ReadAsStream()))
                        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                        {
                            var records = csv.GetRecords<CsvStockDTO>();
                            quote = records.First().Close;
                        }
                    }

                }
            }
            var token = auth.Generate(conf["botUsername"]);
            var url = conf["ChallengeWebApiPostMessage"].Replace("{chatroomId}", command.Chatroom.ToString());
            
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                client.DefaultRequestHeaders
                      .Accept
                      .Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post,"");
                request.Content = new StringContent(JsonConvert.SerializeObject(new MessageDTO("MENSAJE ACA")),
                                                    Encoding.UTF8,
                                                    "application/json");
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
            }
        }
    }
}
