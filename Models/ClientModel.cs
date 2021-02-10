using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerStoc
{
    public class ClientModel
    {
        private int idClient;
        private string numeClient;
        private string descriereClient;
        private string codFiscal;

        public string NumeClient { get => numeClient; set => numeClient = value; }
        public string DescriereClient { get => descriereClient; set => descriereClient = value; }
        public string CodFiscal { get => codFiscal; set => codFiscal = value; }
        public int IdClient { get => idClient; set => idClient = value; }

        public ClientModel(string numeClient, string descriereClient, string codFiscal, int idClient)
        {
            this.numeClient = numeClient;
            this.descriereClient = descriereClient;
            this.codFiscal = codFiscal;
            this.IdClient = idClient;
        }


    }
}
