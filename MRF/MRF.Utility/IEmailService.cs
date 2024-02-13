using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRF.Utility
{
    public interface IEmailService
    {
        void SendEmailAsync(string toEmail, string subject, string htmlContent, string attachmentPath = null);
        void SendEmailAsync(int senderId, string subject, string htmlContent, int mrfId);
        public bool IsValidUpdateValue(object value);
    }
}
