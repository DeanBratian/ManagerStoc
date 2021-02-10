using ManagerStoc.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ManagerStoc.Controllers
{
    public class Produse_Menu_ItemController
    {
        private readonly Service Service;
        private Produse_Menu_Item View;

        public Produse_Menu_ItemController(ref Service s, Produse_Menu_Item v)
        {
            this.View = v;
            this.Service = s;

           View.BindGridProduse += OnBindGridProduse;
           View.GridProduseCellValueChanged += OnGridProduseCellValueChanged;
           View.StergeProdusToolStripPressed += OnStergeProdusToolStripPressed;
           View.AdaugaImagineToolStripPressed += OnAdaugaImagineToolStripPressed;
        }

        public Produse_Menu_ItemController(ref Service s)
        {
            this.Service = s;
            this.View = null;
        }

        public void setView(Produse_Menu_Item v)
        {
            this.View = v;
            View.BindGridProduse += OnBindGridProduse;
            View.GridProduseCellValueChanged += OnGridProduseCellValueChanged;
            View.StergeProdusToolStripPressed += OnStergeProdusToolStripPressed;
            View.AdaugaImagineToolStripPressed += OnAdaugaImagineToolStripPressed;
        }

        public Produse_Menu_Item getView()
        {
            return this.View;
        }

        private bool ValidateEditProdus()
        {

            bool retVal;

            if (
                View.PModel.IdProdus >= 0 && (View.PModel.NumeProdus.Length >= 6 && View.PModel.NumeProdus.Length <= 30) && (View.PModel.DescriereProdus.Length >= 6 && View.PModel.DescriereProdus.Length <= 30)
                && View.PModel.PretCumparare > 0 && View.PModel.PretCumparare.ToString().Length <= 5 && View.PModel.PretVanzare > 0 && View.PModel.PretVanzare.ToString().Length <= 5
                && (View.PModel.UnitateMasura.Length >= 1 && View.PModel.UnitateMasura.Length <= 10)
                )

            {

                retVal = true;

            }
            else
            {
                retVal = false;
            }



            return retVal;
        }



        private void OnBindGridProduse(object sender, EventArgs e)
        {
            DataTable QueryResult = Service.ExecuteSelectAllProduseProcedure();

            if(QueryResult.Rows.Count>0)
            {

                View.BindDTtoGridProduse(QueryResult);

            }
            else
            {

                View.QueryResultEmpty();
            }
        }

        public void OnGridProduseCellValueChanged(object sender, EventArgs e)
        {

            if (ValidateEditProdus())
            {

                if (Service.ExecuteUpdateProdusProcedure(View.PModel))
                {

                    View.UpdateProdusProcedureSuccess();
                }
                else
                {
                    View.EditProdusProcedureFailed();
                }

            }
            else
            {
                View.ProdusValidationFailed();
            }

        }


        public void OnStergeProdusToolStripPressed(object sender, EventArgs e)
        {
            if(Service.ExecuteDeleteProdusProcedure(View.IdAles_int))
            {

                View.DeleteProdusSuccessfull();

            }
            else
            {

                View.DeleteProdusFailed();

            }
        }

        public void OnAdaugaImagineToolStripPressed(object sender, EventArgs e)
        {

            DataTable QueryResult = Service.ExecuteCheckIfImageExistsForProductProcedure(View.IdAles_int);

            if (QueryResult.Rows.Count == 0)
            {
                if(Service.ExecuteInsertImagineProdusProcedure(View.IPModel, View.IdAles_int))
                {

                    View.AddImageSuccessfull();
                }
                else
                {

                    View.AddImageFailed();
                }

            }
            else
            {
                View.ImageExistsForProduct();
            }
        }
    }
}
