using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerStoc.Models
{
    public class IntrareModel
    {

        private int cantitate;
        private int idProdus;
        private int idFurnizor;
        private Decimal pretCumparare;
        public int Cantitate { get => cantitate; set => cantitate = value; }
        public int IdProdus { get => idProdus; set => idProdus = value; }
        public int IdFurnizor { get => idFurnizor; set => idFurnizor = value; }
        public decimal PretCumparare { get => pretCumparare; set => pretCumparare = value; }

        public IntrareModel(int cantitate, int idProdus, int idFurnizor, decimal pretCumparare)
        {
            this.cantitate = cantitate;
            this.idProdus = idProdus;
            this.idFurnizor = idFurnizor;
            this.pretCumparare = pretCumparare;
        }


    }
}
