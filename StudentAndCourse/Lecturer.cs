using System;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
public class Lecturer
{
    public string Name {get; set;}
    public double costToTeach;
    public IList<Course> assignedCourses { get; set; }

    public static int MAX_COURSES = 3;
    public Lecturer(string name, double costToTeach)
    {
        Name = name;
        this.costToTeach = costToTeach;
        assignedCourses = new List<Course>();
    }

    public String getName()
    {
        return Name;
    }

    public double getCostToTeach()
    {
        return costToTeach;
    }

    public bool CanTeachCourse(Course course)
    {
        if(assignedCourses.Count >= MAX_COURSES)
        {
            return false;
        }
        return true;
    }

    public void AssignCourse(Course course)
    {
        if(!assignedCourses.Contains(course) && CanTeachCourse(course))
        {
            assignedCourses.Add(course);
        }
    }


}