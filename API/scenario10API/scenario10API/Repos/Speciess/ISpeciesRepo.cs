using scenario10API.models;
using System.Net.Sockets;

namespace scenario10API.Repos.Speciess
{
    public interface ISpeciesRepo
    {
        IEnumerable<Species> GetAll();
        Species? GetById(int id);
        void Add(Species entity);
        void Update(Species entity);
        void Delete(Species entity);
        bool SpeciesExists(int id);
        int SaveChanges();
    }
}
