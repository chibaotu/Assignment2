using System.Text;

namespace Assignment2
{
    // registrations[row, col]
// row = student index
// col = course index
// true means the student is registered in that course
    public class College
    {
        public List<Student> Students { get; set; }
        public List<Course> Courses { get; set; }

        private bool[,] registrations;

        private readonly string studentsFile = "students.csv";
        private readonly string coursesFile = "courses.csv";
        private readonly string registrationsFile = "registrations.csv";

        public College()
        {
            Students = new List<Student>();
            Courses = new List<Course>();
            registrations = new bool[0, 0];
        }

        public void AddStudent(string name, string email)
        {
            Students.Add(new Student(name, email));
            ResizeRegistrationArray();
        }

        public void AddCourse(string courseName, int creditHours)
        {
            Courses.Add(new Course(courseName, creditHours));
            ResizeRegistrationArray();
        }

        public void RegisterStudentToCourse(int studentId, int courseId)
        {
            int studentIndex = GetStudentIndexById(studentId);
            int courseIndex = GetCourseIndexById(courseId);

            if (studentIndex == -1)
            {
                Console.WriteLine("Student ID not found.");
                return;
            }

            if (courseIndex == -1)
            {
                Console.WriteLine("Course ID not found.");
                return;
            }

            if (registrations[studentIndex, courseIndex])
            {
                Console.WriteLine("This student is already registered in that course.");
                return;
            }

            registrations[studentIndex, courseIndex] = true;
            Console.WriteLine("Registration successful.");
        }

        public void DisplayAllStudents()
        {
            if (Students.Count == 0)
            {
                Console.WriteLine("No students found.");
                return;
            }

            foreach (Student student in Students)
            {
                student.DisplayInfo();
            }
        }

        public void DisplayAllCourses()
        {
            if (Courses.Count == 0)
            {
                Console.WriteLine("No courses found.");
                return;
            }

            foreach (Course course in Courses)
            {
                course.DisplayInfo();
            }
        }

        public void DisplayRegistrations()
        {
            if (Students.Count == 0 || Courses.Count == 0)
            {
                Console.WriteLine("Students or courses are missing.");
                return;
            }

            bool hasAnyRegistration = false;

            for (int i = 0; i < Students.Count; i++)
            {
                Console.WriteLine($"\nStudent: {Students[i].Name} (ID: {Students[i].StudentID})");

                bool studentHasCourse = false;

                for (int j = 0; j < Courses.Count; j++)
                {
                    if (registrations[i, j])
                    {
                        Console.WriteLine($"   Registered in: {Courses[j].CourseName} (ID: {Courses[j].CourseID})");
                        studentHasCourse = true;
                        hasAnyRegistration = true;
                    }
                }

                if (!studentHasCourse)
                {
                    Console.WriteLine("   No registered courses.");
                }
            }

            if (!hasAnyRegistration)
            {
                Console.WriteLine("\nNo registrations found.");
            }
        }

        public int CountTotalRegistrations()
        {
            int count = 0;

            for (int i = 0; i < Students.Count; i++)
            {
                for (int j = 0; j < Courses.Count; j++)
                {
                    if (registrations[i, j])
                    {
                        count++;
                    }
                }
            }

            return count;
        }

        private int GetStudentIndexById(int studentId)
        {
            for (int i = 0; i < Students.Count; i++)
            {
                if (Students[i].StudentID == studentId)
                {
                    return i;
                }
            }

            return -1;
        }

        private int GetCourseIndexById(int courseId)
        {
            for (int i = 0; i < Courses.Count; i++)
            {
                if (Courses[i].CourseID == courseId)
                {
                    return i;
                }
            }

            return -1;
        }

        private void ResizeRegistrationArray()
        {
            bool[,] newRegistrations = new bool[Students.Count, Courses.Count];

            for (int i = 0; i < registrations.GetLength(0); i++)
            {
                for (int j = 0; j < registrations.GetLength(1); j++)
                {
                    newRegistrations[i, j] = registrations[i, j];
                }
            }

            registrations = newRegistrations;
        }

        public void SaveData()
        {
            SaveStudents();
            SaveCourses();
            SaveRegistrations();
            Console.WriteLine("Data saved successfully.");
        }

        public void LoadData()
        {
            LoadStudents();
            LoadCourses();
            ResizeRegistrationArray();
            LoadRegistrations();
            Console.WriteLine("Data loaded successfully.");
        }

        private void SaveStudents()
        {
            using StreamWriter writer = new StreamWriter(studentsFile);

            foreach (Student student in Students)
            {
                writer.WriteLine($"{student.StudentID},{student.Name},{student.Email}");
            }
        }

        private void SaveCourses()
        {
            using StreamWriter writer = new StreamWriter(coursesFile);

            foreach (Course course in Courses)
            {
                writer.WriteLine($"{course.CourseID},{course.CourseName},{course.CreditHours}");
            }
        }

        private void SaveRegistrations()
        {
            using StreamWriter writer = new StreamWriter(registrationsFile);

            for (int i = 0; i < Students.Count; i++)
            {
                for (int j = 0; j < Courses.Count; j++)
                {
                    if (registrations[i, j])
                    {
                        writer.WriteLine($"{Students[i].StudentID},{Courses[j].CourseID}");
                    }
                }
            }
        }

        private void LoadStudents()
        {
            Students.Clear();

            if (!File.Exists(studentsFile))
            {
                File.Create(studentsFile).Close();
                return;
            }

            string[] lines = File.ReadAllLines(studentsFile);
            int maxId = 0;

            foreach (string line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }

                string[] parts = line.Split(',');

                int studentId = int.Parse(parts[0]);
                string name = parts[1];
                string email = parts[2];

                Students.Add(new Student(studentId, name, email));

                if (studentId > maxId)
                {
                    maxId = studentId;
                }
            }

            Student.SetNextId(maxId + 1);
        }

        private void LoadCourses()
        {
            Courses.Clear();

            if (!File.Exists(coursesFile))
            {
                File.Create(coursesFile).Close();
                return;
            }

            string[] lines = File.ReadAllLines(coursesFile);
            int maxId = 0;

            foreach (string line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }

                string[] parts = line.Split(',');

                int courseId = int.Parse(parts[0]);
                string courseName = parts[1];
                int creditHours = int.Parse(parts[2]);

                Courses.Add(new Course(courseId, courseName, creditHours));

                if (courseId > maxId)
                {
                    maxId = courseId;
                }
            }

            Course.SetNextId(maxId + 1);
        }

        private void LoadRegistrations()
        {
            if (!File.Exists(registrationsFile))
            {
                File.Create(registrationsFile).Close();
                return;
            }

            string[] lines = File.ReadAllLines(registrationsFile);

            foreach (string line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }

                string[] parts = line.Split(',');

                int studentId = int.Parse(parts[0]);
                int courseId = int.Parse(parts[1]);

                int studentIndex = GetStudentIndexById(studentId);
                int courseIndex = GetCourseIndexById(courseId);

                if (studentIndex != -1 && courseIndex != -1)
                {
                    registrations[studentIndex, courseIndex] = true;
                }
            }
        }
    }
}