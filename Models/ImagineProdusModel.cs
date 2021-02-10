using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerStoc.Models
{
    public class ImagineProdusModel
    {
        private String fileName;
        private byte[] content;
        private int idProdus;
        public string FileName { get => fileName; set => fileName = value; }
        public byte[] Content { get => content; set => content = value; }
        public int IdProdus { get => idProdus; set => idProdus = value; }

        public ImagineProdusModel(string fileName, byte[] content, int idProdus)
        {
            this.FileName = fileName;
            Content = content;
            IdProdus = idProdus;
        }

        public ImagineProdusModel()
        {
        }

    }
}
