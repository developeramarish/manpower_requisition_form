﻿namespace MRF.Models.DTO
{
    public class EmployeedetailsRequestModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string ContactNo { get; set; } = null!;

        public bool IsAllowed { get; set; }

        public int AllowedByEmployeeId { get; set; }

        public int CreatedByEmployeeId { get; set; }

        public DateTime CreatedOnUtc { get; set; }

        public int UpdatedByEmployeeId { get; set; }

        public DateTime UpdatedOnUtc { get; set; }

    }

    public class EmployeedetailsResponseModel
    {
        public int Id { get; set; }
        public bool IsActive { get; set; }
    }
}
