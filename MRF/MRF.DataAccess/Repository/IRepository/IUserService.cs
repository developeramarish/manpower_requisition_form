using MRF.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRF.DataAccess.Repository.IRepository
{
    public interface IUserService
    {
        public string GetUser();
        public ResponseDTO GetRoledetails(bool login);
        public int GetRoleId();
        public int GetUserId();
    }
}
