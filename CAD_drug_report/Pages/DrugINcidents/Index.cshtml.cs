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
    public class IndexModel : PageModel
    {
        private readonly CAD_drug_report.Models.CAD_drug_reportContext _context;

        public IndexModel(CAD_drug_report.Models.CAD_drug_reportContext context)
        {
            _context = context;
        }

        public IList<ReportDrugIncidents> ReportDrugIncidents { get;set; }

        public async Task OnGetAsync()
        {
            ReportDrugIncidents = await _context.ReportDrugIncidents.ToListAsync();
        }
    }
}
