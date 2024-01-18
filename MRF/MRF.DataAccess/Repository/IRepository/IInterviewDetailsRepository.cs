using MRF.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRF.DataAccess.Repository.IRepository
{
    public interface IInterviewDetailsRepository: IRepository<InterviewDetailsViewModel>
    {
        public List<InterviewDetailsViewModel> GetInterviewDetails(int mrfId, int roleId, int userId);
    }
}
