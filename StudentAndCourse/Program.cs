using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("===== Student Course Management System =====");
        Console.WriteLine();

        // ========== Create students ==========
        var tom = new Student("Tom", 120.0);      // Tom pays $120 per point
        var jane = new Student("Jane", 150.0);    // Jane pays $150 per point
        var bob = new Student("Bob", 100.0);      // Bob pays $100 per point

        // ========== Create lecturers ==========
        var drSmith = new Lecturer("Dr. Smith", 500.0);     // Teaching cost $500
        var profJones = new Lecturer("Prof. Jones", 700.0); // Teaching cost $700

        // ========== Create courses ==========
        var math = new Course("Math 101", "M101", 15, 200.0);       // 15 points, room cost $200
        var physics = new Course("Physics 101", "P101", 20, 250.0); // 20 points, room cost $250

        // ========== Assign lecturers to courses ==========
        math.AssignLecturer(drSmith);
        physics.AssignLecturer(profJones);

        // ========== Students enrol in courses ==========
        tom.EnrolCourse(math);
        jane.EnrolCourse(math);
        bob.EnrolCourse(physics);

        // ========== Assign grades to students ==========
        tom.AssignGrade(math, "A");
        jane.AssignGrade(math, "B");
        bob.AssignGrade(physics, "C");

        // ========== Print results ==========
        Console.WriteLine("--- Student Info ---");
        Console.WriteLine($"{tom.Name} (ID: {tom.Id}) enrolled in {math.Name}, Grade: {tom.GetGrade(math)}");
        Console.WriteLine($"{jane.Name} (ID: {jane.Id}) enrolled in {math.Name}, Grade: {jane.GetGrade(math)}");
        Console.WriteLine($"{bob.Name} (ID: {bob.Id}) enrolled in {physics.Name}, Grade: {bob.GetGrade(physics)}");
        Console.WriteLine();

        Console.WriteLine("--- GPA ---");
        Console.WriteLine($"{tom.Name}'s GPA: {tom.GetGPA()}");
        Console.WriteLine($"{jane.Name}'s GPA: {jane.GetGPA()}");
        Console.WriteLine($"{bob.Name}'s GPA: {bob.GetGPA()}");
        Console.WriteLine();

        Console.WriteLine("--- Course Finances ---");
        Console.WriteLine($"{math.Name} Income: ${math.CalculateIncome()}");
        Console.WriteLine($"{math.Name} Cost: ${math.CalculateCost()}");
        Console.WriteLine($"{math.Name} Profit: ${math.CalculateProfit()}");
        Console.WriteLine();
        Console.WriteLine($"{physics.Name} Income: ${physics.CalculateIncome()}");
        Console.WriteLine($"{physics.Name} Cost: ${physics.CalculateCost()}");
        Console.WriteLine($"{physics.Name} Profit: ${physics.CalculateProfit()}");
    }
}