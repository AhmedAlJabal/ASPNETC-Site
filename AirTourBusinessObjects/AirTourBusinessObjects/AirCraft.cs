using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirTourBusinessObjects
{
    public class AirCraft : Item
    {
        private string model;
        private string manufacturer;
        private string businessCapacity;
        private string economyCapacity;

        public AirCraft(string id)
            : base(id)
        {

        }

        public AirCraft()
        {

        }

        public string AirCraftID
        {
            get { return base.getID(); }
            set { base.setID(value); }
        }

        public string Model
        {
            get { return model; }
            set { model = value; }
        }

        public string Manufacturer
        {
            get { return manufacturer; }
            set { manufacturer = value; }
        }

        public string BusinessCapacity
        {
            get { return businessCapacity; }
            set { businessCapacity = value; }
        }

        public string EconomyCapacity
        {
            get { return economyCapacity; }
            set { economyCapacity = value; }
        }


    }
}
