using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns_Builder
{

    public interface IClassReportBuilder
    {
        void SetHeader();
        void SetFooter();
        void SetBody();

        ClassReport GetReport();

    }

    public class ClassReportBuilder : IClassReportBuilder
    {
        private ClassReport _classReport;
        private IEnumerable<Student> _students;

        public ClassReportBuilder(IEnumerable<Student> students)
        {
            _students = students;
            _classReport = new ClassReport();
        }

        public void SetHeader()
        {
            _classReport.ReportHeader = "This is the Header for the report.";
        }
        public void SetFooter()
        {
            _classReport.ReportFooter = "This is the FOOTER.";
        }
        public void SetBody()
        {
            foreach (var student in _students)
            {
                _classReport.ReportBody += $"Name: {student.Name} DOB: {student.Birthdate} \n";
            }
        }
        public ClassReport GetReport()
        {
            var report = _classReport;
            _classReport = new ClassReport(); // clear out for subsequent usage
            return report;
        }
    }
}
