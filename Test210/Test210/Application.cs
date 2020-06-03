
using System;
using Test210.Infrastructure;

namespace Test210
{
    public class Application : IApplication
    {
        public (string FirstName, string LastName, string FullName) TestTuples(string firstName, string lastName)
        {
            (string FirstName, string LastName, string FullName) person;

            person.FirstName = firstName;
            person.LastName = lastName;
            person.FullName = $"{firstName} {lastName}";


            return person;

            return (
                FirstName: firstName, 
                LastName: lastName, 
                FullName: $"{firstName} {lastName}");
        }
    }
}
