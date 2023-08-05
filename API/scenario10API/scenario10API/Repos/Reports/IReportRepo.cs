using scenario10API.models;

namespace scenario10API.Repos.Reports
{
    public interface IReportRepo
    {
        IEnumerable<Report> GetAll();
        Report? GetById(int id);
        void Add(Report entity);
        void Update(Report entity);
        void Delete(Report entity);
        public bool ReportExists(int id);

        int SaveChanges();
    }
}
