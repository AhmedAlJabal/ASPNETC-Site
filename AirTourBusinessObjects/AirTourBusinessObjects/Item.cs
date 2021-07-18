using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirTourBusinessObjects
{
    public class Item
    {
        private string id;
        private bool valid;
        private string errorMessage;

        public Item(string id)
        {
            this.id = id;
            valid = true;
        }

        public Item()
        {
            valid = true;
        }

        public string getID()
        {
            return id;
        }

        public void setID(string id)
        {
            this.id = id;
        }


        public void setValid(bool valid)
        {
            this.valid = valid;
        }

        public bool getValid()
        {
            return valid;
        }

        public void setErrorMessage(string errorMessage)
        {
            this.errorMessage = errorMessage;
        }

        public string geterrorMessage()
        {
            return errorMessage;
        }

    }
}
