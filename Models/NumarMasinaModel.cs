using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerStoc.Models
{
    public class NumarMasinaModel
    {

        private int idNumar;
        private String numarMasina;

        public int IdNumar { get => idNumar; set => idNumar = value; }
        public string NumarMasina { get => numarMasina; set => numarMasina = value; }

        public NumarMasinaModel(int idNumar, string numarMasina)
        {
            this.IdNumar = idNumar;
            this.NumarMasina = numarMasina;
        }


    }
}
