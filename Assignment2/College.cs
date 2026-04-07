namespace Assignment2;

public class College
{
    public int Id { get; set; }
    public string CollegeName { get; set; }
    public List<Student> StudentsList { get; set; }
    public List<Course> CourseList { get; set; }
    public bool[,] Registrations { get; set; }

    public College(int id, string collegeName)
    {
        Id = id;
        CollegeName = collegeName;
        StudentsList = new List<Student>();
        CourseList = new List<Course>();
        Registrations = new bool[100, 100];
    }

    public override string ToString()
    {
        return $"College ID: {Id}, Name: {CollegeName}";
    }
}