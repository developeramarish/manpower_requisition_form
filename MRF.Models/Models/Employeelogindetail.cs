using System;
using System.Collections.Generic;

namespace MRF.Models.Models;

public partial class Employeelogindetail
{
    public int Id { get; set; }

    public int EmployeeId { get; set; }

    public DateTime LoginDateTime { get; set; }
}
