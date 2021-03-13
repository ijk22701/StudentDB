//////////////////////////////////////////////////////////////////////////////////////
//Date         Developer            Description
//2021-02-26   Isaac K         --Creation of file for program
//2021-02-27   Isaac K         --Completion of program

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentDB
{
    class Program
    {

        static void Main(string[] args)
        {
            //create a single instance of the db application
            StudentApp app = new StudentApp();

            //read in the data from the data file
            app.ReadDataFromInputFile();

            //Rrun the database app
            app.RunDatabaseApp();

            //write data to the output file
            app.WriteDataToOutputFile();
        }
    }
}
