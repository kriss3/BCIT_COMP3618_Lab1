using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using static System.Console;

namespace Serialize_People
{
    // A simple program that accepts a name, year, month date,
    // creates a Person object from that information, 
    // and then displays that person's age on the console.
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                // If they provide no arguments, display the last person
                Person p = Deserialize();
                WriteLine(p.ToString());
            }
            else
            {
                try
                {
                    if (args.Length != 4)
                    {
                        throw new ArgumentException("You must provide four arguments.");
                    }

                    DateTime dob = new DateTime(Int32.Parse(args[1]), Int32.Parse(args[2]), Int32.Parse(args[3]));
                    Person p = new Person(args[0], dob);
                    WriteLine(p.ToString());

                    Serialize(p);
                }
                catch (Exception ex)
                {
                    DisplayUsageInformation(ex.Message);
                }
            }
        }

        private static void DisplayUsageInformation(string message)
        {
            WriteLine("\nERROR: Invalid parameters. " + message);
            WriteLine("\nSerialize_People \"Name\" Year Month Date");
            WriteLine("\nFor example:\nSerialize_People \"Tony\" 1922 11 22");
            WriteLine("\nOr, run the command with no arguments to display that previous person.");
        }

        private static void Serialize(Person sp)
        {
            using (FileStream fs = new FileStream("Person.Dat", FileMode.Create))
            {
                // Create an instance of BinaryFormatter
                BinaryFormatter bf = new BinaryFormatter();
                // Serialize the data to the file
                bf.Serialize(fs, sp);
            }
        }

        private static Person Deserialize()
        {
            Person dsp = new Person();

            using (FileStream fs = new FileStream("Person.Dat", FileMode.Open))
            {
                BinaryFormatter bf = new BinaryFormatter();
                dsp = (Person)bf.Deserialize(fs);
            }
            return dsp;
        }
    }
}
