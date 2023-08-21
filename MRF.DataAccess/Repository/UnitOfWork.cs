using MRF.DataAccess.Data;
using MRF.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRF.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MRFDBContext _db;
        public ICandidatestatusmasterRepository Candidatestatusmaster { get; private set; }        

        public UnitOfWork(MRFDBContext db)
        {
            _db = db;
            Candidatestatusmaster = new CandidatestatusmasterRepository(_db);
        }
        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
