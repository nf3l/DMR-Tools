using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//Libraries needed for HTTP GET
using System.Net;
using System.Net.Http;
using System.IO;

//Library for JSON (requires a reference which is basically a dll I downloaded online, this is already configured in this project)
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;


namespace NF3L
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //use const if you want, not a rule though (who cares)
        //lets make a string called baseURL so we dont have to type this a million times
        public const string baseURL = "https://kg5rki.com/";

        //quick suffix 
        public const string sfxCall = "qrz.php?callsign=";


        //you start a function name with either public or private, just use public all the time who cares..
        public string get_callsign_info(string callsign)
        {
            using (var client = new HttpClient(new HttpClientHandler { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate }))
            {
                //put the url of the api here, so the base part of the url address
                client.BaseAddress = new Uri(baseURL);

                //Add suffix to end of address.. add callsign to end of that
                HttpResponseMessage response = client.GetAsync(sfxCall + callsign).Result;

                response.EnsureSuccessStatusCode();

                //Get result out as string, return it from function
                string result = response.Content.ReadAsStringAsync().Result;
                return result;
            }
        }


        //event handler for button click
        private void btnGO_Click(object sender, EventArgs e)
        {
            //Grab callsign to use from textbox on GUI
            tbOutput.Text = " ";
            string call = this.tbCall.Text;

            //Lets the callsign's info.. store it in a string (callInfo)
            string callInfo = get_callsign_info(call);

            //Parse json string into array of objects (because each callsign can have more than one dmr ID so more than one result)
            QRZInfo[] parsedObj = JsonConvert.DeserializeObject<QRZInfo[]>(callInfo);

            //Clear textbox and add header to output
            // this.tbOutput.Text = "  \r\n";

            //Go through each result (dmr ID) and show the info for it
            foreach (QRZInfo qrz in parsedObj)
            {
                //newline, add some space..
                this.tbOutput.Text += "\r\n";

                //Show ID first
                this.tbOutput.Text += qrz.radio_id + "\r\n";

                //Then name 
                this.tbOutput.Text += qrz.name + "\r\n";

                //City
                this.tbOutput.Text += qrz.city + ",";

                //State
                this.tbOutput.Text += qrz.state + "\r\n";

                //Add whatever else, there are a lot more properties in the qrz object. Lat, Lng, qrz image url, etc...
                this.tbOutput.Text += qrz.country + "\r\n";
            }
            
            // (insert more magic goodies here)
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LH_timer.Start();
            updateLH();
        }

        private void tbOutput_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            label1.Visible = true;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            tbCall.Text = " ";
            tbOutput.Text = " ";
            label1.Text = " ";


        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        public void updateLH()
        {
            LHDataInfo.LHData[] LH = LHDataInfo.LH_Data_Get(15); //look backwards in time by 5 seconds (last heard in 5 seconds)
            foreach (LHDataInfo.LHData lh in LH)
            {
                tbLH.Text = ( lh.callsign + " - " + lh.talkgroup + " - " + lh.duration + "s \r\n") + tbLH.Text;
            }
        }

        private void LH_timer_Tick(object sender, EventArgs e)
        {
            updateLH();
        }

		private void tabPage1_Click(object sender, EventArgs e)
		{

		}

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
