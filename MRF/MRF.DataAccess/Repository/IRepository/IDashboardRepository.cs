using MRF.Models.Models;
using MRF.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRF.DataAccess.Repository.IRepository
{
    public interface IDashboardRepository:IRepository<MrDashboardViewModel>
    {
        public List<MrfSummaryViewModel> GroupByMrfStatus(int roleId, int userId);
        public List<ResultViewModel> GetCountByMrfIdAndResumeStatus(int count, int roleId, int userId);
 
        public List<ResultViewModel> GroupByMrfInterviewStatus(int count, int roleId, int userId);
    }
}
