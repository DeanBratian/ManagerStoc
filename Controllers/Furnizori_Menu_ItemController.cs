using ManagerStoc.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerStoc.Controllers
{
    public class Furnizori_Menu_ItemController
    {

        private readonly Service Service;
        private Furnizori_Menu_Item View;

        public Furnizori_Menu_ItemController(ref Service s, Furnizori_Menu_Item v)
        {
            this.View = v;
            this.Service = s;

            View.BindGridFurnizori += OnBindGridFurnizori;
            View.GridFurnizoriCellValueChanged += OnGridFurnizoriCellValueChanged;
            View.StergeFurnizorToolStripPressed += OnStergeFurnizorToolStripPressed;
        }

        public Furnizori_Menu_ItemController(ref Service s)
        {
            this.Service = s;
            this.View = null;
        }

        public void setView(Furnizori_Menu_Item v)
        {
            this.View = v;
            View.BindGridFurnizori += OnBindGridFurnizori;
            View.GridFurnizoriCellValueChanged += OnGridFurnizoriCellValueChanged;
            View.StergeFurnizorToolStripPressed += OnStergeFurnizorToolStripPressed;
        }

        public Furnizori_Menu_Item getView()
        {
            return this.View;
        }

        private void OnBindGridFurnizori(object sender, EventArgs e)
        {
            DataTable QueryResult = Service.ExecuteSelectAllFurnizoriProcedure();

            if (QueryResult.Rows.Count > 0)
            {

                View.BindDTtoGridFurnizori(QueryResult);

            }
            else
            {

                View.QueryResultEmpty();
            }
        }


        private bool ValidateEditFurnizor()
        {

            bool retVal;

            if (
                View.FModel.IdFurnizor >= 0 && (View.FModel.NumeFurnizor.Length >= 6 && View.FModel.NumeFurnizor.Length <= 30) && 
                (View.FModel.Detalii.Length >= 6 && View.FModel.Detalii.Length <= 30))
            {

                retVal = true;

            }
            else
            {
                retVal = false;
            }



            return retVal;
        }

        public void OnGridFurnizoriCellValueChanged(object sender, EventArgs e)
        {

            if (ValidateEditFurnizor())
            {

                if (Service.ExecuteUpdateFurnizorProcedure(View.FModel))
                {

                    View.UpdateFurnizorProcedureSuccess();
                }
                else
                {
                    View.EditFurnizorProcedureFailed();
                }

            }
            else
            {
                View.FurnizorValidationFailed();
            }

        }

        public void OnStergeFurnizorToolStripPressed(object sender, EventArgs e)
        {
            if (Service.ExecuteDeleteFurnizorProcedure(View.IdAles_int))
            {

                View.DeleteFurnizorSuccessfull();

            }
            else
            {

                View.DeleteFurnizorFailed();

            }
        }
    }
}
