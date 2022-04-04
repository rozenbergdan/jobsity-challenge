using Challenge.Domain.DTO;
using Challenge.Service.Interfaces;
using CsvHelper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
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
            conf = conf;
        }

        

        public async void RetrieveStockMessage(string command)
        {
            string quote;
            using (var client = new HttpClient())
            {
                var stockcode = command.Split('=')[1];

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

        }
    }
}
