using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

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

            //Drug Reports data
            string drugData = GetData("https://data.cincinnati-oh.gov/resource/m3rc-s9gd.json"); 
            List<QuickTypeDrug.Drug> alldrug = QuickTypeDrug.Drug.FromJson(drugData);


            IDictionary<string, QuickTypeDrug.Drug> drugDictionary = new Dictionary<string, QuickTypeDrug.Drug>();
                
            foreach(QuickTypeDrug.Drug drug in alldrug)
            {
               drugDictionary.Add(drug.EventNumber, drug);
            }


            //CAD data
            string jsonData = GetData("https://data.cincinnati-oh.gov/resource/qiik-bpks.json");

              
            List<QuickTypeCad.Cad> allcad = QuickTypeCad.Cad.FromJson(jsonData);

            IList<QuickTypeCad.Cad> drugReport = new List<QuickTypeCad.Cad>();


            foreach (QuickTypeCad.Cad cad in allcad)
            {
               Console.WriteLine(cad);

               if(drugDictionary.ContainsKey(cad.EventNumber))
               {
                  drugReport.Add(cad);
               }

            }//end of foreach()

            ViewData["allcad"] = drugReport;

        }

        public string GetData(string endpoint)

        {
            string downloadedData = "";
            using (WebClient webClient = new WebClient())
            {
                downloadedData = webClient.DownloadString(endpoint);

            }
            return downloadedData;
        }

    }

}
