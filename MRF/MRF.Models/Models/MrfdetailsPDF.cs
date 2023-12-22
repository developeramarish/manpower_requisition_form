namespace MRF.Models.Models;

public class MrfdetailsPDF
{
    public int Id { get; set; }
    public string? ReferenceNo { get; set; } = null!;
    public string? RequisitionType { get; set; }
    public string? PositionTitle { get; set; } = null!;
    public string? Department { get; set; }
    public string? SubDepartment { get; set; }
    public string? Project { get; set; }
    public string? Location { get; set; }
    public string? PositionReportingto { get; set; }
    public string? HiringInitiationDate { get; set; }
    public string? GradeMin { get; set; }
    public string? GradeMax { get; set; }
    public string? TypeOfEmployment { get; set; }
    public bool ReplacementForThEmployee { get; set; }
    public int NumberOfVacancies { get; set; }
    public string? ExperienceMin { get; set; }
    public string? TypeOfVacancy { get; set; }
    public string? ExperienceMax { get; set; }
    public string? Gender { get; set; }
    public string? Qualification { get; set; }
    public string? JobDescription { get; set; }
    public string? Skills { get; set; }
    public string? Justification { get; set; }
    public int? MinTargetSalary { get; set; }
    public int? MaxTargetSalary { get; set; }
}
