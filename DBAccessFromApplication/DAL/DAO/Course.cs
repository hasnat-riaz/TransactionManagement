namespace DBAccessFromApplication.DAL.DAO
{
    public class Course
    {
        private string title;
        private int id;

        public Course(int id, string title)
        {
            this.id = id;
            this.title = title;
        }

        public string Title
        {
            get { return title; }
        }

        public int Id
        {
            get { return id; }
        }
    }
}
