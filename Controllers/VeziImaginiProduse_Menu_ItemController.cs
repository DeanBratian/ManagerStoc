using ManagerStoc.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerStoc.Controllers
{
    public class VeziImaginiProduse_Menu_ItemController
    {

        private readonly Service Service;
        private VeziImaginiProduse_Menu_Item View;

        public VeziImaginiProduse_Menu_ItemController(ref Service s, VeziImaginiProduse_Menu_Item v)
        {
            this.View = v;
            this.Service = s;

            View.BindGridImaginiProduse += OnBindGridImaginiProduse;
            View.StergeImagineToolStripPressed += OnStergeImagineToolStripPressed;

        }

        public VeziImaginiProduse_Menu_ItemController(ref Service s)
        {
            this.Service = s;
            this.View = null;
        }

        public void setView(VeziImaginiProduse_Menu_Item v)
        {
            this.View = v;
            View.BindGridImaginiProduse += OnBindGridImaginiProduse;
            View.StergeImagineToolStripPressed += OnStergeImagineToolStripPressed;
        }

        public VeziImaginiProduse_Menu_Item getView()
        {
            return this.View;
        }


        private void OnBindGridImaginiProduse(object sender, EventArgs e)
        {
            DataTable QueryResult = Service.ExecuteSelectImaginiProduseProcedure();

            if (QueryResult.Rows.Count > 0)
            {

                View.BindDTtoGridImagini(QueryResult);

            }
            else
            {

                //View.QueryResultEmpty();
            }
        }

        public void OnStergeImagineToolStripPressed(object sender, EventArgs e)
        {
            if (Service.ExecuteDeleteImagineProducedure(View.IdAles_int))
            {

                View.DeleteImagineSuccessfull();

            }
            else
            {

                View.DeleteImagineFailed();

            }
        }


    }
}
