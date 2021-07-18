using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirTourBusinessObjects
{
   public class Flight : Item
    {
        private string flightNumber;
        private string orgin;
        private string destination;
        private string departure;
        private string arrival;
        private string airline;
        private string aircraftID;
        private string terminal;
        private string stops;
        private string fare;
        private string days;
        private string miles;

        public Flight(string id)
            : base(id)
        {

        }

        public Flight()
        {

        }

        public string FlightID
        {
            get { return base.getID(); }
            set { base.setID(value); }
        }

        public string FlightNumber
        {
            get { return flightNumber; }
            set { flightNumber = value; }
        }

        public string Orgin
        {
            get { return orgin; }
            set { orgin = value; }
        }

        public string Destination
        {
            get { return destination; }
            set { destination = value; }
        }

        public string Departure
        {
            get { return departure; }
            set { departure = value; }
        }

        public string Arrival
        {
            get { return arrival; }
            set { arrival = value; }
        }

        public string Airline
        {
            get { return airline; }
            set { airline = value; }
        }

        public string AircraftID
        {
            get { return aircraftID; }
            set { aircraftID = value; }
        }

        public string Terminal
        {
            get { return terminal; }
            set { terminal = value; }
        }

        public string Stops
        {
            get { return stops; }
            set { stops = value; }
        }

        public string Fare
        {
            get { return fare; }
            set { fare = value; }
        }

        public string Days
        {
            get { return days; }
            set { days = value; }
        }

        public string Miles
        {
            get { return miles; }
            set { miles = value; }
        }
    }
}
