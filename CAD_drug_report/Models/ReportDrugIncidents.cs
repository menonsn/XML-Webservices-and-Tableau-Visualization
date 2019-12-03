using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CAD_drug_report.Models
{
    public class ReportDrugIncidents
    {
        public int Id { get; set; }
        [Display(Name = "Agency")]
        public string Agency { get; set; }
        [Display(Name = "Incident Type ID")]
        public int IncidentTypeID { get; set; }
        [Display(Name = "Neighborhood")]
        public string Neighborhood { get; set; }
        [Display(Name = "Priority")]
        public string PriorityColor { get; set; }
        [Display(Name = "Latitude")]
        public string Latitude { get; set; }
        [Display(Name = "Longitude")]
        public string Longitude { get; set; }
        

    }
}
