using ManagerStoc.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerStoc.Controllers
{
    public class VanzariServicii_Menu_ItemController
    {
        private readonly Service Service;
        private VanzariServicii_Menu_Item View;

        public VanzariServicii_Menu_ItemController(ref Service s, VanzariServicii_Menu_Item v)
        {
            this.View = v;
            this.Service = s;

            View.BindGridVanzariServicii += OnBindGridVanzariServicii;
        }

        public VanzariServicii_Menu_ItemController(ref Service s)
        {
            this.Service = s;
            this.View = null;
        }

        public void setView(VanzariServicii_Menu_Item v)
        {
            this.View = v;

            View.BindGridVanzariServicii += OnBindGridVanzariServicii;

        }

        public VanzariServicii_Menu_Item getView()
        {
            return this.View;
        }


        private void OnBindGridVanzariServicii(object sender, EventArgs e)
        {
            DataTable QueryResult = Service.ExecuteSelectAllVanzariServiciiProcedure();

            if (QueryResult.Rows.Count > 0)
            {

                View.BindDTtoGridVanzariServicii(QueryResult);

            }
            else
            {

                View.QueryResultEmpty();
            }
        }

    }
}
