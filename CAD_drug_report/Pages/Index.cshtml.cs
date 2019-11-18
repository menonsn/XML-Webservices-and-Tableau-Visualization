using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;

namespace CAD_drug_report.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            //Web Client gives us access to data on the internet
            using (WebClient webClient = new WebClient())
            {
               //Drug Reports data
               string drugData = webClient.DownloadString("https://data.cincinnati-oh.gov/resource/m3rc-s9gd.json");

               List<QuickTypeDrug.Drug> alldrug = QuickTypeDrug.Drug.FromJson(drugData);

               IDictionary<string, QuickTypeDrug.Drug> drugDictionary = new Dictionary<string, QuickTypeDrug.Drug>();
                
                foreach(QuickTypeDrug.Drug drug in alldrug)
                {
                    drugDictionary.Add(drug.EventNumber, drug);
                }

               //CAD data
               string jsonData = webClient.DownloadString("https://data.cincinnati-oh.gov/resource/qiik-bpks.json");
              
               List<QuickTypeCad.Cad> allcad = QuickTypeCad.Cad.FromJson(jsonData);

               IList<QuickTypeCad.Cad> drugReport = new List<QuickTypeCad.Cad>();
                IList<QuickTypeCad.Cad> AddressX = new List<QuickTypeCad.Cad>();


                // get the raw JSON data.
                jsonData = ("https://data.cincinnati-oh.gov/resource/qiik-bpks.json");

               
                string schemaString = System.IO.File.ReadAllText("https://data.cincinnati-oh.gov/resource/qiik-bpks.json");
                
                JSchema schema = JSchema.Parse(schemaString);
                
                JObject jsonObject = JObject.Parse(jsonData);
                
                bool valid = jsonObject.IsValid(schema);

                foreach (QuickTypeCad.Cad cad in allcad)
                {
                  
                    Console.WriteLine(cad);

                 
                    if (drugDictionary.ContainsKey(cad.AddressX))
                    {
                       
                        AddressX.Add(cad);
                    }
                }




                foreach (QuickTypeCad.Cad cad in allcad)
                {
                    Console.WriteLine(cad);

                    if(drugDictionary.ContainsKey(cad.EventNumber))
                    {
                        drugReport.Add(cad);
                    }

                }//end of foreach()

                ViewData["allcad"] = drugReport;

            }//end of using()
        }
    }
}
