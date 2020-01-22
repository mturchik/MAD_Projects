using System;
using System.Collections.Generic;
using System.Linq;
using MAD_Lab1_Program2.Models;
using Newtonsoft.Json;
using NewsStyleUriParser = System.NewsStyleUriParser;

namespace MAD_Lab1_Program2
{
    internal static class Program
    {
        private static readonly List<Person> Persons = new List<Person>();
        private static string _userInput;
        private static int _parsedInput;

        /*
            Includes a Parent class Person (Name, Age, Id, Program - which is an enum), 
            a derived class Student inherits from Person (Credits Earned), 
            an Enum (Computer Science, Accounting, etc...) 
            and another class Teacher (Years of Service)
            Program asks user to enter the data for Students and teachers.  Once all data is entered, a formatted listing of all Persons.
            Program must include a single Collection.
         */

        private static void Main(string[] args)
        {
            while (true)
            {
                GetPersonType();

                switch (_parsedInput)
                {
                    case 1:
                        CreateStudent();
                        break;
                    case 2:
                        CreateTeacher();
                        break;
                    case 3: return;
                }

                PrintPeople();
            }
        }

        private static bool InputOutOfRange(int min, int max, int value) => value < min || value > max;

        private static void GetPersonType()
        {
            do
            {
                Console.Write("1) Add A Student\n" +
                              "2) Add A Teacher\n" +
                              "3) End\n==");
                _userInput = Console.ReadLine();
                if (!int.TryParse(_userInput, out _parsedInput))
                    Console.WriteLine($"Invalid input. Please enter a number.");
            } while (InputOutOfRange(1, 3, _parsedInput));
        }

        private static void PrintPrograms(string[] programs)
        {
            Console.WriteLine($"----------");
            for (var i = 0; i < programs.Length; i++)
            {
                var n = programs[i];
                Console.WriteLine($"-{i + 1}) {n}");
            }

            Console.WriteLine($"----------");
        }

        private static void PrintPeople()
        {
            Console.WriteLine($"----------");
            foreach (var person in Persons)
            {
                Console.WriteLine($"-  ID: {person.Id}  Name: {person.Name}  Age: {person.Age}");
                if (person.GetType() == typeof(Student))
                {
                    var student = (Student) person;
                    Console.WriteLine(
                        $"-  [{student.GetType().Name}]  Program: {person.Program.ToString()}  Credits Earned: {student.CreditsEarned}");
                }
                else if (person.GetType() == typeof(Teacher))
                {
                    var teacher = (Teacher) person;
                    Console.WriteLine(
                        $"-  [{teacher.GetType().Name}]  Program: {person.Program.ToString()}  Years of Service: {teacher.YearsOfService}");
                }
            }

            Console.WriteLine($"----------");
        }

        private static AcademicProgram GetPersonProgram(Person newPerson)
        {
            var programs = typeof(AcademicProgram).GetEnumNames();
            do
            {
                PrintPrograms(programs);
                Console.Write($"Choose the new {newPerson.GetType().Name}'s: Program\n==");
                _userInput = Console.ReadLine();
                if (!int.TryParse(_userInput, out _parsedInput))
                    Console.WriteLine($"Invalid input. Please enter a number.");
            } while (InputOutOfRange(1, programs.Length, _parsedInput));

            if (Enum.TryParse<AcademicProgram>(programs[_parsedInput - 1], out var program))
                return program;

            throw new Exception("Unable to parse the program choice into an enum.");
        }

        private static string GetNextPersonId()
        {
            var lastPerson = Persons.LastOrDefault();
            if (lastPerson is null)
                return "00001";

            int.TryParse(lastPerson.Id, out var lastId);
            var newId = $"{++lastId}";
            while (newId.Length < 5)
                newId = $"0{newId}";
            return newId;
        }

        private static void CreatePerson<T>(ref T newPerson) where T : Person
        {
            do
            {
                Console.Write($"Enter the new {newPerson.GetType().Name}'s: Name\n==");
                _userInput = Console.ReadLine();
            } while (string.IsNullOrWhiteSpace(_userInput));

            newPerson.Name = _userInput;

            do
            {
                Console.Write($"Enter the new {newPerson.GetType().Name}'s: Age\n==");
                _userInput = Console.ReadLine();
                if (!int.TryParse(_userInput, out _parsedInput))
                    Console.WriteLine($"Invalid input. Please enter a number.");
            } while (InputOutOfRange(1, 110, _parsedInput));

            newPerson.Age = _parsedInput;

            newPerson.Program = GetPersonProgram(newPerson);

            newPerson.Id = GetNextPersonId();
        }

        private static void CreateStudent()
        {
            var student = new Student();
            CreatePerson(ref student);

            do
            {
                Console.Write($"Enter the new {student.GetType().Name}'s: Credits Earned\n==");
                _userInput = Console.ReadLine();
                if (!int.TryParse(_userInput, out _parsedInput))
                    Console.WriteLine($"Invalid input. Please enter a number.");
            } while (InputOutOfRange(1, 100, _parsedInput));

            student.CreditsEarned = _parsedInput;

            Persons.Add(student);
        }

        private static void CreateTeacher()
        {
            var teacher = new Teacher();
            CreatePerson(ref teacher);

            do
            {
                Console.Write($"Enter the new {teacher.GetType().Name}'s: Years Of Service\n==");
                _userInput = Console.ReadLine();
                if (!int.TryParse(_userInput, out _parsedInput))
                    Console.WriteLine($"Invalid input. Please enter a number.");
            } while (InputOutOfRange(1, 100, _parsedInput));

            teacher.YearsOfService = _parsedInput;

            Persons.Add(teacher);
        }
    }
}