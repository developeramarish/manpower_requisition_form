using MRF.DataAccess.Repository.IRepository;

namespace MRF.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly Data.MRFDBContext _db;
        public ICandidatestatusmasterRepository Candidatestatusmaster { get; private set; }
        public IDepartmentmasterRepository Departmentmaster { get; private set; }
        public IEmploymenttypemasterRepository Employmenttypemaster { get; private set; }
        public IEvaluationfeedbackmasterRepository Evaluationfeedbackmaster { get; private set; }
        public IEvaluationmasterRepository Evaluationmaster { get; private set; }
        public IEvaluationstatusmasterRepository Evaluationstatusmaster { get; private set; }
        public IGendermasterRepository Gendermaster { get; private set; }
        public IGrademasterRepository Grademaster { get; private set; }
        public ILocationmasterRepository Locationmaster { get; private set; }
        public IMrfstatusmasterRepository Mrfstatusmaster { get; private set; }
        public IProjectmasterRepository Projectmaster { get; private set; }
        public IQualificationmasterRepository Qualificationmaster { get; private set; }
        public IRolemasterRepository Rolemaster { get; private set; }
        public ISubdepartmentmasterRepository Subdepartmentmaster { get; private set; }
        public IVacancytypemasterRepository Vacancytypemaster { get; private set; }

        public UnitOfWork(Data.MRFDBContext db)
        {
            _db = db;
            Candidatestatusmaster = new CandidatestatusmasterRepository(_db);
            Departmentmaster = new DepartmentmasterRepository(_db);
            Employmenttypemaster = new EmploymenttypemasterRepository(_db);
            Evaluationfeedbackmaster = new EvaluationfeedbackmasterRepository(_db);
            Evaluationmaster = new EvaluationmasterRepository(_db);
            Evaluationstatusmaster = new EvaluationstatusmasterRepository(_db);
            Gendermaster = new GendermasterRepository(_db);
            Grademaster = new GrademasterRepository(_db);
            Locationmaster = new LocationmasterRepository(_db);
            Mrfstatusmaster = new MrfstatusmasterRepository(_db);
            Projectmaster = new ProjectmasterRepository(_db);
            Qualificationmaster = new QualificationmasterRepository(_db);
            Rolemaster = new RolemasterRepository(_db);
            Subdepartmentmaster = new SubdepartmentmasterRepository(_db);
            Vacancytypemaster = new VacancytypemasterRepository(_db);
        }
        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
