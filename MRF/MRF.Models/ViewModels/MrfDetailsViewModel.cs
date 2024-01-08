using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRF.Models.ViewModels
{
    public class MrfDetailsViewModel
    {
        //join mrfdetails ,Mrfstatusmaster,Freshmrfdetails ,Employeedetails
        public int MrfId { get; set; }
        public int MrfStatusId { get; set; }

        public int RoleId { get; set; }
        public string   MrfStatus { get; set; } = null!;
        public string ReferenceNo { get; set; } = null!;

        public string positionTitle { get; set; } = null!;
        public int CreatedByEmployeeId { get; set; }
        public string Name { get; set; } = null!;
        public DateTime UpdatedOnUtc { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public string Experience { get; set; } = null!;

        public int VacancyNo { get; set; }
        public string RequisitionType { get; set; } = null!;
        public string Salary { get; set; } = null!;



    }
}
