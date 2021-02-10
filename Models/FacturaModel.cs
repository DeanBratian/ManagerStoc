using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerStoc.Models
{
    public class FacturaModel
    {
        private int idFactura;
        private Decimal sumaTotala;
        private String directieFactura;
        private bool facturaAchitata;

        public decimal SumaTotala { get => sumaTotala; set => sumaTotala = value; }
        public string DirectieFactura { get => directieFactura; set => directieFactura = value; }
        public bool FacturaAchitata { get => facturaAchitata; set => facturaAchitata = value; }
        public int IdFactura { get => idFactura; set => idFactura = value; }

        public FacturaModel(decimal sumaTotala, string directieFactura, bool facturaAchitata, int idFactura)
        {
            this.SumaTotala = sumaTotala;
            this.DirectieFactura = directieFactura;
            this.FacturaAchitata = facturaAchitata;
            this.IdFactura = idFactura;
        }


    }
}
