namespace MRF.Models.DTO
{
    public class EmployeelogindetailRequestModel
    {
        public int Id { get; set; }

        public int EmployeeId { get; set; }

        public DateTime LoginDateTime { get; set; }
    }

    public class EmployeelogindetailResponseModel
    {
        public int Id { get; set; }
       
    }
}
