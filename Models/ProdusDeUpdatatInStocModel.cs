using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerStoc
{
    public class ProdusDeUpdatatInStocModel
    {

        private int idProdus;
        private int cantitate;

        public ProdusDeUpdatatInStocModel(int idProdus, int cantitate)
        {
            this.idProdus = idProdus;
            this.cantitate = cantitate;
        }

        public int IdProdus { get => idProdus; set => idProdus = value; }
        public int Cantitate { get => cantitate; set => cantitate = value; }
    }


}
