namespace Assignment2
{
    // Assignment 2 - COMP1202
// Group Member: Chi Bao Tu - 101490975
    internal class Program
    {
        static void Main(string[] args)
        {
            College college = new College();

            try
            {
                college.LoadData();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error loading data: " + ex.Message);
            }

            bool running = true;

            while (running)
            {
                Console.WriteLine("\n===== COURSE REGISTRATION SYSTEM =====");
                Console.WriteLine("1. Add Student");
                Console.WriteLine("2. Add Course");
                Console.WriteLine("3. Register Student to Course");
                Console.WriteLine("4. Display All Students");
                Console.WriteLine("5. Display All Courses");
                Console.WriteLine("6. Display Registrations");
                Console.WriteLine("7. Save Data");
                Console.WriteLine("8. Load Data");
                Console.WriteLine("9. Exit");
                Console.Write("Enter your choice: ");

                string? choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddStudentMenu(college);
                        break;

                    case "2":
                        AddCourseMenu(college);
                        break;

                    case "3":
                        RegisterStudentMenu(college);
                        break;

                    case "4":
                        college.DisplayAllStudents();
                        break;

                    case "5":
                        college.DisplayAllCourses();
                        break;

                    case "6":
                        college.DisplayRegistrations();
                        Console.WriteLine($"\nTotal Registrations: {college.CountTotalRegistrations()}");
                        break;

                    case "7":
                        try
                        {
                            college.SaveData();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Error saving data: " + ex.Message);
                        }
                        break;

                    case "8":
                        try
                        {
                            college.LoadData();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Error loading data: " + ex.Message);
                        }
                        break;

                    case "9":
                        try
                        {
                            college.SaveData();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Error saving before exit: " + ex.Message);
                        }

                        Console.WriteLine("Exiting program...");
                        running = false;
                        break;

                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        static void AddStudentMenu(College college)
        {
            Console.Write("Enter student name: ");
            string? name = Console.ReadLine();

            Console.Write("Enter student email: ");
            string? email = Console.ReadLine();

            college.AddStudent(name ?? "", email ?? "");
            Console.WriteLine("Student added successfully.");
        }

        static void AddCourseMenu(College college)
        {
            Console.Write("Enter course name: ");
            string? courseName = Console.ReadLine();

            Console.Write("Enter credit hours: ");
            int creditHours = int.Parse(Console.ReadLine() ?? "0");

            college.AddCourse(courseName ?? "", creditHours);
            Console.WriteLine("Course added successfully.");
        }

        static void RegisterStudentMenu(College college)
        {
            Console.Write("Enter Student ID: ");
            int studentId = int.Parse(Console.ReadLine() ?? "0");

            Console.Write("Enter Course ID: ");
            int courseId = int.Parse(Console.ReadLine() ?? "0");

            college.RegisterStudentToCourse(studentId, courseId);
        }
    }
}