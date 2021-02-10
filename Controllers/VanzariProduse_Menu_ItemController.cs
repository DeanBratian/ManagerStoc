using ManagerStoc.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerStoc.Controllers
{
    public class VanzariProduse_Menu_ItemController
    {

        private readonly Service Service;
        private VanzariProduse_Menu_Item View;

        public VanzariProduse_Menu_ItemController(ref Service s, VanzariProduse_Menu_Item v)
        {
            this.View = v;
            this.Service = s;

            View.BindGridVanzariProduse += OnBindGridVanzariProduse;


        }

        public VanzariProduse_Menu_ItemController(ref Service s)
        {
            this.Service = s;
            this.View = null;
        }

        public void setView(VanzariProduse_Menu_Item v)
        {
            this.View = v;

            View.BindGridVanzariProduse += OnBindGridVanzariProduse;

        }

        public VanzariProduse_Menu_Item getView()
        {
            return this.View;
        }

        private void OnBindGridVanzariProduse(object sender, EventArgs e)
        {
            DataTable QueryResult = Service.ExecuteSelectAllVanzariProduseProcedure();

            if (QueryResult.Rows.Count > 0)
            {

                View.BindDTtoGridVanzariProduse(QueryResult);

            }
            else
            {

                View.QueryResultEmpty();
            }
        }
    }
}
