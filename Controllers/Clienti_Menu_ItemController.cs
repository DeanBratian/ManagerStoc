using ManagerStoc.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerStoc.Controllers
{
    public class Clienti_Menu_ItemController
    {

        private readonly Service Service;
        private Clienti_Menu_Item View;

        public Clienti_Menu_ItemController(ref Service s, Clienti_Menu_Item v)
        {
            this.View = v;
            this.Service = s;

            View.BindGridClienti += OnBindGridClienti;
            View.GridClientiCellValueChanged += OnGridClientiCellValueChanged;
            View.StergeClientToolStripPressed += OnStergeClientToolStripPressed;

        }

        public Clienti_Menu_ItemController(ref Service s)
        {
            this.Service = s;
            this.View = null;
        }

        public void setView(Clienti_Menu_Item v)
        {
            this.View = v;

            View.BindGridClienti += OnBindGridClienti;
            View.GridClientiCellValueChanged += OnGridClientiCellValueChanged;
            View.StergeClientToolStripPressed += OnStergeClientToolStripPressed;

        }

        public Clienti_Menu_Item getView()
        {
            return this.View;
        }

        private void OnBindGridClienti(object sender, EventArgs e)
        {
            DataTable QueryResult = Service.ExecuteSelectAllClientiProcedure();

            if (QueryResult.Rows.Count > 0)
            {

                View.BindDTtoGridClienti(QueryResult);

            }
            else
            {

                View.QueryResultEmpty();
            }
        }

        public void OnGridClientiCellValueChanged(object sender, EventArgs e)
        {

            if (ValidateEditClient())
            {

                if (Service.ExecuteUpdateClientProcedure(View.CModel))
                {

                    View.UpdateClientProcedureSuccess();
                }
                else
                {
                    View.EditClientProcedureFailed();
                }

            }
            else
            {
                View.ClientValidationFailed();
            }

        }


        private bool ValidateEditClient()
        {

            bool retVal;

            if (View.CModel.IdClient >= 0 &&
               (View.CModel.NumeClient.Length >= 6 && View.CModel.NumeClient.Length <= 30) && (View.CModel.DescriereClient.Length >= 6 && View.CModel.DescriereClient.Length <= 30)
               && (View.CModel.CodFiscal.Length >= 6 && View.CModel.CodFiscal.Length <= 10)
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


        public void OnStergeClientToolStripPressed(object sender, EventArgs e)
        {
            if (Service.ExecuteDeleteClientProcedure(View.IdAles_int))
            {

                View.DeleteClientSuccessfull();

            }
            else
            {

                View.DeleteClientFailed();

            }
        }

    }
}
