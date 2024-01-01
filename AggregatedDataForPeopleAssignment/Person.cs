using System;

/// <summary>
/// Summary description for Class1
/// </summary>
public class Person
{
    public Person(string firstName, string lastName, int age, double weight, string gender)
    {
        FirstName = firstName;
        LastName = lastName;
        Age = age;
        Weight = weight;
        Gender = gender;
    }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Age { get; set; }
    public double Weight { get; set; }
    public string Gender { get; set; }
}


