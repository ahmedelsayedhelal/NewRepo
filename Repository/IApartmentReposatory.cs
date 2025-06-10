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
        public List<Apartments> SearchByName(string name);
        public List<Apartments> FilterByPrice(decimal? minPrice, decimal? maxPrice);


    }
}
