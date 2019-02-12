using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns_Builder
{
    static class Program
    {
        static void Main(string[] args)
        {
            var students = GetStudentList();

            //var builder = ClassicBuilderPattern(students);
            var builder = FluentBuilderPattern(students);

            var report = builder.GetReport();
            Console.WriteLine(report);
            Console.ReadKey();
        }

        private static dynamic ClassicBuilderPattern(IEnumerable<Student> students)
        {
            // Classic Builder Pattern
            var builder = new ClassReportBuilder(students);
            // These things could go into a director but that may not always be needed.
            builder.SetHeader();
            builder.SetBody();
            builder.SetFooter();
            return builder;
        }

        private static dynamic FluentBuilderPattern(IEnumerable<Student> students)
        {
            // Fluent Builder Pattern
            var builder = new FluentClassReportBuilder(students)
                .SetHeader()
                .SetBody()
                .SetFooter();

            return builder;
        }

        static IEnumerable<Student> GetStudentList()
        {
            var students = new List<Student>
            {
                new Student { Name = "Larry", Birthdate = new DateTime(1941,11,4) },
                new Student { Name = "Moe", Birthdate = new DateTime(1942,1,24) },
                new Student { Name = "Curly", Birthdate = new DateTime(1921,6,9)}
            };

            return students;
        }
    }


}
