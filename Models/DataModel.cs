using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerStoc.Models
{
    public class DataModel
    {
        private DateTime dataInceput;
        private DateTime dataSfarsit;

        public DateTime DataInceput { get => dataInceput; set => dataInceput = value; }
        public DateTime DataSfarsit { get => dataSfarsit; set => dataSfarsit = value; }

        public DataModel(DateTime dataInceput, DateTime dataSfarsit)
        {
            this.DataInceput = dataInceput;
            this.DataSfarsit = dataSfarsit;
        }


    }
}
