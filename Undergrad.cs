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
    public enum YearRank
    {
        Freshman = 1,
        Sophomore = 2,
        Junior = 3,
        Senior = 4
    }

    internal class Undergrad : Student
    {
        public YearRank Rank { get; set; }
        public string DegreeMajor { get; set; }

        public Undergrad(string first, string last, double gpa, string email, DateTime enrolled, YearRank rank, string degree)
            :base (new ContactInfo(first, last, email), gpa, enrolled)
        {
            Rank = rank;
            DegreeMajor = degree;
        }

        //does not go to output file
        public override string ToString() => base.ToString() + $"      Rank: {Rank}\n     Major: {DegreeMajor}\n";

        public override string ToStringForOutputFile()
        {
            string str = this.GetType() + "\n";
            str += base.ToStringForOutputFile();
            str += $"\n{Rank}\n";
            str += $"{DegreeMajor}"; //no CRLF here because it will be called in WriteLine()

            return str;
        }

    }
}
