using ManagerStoc.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerStoc.Controllers
{
    public class StatisticiProduse_Menu_ItemController
    {
        private readonly Service Service;
        private StatisticiProduse_Menu_Item View;

        public StatisticiProduse_Menu_ItemController(ref Service s, StatisticiProduse_Menu_Item v)
        {
            this.View = v;
            this.Service = s;

            View.StatisticiProduseLoad += OnStatisticiProduseLoad;


        }

        public StatisticiProduse_Menu_ItemController(ref Service s)
        {
            this.Service = s;
            this.View = null;
        }

        public void setView(StatisticiProduse_Menu_Item v)
        {
            this.View = v;
            View.StatisticiProduseLoad += OnStatisticiProduseLoad;


        }

        public StatisticiProduse_Menu_Item getView()
        {
            return this.View;
        }

        private void OnStatisticiProduseLoad(object sender, EventArgs e)
        {
            DataTable QueryResult = Service.ExecuteProduseGraphProcedure();


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
