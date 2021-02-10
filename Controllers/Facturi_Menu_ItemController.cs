using ManagerStoc.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerStoc.Controllers
{
    public class Facturi_Menu_ItemController
    {

        private readonly Service Service;
        private Facturi_Menu_Item View;

        public Facturi_Menu_ItemController(ref Service s, Facturi_Menu_Item v)
        {
            this.View = v;
            this.Service = s;

            View.BindGridFacturi += OnBindGridFacturi;
            View.GridFacturiCellValueChanged += OnGridFacturiCellValueChanged;
            View.StergeFacturaToolStripPressed += OnStergeFacturaToolStripPressed;
        }

        public Facturi_Menu_ItemController(ref Service s)
        {
            this.Service = s;
            this.View = null;
        }

        public void setView(Facturi_Menu_Item v)
        {
            this.View = v;

            View.BindGridFacturi += OnBindGridFacturi;
            View.GridFacturiCellValueChanged += OnGridFacturiCellValueChanged;
            View.StergeFacturaToolStripPressed += OnStergeFacturaToolStripPressed;

        }

        public Facturi_Menu_Item getView()
        {
            return this.View;
        }


        private bool ValidateEditFactura()
        {

            bool retVal;
            
            if (View.FModel.IdFactura >= 0)
            {

                retVal = true;

            }
            else
            {
                retVal = false;
            }



            return retVal;
        }

        private void OnBindGridFacturi(object sender, EventArgs e)
        {
            DataTable QueryResult = Service.ExecuteSelectAllFacturiProcedure();

            if (QueryResult.Rows.Count > 0)
            {

                View.BindDTtoGridFacturi(QueryResult);

            }
            else
            {

                View.QueryResultEmpty();
            }
        }


        public void OnGridFacturiCellValueChanged(object sender, EventArgs e)
        {

            if (ValidateEditFactura())
            {

                if (Service.ExecuteUpdateFacturaProcedure(View.FModel))
                {

                    View.UpdateFacturaProcedureSuccess();
                }
                else
                {
                    View.EditFacturaProcedureFailed();
                }

            }
            else
            {
                View.FacturaValidationFailed();
            }

        }

        public void OnStergeFacturaToolStripPressed(object sender, EventArgs e)
        {
            if (Service.ExecuteDeleteFacturaProcedure(View.IdAles_int))
            {

                View.DeleteFacturaSuccessfull();

            }
            else
            {

                View.DeleteFacturaFailed();

            }
        }



    }
}
