using System;

namespace StudentAndCourse;

class Program
{
    static void Main(string[] args)
    {
        StudentAndCourseApp.Run();
        Student tom = new Student(1, "Tom", 120.0);
        Student jane = new Student(2, "Jane", 150.0);
        Student bob = new Student(3, "Bob", 100.0);
        Lecturer drSmith = new Lecturer("Dr. Smith", 500.0);
        Lecturer profJones = new Lecturer("Prof. Jones", 700.0);
        Course math101 = new Course("Math 101", "M101", 15, 200.0);
        Course phys101 = new Course("Physics 101", "P101", 20, 250.0);

        //Assign lecturers
        math101.AssignLecturer(drSmith);
        phys101.AssignLecturer(profJones);

        //Assign grades
        tom.EnrolCourse(math101);
        tom.AssignGrade(math101, "A");

        //Assgn students
        jane.EnrolCourse(math101);
        jane.AssignGrade(math101, "B");
        bob.EnrolCourse(phys101);
        bob.AssignGrade(phys101, "C");

        //print results
        Console.WriteLine($"{tom.Name} enrolled in {math101.Name} with grade {tom.GetGrade(math101)}");
        Console.WriteLine($"{jane.Name} enrolled in {math101.Name} with grade {jane.GetGrade(math101)}");
        Console.WriteLine($"{bob.Name} enrolled in {phys101.Name} with grade {bob.GetGrade(phys101)}");
        Console.WriteLine($"{tom.Name}'s GPA: {tom.GetGPA()}");
        Console.WriteLine($"{jane.Name}'s GPA: {jane.GetGPA()}");
        Console.WriteLine($"{bob.Name}'s GPA: {bob.GetGPA()}");
    }

}

class StudentAndCourseApp
{
    public static void Run()
    {
        // Minimal run implementation to satisfy the call from Main.
        Console.WriteLine("Student and Course app started.");
          }
}
