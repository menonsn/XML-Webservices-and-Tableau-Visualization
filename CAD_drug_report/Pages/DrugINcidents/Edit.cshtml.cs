using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CAD_drug_report.Models;

namespace CAD_drug_report.Pages.DrugINcidents
{
    public class EditModel : PageModel
    {
        private readonly CAD_drug_report.Models.CAD_drug_reportContext _context;

        public EditModel(CAD_drug_report.Models.CAD_drug_reportContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(ReportDrugIncidents).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReportDrugIncidentsExists(ReportDrugIncidents.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ReportDrugIncidentsExists(int id)
        {
            return _context.ReportDrugIncidents.Any(e => e.Id == id);
        }
    }
}
