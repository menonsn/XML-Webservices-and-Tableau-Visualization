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
    public class JsonDataModel : PageModel
    {
        private readonly ILogger<JsonDataModel> _logger;

        public JsonDataModel(ILogger<JsonDataModel> logger)
        {
            _logger = logger;
        }

        public JsonResult OnGet()
        {

            //Drug Reports JSON data download 
            string drugData = GetData("https://data.cincinnati-oh.gov/resource/m3rc-s9gd.json");
            List<QuickTypeDrug.Drug> alldrug = QuickTypeDrug.Drug.FromJson(drugData);


            IDictionary<string, QuickTypeDrug.Drug> drugDictionary = new Dictionary<string, QuickTypeDrug.Drug>();

            foreach (QuickTypeDrug.Drug drug in alldrug)
            {
                drugDictionary.Add(drug.EventNumber, drug);
            }


            //All police reported incidents JSON data download
            string jsonData = GetData("https://data.cincinnati-oh.gov/resource/qiik-bpks.json");


            List<QuickTypeCad.Cad> allcad = QuickTypeCad.Cad.FromJson(jsonData);

            IList<QuickTypeCad.Cad> drugReport = new List<QuickTypeCad.Cad>();


            foreach (QuickTypeCad.Cad cad in allcad)
            {
                Console.WriteLine(cad);

                if (drugDictionary.ContainsKey(cad.EventNumber))
                {
                    drugReport.Add(cad);
                }

            }

            
            ViewData["allcad"] = drugReport;

            //Return the JSON format of data merged
            return new JsonResult(drugReport);

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