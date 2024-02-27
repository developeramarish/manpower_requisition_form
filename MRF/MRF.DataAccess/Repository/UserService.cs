using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Reflection.Metadata;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using MRF.DataAccess.Repository.IRepository;
using MRF.Models.DTO;
using MRF.Models.Models;
using Microsoft.AspNetCore.Http.Extensions;
using static System.Collections.Specialized.BitVector32;


namespace MRF.DataAccess.Repository
{
    public class UserService: IUserService
    {
        private readonly Data.MRFDBContext _db;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ResponseDTO _response;
        private readonly EmployeedetailsRepository _employeedetailsRepository;
        private readonly EmployeelogindetailRepository _employeelogindetailRepository;
        private readonly EmployeerolemapRepository _employeerolemapRepository;
        public UserService(IHttpContextAccessor httpContextAccessor, Data.MRFDBContext db)
        {
            _httpContextAccessor = httpContextAccessor;
            _response = new ResponseDTO();
            _employeedetailsRepository = new EmployeedetailsRepository(db);
            _employeelogindetailRepository = new EmployeelogindetailRepository(db);
            _employeerolemapRepository = new EmployeerolemapRepository(db);
            _db = db;
        }

        public string GetUser()
        {
            return Convert.ToString(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name)?.Value);
        }

        public ResponseDTO GetRoledetails(bool login)
        {
            Employeerolemap Employeerolemap =new Employeerolemap();
            try
            {
                string Emailaddress = GetUser();
                
                Employeedetails Employeedetail = _employeedetailsRepository.Get(u => u.Email == Emailaddress && !u.IsDeleted);
                if (Employeedetail == null)
                {
                    return _response;
                }
                else
                {
                    if (login)
                    {
                        var Employeelogindetail = new Employeelogindetails
                        {

                            EmployeeId = Employeedetail.Id,
                            LoginDateTime = DateTime.Now,

                        };

                        _employeelogindetailRepository.Add(Employeelogindetail);
                        //_employeelogindetailRepository.;
                        _db.SaveChanges();
                    }
                    if (Employeedetail.Id != 0)
                    {
                        Employeerolemap = _employeerolemapRepository.Get(u => u.EmployeeId == Employeedetail.Id);
                        if (Employeerolemap == null)
                        {
                            return _response;
                        }
                        else
                        {
                            Employeerolemap.name = Employeedetail.Name;
                            Employeerolemap.Email= Employeedetail.Email;
                            Employeerolemap.ContactNo= Employeedetail.ContactNo;
                            Employeerolemap.EmployeeCode= Employeedetail.EmployeeCode;
                            SetRoleId(Employeerolemap.RoleId);
                            SetUserId(Employeerolemap.EmployeeId);
                            _response.Result = Employeerolemap;
                        }

                        return _response;

                    }

                }
            }catch(Exception ex) { }
            return _response;
        }

        public void SetRoleId(int roleId)
        {
            _httpContextAccessor.HttpContext.Session.Set("roleId", Encoding.UTF8.GetBytes(roleId.ToString()));
        }

        public void SetUserId(int UserId)
        {
           _httpContextAccessor.HttpContext.Session.Set("UserId", Encoding.UTF8.GetBytes(UserId.ToString()));
        }


        public int GetRoleId()
        {
            int RoleId = 0;
            ISession session = _httpContextAccessor.HttpContext.Session;
            byte[] roleIdBytes = session.Get("roleId");

            if (roleIdBytes != null)
            {
                string roleIdValue = Encoding.UTF8.GetString(roleIdBytes);
                RoleId=Convert.ToInt32(roleIdValue);
            }
            return RoleId;
        }

        public int GetUserId()
        {
            int UserId = 0;
            ISession session = _httpContextAccessor.HttpContext.Session;
            byte[] UserIdBytes = session.Get("UserId");

            if (UserIdBytes != null)
            {
                string roleIdValue = Encoding.UTF8.GetString(UserIdBytes);
                UserId = Convert.ToInt32(roleIdValue);
            }
            return UserId;
        }

    }

}
