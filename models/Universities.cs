namespace Sakan_project.models
{
    public class Universities
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Location { get; set; }

        public virtual List <Colleges> Colleges { get; set; }
    }
}
