// See https://aka.ms/new-console-template for more information
using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;

namespace LastNameListApp
{
    class Program
    {
        static string FilePath = @"C:\\Users\\Shana Glazer\\source\\repos\\Software\\person.csv";
        static Random rnd = new Random();
        static void CreateDataSet()
        {
            List<string[]> PersonData = new();
            List<string> GenderList = new() { "Mail", "Femail", "Unknown" };
            for (int i = 0; i < 1000; i++)
            {
                string firstname = GetFirstName();
                string lastname = GetLastName();
                int age = rnd.Next(18, 70);
                double weight = Math.Round(rnd.NextDouble() * (330 - 77) + 77, 2);
                string gender = GenderList[rnd.Next(0, GenderList.Count)];
                PersonData.Add(new string[] { firstname, lastname, age.ToString(), weight.ToString(), gender });
            }
            bool fileexists = File.Exists(FilePath);
            if (fileexists == false)
            {
                using (var file = File.CreateText(FilePath))
                {
                    foreach (var arr in PersonData)
                    {
                        file.WriteLine(string.Join(",", arr));
                    }
                }
            }
        }

        static string GetLastName()
        {
            List<string> lastNames = new List<string>
            {
                "Smith", "Johnson", "Williams", "Jones", "Brown", "Davis", "Miller", "Wilson", "Moore", "Taylor",
                "Anderson", "Thomas", "Jackson", "White", "Harris", "Martin", "Thompson", "Garcia", "Martinez", "Robinson",
                "Clark", "Rodriguez", "Lewis", "Lee", "Walker", "Hall", "Allen", "Young", "Hernandez", "King",
                "Wright", "Lopez", "Hill", "Scott", "Green", "Adams", "Baker", "Gonzalez", "Nelson", "Carter",
                "Mitchell", "Perez", "Roberts", "Turner", "Phillips", "Campbell", "Parker", "Evans", "Edwards", "Collins"
            };
            return lastNames[rnd.Next(0, lastNames.Count)];
        }

        static string GetFirstName()
        {
            List<string> firstNames = new List<string>
            {
                "John", "Emma", "Michael", "Sophia", "William", "Olivia", "James", "Ava", "Alexander", "Isabella",
                "Liam", "Mia", "Benjamin", "Charlotte", "Elijah", "Amelia", "Lucas", "Harper", "Henry", "Evelyn",
                "Mason", "Abigail", "Ethan", "Emily", "Logan", "Elizabeth", "Oliver", "Avery", "Jacob", "Sofia",
                "Daniel", "Ella", "Matthew", "Madison", "Jackson", "Scarlett", "Sebastian", "Victoria", "Aiden", "Grace",
                "David", "Chloe", "Joseph", "Riley", "Samuel", "Aria", "Gabriel", "Luna", "Carter", "Penelope"
            };
            return firstNames[rnd.Next(0, firstNames.Count)];
        }

        static List<Person> ReadCsv(string filepath)
        {
            List<Person> people = new List<Person>();

            try
            {
                using (StreamReader reader = new StreamReader(filepath))
                {
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        var values = line.Split(',');

                        string firstName = values[0];
                        string lastName = values[1];
                        int age = int.Parse(values[2]);
                        double weight = double.Parse(values[3]);
                        string gender = values[4];

                        people.Add(new Person(firstName, lastName, age, weight, gender));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Cannot read CSV file: {ex.Message}");
            }

            return people;
        }

        static float CalAverageAgeOfPeople(List<Person> people)
        {
            float average = 0;
            var sumAge = people?.Sum(x => x.Age) ?? 0;
            if(sumAge > 0) 
                average = sumAge / people.Count;
            return average;
        }

        static List<Person>? GetPeopleInWeightRange(List<Person> people, double minWeight, double maxWeight)
        {
            return people?.Where(x => x.Weight >= minWeight && x.Weight <= maxWeight)?.ToList();
        }

        static int CountPeopleInWeightRange(List<Person> people, double minWeight, double maxWeight)
        {
            return GetPeopleInWeightRange(people, minWeight, maxWeight)?.Count ?? 0;
        }

        static float AverageAgeOfPeopleInWeightRange(List<Person> people, double minWeight, double maxWeight)
        {
            var peopleInWeightRange = GetPeopleInWeightRange(people, minWeight, maxWeight);
            if(peopleInWeightRange?.Count > 0)
                return CalAverageAgeOfPeople(peopleInWeightRange);
            return 0;
        }

        static void Main()
        {
            CreateDataSet();
            List<Person> people = ReadCsv(FilePath);

            float averageage = CalAverageAgeOfPeople(people);
            Console.WriteLine($"Average age of all people is: {averageage}");

            int countintweightrange = CountPeopleInWeightRange(people, 120, 140);
            Console.WriteLine($"Total number of people weighing between 120lbs and 140lbs is: {countintweightrange}");

            float averageageofpeopleinrange = AverageAgeOfPeopleInWeightRange(people, 120, 140);
            Console.WriteLine($"Average age of the people weighing between 120lbs and 140lbs is: {averageageofpeopleinrange}");
        }

    }
}
