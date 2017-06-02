using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Libraries needed for HTTP GET
using System.Net;
using System.Net.Http;
using System.IO;

//Library for JSON (requires a reference which is basically a dll I downloaded online, this is already configured in this project)
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace NF3L
{

    static public class LHDataInfo
    {
       
        public const string LHDataSfx = "lastheard.php?TIME=";

        
        static public LHData[] LH_Data_Get(int secondsBack)
        {
            long epochTicks = new DateTime(1970, 1, 1).Ticks;
            long unixTime = ((DateTime.UtcNow.Ticks - epochTicks) / TimeSpan.TicksPerSecond);
            unixTime -= secondsBack;

            using (var client = new HttpClient(new HttpClientHandler { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate }))
            {
                //put the url of the api here, so the base part of the url address
                client.BaseAddress = new Uri("https://kg5rki.com/");

                //Add suffix to end of address.. add current unix timestamp here..
                HttpResponseMessage response = client.GetAsync(LHDataSfx + unixTime).Result;

                response.EnsureSuccessStatusCode();

                //Get result out as string, return it from function
                string result = response.Content.ReadAsStringAsync().Result;

                LHData[] parsedObj = JsonConvert.DeserializeObject<LHData[]>(result);

                return parsedObj;
            }
        }

        public class LHDataRoot
        {
            public LHData[] results { get; set; }
            public string status { get; set; }
        }

        public class LHData
        {
            public string id { get; set; }
            public string radio_id { get; set; }
            public string talkgroup { get; set; }
            public string timeout { get; set; }
            public string duration { get; set; }
            public string timestamp { get; set; }
            public string callsign { get; set; }
            public string repeater { get; set; }
            public string city { get; set; }
            public string state { get; set; }
            public string country { get; set; }
            public string imgurl { get; set; }
            public string name { get; set; }
            public string lat { get; set; }
            public string lon { get; set; }
            public string uid { get; set; }
            public string time { get; set; }
        }

    }
}
