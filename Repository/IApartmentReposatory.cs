using Sakan_project.models;

namespace Sakan_project.Repository
{
    public interface IApartmentReposatory
    {
        List<Apartments> GetAll();
        Apartments GetById(int id);
        void Insert(Apartments item);
        void Edit(int id, Apartments item);
        void Delete(int id);
    }
}
