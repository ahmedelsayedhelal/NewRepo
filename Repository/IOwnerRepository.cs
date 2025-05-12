using Sakan_project.models;

namespace Sakan_project.Repository
{
    public interface IOwnerRepository
    {
        List<Owners> GetAll();
        Owners GetById(int id);
        void Insert(Owners item);
        void Edit(int id, Owners item);
        void Delete(int id);
    }
}
