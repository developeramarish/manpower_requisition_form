using MRF.DataAccess.Repository.IRepository;
using MRF.Models.Models;
using MRF.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRF.DataAccess.Repository
{
    public class MrfEmailApprovalRepository:Repository<MrfEmailApproval>, IMrfEmailApprovalRepository
    {
        private readonly Data.MRFDBContext _db;
        public MrfEmailApprovalRepository(Data.MRFDBContext db) : base(db)
        {
            _db = db;
        }
        public void Update(MrfEmailApproval MrfEmailApproval)
        {
            _db.MrfEmailApproval.Update(MrfEmailApproval);
        }

        public List<MrfEmailApproval> GetList(int mrfId)
        {

            IQueryable<MrfEmailApproval> query = from mrfDetails in _db.MrfEmailApproval
                                                 where mrfDetails.MrfId == mrfId
                                                 select mrfDetails;
                                                       
            return query.ToList();
        }
    }
}
