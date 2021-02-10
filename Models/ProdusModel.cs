using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerStoc.Models
{
    public class ProdusModel
    {

        private int idProdus;
        private String numeProdus;
        private String descriereProdus;
        private Decimal pretCumparare;
        private Decimal pretVanzare;
        private String unitateMasura;

        public ProdusModel(int idProdus, string numeProdus, string descriereProdus, Decimal pretCumparare, Decimal pretVanzare, string unitateMasura)
        {
            this.idProdus = idProdus;
            this.numeProdus = numeProdus;
            this.descriereProdus = descriereProdus;
            this.pretCumparare = pretCumparare;
            this.pretVanzare = pretVanzare;
            this.unitateMasura = unitateMasura;
        }

        public int IdProdus { get => idProdus; set => idProdus = value; }
        public string NumeProdus { get => numeProdus; set => numeProdus = value; }
        public string DescriereProdus { get => descriereProdus; set => descriereProdus = value; }
        public Decimal PretCumparare { get => pretCumparare; set => pretCumparare = value; }
        public Decimal PretVanzare { get => pretVanzare; set => pretVanzare = value; }
        public string UnitateMasura { get => unitateMasura; set => unitateMasura = value; }
    }
}
