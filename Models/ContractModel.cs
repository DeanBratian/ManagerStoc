using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerStoc.Models
{
    public class ContractModel
    {

        private int idContract;
        private String detaliiContract;
        private int durata;

        public int IdContract { get => idContract; set => idContract = value; }
        public string DetaliiContract { get => detaliiContract; set => detaliiContract = value; }
        public int Durata { get => durata; set => durata = value; }

        public ContractModel(int idContract, string detaliiContract, int durata)
        {
            this.IdContract = idContract;
            this.DetaliiContract = detaliiContract;
            this.Durata = durata;
        }


    }
}
