using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Library for JSON (requires a reference which is basically a dll I downloaded online, this is already configured in this project)
using Newtonsoft.Json.Linq;


//This is a class created to represent the data returned by the HTTP GET request.  You just go to the site in a browser and copy the json string, 
//   then right click Edit->Paster special->Paste JSON as Classes. In your case it will be these classes

namespace NF3L
{

    public class Rootobject
    {
        public QRZInfo[] results { get; set; }
        public string status { get; set; }
    }

    public class QRZInfo
    {
        public string id { get; set; }
        public string radio_id { get; set; }
        public string callsign { get; set; }
        public string name { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string country { get; set; }
        public string remarks { get; set; }
        public string image { get; set; }
        public string lat { get; set; }
        public string lon { get; set; }
    }
}

