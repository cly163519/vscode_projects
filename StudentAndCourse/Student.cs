using System;
using System.Collections.Generic;

public class Student
{
    // ========== Static variables and constants ==========
    private static int nextId = 1;           // Static variable shared by all students, used to generate unique ID
    private const int MAX_COURSES = 5;       // Maximum 5 courses allowed

    // ========== Properties ==========
    public int Id { get; private set; }                      // Student ID
    public string Name { get; private set; }                 // Student name
    public double PricePerPoint { get; private set; }        // Price per point
    public IList<Course> Courses { get; private set; }       // List of courses this student enrolled
    public IList<string> Grades { get; private set; }        // Corresponding grades for each course

    // ========== Constructor ==========
    public Student(string name, double pricePerPoint)
    {
        Id = nextId;           // Assign current nextId to this student
        nextId = nextId + 1;   // Increment nextId for next student
        Name = name;
        PricePerPoint = pricePerPoint;
        Courses = new List<Course>();    // Create empty course list
        Grades = new List<string>();     // Create empty grades list
    }

    // ========== Enrol course method ==========
    public bool EnrolCourse(Course course)
    {
        // Check 1: Already enrolled in maximum courses?
        if (Courses.Count >= MAX_COURSES)
        {
            Console.WriteLine(Name + " has already enrolled in 5 courses!");
            return false;
        }

        // Check 2: Already enrolled in this course?
        if (Courses.Contains(course))
        {
            Console.WriteLine(Name + " has already enrolled in this course!");
            return false;
        }

        // Passed all checks, can enrol
        Courses.Add(course);    // Add course to student's course list
        Grades.Add("N/A");      // Add placeholder grade
        return true;
    }

    // ========== Grade related methods ==========
    public void AssignGrade(Course course, string grade)
    {
        // Find the course in the list and set the grade
        for (int i = 0; i < Courses.Count; i++)
        {
            if (Courses[i] == course)
            {
                Grades[i] = grade;  // Set grade at the same position
                return;
            }
        }
    }

    public string GetGrade(Course course)
    {
        // Find the course and return its grade
        for (int i = 0; i < Courses.Count; i++)
        {
            if (Courses[i] == course)
            {
                return Grades[i];
            }
        }
        return "N/A";  // Return N/A if not found
    }

    public double GetGPA()
    {
        if (Grades.Count == 0)
        {
            return 0.0;  // Return 0 if no grades
        }

        double total = 0;
        foreach (var grade in Grades)
        {
            total = total + GradeToNumber(grade);
        }
        return total / Grades.Count;  // Average
    }

    // Convert letter grade to number
    private double GradeToNumber(string grade)
    {
        if (grade == "A") return 4.0;
        if (grade == "B") return 3.0;
        if (grade == "C") return 2.0;
        if (grade == "D") return 1.0;
        return 0.0;
    }
}