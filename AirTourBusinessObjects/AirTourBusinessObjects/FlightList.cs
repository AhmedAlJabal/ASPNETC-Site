using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirTourBusinessObjects
{
    public class FlightList : DataList
    {
        public FlightList()
           : base("Flight", "FlightID")
        { }

        protected override void GenerateList()
        {
            Flight flight = new Flight();
            SetDataTableColumns(flight);
            List.Clear();

            while (Reader.Read())
            {
                flight = new Flight(Reader.GetValue(0).ToString());
                base.SetValues(flight);
                List.Add(flight);
                AddDataTableRow(flight);
            }
            Reader.Close();
            Connection.Close();

        }

    }
}
