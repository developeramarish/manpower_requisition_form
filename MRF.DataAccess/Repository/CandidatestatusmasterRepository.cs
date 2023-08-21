using MRF.DataAccess.Repository.IRepository;
using MRF.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRF.DataAccess.Repository
{
    public class CandidatestatusmasterRepository : Repository<Candidatestatusmaster>, ICandidatestatusmasterRepository
    {
        private readonly Data.MRFDBContext _db;
        public CandidatestatusmasterRepository(Data.MRFDBContext db) : base(db)
        {
            _db = db;
        }
        public void Update(Candidatestatusmaster candidatestatusmaster)
        {
            _db.Candidatestatusmaster.Update(candidatestatusmaster);
        }
    }
}
