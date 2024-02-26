using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRF.Models.Models
{
    public class MrfEmailApproval
    {
        public int Id { get; set; }
        public int MrfId { get; set; }
        public int EmployeeId { get; set; }
        
        public int RoleId { get; set; } 
        public DateOnly ApprovalDate { get; set; }
    }
}
