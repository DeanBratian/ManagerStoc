using ManagerStoc.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerStoc.Controllers
{
    public class Preturi_Menu_ItemController
    {

        private readonly Service Service;
        private Preturi_Menu_Item View;

        public Preturi_Menu_ItemController(ref Service s, Preturi_Menu_Item v)
        {
            this.View = v;
            this.Service = s;

            View.BindGridPreturi += OnBindGridPreturi;
            View.GridPreturiCellValueChanged += OnGridPreturiCellValueChanged;
            View.StergePretToolStripPressed += OnStergePretToolStripPressed;
        }

        public Preturi_Menu_ItemController(ref Service s)
        {
            this.Service = s;
            this.View = null;
        }

        public void setView(Preturi_Menu_Item v)
        {
            this.View = v;
            View.BindGridPreturi += OnBindGridPreturi;
            View.GridPreturiCellValueChanged += OnGridPreturiCellValueChanged;
            View.StergePretToolStripPressed += OnStergePretToolStripPressed;
        }

        public Preturi_Menu_Item getView()
        {
            return this.View;
        }

        private void OnBindGridPreturi(object sender, EventArgs e)
        {
            DataTable QueryResult = Service.ExecuteSelectAllPreturiProcedure();

            if (QueryResult.Rows.Count > 0)
            {

                View.BindDTtoGridPreturi(QueryResult);

            }
            else
            {

                View.QueryResultEmpty();
            }
        }

        private bool ValidateEditPret()
        {

            bool retVal;

            if (
               View.PTModel.IdPretTotal >= 0 && (View.PTModel.NumeServiciuTotal.Length >= 6 && View.PTModel.NumeServiciuTotal.Length <= 40) && (View.PTModel.Detalii.Length >= 6 && View.PTModel.Detalii.Length <= 40)
               && View.PTModel.Pret > 0
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

        public void OnGridPreturiCellValueChanged(object sender, EventArgs e)
        {

            if (ValidateEditPret())
            {

                if (Service.ExecuteUpdatePretProcedure(View.PTModel))
                {

                    View.UpdatePretProcedureSuccess();
                }
                else
                {
                    View.EditPretProcedureFailed();
                }

            }
            else
            {
                View.PretValidationFailed();
            }

        }


        public void OnStergePretToolStripPressed(object sender, EventArgs e)
        {
            if (Service.ExecuteDeletePretProcedure(View.IdAles_int))
            {

                View.DeletePretSuccessfull();

            }
            else
            {

                View.DeletePretFailed();

            }
        }

    }
}
