using ManagerStoc.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerStoc.Controllers
{
    public class StatisticiServicii_Menu_ItemController
    {
        private readonly Service Service;
        private StatisticiServicii_Menu_Item View;

        public StatisticiServicii_Menu_ItemController(ref Service s, StatisticiServicii_Menu_Item v)
        {
            this.View = v;
            this.Service = s;

            View.StatisticiServiciiLoad += OnStatisticiServiciiLoad;

        }

        public StatisticiServicii_Menu_ItemController(ref Service s)
        {
            this.Service = s;
            this.View = null;
        }

        public void setView(StatisticiServicii_Menu_Item v)
        {
            this.View = v;
            View.StatisticiServiciiLoad += OnStatisticiServiciiLoad;
        }

        public StatisticiServicii_Menu_Item getView()
        {
            return this.View;
        }

        private void OnStatisticiServiciiLoad(object sender, EventArgs e)
        {
            DataTable QueryResult = Service.ExecuteServiciiGraphProcedure();


            if (QueryResult.Rows.Count > 0)
            {

                View.BindDtToChart(QueryResult);
                View.fillChart();
            }
            else
            {
                //View.NoStatisticsAvailable();

            }

        }
    }
}
