using System;
using System.Runtime.Serialization;

namespace Serialize_People
{
    /// <summary>
    /// Krzysztof Szczurowski
    /// BCIT COMP3618 Lab 1;
    /// Class Person to represent objects of type person;
    /// </summary>
    [Serializable]
    public class Person : IDeserializationCallback
    {
        public string name;
        public DateTime dateOfBirth;
        [NonSerialized]
        public int age;

        public Person(string _name, DateTime _dateOfBirth)
        {
            name = _name;
            dateOfBirth = _dateOfBirth;
            CalculateAge();
        }

        public Person()
        {
        }

        public override string ToString()
        {
            return $"{name} was born on {dateOfBirth.ToShortDateString()} and is {age.ToString()} years old.";
        }

        private void CalculateAge()
        {
            age = DateTime.Now.Year - dateOfBirth.Year;

            // If they haven't had their birthday this year, 
            // subtract a year from their age
            if (dateOfBirth.AddYears(DateTime.Now.Year - dateOfBirth.Year) > DateTime.Now)
            {
                age--;
            }
        }

        public void OnDeserialization(object sender)
        {
            CalculateAge();
        }
    }
}
