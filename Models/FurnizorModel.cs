using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerStoc.Models
{
    public class FurnizorModel
    {
        private int idFurnizor;
        private String numeFurnizor;
        private String detalii;

        public int IdFurnizor { get => idFurnizor; set => idFurnizor = value; }
        public string NumeFurnizor { get => numeFurnizor; set => numeFurnizor = value; }
        public string Detalii { get => detalii; set => detalii = value; }

        public FurnizorModel(int idFurnizor, string numeFurnizor, string detalii)
        {
            this.IdFurnizor = idFurnizor;
            this.NumeFurnizor = numeFurnizor;
            this.Detalii = detalii;
        }


    }
}
