using MRF.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRF.DataAccess.Repository.IRepository
{
    public interface IResumeDetailsRepository: IRepository<ResumeDetailsViewModel>
    {
        public List<ResumeDetailsViewModel> GetResumeStatusDetails(int mrfId);
    }
}
