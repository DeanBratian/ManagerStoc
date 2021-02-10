using ManagerStoc.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerStoc.Controllers
{
    public class StatisticiClienti_Menu_ItemController
    {
        private readonly Service Service;
        private StatisticiClienti_Menu_Item View;

        DataTable StatisticiPentruClient = new DataTable();



        public StatisticiClienti_Menu_ItemController(ref Service s, StatisticiClienti_Menu_Item v)
        {
            this.View = v;
            this.Service = s;

            View.StatisticiClientiLoaded += OnStatisticiClientiLoaded;
            View.PictureBoxLoadDataPressed += OnPictureBoxLoadDataPressed;

        }

        public StatisticiClienti_Menu_ItemController(ref Service s)
        {
            this.Service = s;
            this.View = null;
        }

        public void setView(StatisticiClienti_Menu_Item v)
        {
            this.View = v;


            View.StatisticiClientiLoaded += OnStatisticiClientiLoaded;
            View.PictureBoxLoadDataPressed += OnPictureBoxLoadDataPressed;
        }

        public StatisticiClienti_Menu_Item getView()
        {
            return this.View;
        }

        private void GetClientDataTable()
        {
            View.ClientDataTableDT = Service.ExecuteGetNumeClienti();
        }

        private void OnStatisticiClientiLoaded(object sender, EventArgs e)
        {

            GetClientDataTable();

            if (View.ClientDataTableDT.Rows.Count > 0)
            {
                View.ClientTableReadSuccessfull();
            }
            else
            {
                View.ClientTableReadFailed();
            }

        }

        private void OnPictureBoxLoadDataPressed(object sender, EventArgs e)
        {

            if (View.flagNoClient_bool == false)
            {

                BindGridStatistici();

                CalculateTotalNumarVanzari();


            }

        }

        private void BindGridStatistici()
        {

            this.StatisticiPentruClient = Service.ExecuteGetVanzariForClientInPerioada(View.DModel,View.ClientAles_int);

            if(StatisticiPentruClient.Rows.Count > 0)
            {

                View.BindDTtoGridVanzari(StatisticiPentruClient);
            }
            else
            {

                View.NoSalesForClient();

            }


        }


        private void CalculateTotalNumarVanzari()
        {

            foreach (DataRow dtRow in StatisticiPentruClient.Rows)
            {
                Decimal pretTotal = Decimal.Parse(dtRow.ItemArray[3].ToString());

                View.SumaTotala += pretTotal;

                View.NumarVanzari++;

            }

            View.SetTextTotaluri();

        }





    }
}
