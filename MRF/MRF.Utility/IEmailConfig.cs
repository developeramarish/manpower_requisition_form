using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRF.Utility
{
     public interface IEmailConfig
    {
         void SendEmail(string receiverEmail, string subject, string body, string? attachmentPath = null);
    }
}
 
 