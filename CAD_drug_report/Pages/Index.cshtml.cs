using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using QuickTypeDrug;
using QuickTypeCad;


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

            SearchCompleted = false;

        }

        [BindProperty]
        public string Search { get; set; }

        public bool SearchCompleted { get; set; }

        List<Drug> DrugReport = new List<Drug>();
        Cad newCAD = new Cad();
        public void OnPost()
        {

            string drugData = GetData("https://data.cincinnati-oh.gov/resource/m3rc-s9gd.json");
            Drug[] alldrug = Drug.FromJson(drugData).ToArray();
            string jsonData = GetData("https://data.cincinnati-oh.gov/resource/qiik-bpks.json");
            Cad[] allCAD = Cad.FromJson(jsonData).ToArray();
            foreach (Drug drug in alldrug)
            {
                if (drug.SnaNeighborhood != null && Search != null)
                {
                    if (drug.SnaNeighborhood.ToLower() == Search.ToLower())
                    {
                        DrugReport.Add(drug);
                    }
                }
                else
                {
                    DrugReport.Add(drug);
                }
            }
            foreach (Cad caddata in allCAD)
            {
                if (caddata.SnaNeighborhood != null && Search != null)
                {
                    if (caddata.SnaNeighborhood.ToLower() == Search.ToLower())
                    {
                        newCAD = caddata;
                    }
                }
                else
                {
                    newCAD = caddata;
                }
            }
            ViewData["drugReport"] = DrugReport;
            ViewData["CAD"] = newCAD;
            SearchCompleted = true;

            //string neighborhoodData = GetData("https://data.cincinnati-oh.gov/resource/h8mv-4fsc.json");
            //List<NeighborhoodData> neighborhoods = NeighborhoodData.FromJson(neighborhoodData);
            //ViewData["Houses"] = neighborhoods;


        }
        public string GetData(string endpoint)
        {
            string downloadedJson;
            using (WebClient webClient = new WebClient())
            {
                downloadedJson = webClient.DownloadString(endpoint); ;
            }
            return downloadedJson;
        }
    }

}
