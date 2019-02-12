using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns_Builder
{
    public interface IFluentClassReportBuilder
    {
        IFluentClassReportBuilder SetHeader();
        IFluentClassReportBuilder SetFooter();
        IFluentClassReportBuilder SetBody();

        ClassReport GetReport();

    }

    public class FluentClassReportBuilder : IFluentClassReportBuilder
    {
        private ClassReport _classReport;
        private IEnumerable<Student> _students;

        public FluentClassReportBuilder(IEnumerable<Student> students)
        {
            _students = students;
            _classReport = new ClassReport();
        }

        public IFluentClassReportBuilder SetHeader()
        {
            _classReport.ReportHeader = "This is the Header for the report.\n";
            return this;
        }
        public IFluentClassReportBuilder SetFooter()
        {
            _classReport.ReportFooter = "This is the FOOTER.";
            return this;
        }
        public IFluentClassReportBuilder SetBody()
        {
            foreach (var student in _students)
            {
                _classReport.ReportBody += $"Name: {student.Name} DOB: {student.Birthdate} \n";
            }
            return this;
        }
        public ClassReport GetReport()
        {
            var report = _classReport;
            _classReport = new ClassReport(); // clear out for subsequent usage
            return report;
        }
    }
}
