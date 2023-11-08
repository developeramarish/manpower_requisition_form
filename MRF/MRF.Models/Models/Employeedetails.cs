using System.ComponentModel.DataAnnotations.Schema;

namespace MRF.Models.Models;

public class Employeedetails
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string ContactNo { get; set; } = null!;
    
    [NotMapped]
    public string  RoleName { get; set; } = null!;
   
    [NotMapped]
    public int RoleId { get; set; }
    
    public bool IsDeleted { get; set; }

    public  int EmployeCode { get; set; } 

    public bool IsAllowed { get; set; }

    public int AllowedByEmployeeId { get; set; }

    public int CreatedByEmployeeId { get; set; }

    public DateTime CreatedOnUtc { get; set; }

    public int UpdatedByEmployeeId { get; set; }

    public DateTime UpdatedOnUtc { get; set; }
}
