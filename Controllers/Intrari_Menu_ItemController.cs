using ManagerStoc.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerStoc.Controllers
{
    public class Intrari_Menu_ItemController
    {

        private readonly Service Service;
        private Intrari_Menu_Item View;
        private List<ProdusDeUpdatatInStocModel> ProdusedeUpdatatInStoc_List = new List<ProdusDeUpdatatInStocModel>();

        public Intrari_Menu_ItemController(ref Service s, Intrari_Menu_Item v)
        {
            this.View = v;
            this.Service = s;

            View.BindGridIntrari += OnBindGridIntrari;
            View.StergeIntrareToolStripPressed += OnStergeIntrareToolStripPressed;
            View.ReaduIntrareToolStripPressed += OnReaduIntrareToolStripPressed;

        }

        public Intrari_Menu_ItemController(ref Service s)
        {
            this.Service = s;
            this.View = null;
        }

        public void setView(Intrari_Menu_Item v)
        {
            this.View = v;

            View.BindGridIntrari += OnBindGridIntrari;
            View.StergeIntrareToolStripPressed += OnStergeIntrareToolStripPressed;
            View.ReaduIntrareToolStripPressed += OnReaduIntrareToolStripPressed;
        }

        public Intrari_Menu_Item getView()
        {
            return this.View;
        }


        private void OnBindGridIntrari(object sender, EventArgs e)
        {
            DataTable QueryResult = Service.ExecuteSelectIntrariProducedure();

            if (QueryResult.Rows.Count > 0)
            {

                View.BindDTtoGridProduse(QueryResult);

            }
            else
            {

                View.QueryResultEmpty();
            }
        }

        public void OnStergeIntrareToolStripPressed(object sender, EventArgs e)
        {
            if (Service.ExecuteDeleteIntrareProcedure(View.IdAles_int))
            {

                View.DeleteIntrareSuccessfull();
                
                ScadeCantitatiStoc(GetProdusDeModificatInStoc());
            }
            else
            {

                View.DeleteIntrareFailed();

            }
        }

        public void OnReaduIntrareToolStripPressed(object sender, EventArgs e)
        {
            if (Service.ExecuteUndeleteIntrareProcedure(View.IdAles_int))
            {

                View.ReaducereIntrareSuccessfull();

                CresteCantitatiStoc(GetProdusDeModificatInStoc());
            }
            else
            {

                View.ReaducereIntrareFailed();

            }
        }


        private DataTable GetProdusDeModificatInStoc()
        {

           DataTable DataTable_retVal = new DataTable();

           DataTable_retVal = Service.ExecuteGetProdusCantitateDeModificat(View.IdAles_int);

           return DataTable_retVal;

        }


        private void ScadeCantitatiStoc(DataTable inputDT)
        {

            foreach (DataRow row in inputDT.Rows)
            {
                ProdusDeUpdatatInStocModel local_ProdusDeUpdatatInStoc = new ProdusDeUpdatatInStocModel(Convert.ToInt32(row["IdProdus"].ToString()), Convert.ToInt32(row["Cantitate"].ToString()));

                ProdusedeUpdatatInStoc_List.Add(local_ProdusDeUpdatatInStoc);

            }

            foreach (ProdusDeUpdatatInStocModel PDU in ProdusedeUpdatatInStoc_List)
            {

                if(Service.ExecuteScadeStocProcedure(PDU))
                {

                    View.ScadeStocSuccessfull(PDU);
                }
                else
                {
                    View.ScadeStocFailed();

                }

            }


            ProdusedeUpdatatInStoc_List.Clear();

        }

        private void CresteCantitatiStoc(DataTable inputDT)
        {

            foreach (DataRow row in inputDT.Rows)
            {
                ProdusDeUpdatatInStocModel local_ProdusDeUpdatatInStoc = new ProdusDeUpdatatInStocModel(Convert.ToInt32(row["IdProdus"].ToString()), Convert.ToInt32(row["Cantitate"].ToString()));

                ProdusedeUpdatatInStoc_List.Add(local_ProdusDeUpdatatInStoc);

            }

            foreach (ProdusDeUpdatatInStocModel PDU in ProdusedeUpdatatInStoc_List)
            {

                if (Service.ExecuteCresteStocProcedure(PDU))
                {

                    View.CresteStocSuccessfull(PDU);
                }
                else
                {
                    View.CresteStocFailed();

                }

            }

            ProdusedeUpdatatInStoc_List.Clear();

        }



    }
}
