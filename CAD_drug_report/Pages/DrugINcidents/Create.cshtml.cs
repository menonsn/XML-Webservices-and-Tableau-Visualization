using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using CAD_drug_report.Models;

namespace CAD_drug_report.Pages.DrugINcidents
{
    public class CreateModel : PageModel
    {
        private readonly CAD_drug_report.Models.CAD_drug_reportContext _context;

        public CreateModel(CAD_drug_report.Models.CAD_drug_reportContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public ReportDrugIncidents ReportDrugIncidents { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.ReportDrugIncidents.Add(ReportDrugIncidents);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
