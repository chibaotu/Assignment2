namespace Assignment2;

public class Course
{
    private static int idGenerator = 1;
    public int Id { get; private set; }
    public string CourseName { get; set; }
    public int Credits { get; set; }
    public Course (string  courseName,int credits)
    {
        Id = idGenerator++;
        CourseName = courseName;
        Credits = credits;
    }

    public override string ToString()
    {
        return $"ID: {Id}, Name: {CourseName}, Credits: {Credits}";
    }
}