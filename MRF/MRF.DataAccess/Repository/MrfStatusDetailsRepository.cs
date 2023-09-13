using Microsoft.EntityFrameworkCore;
using MRF.DataAccess.Repository.IRepository;
using MRF.Models.Models;
using MRF.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MRF.DataAccess.Repository
{
    public class MrfStatusDetailsRepository : Repository<MrfDetailsViewModel>, IMrfStatusDetailsRepository
    {
        private readonly Data.MRFDBContext _db;
        public MrfStatusDetailsRepository(Data.MRFDBContext db) : base(db)
        {
            _db = db;
        }

        public MrfDetailsViewModel GetMrfStatusDetails(int mrfId)
        {
            var query = from mrfDetails in _db.Mrfdetails
                        join mrfStatus in _db.Mrfstatusmaster on mrfDetails.MrfStatusId equals mrfStatus.Id
                        join Emp in _db.Employeedetails on mrfDetails.CreatedByEmployeeId equals Emp.Id
                        join salary in _db.Freshmrfdetails on mrfDetails.Id equals salary.Id
                        where mrfDetails.Id==mrfId
                        //     group new { mrfDetails, mrfStatus } by new
                        //     {
                        //         mrfDetails.Id,
                        //         mrfDetails.MrfStatusId,
                        //         mrfDetails.ReferenceNo,
                        //     }
                        //into grouped
                        select new MrfDetailsViewModel
                        {
                            ReferenceNo = mrfDetails.ReferenceNo,
                            Experience = mrfDetails.MinExperience + "-" + mrfDetails.MaxExperience,
                            MrfStatusId = mrfDetails.MrfStatusId,
                            MrfStatus= mrfStatus.Status,
                            CreatedByEmployeeId = mrfDetails.CreatedByEmployeeId,
                            Name=Emp.Name,
                            CreatedOnUtc = mrfDetails.CreatedOnUtc,
                            UpdatedOnUtc = mrfDetails.UpdatedOnUtc,
                            Salary = salary.MinTargetSalary + "-" + salary.MaxTargetSalary,
                            VacancyNo=mrfDetails.VacancyNo,
                            RequisitionType = mrfDetails.PositionTitle,
                        };

            return query.FirstOrDefault();
        }
    }
}
