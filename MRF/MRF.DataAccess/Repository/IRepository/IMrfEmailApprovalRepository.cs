using MRF.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRF.DataAccess.Repository.IRepository
{
    public interface IMrfEmailApprovalRepository : IRepository<MrfEmailApproval>
    {
        public List<MrfEmailApproval> GetList(int mrfId);

        MrfEmailApproval GetListBymrfIdandEmployeeId(int mrfId, int empId);

        public void Update(MrfEmailApproval MrfEmailApproval);

        public List<int> GetListFromMrfIds(List<int> mrfIds, int empId, int roleId);
    }
}
