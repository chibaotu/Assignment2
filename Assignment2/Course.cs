namespace Assignment2
{
    public class Course
    {
        private static int nextId = 1;

        public int CourseID { get; set; }
        public string CourseName { get; set; }
        public int CreditHours { get; set; }

        // Constructor will use when it creates a new one
        public Course(string courseName, int creditHours)
        {
            CourseID = nextId++;
            CourseName = courseName;
            CreditHours = creditHours;
        }

        // Constructor use when it loads from file
        public Course(int courseId, string courseName, int creditHours)
        {
            CourseID = courseId;
            CourseName = courseName;
            CreditHours = creditHours;

            if (courseId >= nextId)
            {
                nextId = courseId + 1;
            }
        }

        public void DisplayInfo()
        {
            Console.WriteLine($"Course ID: {CourseID}, Course Name: {CourseName}, Credit Hours: {CreditHours}");
        }

        public bool IsFullCreditCourse()
        {
            return CreditHours >= 3;
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