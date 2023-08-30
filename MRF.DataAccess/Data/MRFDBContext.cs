using Microsoft.EntityFrameworkCore;
using MRF.Models.Models;

namespace MRF.DataAccess.Data
{
    public class MRFDBContext : DbContext
    {
        public MRFDBContext(DbContextOptions<MRFDBContext> options) : base(options)
        {

        }
        public DbSet<Candidatestatusmaster> Candidatestatusmaster { get; set; }
        public DbSet<Departmentmaster> Departmentmaster { get; set; }
        public DbSet<Employmenttypemaster> Employmenttypemaster { get; set; }
        public DbSet<Evaluationfeedbackmaster> Evaluationfeedbackmaster { get; set; }
        public DbSet<Evaluationmaster> Evaluationmaster { get; set; }
        public DbSet<Evaluationstatusmaster> Evaluationstatusmaster { get; set; }
        public DbSet<Gendermaster> Gendermaster { get; set; }
        public DbSet<Grademaster> Grademaster { get; set; }
        public DbSet<Locationmaster> Locationmaster { get; set; }
        public DbSet<Mrfstatusmaster> Mrfstatusmaster { get; set; }
        public DbSet<Projectmaster> Projectmaster { get; set; }
        public DbSet<Qualificationmaster> Qualificationmaster { get; set; }
        public DbSet<Rolemaster> Rolemaster { get; set; }
        public DbSet<Subdepartmentmaster> Subdepartmentmaster { get; set; }
        public DbSet<Vacancytypemaster> Vacancytypemaster { get; set; }
        public DbSet<Candidatedetails> Candidatedetails { get; set; }
        public DbSet<Employeedetail> Employeedetail { get; set; }
        public DbSet<Employeelogindetail> Employeelogindetail { get; set; }
        public DbSet<Employeerolemap> Employeerolemap { get; set; }
        public DbSet<Freshmrfdetail> Freshmrfdetail { get; set; }
        public DbSet<Interviewevaluation> Interviewevaluation { get; set; }
        public DbSet<Mrfdetails> Mrfdetails { get; set; }
        public DbSet<Mrffeedback> Mrffeedback { get; set; }
        public DbSet<Mrfinterviewermap> Mrfinterviewermap { get; set; }
        public DbSet<Mrfresumereviewermap> Mrfresumereviewermap { get; set; }
        public DbSet<Replacementmrfdetails> Replacementmrfdetails { get; set; }
        public DbSet<Resumeforwarddetails> Resumeforwarddetails { get; set; }
    }
}
