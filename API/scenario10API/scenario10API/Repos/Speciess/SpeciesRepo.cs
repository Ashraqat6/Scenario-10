using Microsoft.EntityFrameworkCore;
using scenario10API.models;
using System.Net.Sockets;

namespace scenario10API.Repos.Speciess
{
    public class SpeciesRepo:ISpeciesRepo
    {
        private readonly MyDBContext _context;

        public SpeciesRepo(MyDBContext context)
        {
            _context = context;
        }
        public IEnumerable<Species> GetAll()
        {
            return _context.Set<Species>().AsNoTracking(); //ReadOnly
        }

        public Species? GetById(int id)
        {
            return _context.Set<Species>().Find(id);
        }

        public void Add(Species entity)
        {
            _context.Set<Species>().Add(entity);
        }

        public void Update(Species entity)
        {
        }

        public void Delete(Species entity)
        {
            _context.Set<Species>().Remove(entity);
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }
        public bool SpeciesExists(int id)
        {
            return _context.Set<Species>().Any(e => e.Id == id);
        }
    }
}
