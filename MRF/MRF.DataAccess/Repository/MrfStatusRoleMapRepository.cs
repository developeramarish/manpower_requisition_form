using MRF.DataAccess.Repository.IRepository;
using MRF.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MRF.DataAccess.Repository
{
   
    public class MrfStatusRoleMapRepository : Repository<MrfStatusRoleMap>, IMrfStatusRoleMapRepository
    {
        private readonly Data.MRFDBContext _db;
        public MrfStatusRoleMapRepository(Data.MRFDBContext db) : base(db)
        {
            _db = db;
        }
        public void Update(MrfStatusRoleMap MrfStatusRoleMap)
        {
            _db.Update(MrfStatusRoleMap);
        }

        
    }
}
