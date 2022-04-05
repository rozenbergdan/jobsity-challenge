using Challenge.Domain.DTO;
using Challenge.Domain.Entities;
using Challenge.Infrastructure.Exceptions;
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
            try
            {
                var stockcode = command.Content.Split('=')[1];
                var quote = await GetQuote(stockcode);
                var messageContent = $"{stockcode.ToUpper()} quote is ${quote} per share";
                if (!decimal.TryParse(quote, out decimal result))
                    messageContent = $"The stock code \"{stockcode.ToUpper()}\" does not exists, please check the command";

                await SendTheNewMessage(command.Chatroom, messageContent);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private async Task SendTheNewMessage(int chatroomId, string messageContent)
        {
            var token = auth.Generate(conf["botUsername"]);
            var url = conf["ChallengeWebApiPostMessage"].Replace("{chatroomId}", chatroomId.ToString());

            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                client.DefaultRequestHeaders
                      .Accept
                      .Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "");
                request.Content = new StringContent(JsonConvert.SerializeObject(new MessageDTO(messageContent)),
                                                    Encoding.UTF8,
                                                    "application/json");
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
            }
        }

        private static async Task<string> GetQuote(string stockcode)
        {
            using (var client = new HttpClient())
            {
                using (var result = await client.GetAsync("https://stooq.com/q/l/?s=" + stockcode + "&f=sd2t2ohlcv&h&e=csv"))
                {
                    if (result.IsSuccessStatusCode)
                    {
                        using (var reader = new StreamReader(result.Content.ReadAsStream()))
                        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                        {
                            var records = csv.GetRecords<CsvStockDTO>();
                            return records.First().Close;
                        }
                    }

                }
            }
            throw new DomainException("We can't reach the quote now, try later");
        }
    }
}
