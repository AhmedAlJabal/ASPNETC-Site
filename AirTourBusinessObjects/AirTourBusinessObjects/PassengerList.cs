using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirTourBusinessObjects
{
    public class PassengerList : DataList
    {
        public PassengerList()
           : base("Passenger", "PassengerID")
        { }

        protected override void GenerateList()
        {
            Passenger passenger = new Passenger();
            SetDataTableColumns(passenger);
            List.Clear();

            while (Reader.Read())
            {
                passenger = new Passenger(Reader.GetValue(0).ToString());
                base.SetValues(passenger);
                List.Add(passenger);
                AddDataTableRow(passenger);
            }
            Reader.Close();
            Connection.Close();

        }

    }
}
