using ManagerStoc.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerStoc.Controllers
{
    public class NumereMasina_Menu_ItemController
    {
        private readonly Service Service;
        private NumereMasina_Menu_Item View;

        public NumereMasina_Menu_ItemController(ref Service s, NumereMasina_Menu_Item v)
        {
            this.View = v;
            this.Service = s;

            View.BindGridNumere += OnBindGridNumere;
            View.GridNumereCellValueChanged += OnGridNumereCellValueChanged;
            View.StergeNumarToolStripPressed += OnStergeNumarToolStripPressed;
        }

        public NumereMasina_Menu_ItemController(ref Service s)
        {
            this.Service = s;
            this.View = null;
        }

        public void setView(NumereMasina_Menu_Item v)
        {
            this.View = v;

            View.BindGridNumere += OnBindGridNumere;
            View.GridNumereCellValueChanged += OnGridNumereCellValueChanged;
            View.StergeNumarToolStripPressed += OnStergeNumarToolStripPressed;
        }

        public NumereMasina_Menu_Item getView()
        {
            return this.View;
        }

        private void OnBindGridNumere(object sender, EventArgs e)
        {
            DataTable QueryResult = Service.ExecuteSelectNumereMasinaProcedure();

            if (QueryResult.Rows.Count > 0)
            {

                View.BindDTtoGridNumere(QueryResult);

            }
            else
            {

                View.QueryResultEmpty();
            }
        }


        private bool ValidateEditNumar()
        {
            
            bool retVal;

            if (
                View.NMModel.IdNumar >= 0 && (View.NMModel.NumarMasina.Length >= 3 && View.NMModel.NumarMasina.Length <= 10))
            {

                retVal = true;

            }
            else
            {
                retVal = false;
            }

            return retVal;
        }

        public void OnGridNumereCellValueChanged(object sender, EventArgs e)
        {

            if (ValidateEditNumar())
            {

                if (Service.ExecuteUpdateNumarProcedure(View.NMModel))
                {

                    View.UpdateNumarProcedureSuccess();
                }
                else
                {
                    View.EditNumarProcedureFailed();
                }

            }
            else
            {
                View.NumarValidationFailed();
            }

        }

        public void OnStergeNumarToolStripPressed(object sender, EventArgs e)
        {
            if (Service.ExecuteDeleteNumarProcedure(View.IdAles_int))
            {

                View.DeleteNumarSuccessfull();

            }
            else
            {

                View.DeleteNumarFailed();

            }
        }


    }
}
