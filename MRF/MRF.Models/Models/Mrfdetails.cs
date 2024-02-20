namespace MRF.Models.Models;

public class Mrfdetails
{
    public int Id { get; set; }

    public string? ReferenceNo { get; set; } = null!;
    public string? RequisitionType { get; set; }

    public  int? PositionTitleId { get; set; } 

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

    public bool IsReplacement { get; set; }

    public int MrfStatusId { get; set; }

    public string? JdDocPath { get; set; } 

    public int? LocationId { get; set; }
    public int? QualificationId { get; set; }
    public int CreatedByEmployeeId { get; set; }

    public DateTime CreatedOnUtc { get; set; }

    public int UpdatedByEmployeeId { get; set; }

    public DateTime UpdatedOnUtc { get; set; }

    public string?  Note { get; set; }

    public int?  HrId { get; set; }


}
