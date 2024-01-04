using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MRF.Models.DTO
{
    public class MrfEmailApprovalRequestModel
    {
        public int Id { get; set; }
        public int MrfId { get; set; }
        public int EmployeeId { get; set; }
        public DateOnly ApprovalDate { get; set; }
    }

    public class MrfEmailApprovalResponseModel
    {
        public int Id { get; set; }
        
    }
}
