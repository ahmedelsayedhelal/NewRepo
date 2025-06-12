using Sakan_project.models;

namespace Sakan_project.Repository
{
    public class CollegeRepository : ICollegeRepository
    {
        private readonly Sakancontext context;
        public CollegeRepository(Sakancontext context)
        {
            this.context = context;
        }

        public List<Colleges> GetAll()
        {
            return context.Colleges.ToList();
        }
        public Colleges GetById(int id)
        {
            return context.Colleges.FirstOrDefault(x => x.Id == id);
        }
        public void Insert(Colleges item)
        {
            context.Colleges.Add(item);
            context.SaveChanges();
        }
        public void Edit(int id, Colleges item)
        {
            Colleges oldDept = GetById(id);
            oldDept.Name = item.Name;
            oldDept.Unversityid = item.Unversityid;
          
            context.SaveChanges(true);
        }
        public void Delete(int id)
        {
            Colleges oldDept = GetById(id);
            context.Colleges.Remove(oldDept);
            context.SaveChanges();
        }
        public List<Colleges> SearchByName(string name)
        {
            return context.Colleges
                .Where(a => a.Name.Contains(name))
                .ToList();
        }
      
    }
}
