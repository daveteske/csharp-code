using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns_Builder
{
    public class Student
    {
        public string Name { get; set; }
        public DateTime Birthdate { get; set; }

    }

    public class ClassReport
    {
        public string ReportHeader { get; set; }
        public string ReportFooter { get; set; }
        public string ReportBody { get; set; }

        public override string ToString()
        {
            return new StringBuilder()
                    .AppendLine(ReportHeader)
                    .AppendLine(ReportBody)
                    .AppendLine(ReportFooter)
                    .ToString();
        }
    }

}
