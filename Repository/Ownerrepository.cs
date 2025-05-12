using Sakan_project.models;

namespace Sakan_project.Repository
{
    public class Ownerrepository : IOwnerRepository
    {
        Sakancontext context;

        public Ownerrepository(Sakancontext context)
        {
            this.context = context;
        }


        public List<Owners> GetAll()
        {
            return context.Owners.ToList();
        }
        public Owners GetById(int id)
        {
            return context.Owners.FirstOrDefault(x => x.Id == id);
        }
        public void Insert(Owners item)
        {
            context.Owners.Add(item);
            context.SaveChanges();
        }
        public void Edit(int id, Owners item)
        {
            Owners oldDept = GetById(id);
            oldDept.Firstname = item.Firstname;
            oldDept.Lastname = item.Lastname;
            oldDept.Phonenumber = item.Phonenumber;
            context.SaveChanges(true);
        }
        public void Delete(int id)
        {
            Owners oldDept = GetById(id);
            context.Owners.Remove(oldDept);
            context.SaveChanges();
        }
    }
}
