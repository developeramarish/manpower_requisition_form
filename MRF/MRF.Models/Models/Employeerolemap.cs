namespace MRF.Models.Models;

public class Employeerolemap
{
    public int Id { get; set; }

    public int EmployeeId { get; set; }

    public int RoleId { get; set; }

    public bool IsActive { get; set; }

    public int CreatedByEmployeeId { get; set; }

    public DateTime CreatedOnUtc { get; set; }

    public int UpdatedByEmployeeId { get; set; }

    public DateTime UpdatedOnUtc { get; set; }
}
