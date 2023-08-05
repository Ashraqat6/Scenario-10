using Microsoft.EntityFrameworkCore;
using scenario10API.models;
using System.Net.Sockets;

namespace scenario10API.Repos.Reports
{
    public class ReportRepo: IReportRepo
    {
        private readonly MyDBContext _context;

        public ReportRepo(MyDBContext context)
        {
            _context = context;
        }
        public IEnumerable<Report> GetAll()
        {
            return _context.Set<Report>().AsNoTracking(); //ReadOnly
        }

        public Report? GetById(int id)
        {
            return _context.Set<Report>().Find(id);
        }

        public void Add(Report entity)
        {
            _context.Set<Report>().Add(entity);
        }

        public void Update(Report entity)
        {
            _context.Update(entity); 
        }


        public void Delete(Report entity)
        {
            _context.Set<Report>().Remove(entity);
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }
        public bool ReportExists(int id)
        {
            // Assuming you have a method that checks if the report exists by ID.
            // Replace the condition below with your logic to check if the report exists.
            return _context.Set<Report>().Any(r => r.Id == id);
        }

    }
}
