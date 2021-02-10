using ManagerStoc.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerStoc.Controllers
{
    public class Servicii_Menu_ItemController
    {

        private readonly Service Service;
        private Servicii_Menu_Item View;

        public Servicii_Menu_ItemController(ref Service s, Servicii_Menu_Item v)
        {
            this.View = v;
            this.Service = s;

            View.BindGridServicii += OnBindGridServicii;
            View.GridServiciiCellValueChanged += OnGridServiciiCellValueChanged;
            View.StergeServiciuToolStripPressed += OnStergeServiciuToolStripPressed;
        }

        public Servicii_Menu_ItemController(ref Service s)
        {
            this.Service = s;
            this.View = null;
        }

        public void setView(Servicii_Menu_Item v)
        {
            this.View = v;
            View.BindGridServicii += OnBindGridServicii;
            View.GridServiciiCellValueChanged += OnGridServiciiCellValueChanged;
            View.StergeServiciuToolStripPressed += OnStergeServiciuToolStripPressed;
        }

        public Servicii_Menu_Item getView()
        {
            return this.View;
        }

        private bool ValidateEditServiciu()
        {

            bool retVal;

            if (
                View.SModel.IdServiciu >= 0 && (View.SModel.NumeServiciu.Length >= 6 && View.SModel.NumeServiciu.Length <= 40) && (View.SModel.DescriereServiciu.Length >= 6 && View.SModel.DescriereServiciu.Length <= 40)
                && View.SModel.Pret > 0 && View.SModel.Pret.ToString().Length <= 5 && View.SModel.Durata > 0 && View.SModel.Durata.ToString().Length <= 5
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

        private void OnBindGridServicii(object sender, EventArgs e)
        {
            DataTable QueryResult = Service.ExecuteSelectAllServiciiProcedure();

            if (QueryResult.Rows.Count > 0)
            {

                View.BindDTtoGridServicii(QueryResult);

            }
            else
            {

                View.QueryResultEmpty();
            }
        }


        public void OnGridServiciiCellValueChanged(object sender, EventArgs e)
        {

            if (ValidateEditServiciu())
            {

                if (Service.ExecuteUpdateServiciuProcedure(View.SModel))
                {

                    View.UpdateServiciuProcedureSuccess();
                }
                else
                {
                    View.EditServiciuProcedureFailed();
                }

            }
            else
            {
                View.ServiciuValidationFailed();
            }

        }

        public void OnStergeServiciuToolStripPressed(object sender, EventArgs e)
        {
            if (Service.ExecuteDeleteServiciuProcedure(View.IdAles_int))
            {

                View.DeleteServiciuSuccessfull();

            }
            else
            {

                View.DeleteServiciuFailed();

            }
        }

    }
}
