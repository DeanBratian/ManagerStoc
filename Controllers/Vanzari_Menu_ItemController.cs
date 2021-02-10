using ManagerStoc.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerStoc.Controllers
{
    public class Vanzari_Menu_ItemController
    {

        private readonly Service Service;
        private Vanzari_Menu_Item View;
        private List<ProdusDeUpdatatInStocModel> ProdusedeUpdatatInStoc_List = new List<ProdusDeUpdatatInStocModel>();

        public Vanzari_Menu_ItemController(ref Service s, Vanzari_Menu_Item v)
        {
            this.View = v;
            this.Service = s;

            View.BindGridVanzari += OnBindGridVanzari;
            View.StergeVanzareToolStripPressed += OnStergeVanzareToolStripPressed;
            View.ReaduVanzareToolStripPressed += OnReaduVanzareToolStripPressed;

        }

        public Vanzari_Menu_ItemController(ref Service s)
        {
            this.Service = s;
            this.View = null;
        }

        public void setView(Vanzari_Menu_Item v)
        {
            this.View = v;

            View.BindGridVanzari += OnBindGridVanzari;
            View.StergeVanzareToolStripPressed += OnStergeVanzareToolStripPressed;
            View.ReaduVanzareToolStripPressed += OnReaduVanzareToolStripPressed;
        }

        public Vanzari_Menu_Item getView()
        {
            return this.View;
        }

        private void OnBindGridVanzari(object sender, EventArgs e)
        {
            DataTable QueryResult = Service.ExecuteSelectAllVanzariProcedure();

            if (QueryResult.Rows.Count > 0)
            {

                View.BindDTtoGridVanzari(QueryResult);

            }
            else
            {

                View.QueryResultEmpty();
            }
        }

        public void OnStergeVanzareToolStripPressed(object sender, EventArgs e)
        {
            if (Service.ExecuteDeleteVanzareProcedure(View.IdAles_int))
            {

                View.DeleteVanzareSuccessfull();


                if (CheckIfRelatedVanzariProduseExistAndGetProduseDeUpdatat())
                {
                    DeleteRelatedVanzareProdus();

                    CresteCantitatiStoc();

                }
                else
                {


                }

                if (CheckIfRelatedVanzariServiciiExist())
                {

                    DeleteRelatedVanzareServiciu();

                }
                else
                {


                }

            }
            else
            {

                View.DeleteVanzareFailed();

            }
        }


        private bool CheckIfRelatedVanzariProduseExistAndGetProduseDeUpdatat()
        {
            bool retVal;


            DataTable QueryResult = Service.ExecuteCheckIfRelatedVanzariProduseExistProcedure(View.IdAles_int);

            if (QueryResult.Rows.Count > 0)
            {


                
                View.ExistaVanzariProduseRelationate(QueryResult.Rows.Count);
                ProdusedeUpdatatInStoc_List = GetProduseDeUpdatatInStoc(QueryResult);
                
                retVal = true;

            }
            else
            {
                View.NuExistaVanzariProduseRelationate();

                retVal = false;
            }

            return retVal;
        }

        private List<ProdusDeUpdatatInStocModel> GetProduseDeUpdatatInStoc(DataTable DT)
        {
            List<ProdusDeUpdatatInStocModel> localProdusedeUpdatatInStoc = new List<ProdusDeUpdatatInStocModel>();

            foreach (DataRow row in DT.Rows)
            {
                ProdusDeUpdatatInStocModel local_ProdusDeUpdatatInStoc = new ProdusDeUpdatatInStocModel(Convert.ToInt32(row["IdProdus"].ToString()), Convert.ToInt32(row["Cantitate"].ToString()));

                localProdusedeUpdatatInStoc.Add(local_ProdusDeUpdatatInStoc);

            }

            return localProdusedeUpdatatInStoc;
        }


        private void DeleteRelatedVanzareProdus()
        {

            if(Service.ExecuteDeleteVanzareProdusFromDeleteVanzareProcedure(View.IdAles_int))
            {

                View.VanzariProduseRelationateDeletedSuccessfull();
            }
            else
            {

                View.VanzariProduseRelationateDeletedFailed();
            }

        }

        private void CresteCantitatiStoc()
        {
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

        private bool CheckIfRelatedVanzariServiciiExist()
        {
            bool retVal;


            DataTable QueryResult = Service.ExecuteCheckIfRelatedVanzariServiciiExistProcedure(View.IdAles_int);

            if (QueryResult.Rows.Count > 0)
            {

                View.ExistaVanzariServiciiRelationate(QueryResult.Rows.Count);
                retVal = true;

            }
            else
            {
                View.NuExistaVanzariServiciiRelationate();
                retVal = false;
            }

            return retVal;
        }

        private void DeleteRelatedVanzareServiciu()
        {
            if (Service.ExecuteDeleteVanzareServiciuFromDeleteVanzareProcedure(View.IdAles_int))
            {

                View.VanzariServiciiRelationateDeletedSuccessfull();
            }
            else
            {

                View.VanzariServiciiRelationateDeletedFailed();
            }
            
        }

        public void OnReaduVanzareToolStripPressed(object sender, EventArgs e)
        {



            if (Service.ExecuteUndeleteVanzareProcedure(View.IdAles_int))
            {

                View.UnDeleteVanzareSuccessfull();


                if (CheckIfRelatedVanzariProduseExistAndGetProduseDeUpdatat())
                {

                    UndeleteRelatedVanzareProdus();
                    ScadeCantitatiStoc();

                }
                else
                {



                }

                if (CheckIfRelatedVanzariServiciiExist())
                {

                    UndeleteRelatedVanzareServiciu();

                }
                else
                {


                }


            }
            else
            {

                View.UnDeleteVanzareFailed();

            }

        }


        private void UndeleteRelatedVanzareProdus()
        {
            

            if (Service.ExecuteUnDeleteVanzareProdusFromUnDeleteVanzareProcedure(View.IdAles_int))
            {

                View.VanzariProduseRelationateUnDeletedSuccessfull();
            }
            else
            {

                View.VanzariProduseRelationateUnDeletedFailed();
            }
        }


        private void ScadeCantitatiStoc()
        {
            foreach (ProdusDeUpdatatInStocModel PDU in ProdusedeUpdatatInStoc_List)
            {

                if (Service.ExecuteScadeStocProcedure(PDU))
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


        private void UndeleteRelatedVanzareServiciu()
        {
            if (Service.ExecuteUnDeleteVanzareServiciuFromUnDeleteVanzareProcedure(View.IdAles_int))
            {

                View.VanzariProduseRelationateUnDeletedSuccessfull();
            }
            else
            {

                View.VanzariProduseRelationateUnDeletedFailed();
            }
        }

    }
}
