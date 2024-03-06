using MRF.DataAccess.Repository.IRepository;
using MRF.Models.Models;
using MRF.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRF.DataAccess.Repository
{
    public class MrfEmailApprovalRepository:Repository<MrfEmailApproval>, IMrfEmailApprovalRepository
    {
        private readonly Data.MRFDBContext _db;
        public MrfEmailApprovalRepository(Data.MRFDBContext db) : base(db)
        {
            _db = db;
        }
        public void Update(MrfEmailApproval MrfEmailApproval)
        {
            _db.MrfEmailApproval.Update(MrfEmailApproval);
        }

        public List<MrfEmailApproval> GetList(int mrfId)
        {

            IQueryable<MrfEmailApproval> query = from mrfDetails in _db.MrfEmailApproval
                                                 join employee in _db.Employeedetails on mrfDetails.EmployeeId equals employee.Id
                                                 join role in _db.Employeerolemap on employee.Id equals role.EmployeeId
                                                 where mrfDetails.MrfId == mrfId
                                                  
               select new MrfEmailApproval
               {
                  Id = mrfDetails.Id,
                  MrfId = mrfId,
                  EmployeeId=mrfDetails.EmployeeId,
                  RoleId = role.RoleId,
                  ApprovalDate=mrfDetails.ApprovalDate,
               };


            return query.ToList();
        }

        
        public MrfEmailApproval GetListBymrfIdandEmployeeId(int mrfId,int empId)
        {

            IQueryable<MrfEmailApproval> query = from mrfDetails in _db.MrfEmailApproval
                                                 where mrfDetails.MrfId == mrfId && mrfDetails.EmployeeId == empId
                                                 select mrfDetails;
 
            return query.FirstOrDefault(); ;
        }

        //to get mrfIds having a particular emp as a role
        public List<int> GetListFromMrfIds(List<int> mrfIds, int empId, int roleId)
        {
            IQueryable<int> query = (from mrfDetails in _db.MrfEmailApproval
                                    where mrfIds.Contains(mrfDetails.MrfId) && mrfDetails.RoleId == roleId && mrfDetails.EmployeeId == empId
                                    select mrfDetails.MrfId).Distinct();

            return query.ToList();
        }
    }
}
