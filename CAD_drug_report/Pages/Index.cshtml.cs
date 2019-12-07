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

        //Creating a new list for the data to be parsed and saved
        List<Drug> DrugReport = new List<Drug>();
        Cad newCAD = new Cad();
        public void OnPost()
        {
            //Download the first set of JSON Data(Live report of Drug Incidents by Cincinnnati Police) and save it inside alldrug variable
            string drugData = GetData("https://data.cincinnati-oh.gov/resource/m3rc-s9gd.json");
            Drug[] alldrug = Drug.FromJson(drugData).ToArray();
            //Download the second set of JSON Data(Live Report of all reported incidents by Cincinnati Police) and save it in allcad variable
            string jsonData = GetData("https://data.cincinnati-oh.gov/resource/qiik-bpks.json");
            Cad[] allCAD = Cad.FromJson(jsonData).ToArray();

            //Iterating over all the drug reports
            foreach (Drug drug in alldrug)
            {
                //checking if the search term for the Nieghborhood has been entered
                if (drug.SnaNeighborhood != null && Search != null)
                {
                    //convert search term to lower case and add to the list to display based on search criterion
                    if (drug.SnaNeighborhood.ToLower() == Search.ToLower())
                    {
                        DrugReport.Add(drug);
                    }
                }
                else
                {
                    //if search term is empty then display all data
                    DrugReport.Add(drug);
                }
            }

            //Same searches and iteration over all the police incidents reported dataset
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

            //Set the datasets to display in the cshtml file
            ViewData["drugReport"] = DrugReport;
            ViewData["CAD"] = newCAD;
            SearchCompleted = true;


        }

        //Refactored code to re-use for downloading JSON datasets
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
