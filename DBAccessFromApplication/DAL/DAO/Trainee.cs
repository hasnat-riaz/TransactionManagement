using System.Collections.Generic;

namespace DBAccessFromApplication.DAL.DAO
{
    public class Trainee
    {
        private string id;
        private string name;
        private List<Course> courses = new List<Course>();

        public Trainee(string id, string name)
        {
            this.id = id;
            this.name = name;
        }

        public string Id
        {
            get { return id; }
        }

        public string Name
        {
            get { return name; }
        }
    }
}
