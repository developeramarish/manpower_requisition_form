using MRF.Models.Models;
using MRF.DataAccess.Repository.IRepository;

namespace MRF.Utility
{
    public class GetEmailId
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetEmailId(IUnitOfWork unitOfWork)
        {
              _unitOfWork = unitOfWork;
        }
        public string getUserEmail(int id)
        {
            Employeedetails Employeedetail = _unitOfWork.Employeedetails.Get(u => u.Id == id);

            if (Employeedetail != null)
                return Employeedetail.Email;
            return string.Empty;
        }
    }
}
