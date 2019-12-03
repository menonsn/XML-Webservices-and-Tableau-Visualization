using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using CAD_drug_report.Models;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace CAD_drug_report.Pages.DrugINcidents
{
    public class CreateModel : PageModel
    {
        //private readonly CAD_drug_report.Models.CAD_drug_reportContext _context;
        private readonly IHostingEnvironment _environment;

        //public CreateModel(CAD_drug_report.Models.CAD_drug_reportContext context)
        //{
        //    _context = context;
        //}

        public CreateModel(IHostingEnvironment environment)
        {
            _environment = environment;
        }
        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public ReportDrugIncidents ReportDrugIncidents { get; set; }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

           

            string addition = ReportDrugIncidents.Agency + "," + ReportDrugIncidents.Neighborhood+ "," + ReportDrugIncidents.IncidentTypeID + ","
          + ReportDrugIncidents.Latitude + "," + ReportDrugIncidents.Longitude + "," + ReportDrugIncidents.PriorityColor;
            string path = Path.Combine(_environment.ContentRootPath, "ReportDrugIncidents.txt");
                       
            System.IO.File.AppendAllText(path, addition + Environment.NewLine);
             return RedirectToPage("./Index");
        }
    }
}
