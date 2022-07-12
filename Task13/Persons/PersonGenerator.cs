using System;
using System.Collections.Generic;
using Task13.FileHandler;

namespace Task13.Persons
{
    internal class PersonGenerator
    {
        private static Random random = new Random();
        public Person GeneratePerson(int ageMinBound = 0, int ageMaxBound = 100, int coordinateMinBound = 0, int coordinateMaxBound = 10, int minTimeServiceBound = 0, int maxTimeServiceBound = 100)
        {
            Person person = new()
            {
                Age = random.Next(ageMinBound, ageMaxBound),
                Coordinate = random.Next(coordinateMinBound, coordinateMaxBound - 1) + Math.Round(random.NextDouble(), 3),
                TimeService = random.Next(minTimeServiceBound, maxTimeServiceBound)
            };
            person.Name = $"Random{person.Id.ToString()[..3]}";
            person.Status = $"Status{person.Id.ToString()[..3]}";
            return person;
        }
        public IEnumerable<Person> GeneratePersonsCollection(int amount, int ageMinBound = 0, int ageMaxBound = 100, int coordinateMinBound = 0, int coordinateMaxBound = 10)
        {
            for (int i = 0; i < amount; i++)
            {
                yield return GeneratePerson(ageMinBound, ageMaxBound, coordinateMinBound, coordinateMaxBound);
            }
        }
        public void RandomPersonsIntoFIleGenerate(string filePath, int amount, bool append = false, int ageMinBound = 0, int ageMaxBound = 100, int coordinateMinBound = 0, int coordinateMaxBound = 10)
        {
            FileHandlerService.WriteToFileCollection
            (
                obj: GeneratePersonsCollection(amount, ageMinBound, ageMaxBound, coordinateMinBound, coordinateMaxBound),
                serializer: new TxtSerializer(),
                path: filePath,
                append: append
            );
        }
    }
}
