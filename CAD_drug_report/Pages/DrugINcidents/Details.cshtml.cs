using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CAD_drug_report.Models;

namespace CAD_drug_report.Pages.DrugINcidents
{
    public class DetailsModel : PageModel
    {
        private readonly CAD_drug_report.Models.CAD_drug_reportContext _context;

        public DetailsModel(CAD_drug_report.Models.CAD_drug_reportContext context)
        {
            _context = context;
        }

        public ReportDrugIncidents ReportDrugIncidents { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ReportDrugIncidents = await _context.ReportDrugIncidents.FirstOrDefaultAsync(m => m.Id == id);

            if (ReportDrugIncidents == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
