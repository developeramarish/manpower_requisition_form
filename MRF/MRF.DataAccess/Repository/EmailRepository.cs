using MRF.DataAccess.Repository.IRepository;
using MRF.Models.Models;
using MRF.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;

namespace MRF.DataAccess.Repository
{
    public class EmailRepository : Repository<emailmaster>, IEmailRepository
    {
        private readonly Data.MRFDBContext _db;
        public EmailRepository(Data.MRFDBContext db) : base(db)
        {
            _db = db;
        }
        
    }
}
