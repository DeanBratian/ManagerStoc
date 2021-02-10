using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerStoc.Models
{
    public class ServiciuModel
    {
        int idServiciu;
        String numeServiciu;
        String descriereServiciu;
        Decimal pret;
        int durata;

        public ServiciuModel(int idServiciu, string numeServiciu, string descriereServiciu, decimal pret, int durata)
        {
            this.idServiciu = idServiciu;
            this.numeServiciu = numeServiciu;
            this.descriereServiciu = descriereServiciu;
            this.pret = pret;
            this.durata = durata;
        }

        public int IdServiciu { get => idServiciu; set => idServiciu = value; }
        public string NumeServiciu { get => numeServiciu; set => numeServiciu = value; }
        public string DescriereServiciu { get => descriereServiciu; set => descriereServiciu = value; }
        public decimal Pret { get => pret; set => pret = value; }
        public int Durata { get => durata; set => durata = value; }
    }
}
