using System.Net;

namespace MRF.Models.DTO
{
    public class MrfdetailsPDFRequestModel
    {
        public int Id { get; set; }
        public string? ReferenceNo { get; set; } = null!;
        public string? RequisitionType { get; set; }
        public int? PositionTitleId { get; set; }
        public string? Department { get; set; }
        public string? SubDepartment { get; set; }
        public string? Project { get; set; }
        public string? Location { get; set; }
        public string? PositionReportingto { get; set; }
        public DateOnly? HiringInitiationDate { get; set; }
        public string? GradeMin { get; set; }
        public string? GradeMax { get; set; }
        public string? TypeOfEmployment { get; set; }
        public bool ReplacementForThEmployee { get; set; }
        public int NumberOfVacancies { get; set; }
        public int? ExperienceMin { get; set; }
        public string? TypeOfVacancy { get; set; }
        public int? ExperienceMax { get; set; }
        public string? Gender { get; set; }
        public string? Qualification { get; set; }
        public string? JobDescription { get; set; }
        public string? Skills { get; set; }
        public string? Justification { get; set; }
        public int? MinTargetSalary { get; set; }
        public int? MaxTargetSalary { get; set; }
    }

    public class MrfdetailsPDFResponseModel
    {
        public int Id { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public String message { get; set; }
    }
}
