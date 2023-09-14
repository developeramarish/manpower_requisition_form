namespace MRF.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        ICandidatestatusmasterRepository Candidatestatusmaster { get; }
        IDepartmentmasterRepository Departmentmaster { get; }
        IEmploymenttypemasterRepository Employmenttypemaster { get; }
        IEvaluationfeedbackmasterRepository Evaluationfeedbackmaster { get; }
        IEvaluationmasterRepository Evaluationmaster { get; }
        IEvaluationstatusmasterRepository Evaluationstatusmaster { get; }
        IGendermasterRepository Gendermaster { get; }
        IGrademasterRepository Grademaster { get; }
        ILocationmasterRepository Locationmaster { get; }
        IMrfstatusmasterRepository Mrfstatusmaster { get; }
        IProjectmasterRepository Projectmaster { get; }
        IQualificationmasterRepository Qualificationmaster { get; }
        IRolemasterRepository Rolemaster { get; }
        ISubdepartmentmasterRepository Subdepartmentmaster { get; }
        IVacancytypemasterRepository Vacancytypemaster { get; }
        IMrfdetailRepository Mrfdetail { get; }
        IMrffeedbackRepository Mrffeedback { get; }
        IMrfinterviewermapRepository Mrfinterviewermap { get; }
        IMrfresumereviewermapRepository Mrfresumereviewermap { get; }
        IReplacementmrfdetailRepository Replacementmrfdetail { get; }
        IResumeforwarddetailRepository Resumeforwarddetail { get; }
        IEmployeedetailsRepository Employeedetails { get; }
        ICandidatedetailRepository Candidatedetail { get; }
        IEmployeelogindetailRepository Employeelogindetail { get; }
        IEmployeerolemapRepository Employeerolemap { get; }
        IFreshmrfdetailRepository Freshmrfdetail { get; }
        IDashboardRepository Dashboard { get; }
        IMrfStatusDetailsRepository MrfStatusDetail { get; }
        IResumeDetailsRepository ResumeDetail { get; }
        IInterviewDetailsRepository InterviewDetail { get; }
        void Save();
    }
}
