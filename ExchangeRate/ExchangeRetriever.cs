using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Diagnostics;
using System.IO.IsolatedStorage;
using System.IO;

namespace ExchangeRate
{
    class ExchangeRetriever
    {
        const string MyDirectory = "offline";
        readonly string _offlineDataFile = Path.Combine(MyDirectory, "rates.json");

        private OpenExchangeResponse currencyJsonData = null;
        private bool fromFile = false;

        /* under the free license the base will always be USD **/
        /* this can be modified for an enterprise license by adding &base=CDN to the request */

       // http://openexchangerates.org/api/latest.json?app_id=cab5b2a0c3334847b6c62f2d32e0e954

        [DataContract]
        public class OpenExchangeResponse
        {
            [DataMember(Name = "disclaimer")]
            public string Disclaimer { get; set; }

            [DataMember(Name = "license")]
            public string License { get; set; }

            [DataMember(Name = "timestamp")]
            public string Timestamp { get; set; }

            [DataMember(Name = "base")]
            public string Base { get; set; }

            [DataMember(Name = "rates")]
            public Dictionary<string, decimal> Rates { get; set; }
        }


        public ExchangeRetriever()
        {

            RetrieveExchangeRates();        
            
        }

        public void RetrieveExchangeRates()
        {
            /*
             * •We create a new WebClient object (called webClient). 
             * This contains the methods and variables needed to make a request, and retrieve a webpage.
                •We then add a new event listener, called webClient_DownloadStringCompleted,
             * and tell the webClient we want to call this event when it has retrieved the data. 
             * This method (which we will add shortly) will parse our retrieved json file.
                •We then call our download method, and the download initiates asynchronously 
             * (ie. we don't wait for it).  When it has completed, 
             * the event handler specified above will automatically be called.
            */

            WebClient webClient = new WebClient();
            webClient.DownloadStringCompleted += new DownloadStringCompletedEventHandler(webClient_DownloadStringCompleted);
            webClient.DownloadStringAsync(new Uri("http://openexchangerates.org/api/latest.json?app_id=cab5b2a0c3334847b6c62f2d32e0e954"));

        }

        /* event listener */

        private void webClient_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            string json = e.Result;
            if (!string.IsNullOrEmpty(json))
            {
                currencyJsonData = JsonConvert.DeserializeObject<OpenExchangeResponse>(json);
                /* TESTING AND DEBUGGING
                 * String gg;
                 Dictionary<string, decimal> dd = currencyJsonData.Rates;
                 foreach (KeyValuePair<string, decimal> objKVP in dd)
                 {
                     gg= objKVP.Key + " - " + objKVP.Value ;
                     Debug.WriteLine(gg);

                 }
                 END TESTING AND DEBUGGING */

               
                //display the dictionary objects

                WriteToFile(json);
            }
        }

        public float GetExchangeRate(string country)
        {
            //FOR TESTING
            //currencyJsonData = null;
            //END FOR TESTING
            float val;
            if (currencyJsonData != null)
            {
               val= GetRate(country);
            }
            else   // no dynamic data use data stored in file
            {
                ReadFromFile();
                if (currencyJsonData != null)
                {
                    fromFile = true;
                    val = GetRate(country);
                }
                else
                {
                    val = -2.0f;
                }
            } 
            return val;
         }

        /* the function Writetofile
         * Writes the serialized json string to a local file
         * This file is used to extract data if the service is not available
         * @param String- the json serialized string that was downloaded
         * */
        private void WriteToFile(String json)
        {
            var store = IsolatedStorageFile.GetUserStoreForApplication();
            if (!store.DirectoryExists(MyDirectory))
            {
                store.CreateDirectory(MyDirectory);
            }


            using (var fileStream = new IsolatedStorageFileStream(_offlineDataFile, FileMode.Create, store))
            {
                using (var stream = new StreamWriter(fileStream))
                {
                    stream.Write(json);
                }
            }

        }
        /* the function ReadFromfile
        * Opens the file and creates a json object
        * */
        private void ReadFromFile()
        {
            var store = IsolatedStorageFile.GetUserStoreForApplication();
            


            using (var fileStream = new IsolatedStorageFileStream(_offlineDataFile, FileMode.Open, store))
            {
                using (var stream = new StreamReader(fileStream))
                {
                    var data=stream.ReadToEnd();
                    currencyJsonData = JsonConvert.DeserializeObject<OpenExchangeResponse>(data);
                }
            }

        }

        private float GetRate(String country)
        {
            float val;
            try
            {
                decimal rate = currencyJsonData.Rates[country]; // decimal is like float, but used for currency
                val = (float)rate;    // convert to float
            }
            catch (KeyNotFoundException)
            {
                val = -1.0f;
            }
            return val;
        }

        public bool GetFromFile()
        {
            return fromFile;
        }
    }
}
