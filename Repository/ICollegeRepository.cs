using Sakan_project.models;

namespace Sakan_project.Repository
{
    public interface ICollegeRepository
    {

        List<Colleges> GetAll();
        Colleges GetById(int id);
        void Insert(Colleges item);
        void Edit(int id, Colleges item);
        void Delete(int id);
        public List<Colleges> SearchByName(string name);

    }
}
