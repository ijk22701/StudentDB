//////////////////////////////////////////////////////////////////////////////////////
//Date         Developer            Description
//2021-02-26   Isaac K         --Creation of file for program
//2021-02-27   Isaac K         --Completion of program

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentDB
{
    internal class GradStudent : Student
    {
        public decimal TuitionCredit { get; set; }
        public string FacultyAdvisor { get; set; }

        public GradStudent(string first, string last, double gpa, string email, DateTime enrolled, decimal credit, string advisor)
            : base (new ContactInfo(first, last, email), gpa, enrolled)
        {
            TuitionCredit = credit;
            FacultyAdvisor = advisor;
        }

        
        public override string ToString()
        {
            return base.ToString() + $"    credit: {TuitionCredit:C}\n       Fac: {FacultyAdvisor}\n";
        }

        public override string ToStringForOutputFile()
        {
            string str = this.GetType() + "\n";
            str += base.ToStringForOutputFile();
            str += $"\n{TuitionCredit}\n";
            str += $"{FacultyAdvisor}"; //no CRLF here because it will be called in WriteLine()

            return str;
        }
    }
}
