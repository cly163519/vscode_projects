using System;
using System.Collections.Generic;

public class Course
{
    // ========== Constants ==========
    private const int MAX_STUDENTS = 30;  // Maximum 30 students allowed

    // ========== Properties ==========
    public string Name { get; private set; }                    // Course name
    public string Id { get; private set; }                      // Course code
    public int Points { get; private set; }                     // Points/credits
    public double RoomCost { get; private set; }                // Room cost
    public IList<Student> Students { get; private set; }        // List of enrolled students
    public IList<Lecturer> Lecturers { get; private set; }      // List of lecturers teaching this course

    // ========== Constructor ==========
    public Course(string name, string id, int points, double roomCost)
    {
        Name = name;
        Id = id;
        Points = points;
        RoomCost = roomCost;
        Students = new List<Student>();
        Lecturers = new List<Lecturer>();
    }

    // ========== Student related methods ==========
    public bool EnrolStudent(Student student)
    {
        // Check 1: Course full?
        if (Students.Count >= MAX_STUDENTS)
        {
            Console.WriteLine(Name + " is full!");
            return false;
        }

        // Check 2: Student already enrolled?
        if (Students.Contains(student))
        {
            return false;
        }

        // Two-way association: course records student, student records course
        Students.Add(student);           // Add student to course
        student.EnrolCourse(this);       // Add course to student, "this" is current Course
        return true;
    }

    // ========== Lecturer related methods ==========
    public void AssignLecturer(Lecturer lecturer)
    {
        // Check: Already assigned?
        if (Lecturers.Contains(lecturer))
        {
            return;
        }

        // Check if lecturer can teach more courses
        if (lecturer.CanTeachMore())
        {
            Lecturers.Add(lecturer);           // Add lecturer to course
            lecturer.AssignCourse(this);       // Add course to lecturer
        }
    }

    // ========== Financial calculation methods ==========
    // Calculate income: money from all students
    public double CalculateIncome()
    {
        double total = 0;
        foreach (var s in Students)
        {
            total = total + s.PricePerPoint * Points;
        }
        return total;
    }

    // Calculate cost: room cost + all lecturer costs
    public double CalculateCost()
    {
        double total = RoomCost;
        foreach (var l in Lecturers)
        {
            total = total + l.CostToTeach;
        }
        return total;
    }

    // Calculate profit: income - cost
    public double CalculateProfit()
    {
        return CalculateIncome() - CalculateCost();
    }
}