using System;
using System.Collections.Generic;

class Message
{
    public string Name { get; set; }
    public string Content { get; set; }
}

class Program
{
    static List<Message> messages = new List<Message>();//Is static necessary?

    static void ShowMessages()
    {
        if (messages.Count == 0)
        {
            Console.WriteLine("No message yet.");
            return;
        }
        for (int i = 0; i < messages.Count; i++)
        {
            Console.WriteLine($"{i+1}. {messages[i].Name}: {messages[i].Content}");
        }
    }

    static void AddMessage()
    {
        Console.Write("Your name: ");
        string name = Console.ReadLine();
        Console.Write("Your message: ");
        string content = Console.ReadLine();
        messages.Add(new Message { Name = name, Content = content });
        Console.WriteLine("Message posted!");
    }

    static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("\n--- Message Board ---");
            Console.WriteLine("1. View messages");
            Console.WriteLine("2. Post message");
            Console.WriteLine("3. Exit");

            Console.Write("Choose: ");
            string choice = Console.ReadLine();

            if (choice == "1") {
                ShowMessages();
            } else if (choice == "2") {
                AddMessage();
            } else if (choice == "3") {
                break;
            } else {
                Console.WriteLine("Invalid choice");
            }
        }
    }
}