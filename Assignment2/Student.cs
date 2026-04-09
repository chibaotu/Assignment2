namespace Assignment2
{
    public class Student
    {
        private static int nextId = 1;

        public int StudentID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public Student(string name, string email)
        {
            StudentID = nextId++;
            Name = name;
            Email = email;
        }

        // Constructor will use when it creates a new one
        public Student(int studentId, string name, string email)
        {
            StudentID = studentId;
            Name = name;
            Email = email;

            if (studentId >= nextId)
            {
                nextId = studentId + 1;
            }
        }

        public void DisplayInfo()
        {
            Console.WriteLine($"Student ID: {StudentID}, Name: {Name}, Email: {Email}");
        }

        public static void SetNextId(int value)
        {
            if (value > nextId)
            {
                nextId = value;
            }
        }
    }
}