using MRF.DataAccess.Data;
using MRF.DataAccess.Repository.IRepository;

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
