using Sakan_project.models;

namespace Sakan_project.Repository
{
    public class Studentsreposaitory : IStudentsRepository
    {
        Sakancontext context;

       public Studentsreposaitory (Sakancontext _context)
        {
            this.context = _context;
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Edit(int id, Students item)
        {
            throw new NotImplementedException();
        }

        public void Edit(int id, Owners item)
        {
            throw new NotImplementedException();
        }

        public List<Owners> GetAll()
        {
            throw new NotImplementedException();
        }

        public Owners GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Insert(Students item)
        {
            throw new NotImplementedException();
        }

        public void Insert(Owners item)
        {
            throw new NotImplementedException();
        }
    }
}
