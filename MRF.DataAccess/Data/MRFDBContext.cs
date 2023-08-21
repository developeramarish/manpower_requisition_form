using Microsoft.EntityFrameworkCore;
using MRF.Models.Models;

namespace MRF.DataAccess.Data
{
    public class MRFDBContext : DbContext
    {
        public MRFDBContext(DbContextOptions<MRFDBContext> options) : base(options)
        {

        }

        public DbSet<Candidatestatusmaster> Candidatestatusmaster { get; set; }
    }
}
