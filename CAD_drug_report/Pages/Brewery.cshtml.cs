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
        List<BreweryJson> allBreweries = new List<BreweryJson>();

        public BreweryModel(ILogger<BreweryModel> logger)
        {
            _logger = logger;
        }

        BreweryJson newBrewery = new BreweryJson();

        public void OnGet()
        {

            string breweryJson = GetData("https://breweryratings20191109050130.azurewebsites.net/Privacy");
            BreweryJson[] allBrewery = BreweryJson.FromJson(breweryJson);

            foreach (Brewery.BreweryJson brewery in allBrewery)
            {
                if (brewery.Location.City != null)
                {
                    allBreweries.Add(brewery);
                }

            }
            ViewData["allBrewery"] = allBreweries;


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

