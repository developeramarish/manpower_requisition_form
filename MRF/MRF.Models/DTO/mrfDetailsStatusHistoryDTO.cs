using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRF.Models.DTO
{
    public class mrfDetailsStatusHistoryRequestModel
    {
        public int Id { get; set; }

        public int MrfId { get; set; }

        public int mrfStatusId { get; set; }

        public int CreatedByEmployeeId { get; set; }

        public DateTime CreatedOnUtc { get; set; }
        public bool IsActive { get; set; }
    }

    public class mrfDetailsStatusHistoryResponseModel
    {
        public int Id { get; set; }
        public bool IsActive { get; set; }
    }
}
