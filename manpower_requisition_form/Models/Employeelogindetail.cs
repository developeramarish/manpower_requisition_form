using System;
using System.Collections.Generic;

namespace manpower_requisition_form.Models;

public partial class Employeelogindetail
{
    public int Id { get; set; }

    public int EmployeeId { get; set; }

    public DateTime LoginDateTime { get; set; }
}
