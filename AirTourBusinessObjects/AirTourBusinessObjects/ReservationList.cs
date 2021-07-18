using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirTourBusinessObjects
{
    public class ReservationList : DataList
    {
        public ReservationList()
          : base("Reservation", "ReservationID")
        { }

        protected override void GenerateList()
        {
            Reservation reservation = new Reservation();
            SetDataTableColumns(reservation);
            List.Clear();

            while (Reader.Read())
            {
                reservation = new Reservation(Reader.GetValue(0).ToString());
                base.SetValues(reservation);
                List.Add(reservation);
                AddDataTableRow(reservation);
            }
            Reader.Close();
            Connection.Close();

        }


        //extra function
        //this function is used to get the future reservation of the user by joining in 4 tables
        //where a column value(customer ID) = the the CustomerID
        //and the scheduled flight date is larger than the Current_TimeStamp (current date)
        public void getFutureReservations(string custID)
        {
            //opening the connection and clearing the parameters
            connection.Open();
            command.Parameters.Clear();

            //adding the customer id as a parameter
            command.Parameters.AddWithValue("@custID", custID);

            //the command
            command.CommandText = $"SELECT distinct Reservation.* FROM Reservation , Passenger, ReservedSeat, ScheduledFlight " +
                " where Reservation.CustomerID = @custID and Passenger.ReservationID = Reservation.ReservationID" +
                " and ReservedSeat.PassengerID = Passenger.PassengerID" +
                " and ReservedSeat.ScheduledFlightID = ScheduledFlight.ScheduledFlightID " +
                " and ScheduledFlight.FlightDate > CURRENT_TIMESTAMP;";

            //executing the command
            reader = command.ExecuteReader();

            //calling the generateList method
            GenerateList();

        }


    }
}
