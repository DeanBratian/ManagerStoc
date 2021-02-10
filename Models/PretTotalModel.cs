using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerStoc.Models
{
    public class PretTotalModel
    {
        private int idPretTotal;
        private String numeServiciuTotal;
        private String detalii;
        private Decimal pret;

        public int IdPretTotal { get => idPretTotal; set => idPretTotal = value; }
        public string NumeServiciuTotal { get => numeServiciuTotal; set => numeServiciuTotal = value; }
        public string Detalii { get => detalii; set => detalii = value; }
        public decimal Pret { get => pret; set => pret = value; }

        public PretTotalModel(int idPretTotal, string numeServiciuTotal, string detalii, decimal pret)
        {
            this.idPretTotal = idPretTotal;
            this.numeServiciuTotal = numeServiciuTotal;
            this.detalii = detalii;
            this.pret = pret;
        }

    }
}
