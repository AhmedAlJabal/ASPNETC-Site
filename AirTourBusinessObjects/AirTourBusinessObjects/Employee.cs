using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirTourBusinessObjects
{
    public class Employee : Item
    {
        public Employee(string id)
            : base(id)
        {

        }

        public Employee()
        {

        }

        private string lname;
        private string fname;
        private string birthDate;
        private string hireDate;
        private string address;
        private string city;
        private string region;
        private string phone;
        private string notes;

        public string EmployeeID
        {
            get { return base.getID(); }
            set { base.setID(value); }
        }

        public string LName
        {
            get { return lname; }
            set { lname = value; }
        }

        public string FName
        {
            get { return fname; }
            set { fname = value; }
        }

        public string BirthDate
        {
            get { return birthDate; }
            set { birthDate = value; }
        }

        public string HireDate
        {
            get { return hireDate; }
            set { hireDate = value; }
        }

        public string Address
        {
            get { return address; }
            set { address = value; }
        }

        public string City
        {
            get { return city; }
            set { city = value; }
        }

        public string Region
        {
            get { return region; }
            set { region = value; }
        }

        public string Phone
        {
            get { return phone; }
            set { phone = value; }
        }

        public string Notes
        {
            get { return notes; }
            set { notes = value; }
        }







    }
}
