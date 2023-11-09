using MRF.DataAccess.Repository.IRepository;
using MRF.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRF.DataAccess.Repository
{
    public class MrfLastNumberRepository : Repository<MrfLastNumber>, IMrfLastNumberRepository
    {
        private readonly Data.MRFDBContext _db;
        public MrfLastNumberRepository(Data.MRFDBContext db) : base(db)
        {
            _db = db;
        }
        public void Update(MrfLastNumber MrfLastNumbers)
        {
            _db.MrfLastNumber.Update(MrfLastNumbers);
        }



    }
}

