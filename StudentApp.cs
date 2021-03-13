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
    internal class StudentApp
    {
        //create the storage for a collection of students
        private const string STUDENTDB_DATAFILE = "STUDENTDB_DATAFILE.txt";
        private List<Student> students = new List<Student>();

        internal void ReadDataFromInputFile()
        {
            //create a stream reader to attatch to the input file on disk
            StreamReader infile = new StreamReader(STUDENTDB_DATAFILE);

            //use the file to read in student data
            string studentType;
            while ((studentType = infile.ReadLine()) !=null)
            {
                string first = infile.ReadLine();
                string last = infile.ReadLine();
                double gpa = double.Parse(infile.ReadLine());
                string email = infile.ReadLine();
                DateTime date = DateTime.Parse(infile.ReadLine());

                //now we've read everything for a studnt - branch depending on
                //what kind of student

                if (studentType == "StudentDB.Undergrad")
                {
                    YearRank rank = (YearRank)Enum.Parse(typeof(YearRank), infile.ReadLine());
                    string major = infile.ReadLine();

                    Undergrad undergrad = new Undergrad(first, last, gpa, email, date, rank, major);
                    students.Add(undergrad);

                } else if (studentType == "StudentDB.GradStudent")
                {
                    decimal tuition = decimal.Parse(infile.ReadLine());
                    string facAdvisor = infile.ReadLine();

                    GradStudent grad = new GradStudent(first, last, gpa, email, date, tuition, facAdvisor);
                    students.Add(grad);
                }

                /*
                //create the new read-in student from the data and store in the list
                Student stu = new Student(first, last, gpa, email, date);
                students.Add(stu);
                Console.WriteLine($"Done recording record for: \n {stu}");
                */
            }
            //release the resource
            Console.WriteLine("Reading input file is complete...");
            infile.Close();

        }

        internal void RunDatabaseApp()
        {
            //display a menu for the main selection of CRUD operations
            while (true)
            {
                //display a menu for the main selection of CRUD operations
                DisplayMainMenu();

                //capturing user's selection
                char selection = GetUserSelection();
                string email = string.Empty;

                //doing that thing (method call) that the user indicated
                switch (selection)
                {
                    case 'A':
                    case 'a':
                        AddStudentRecord();
                        break;
                    case 'F':
                    case 'f':
                        FindStudentRecord(out email);
                        break;
                    case 'P':
                    case 'p':
                        PrintAllRecords();
                        break;
                    case 'D':
                    case 'd':
                        DeleteStudentRecord();
                        break;
                    case 'M':
                    case 'm':
                        ModifyStudentRecord();
                        break;
                    case 'E':
                    case 'e':
                        ExitApplicationWithoutSave();
                        break;
                    case 'S':
                    case 's':
                        SaveAllChangesAndQuit();
                        break;
                    default:
                        Console.WriteLine($"{selection} is not a valid choice!"); //restarts user until they make a valid choice
                        break;

                }
                Console.WriteLine();

            }
        }

        //Searches for and modifies the student
        private void ModifyStudentRecord()
        {
            string email = string.Empty;
            Student stu = FindStudentRecord(out email);
            if (stu != null)
            {
                ModifyStudent(stu);
            }
            else
            {
                //record is not in database
                Console.WriteLine($"**Record not found. \nCannot edit record for user {email}");
            }
        }

        //modifies the student profile
        private void ModifyStudent(Student stu)
        {
            string studentType = stu.GetType().ToString();
            Console.WriteLine(stu);
            Console.WriteLine($"Editing student type: {studentType.Substring(10)}");
            DisplayModifyMenu(); //displays list of options for user
            char selection = GetUserSelection();
            if (studentType == "StudentDB.Undergrad")
            {
                Undergrad undergrad = stu as Undergrad;
                //if student is an undergrad
                switch (selection)
                {
                    case 'Y':
                    case 'y':
                        Console.WriteLine("\nEnter the new year/rank in your school from the following choices:");
                        Console.Write("[1] Freshman, [2] Sophomore, [3] Junior, [4] Senior: ");
                        undergrad.Rank = (YearRank)int.Parse(Console.ReadLine());
                        break;
                    case 'D':
                    case 'd':
                        Console.Write("\nEnter the new degree major: ");
                        undergrad.DegreeMajor = Console.ReadLine();
                        break;
                }
            }
            else if (studentType == "StudentDB.GradStudent")
            {
                GradStudent grad = stu as GradStudent;
                //if student is a grad
                switch (selection)
                {
                    case 'T':
                    case 't':  //tuition credit
                        Console.Write("\nEnter new tuiton reimbursement credit:");
                        grad.TuitionCredit = decimal.Parse(Console.ReadLine());
                        break;
                    case 'A':
                    case 'a':
                        Console.Write("\nEnter new faculty advisor name: ");
                        grad.FacultyAdvisor = Console.ReadLine();
                        break;
                }
            }

            switch (selection)
            {
                case 'F':
                case 'f':
                    Console.Write("\nEnter new student first name: ");
                    stu.Info.FirstName = Console.ReadLine();
                    break;
                case 'L':
                case 'l':
                    Console.Write("\nEnter new student last name: ");
                    stu.Info.LastName = Console.ReadLine();
                    break;
                case 'G':
                case 'g':
                    Console.Write("\nEnter new student grade pt average: ");
                    stu.GradePtAvg = double.Parse(Console.ReadLine());
                    break;
                case 'E':
                case 'e':
                    Console.Write("\nEnter new student Enrollment date: ");
                    stu.EnrollmentDate = DateTime.Parse(Console.ReadLine());
                    break;
            }
            Console.WriteLine($"\n Edit operation finished. Current record info :\n{stu}\nPress any key to continue...");
            Console.ReadKey();
        }

        private void DisplayModifyMenu()
        {
            Console.WriteLine(@"
        *************************************************
        ****************Edit Student Menu****************
        ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        [F]irst name
        [L]ast name
        [G]rade pt average
        [E]nrollment date
        [Y]ear in school                 (Undergrad only)
        [D]egree major                   (Undergrad only)
        [T]uition teaching credit         (Graduate only)
        Faculty [A]dvisor                 (Graduate only)
        **Email address can never be modified. See admin.
        ");
        }

        private void AddStudentRecord()
        {
            //first search the list to see if this email record already exists
            string email = string.Empty;
            Student stu = FindStudentRecord(out email);
            if(stu == null)
            {
                //Record was not found - go ahead and add
                //gather all data needed for a new student
                Console.WriteLine($"Adding new student, email: {email}");

                //start gathering data
                //do not need email

                Console.WriteLine("Enter first name: ");
                string first = Console.ReadLine();
                Console.WriteLine("Enter last name: ");
                string last = Console.ReadLine();
                Console.WriteLine("Enter grade point average: ");
                double gpa = double.Parse(Console.ReadLine());

                //find out what kind of student
                Console.WriteLine("[U]ndergrad or [G]rad student? ");
                char studentType = char.Parse(Console.ReadLine().ToUpper());

                //branch based on type of student

                if (studentType == 'U')
                {
                    //reading an enumerated type
                    Console.WriteLine("[1]Freshman, [2]Sophomore, [3]Junior, [4]Senior");
                    Console.Write("Enter year/rank in school from above choices: ");
                    YearRank rank = (YearRank)int.Parse(Console.ReadLine());

                    Console.Write("Enter the major degree program: ");
                    string major = Console.ReadLine();


                    //TODO: test if this use of polymorphism is allowing undergrad info
                    //in the list collection
                    stu = new Undergrad(first, last, gpa, email, DateTime.Now, rank, major);
                    students.Add(stu);
                }
                else if (studentType == 'G')
                {
                    //gather additional grad student info
                    Console.Write("Enter the tuition reimbursement earned (no commas): $");
                    decimal discount = decimal.Parse(Console.ReadLine());
                    Console.Write("Enter full name of graduate faculty advisor: ");
                    string facAdvisor = Console.ReadLine();

                    GradStudent grad = new GradStudent(first, last, gpa, email, DateTime.Now, discount, facAdvisor);
                    students.Add(grad);
                } 
                else
                {
                    Console.WriteLine($"ERROR: No student {email} created \n\"{studentType}\" is not valid type.");
                }
            }
            else
            {
                Console.WriteLine($"{email} record is already in the database \nRecord cannot be added.");
                //TODO would you like to update?
            }
        }

        //Finds and deletes a student based on email
        private void DeleteStudentRecord()
        {
            string email = string.Empty;
            Student stu = FindStudentRecord(out email);
            if(stu != null)
            {
                //record was found - go ahead and remove it
                students.Remove(stu);
                Console.WriteLine($"User {email} was deleted from the record.");
            }
            else
            {
                Console.WriteLine($"**Record not found** \nCan't delete record for user: {email}");
            }
        }

        //Searches the current list for a student record
        //OUTPUT: returns the student object if found, null if not found
        //email contains the search string
        private Student FindStudentRecord(out string email)
        {
            Console.WriteLine("\nENTER email address (primary key) to search");
            email = Console.ReadLine();

            foreach (var student in students)
            {
                if(email == student.Info.EmailAddress)
                {
                    //found it
                    Console.WriteLine($"Found email address: {student.Info.EmailAddress}");
                    return student;
                }
            }

            Console.WriteLine($"{email} was not found.");
            return null;
        }

        //prints all student records
        private void PrintAllRecords()
        {
            foreach (var student in students)
            {
                Console.WriteLine(student);
            }
        }

        //gets char from user input
        private char GetUserSelection()
        {
            ConsoleKeyInfo keyPressed = Console.ReadKey();
            return keyPressed.KeyChar;
        }

        //saves program before quitting
        private void SaveAllChangesAndQuit()
        {
            Console.WriteLine("\nAre you sure you want to save all changes?"); //Saving changes can be a mistake sometimes, so I wanted to check
            if (PromptYesOrNo())
            {
                WriteDataToOutputFile();
                Environment.Exit(0);
            }
        }

        //does not save before quitting
        private void ExitApplicationWithoutSave()
        {
            Console.WriteLine("\nAre you sure you would like to quit without saving?"); //makes sure user wants to quit without saving
            if (PromptYesOrNo()) 
            {
                Environment.Exit(0);
            }
        }

        //prompts the user for yes or no
        private bool PromptYesOrNo()
        {
            //prompt user
            Console.WriteLine("[Y] for yes, [N] for no.");
            char response = GetUserSelection();
            //decode response
            if (response == 'N' || response == 'n')
            {
                Console.WriteLine("\nPress any key to return to menu");
                return false;
            }
            else if (response == 'Y' || response == 'y')
            {
                Console.WriteLine();
                return true; //user says yes
            }

            //return user to menu, no harm done

            Console.WriteLine("\nInvalid input: returning to menu");

            return false;
            

        }

        //displays menu
        private void DisplayMainMenu()
        {
            Console.WriteLine(@"
        ********************************************
        ************Student Database App************
        ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        [A]dd a student record
        [F]ind a record
        [P]rint all records
        [D]elete an existing record
        [M]odify an existing record
        [E]xit without saving changes
        [S]ave all changes and quit
        ");
            Console.WriteLine("ENTER user selection: ");
        }


        internal void WriteDataToOutputFile()
        {
            //create the output file details
            StreamWriter outFile = new StreamWriter(STUDENTDB_DATAFILE);

            
            foreach (var student in students)
            {
                //using the output file
                outFile.WriteLine(student.ToStringForOutputFile());
                Console.WriteLine(student.ToStringForOutputFile());
            }


            //closing the output file
            outFile.Close();
        }
    }
}