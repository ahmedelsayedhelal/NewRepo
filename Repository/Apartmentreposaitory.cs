using Sakan_project.models;

namespace Sakan_project.Repository
{
    public class Apartmentreposaitory : IApartmentReposatory
    {
        private readonly Sakancontext context;
        public Apartmentreposaitory(Sakancontext context)
        {
            this.context = context;
        }

        public List<Apartments> GetAll()
        {
            return context.Apartments.ToList();
        }
        public Apartments GetById(int id)
        {
            return context.Apartments.FirstOrDefault(x => x.Id == id);
        }
        public void Insert(Apartments item)
        {
            context.Apartments.Add(item);
            context.SaveChanges();
        }
        public void Edit(int id, Apartments item)
        {
            Apartments oldDept = GetById(id);
            oldDept.PricePerMonth = item.PricePerMonth;
            oldDept.Description = item.Description;
            oldDept.Location = item.Location;
            oldDept.Rooms = item.Rooms;
            oldDept.Title = item.Title;
            oldDept.ImageUrl = item.ImageUrl;
            context.SaveChanges(true);
        }
        public void Delete(int id)
        {
            Apartments oldDept = GetById(id);
            context.Apartments.Remove(oldDept);
            context.SaveChanges();
        }
    }
}

