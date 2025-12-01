using System; 
using System.Collections.Generic;      

class Student
{
    public const int MAX_COURSES = 5;

    public int Id { get; set; }
    public string Name { get; set; }
    public double PricePerPoint { get; set; }
    public IList<Course> courses { get; set; }
    public IList<String> grades { get; set; }

    public Student(int id, string name, double pricePerPoint)
    {
        Id = id;
        Name = name;
        PricePerPoint = pricePerPoint;
        courses = new List<Course>();
        grades = new List<String>();
    }
    public bool EnrolCourse(Course course)
    {
        if(!courses.Contains(course))
        {
            courses.Add(course);
        }

        if(courses.Count > MAX_COURSES)
        {
            return false;
        }

        grades.Add("N/A");
        return true;
    }

    public void AssignGrade(Course course, string grade)
    {
        for(int i = 0; i < courses.Count; i++)
        {
            if(courses[i] == course)
            {
                grades[i] = grade;
                return;
            }
        }
    }

    public string GetGrade(Course course)
    {
        for(int i = 0; i < courses.Count; i++)//什么时候用EnrolledCourses?
        {
            if(courses[i] == course)
            {
                return grades[i];
            }
        }
        return "N/A";
    }

    public double GetGPA()
    {
        if(grades.Count == 0)
        {
            return 0.0;
        }
        double total = 0;
        foreach(var grade in grades)//什么时候用大写Grades?
        {
            total += GradeToGPA(grade);
        }
        return total / grades.Count;
    }

    public double GradeToGPA(string grade)
    {
        if(grade =="A") return 4.0;
        else if(grade =="B") return 3.0;
        else if(grade =="C") return 2.0;
        else if(grade =="D") return 1.0;
        else return 0.0;
    }



}