using System.Text.Json;
using System.Linq;

namespace HelloWorld
{
    public class Student
    {
        public int StudentNumber { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }

    class Number
    {
        public int StudentNumber { get; set; }
        public string? Lesson { get; set; }
        public float Score { get; set; }
    }

    class Program
    {
        public static string Path_To_Numbers = "./numbers.json";
        public static string Path_To_Students = "./students.json";

        public static void Main(string[] args)
        {
            string jsonString = File.ReadAllText(Path_To_Numbers);
            var numbers = JsonSerializer.Deserialize<List<Number>>(jsonString)!;
            getList(jsonString, Number.Class);
            jsonString = File.ReadAllText(Path_To_Students);
            var students = JsonSerializer.Deserialize<List<Student>>(jsonString)!;


            
            var topThreeScores = numbers
            .GroupBy(n => n.StudentNumber)
            .Select(g => new { StudentNumber = g.Key, AverageScore = g.Average(n => n.Score) })
            .OrderByDescending(p => p.AverageScore)
            .Take(3);

            
            var mergedListOfNamesAndAverages = topThreeScores.Join(
            students,
            sc => sc.StudentNumber,
            nc => nc.StudentNumber,
            (sc, nc) => new 
                {Average = sc.AverageScore, 
                FirstName = nc.FirstName, 
                LastName = nc.LastName });

            foreach (var item in mergedListOfNamesAndAverages)
            {
                Console.WriteLine($"Average: {item.Average}, First Name: {item.FirstName}, Last Name: {item.LastName}");
            }

        }

        private static List<T> getList <T>(string path, T o)
        {
            var jsonString = File.ReadAllText(path);
            var numbers = JsonSerializer.Deserialize<List<T>>(jsonString)!;
            return numbers;
        }
    }
}