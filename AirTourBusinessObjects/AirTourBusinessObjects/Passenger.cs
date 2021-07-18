using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirTourBusinessObjects
{
    public class Passenger : Item
    {
        private string firstName;
        private string lastName;
        private string reservaionID;

        public Passenger(string id)
            : base(id)
        {

        }

        public Passenger()
        {

        }

        public string PassengerID
        {
            get { return base.getID(); }
            set { base.setID(value); }
        }

        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }

        public string LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }

        public string ReservationID
        {
            get { return reservaionID; }
            set { reservaionID = value; }
        }


    }
}
