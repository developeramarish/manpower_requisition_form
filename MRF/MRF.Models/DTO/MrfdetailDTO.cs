using System.Net;

namespace MRF.Models.DTO
{
    public class MrfdetailRequestModel
    {
        public int? mrfID { get; set; }
        public string? RequisitionType { get; set; }
        public string? ReferenceNo { get; set; } 
        public int? PositionTitleId { get; set; }
        public int? DepartmentId { get; set; } 
        public int? SubDepartmentId { get; set; }
        public int? ProjectId { get; set; }
        public int VacancyNo { get; set; }
        public int? GenderId { get; set; }
        public DateOnly RequisitionDateUtc { get; set; }
        public int? ReportsToEmployeeId { get; set; }
        public int? MinGradeId { get; set; }
        public int? MaxGradeId { get; set; }
        public int? EmploymentTypeId { get; set; }
        public int? MinExperience { get; set; }
        public int? MaxExperience { get; set; }
        public int? VacancyTypeId { get; set; }
        public bool IsReplacement { get; set; } = false;
        public int MrfStatusId { get; set; }
        public string? JdDocPath { get; set; } 
        public int? LocationId { get; set; }
        public int? QualificationId { get; set; }
        public int CreatedByEmployeeId { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public int UpdatedByEmployeeId { get; set; }
        public DateTime UpdatedOnUtc { get; set; }


        public string? Justification { get; set; }

        public string? JobDescription { get; set; }

        public string? Skills { get; set; } 

        public float? MinTargetSalary { get; set; }

        public float? MaxTargetSalary { get; set; }

        public string? EmployeeName { get; set; }
        public string? EmailId { get; set; } 
        public int EmployeeCode { get; set; }
        public DateOnly LastWorkingDate { get; set; }
        public string? ReplaceJustification { get; set; }
        public float AnnualCtc { get; set; }
        public float AnnualGross { get; set; }

        public int ResumeReviewerEmployeeId { get; set; }
        public int InterviewerEmployeeId { get; set; }
        public string? ResumeReviewerEmployeeIds { get; set; } = string.Empty;
        public string? InterviewerEmployeeIds { get; set; } = string.Empty;

        public int  HiringManagerId { get; set; }
        public int HiringManagerEmpId { get; set; }
        public DateOnly HMApprovalDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        public int FunctionHeadId { get; set; }
        public int FunctionHeadEmpId { get; set; }
        public DateOnly FHApprovalDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        public int  SiteHRSPOCId { get; set; }
        public int SiteHRSPOCEmpId { get; set; }
        public DateOnly SPApprovalDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        public int FinanceHeadId { get; set; }
        public int FinanceHeadEmpId { get; set; }
        public DateOnly FIApprovalDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        public int PresidentnCOOId { get; set; }
        public int PresidentnCOOEmpId { get; set; }
        public DateOnly PCApprovalDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        public int roleId { get; set; }

        public string? roleIds { get; set; }

        public string? Note { get; set; }

        public string? MrfStatus { get; set; }

        public int? HrId { get; set; }

        public int? RejectedById { get; set; }

    }
    public class MrfdetaiResponseModel
    {
        public int Id { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public String message { get; set; }
    }

}
