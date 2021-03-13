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
    internal class Student
    {

        public ContactInfo Info { get; set; }

        private double gradePtAvg;

        public DateTime EnrollmentDate {get; set;}

        public Student()
        {

        }

        //overloading the ctor for the student class
        //fully specified ctor for making student POCO objects
        public Student(ContactInfo info, double grades, DateTime enrolled)
        {
            Info = info;
            GradePtAvg = grades;
            EnrollmentDate = enrolled;
        }

        public double GradePtAvg
        {
            get
            {
                return gradePtAvg;
            }
            set
            {
                //only set the gpa if the passed in value is between
                //"legal" defined gpa range, 0.7 to 4 inclusive
                if (0.7 <= value && value <= 4)
                {
                    gradePtAvg = value;
                } 
                else
                {
                    gradePtAvg = 0.7;
                }
            }
        }



        //format a student object to a string for
        //displaying student data to the user in the UI
        public override string ToString()
        {
            //create a temporary string to hold the output
            string str = string.Empty;

            str += "************Student Record************\n";
            //build up the string with data from the object
            str += $"First Name: {Info.FirstName} \n";
            str += $" Last Name: {Info.LastName} \n";
            str += $"       GPA: {gradePtAvg}\n";
            str += $"     Email: {Info.EmailAddress}\n";
            str += $"  Enrolled: {EnrollmentDate}\n";


            //return the string
            return str;
        }

        //format a student object to a string for
        //writing the data to the output file
        public virtual string ToStringForOutputFile()
        {
            //create a temporary string to hold the output
            string str = string.Empty;

            //build up the string with data from the object
            str += $"{Info.FirstName} \n";
            str += $"{Info.LastName} \n";
            str += $"{gradePtAvg}\n";
            str += $"{Info.EmailAddress}\n";
            str += $"{EnrollmentDate}";


            //return the string
            return str;
        }
    }
}