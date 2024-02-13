using System.Text.Json;

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
            var numbers = GetLists<Number>(Path_To_Numbers);
            var students = GetLists<Student>(Path_To_Students);
            
            var topThreeScores = GetTopThreeScores(numbers);

            
            var mergedListOfNamesAndAverages = GetMergedListOfNamesAndAverages(topThreeScores, students);

            foreach (var item in mergedListOfNamesAndAverages)
            {
                Console.WriteLine($"Average: {item.Average}, First Name: {item.FirstName}, Last Name: {item.LastName}");
            }

        }

        private static IEnumerable<(float AverageScore, string? FirstName, string? LastName)> GetMergedListOfNamesAndAverages(
            IEnumerable<dynamic> topThreeScores, List<Student> students)
        {
            var mergedListOfNamesAndAverages = topThreeScores.Join(
                students,
                sc => sc.StudentNumber,
                nc => nc.StudentNumber,
                (sc, nc) =>  
                (sc.AverageScore, 
                    nc.FirstName, 
                    nc.LastName ));
            return mergedListOfNamesAndAverages;
        }

        private static IEnumerable<dynamic> GetTopThreeScores(List<Number> numbers)
        {
            var topThreeScores = numbers
                .GroupBy(n => n.StudentNumber)
                .Select(g => new { StudentNumber = g.Key, AverageScore = g.Average(n => n.Score) })
                .OrderByDescending(p => p.AverageScore)
                .Take(3);
            return topThreeScores;
        }

        private static List<T> GetLists<T>(string path)
        {
            string jsonString = File.ReadAllText(path);
            var numbers = JsonSerializer.Deserialize<List<T>>(jsonString)!;
            return numbers;
        }
        
        
    }
}