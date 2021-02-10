using ManagerStoc.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerStoc.Controllers
{
    public class Contracte_Menu_ItemController
    {
        private readonly Service Service;
        private Contracte_Menu_Item View;

        public Contracte_Menu_ItemController(ref Service s, Contracte_Menu_Item v)
        {
            this.View = v;
            this.Service = s;

            View.BindGridContracte += OnBindGridContracte;
            View.GridContracteCellValueChanged += OnGridContracteCellValueChanged;
            View.StergeContractToolStripPressed += OnStergeContractToolStripPressed;
        }

        public Contracte_Menu_ItemController(ref Service s)
        {
            this.Service = s;
            this.View = null;
        }

        public void setView(Contracte_Menu_Item v)
        {
            this.View = v;

            View.BindGridContracte += OnBindGridContracte;
            View.GridContracteCellValueChanged += OnGridContracteCellValueChanged;
            View.StergeContractToolStripPressed += OnStergeContractToolStripPressed;
        }

        public Contracte_Menu_Item getView()
        {
            return this.View;
        }

        private bool ValidateEditContract()
        {

            bool retVal;

            if (
              View.CModel.IdContract >= 0 && (View.CModel.DetaliiContract.Length >= 6 && View.CModel.DetaliiContract.Length <= 30) && View.CModel.Durata > 0)
            {

                retVal = true;

            }
            else
            {
                retVal = false;
            }

            return retVal;
        }


        private void OnBindGridContracte(object sender, EventArgs e)
        {
            DataTable QueryResult = Service.ExecuteSelectAllContracteProcedure();

            if (QueryResult.Rows.Count > 0)
            {

                View.BindDTtoGridContracte(QueryResult);

            }
            else
            {

                View.QueryResultEmpty();
            }
        }


        public void OnGridContracteCellValueChanged(object sender, EventArgs e)
        {

            if (ValidateEditContract())
            {

                if (Service.ExecuteUpdateContractProcedure(View.CModel))
                {

                    View.UpdateContractProcedureSuccess();
                }
                else
                {
                    View.EditContractProcedureFailed();
                }

            }
            else
            {
                View.ContractValidationFailed();
            }

        }

        public void OnStergeContractToolStripPressed(object sender, EventArgs e)
        {
            if (Service.ExecuteDeleteContractProcedure(View.IdAles_int))
            {

                View.DeleteContractSuccessfull();

            }
            else
            {

                View.DeleteContractFailed();

            }
        }


    }
}
