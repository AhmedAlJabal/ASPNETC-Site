using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirTourBusinessObjects
{
    public class ItemJoin : Item
    {
        private string idJoin;

        public ItemJoin(string id, string idJoin)
            : base(id)
        {
            this.idJoin = idJoin;
        }

        public ItemJoin() { }


        public string getIdJoin()
        {
            return idJoin;
        }

        public void setIdJoin(string idJoin)
        {
            this.idJoin = idJoin;
        }

    }
}
