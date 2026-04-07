using System.Text.Json;

namespace Assignment2;

class Program
{
    static List<College> colleges = new List<College>();

    static void Main(string[] args)
    {
        SeedColleges();
        LoadData();

        while (true)
        {
            College? selectedCollege = ShowCollegeMenu();

            if (selectedCollege == null)
            {
                Console.WriteLine("Exiting program...");
                return;
            }

            RunOldMenu(selectedCollege);
        }
    }

    static College? ShowCollegeMenu()
    {
        while (true)
        {
            Console.WriteLine("\n====== COLLEGE MENU ======");
            foreach (College college in colleges)
            {
                Console.WriteLine($"{college.Id}. {college.CollegeName}");
            }
            Console.WriteLine("0. Exit");
            Console.Write("Select a college: ");

            string? input = Console.ReadLine();
            int choice;

            if (!int.TryParse(input, out choice))
            {
                Console.WriteLine("Invalid input.");
                continue;
            }

            if (choice == 0)
            {
                return null;
            }

            foreach (College college in colleges)
            {
                if (college.Id == choice)
                {
                    return college;
                }
            }

            Console.WriteLine("College not found.");
        }
    }

    static string? CollegeTemplateMenu()
    {
        Console.WriteLine("\n====== MAIN MENU ======");
        Console.WriteLine("1. Add Students");
        Console.WriteLine("2. Add Courses");
        Console.WriteLine("3. Register Student To Course");
        Console.WriteLine("4. Display all students");
        Console.WriteLine("5. Display all courses");
        Console.WriteLine("6. Display Registrations");
        Console.WriteLine("7. Save Data");
        Console.WriteLine("8. Load Data");
        Console.WriteLine("9. Back to College Menu");
        Console.WriteLine("10. Exit");
        Console.Write("Enter choice: ");
        return Console.ReadLine();
    }

    static void RunOldMenu(College college)
    {
        while (true)
        {
            Console.WriteLine($"\n=== Selected College: {college.CollegeName} ===");
            string? choice = CollegeTemplateMenu();

            switch (choice)
            {
                case "1":
                    AddStudent(college);
                    break;
                case "2":
                    AddCourse(college);
                    break;
                case "3":
                    RegisterStudentToCourse(college);
                    break;
                case "4":
                    DisplayAllStudents(college);
                    break;
                case "5":
                    DisplayAllCourses(college);
                    break;
                case "6":
                    DisplayRegistrations(college);
                    break;
                case "7":
                    SaveData();
                    break;
                case "8":
                    LoadData();
                    Console.WriteLine("Data loaded.");
                    break;
                case "9":
                    return;
                case "10":
                    SaveData();
                    Console.WriteLine("Data saved. Exiting...");
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }
        }
    }

    static void SeedColleges()
    {
        if (colleges.Count > 0)
            return;

        colleges.Add(new College(1, "George Brown College"));
        colleges.Add(new College(2, "Seneca Polytechnic"));
        colleges.Add(new College(3, "Humber College"));
    }

    static void AddStudent(College college)
    {
        Console.Write("Enter student name: ");
        string? name = Console.ReadLine();

        Console.Write("Enter student email: ");
        string? email = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(email))
        {
            Console.WriteLine("Invalid input.");
            return;
        }

        Student student = new Student(name, email);
        college.StudentsList.Add(student);
        Console.WriteLine("Student added.");
    }

    static void AddCourse(College college)
    {
        Console.Write("Enter course name: ");
        string? courseName = Console.ReadLine();

        Console.Write("Enter course credits: ");
        string? input = Console.ReadLine();
        int credits;

        if (string.IsNullOrWhiteSpace(courseName) || !int.TryParse(input, out credits))
        {
            Console.WriteLine("Invalid input.");
            return;
        }

        Course course = new Course(courseName, credits);
        college.CourseList.Add(course);
        Console.WriteLine("Course added.");
    }

    static void RegisterStudentToCourse(College college)
    {
        Console.Write("Enter Student ID: ");
        string? studentInput = Console.ReadLine();

        Console.Write("Enter Course ID: ");
        string? courseInput = Console.ReadLine();

        int studentId, courseId;

        if (!int.TryParse(studentInput, out studentId) || !int.TryParse(courseInput, out courseId))
        {
            Console.WriteLine("Invalid input.");
            return;
        }

        int studentIndex = -1;
        int courseIndex = -1;

        for (int i = 0; i < college.StudentsList.Count; i++)
        {
            if (college.StudentsList[i].Id == studentId)
            {
                studentIndex = i;
                break;
            }
        }

        for (int j = 0; j < college.CourseList.Count; j++)
        {
            if (college.CourseList[j].Id == courseId)
            {
                courseIndex = j;
                break;
            }
        }

        if (studentIndex != -1 && courseIndex != -1)
        {
            college.Registrations[studentIndex, courseIndex] = true;
            Console.WriteLine("Registration successful.");
        }
        else
        {
            Console.WriteLine("Invalid Student ID or Course ID.");
        }
    }

    static void DisplayAllStudents(College college)
    {
        if (college.StudentsList.Count == 0)
        {
            Console.WriteLine("No students found.");
            return;
        }

        foreach (Student student in college.StudentsList)
        {
            Console.WriteLine(student);
        }
    }

    static void DisplayAllCourses(College college)
    {
        if (college.CourseList.Count == 0)
        {
            Console.WriteLine("No courses found.");
            return;
        }

        foreach (Course course in college.CourseList)
        {
            Console.WriteLine(course);
        }
    }

    static void DisplayRegistrations(College college)
    {
        bool hasRegistrations = false;

        for (int i = 0; i < college.StudentsList.Count; i++)
        {
            for (int j = 0; j < college.CourseList.Count; j++)
            {
                if (college.Registrations[i, j])
                {
                    Console.WriteLine(
                        $"Student: {college.StudentsList[i].Name} (ID: {college.StudentsList[i].Id}) " +
                        $"is registered in Course: {college.CourseList[j].CourseName} (ID: {college.CourseList[j].Id})"
                    );
                    hasRegistrations = true;
                }
            }
        }

        if (!hasRegistrations)
        {
            Console.WriteLine("No registrations found.");
        }
    }

    static void SaveData()
    {
        string json = JsonSerializer.Serialize(colleges);
        File.WriteAllText("colleges.json", json);
        Console.WriteLine("Data saved.");
    }

    static void LoadData()
    {
        if (File.Exists("colleges.json"))
        {
            string json = File.ReadAllText("colleges.json");
            List<College>? loaded = JsonSerializer.Deserialize<List<College>>(json);

            if (loaded != null)
            {
                colleges = loaded;
            }
        }
    }
}