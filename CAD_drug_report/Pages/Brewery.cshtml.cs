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
using Brewery;

namespace CAD_drug_report.Pages
{
    public class BreweryModel : PageModel
    {
        private readonly ILogger<BreweryModel> _logger;
        List<BreweryData> allBreweries = new List<BreweryData>();

        public BreweryModel(ILogger<BreweryModel> logger)
        {
            _logger = logger;
        }

        BreweryData newBrewery = new BreweryData();

        //Using Sidney Schaepers JSON dataset to display all Brewery Data in Cincinnati
        public void OnGet()
        {

            string BreweryAllData = GetData("https://breweryratings20191109050130.azurewebsites.net/Privacy");
            BreweryData[] allBrewery = BreweryData.FromJson(BreweryAllData);

            foreach (Brewery.BreweryData brewery in allBrewery)
            {
                if (brewery.Location.City != null)
                {
                    allBreweries.Add(brewery);
                }

            }
            ViewData["Brewery"] = allBreweries;


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

