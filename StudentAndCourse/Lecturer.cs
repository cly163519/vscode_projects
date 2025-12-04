using System;
using System.Collections.Generic;

public class Lecturer
{
    // ========== Constants ==========
    private const int MAX_COURSES = 3;  // Maximum 3 courses allowed

    // ========== Properties ==========
    public string Name { get; private set; }                  // Lecturer name
    public double CostToTeach { get; private set; }           // Cost to teach
    public IList<Course> Courses { get; private set; }        // List of courses this lecturer teaches

    // ========== Constructor ==========
    public Lecturer(string name, double costToTeach)
    {
        Name = name;
        CostToTeach = costToTeach;
        Courses = new List<Course>();
    }

    // ========== Methods ==========
    // Check if can teach more courses
    public bool CanTeachMore()
    {
        return Courses.Count < MAX_COURSES;
    }

    // Assign a course to this lecturer
    public void AssignCourse(Course course)
    {
        // Check if already teaching this course
        if (Courses.Contains(course))
        {
            return;  // Already teaching, no need to add
        }
        Courses.Add(course);
    }
}