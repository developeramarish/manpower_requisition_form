using MRF.Models.Models;
using MRF.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MRF.DataAccess.Repository.IRepository
{
    public interface IMrfStatusDetailsRepository: IRepository<MrfDetailsViewModel>
    {
        public List<MrfDetailsViewModel> GetMrfStatusDetails(int statusId,int roleId, int userId);
    }
}
