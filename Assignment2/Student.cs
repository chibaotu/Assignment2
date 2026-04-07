namespace Assignment2;

public class Student
{
    private static int idGenerator = 1;
    public int Id {  get; private set; }
    public string Name { get; set; }
    public string Email { get;  set; }

    public Student (string name, string email)
    {
        Id = idGenerator;
        idGenerator++;
        Name = name;
    }

    public override string ToString()
    {
        return $"ID: {Id}, Name: {Name},  Email: {Email}";
    }
}