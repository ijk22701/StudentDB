//////////////////////////////////////////////////////////////////////////////////////
//Date         Developer            Description
//2021-02-26   Isaac K         --Creation of file for program
//2021-02-27   Isaac K         --Completion of program

namespace StudentDB
{
    public class ContactInfo
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }

        private string emailAddress;

        public ContactInfo(string first, string last, string email)
        {
            FirstName = first;
            LastName = last;
            EmailAddress = email;
        }

        public string EmailAddress
        {
            get
            {
                return emailAddress;
            }
            set
            {
                //passed in email must have at least 3 chars and one must be "@"
                if (value.Contains("@") && value.Length > 3)
                {
                    emailAddress = value;
                }
                else
                {
                    //TODO: not sure how we want to handle this - look into possible regex
                    EmailAddress = "ERROR - INVALID EMAIL";
                }

            }
        }

        //lamda expression for user friendly printing of the contact ingo
        public override string ToString() => $"{FirstName}\n{LastName}\n{EmailAddress}\n";
        public string ToStringLegal() => $"{LastName}, {FirstName}\n{EmailAddress}\n";

    }
}