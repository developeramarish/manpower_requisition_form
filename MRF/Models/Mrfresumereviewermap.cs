using System;
using System.Collections.Generic;

namespace manpower_requisition_form.Models;

public partial class Mrfresumereviewermap
{
    public int Id { get; set; }

    public int MrfId { get; set; }

    public int ResumeReviewerEmployeeId { get; set; }

    public bool IsActive { get; set; }

    public int CreatedByEmployeeId { get; set; }

    public DateTime CreatedOnUtc { get; set; }

    public int UpdatedByEmployeeId { get; set; }

    public DateTime UpdatedOnUtc { get; set; }
}
