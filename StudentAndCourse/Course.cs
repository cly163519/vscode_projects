using System;
using System.Collections.Generic;
using StudentAndCourse;
public class Course
{
    public string Name {get; set; }
    public string Id { get; set; }
    public int Points { get; set; }
    public double RoomCost { get; set; }
    
    internal IList<Student> Students { get; set; } //Internal干什么用的?
    public IList<Lecturer> lecturers { get; set; }

    private const int MAX_STUDENTS = 30;

    public Course(string name, string id, int points, double roomCost)
    {
        Name = name;
        Id = id;
        Points = points;
        RoomCost = roomCost;
        Students = new List<Student>();
        lecturers = new List<Lecturer>();
    }
    internal bool EnrolStudent(Student student)
    {
        if (Students.Count >= MAX_STUDENTS)
        {
            return false;
        }
        if (Students.Contains(student))
        {
            return false;
        }

        Students.Add(student);
        student.EnrolCourse(this);
        return true;
    }

    internal void AssignLecturer(Lecturer lecturer)
    {
        if(!lecturers.Contains(lecturer) && lecturer.CanTeachCourse(this)) 
        {
            lecturers.Add(lecturer);
            lecturer.AssignCourse(this);
        }
    }

    public double CalculateIncome()
    {
        double total = 0;
        foreach(var student in Students)
        {
            total += student.PricePerPoint * Points;
        }
        return total;
    }

    public double CalculateCost()
    {
        double total = RoomCost;
        foreach(var lecturer in lecturers)
        {
            total += lecturer.costToTeach;
        }
        return total;
    }
    public double CalculateProfit()
    {
        return CalculateIncome() - CalculateCost();
    }
}