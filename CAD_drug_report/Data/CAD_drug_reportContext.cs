using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CAD_drug_report.Models
{
    public class CAD_drug_reportContext : DbContext
    {
        public CAD_drug_reportContext (DbContextOptions<CAD_drug_reportContext> options)
            : base(options)
        {
        }

        public DbSet<CAD_drug_report.Models.ReportDrugIncidents> ReportDrugIncidents { get; set; }
    }
}
