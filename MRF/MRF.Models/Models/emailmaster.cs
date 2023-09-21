using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRF.Models.Models
{
    public class emailmaster
    {
        public int Id { get; set; }
        public string emailTo { get; set; } = null!;
        //public string emailToUserName { get; set; } = null!;
        //public string fromEmail { get; set; } = null!;
        //public string fromEmaliUserName { get; set; } = null!;
        //public string htmlContent { get; set; } = null!;
        public string Content { get; set; } = null!;
        public string status { get; set; } = null!;
        public string Subject { get; set; } = null!;
    }
}
