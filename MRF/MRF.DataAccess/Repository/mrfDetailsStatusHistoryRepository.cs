using MRF.DataAccess.Repository.IRepository;
using MRF.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRF.DataAccess.Repository
{
    public class mrfDetailsStatusHistoryRepository : Repository<mrfDetailsStatusHistory>, ImrfDetailsStatusHistory
    {
        private readonly Data.MRFDBContext _db;
        public mrfDetailsStatusHistoryRepository(Data.MRFDBContext db) : base(db)
        {
            _db = db;
        }

        public void Update(mrfDetailsStatusHistory mrfDetailsStatusHistory)
        {
            _db.mrfDetailsStatusHistory.Update(mrfDetailsStatusHistory);
        }
    }

}
