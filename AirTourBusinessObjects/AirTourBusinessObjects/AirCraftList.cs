using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirTourBusinessObjects
{
    public class AirCraftList : DataList
    {
        public AirCraftList()
           : base("Aircraft", "AircraftID")
        { }

        protected override void GenerateList()
        {
            AirCraft aircraft = new AirCraft();
            SetDataTableColumns(aircraft);
            List.Clear();

            while (Reader.Read())
            {
                aircraft = new AirCraft(Reader.GetValue(0).ToString());
                base.SetValues(aircraft);
                List.Add(aircraft);
                AddDataTableRow(aircraft);
            }
            Reader.Close();
            Connection.Close();

        }
    }
}
