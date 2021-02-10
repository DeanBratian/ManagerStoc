using ManagerStoc.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerStoc.Controllers
{
    public class HomePageController
    {

        private readonly Service Service;
        private HomePage View;

        public Produse_Menu_ItemController Home_ProdusePageController;
        public AdaugaProdus_Menu_ItemController Home_AdaugaProdusPageController;
        public StatisticiProduse_Menu_ItemController Home_StatisticiProdusePageController;
        public Servicii_Menu_ItemController Home_ServiciiPageController;
        public AdaugaServiciu_Menu_ItemController Home_AdaugaServiciuPageController;
        public StatisticiServicii_Menu_ItemController Home_StatisticiServiciiPageController;
        public VeziImaginiProduse_Menu_ItemController Home_VeziImaginiProdusePageController;
        public Intrari_Menu_ItemController Home_IntrariPageController;
        public AdaugaIntrare_Menu_ItemController Home_AdaugaIntrarePageController;

        public Clienti_Menu_ItemController Home_ClientiPageController;
        public AdaugaClient_Menu_ItemController Home_AdaugaClientPageController;
        public StatisticiClienti_Menu_ItemController Home_StatisticiClientiPageController;

        public AdaugaNumar_Menu_ItemController Home_AdaugaNumarPageController;
        public NumereMasina_Menu_ItemController Home_NumereMasinaPageController;

        public AdaugaPret_Menu_ItemController Home_AdaugaPretPageController;
        public Preturi_Menu_ItemController Home_PreturiPageController;

        public AdaugaFurnizor_Menu_ItemController Home_AdaugaFurnizorPageController;
        public Furnizori_Menu_ItemController Home_FurnizoriPageController;

        public AdaugaContract_Menu_ItemController Home_AdaugaContractPageController;
        public Contracte_Menu_ItemController Home_ContractePageController;

        public AdaugaFactura_Menu_ItemController Home_AdaugaFacturaPageController;
        public Facturi_Menu_ItemController Home_FacturiPageController;

        public Vanzari_Menu_ItemController Home_VanzariPageController;

        public VanzariProduse_Menu_ItemController Home_VanzariProdusePageController;
        public VanzariServicii_Menu_ItemController Home_VanzariServiciiPageController;


        public HomePageController(ref Service s, HomePage v)
        {
            this.View = v;
            this.Service = s;

            View.HomePageLoaded += OnHomePageLoaded;
            
            View.ProduseMIRequested += OnProduseMIRequested;
            View.AdaugaProdusMIRequested += OnAdaugaProdusMIRequested;
            View.StatisticiProduseMIRequested += OnStatisticiProduseMIRequested;
            View.ServiciiMIRequested += OnServiciiMIRequested;
            View.AdaugaServiciuMIRequested += OnAdaugaServiciuMIRequested;
            View.StatisticiServiciiMIRequested += OnStatisticiServiciiMIRequested;
            View.VeziImaginiProduseMIRequested += OnVeziImaginiProduseMIRequested;
            View.IntrariMIRequested += OnIntrariMIRequested;
            View.AdaugaIntrareMIRequested += OnAdaugaIntrareMIRequested;
            View.ClientiMIRequested += OnClientiMIRequested;

            View.AdaugaClientMIRequested += OnAdaugaClientMIRequested;
            View.StatisticiClientiMIRequested += OnStatisticiClientiMIRequested;
            View.AdaugaNumarMIRequested += OnAdaugaNumarMIRequested;
            View.NumereMasinaMIRequested += OnNumereMasinaMIRequested;
            View.AdaugaPretMIRequested += OnAdaugaPretMIRequested;
            View.PreturiMIRequested += OnPreturiMIRequested;
            View.AdaugaFurnizorMIRequested += OnAdaugaFurnizorMIRequested;
            View.FurnizoriMIRequested += OnFurnizoriMIRequested;
            View.AdaugaContractMIRequested += OnAdaugaContractMIRequested;
            View.ContracteMIRequested += OnContracteMIRequested;

            View.AdaugaFacturaMIRequested += OnAdaugaFacturaMIRequested;
            View.FacturiMIRequested += OnFacturiMIRequested;

            View.VanzariMIRequested += OnVanzariMIRequested;

            View.VanzariProduseMIRequested += OnVanzariProduseMIRequested;
            View.VanzariServiciiMIRequested += OnVanzariServiciiMIRequested;


        }

        public HomePageController(ref Service s)
        {
            this.Service = s;
            this.View = null;
        }

        public void setView(HomePage v)
        {
            this.View = v;
            View.HomePageLoaded += OnHomePageLoaded;
            
            View.ProduseMIRequested += OnProduseMIRequested;
            View.AdaugaProdusMIRequested += OnAdaugaProdusMIRequested;
            View.StatisticiProduseMIRequested += OnStatisticiProduseMIRequested;
            View.ServiciiMIRequested += OnServiciiMIRequested;
            View.AdaugaServiciuMIRequested += OnAdaugaServiciuMIRequested;
            View.StatisticiServiciiMIRequested += OnStatisticiServiciiMIRequested;
            View.VeziImaginiProduseMIRequested += OnVeziImaginiProduseMIRequested;
            View.IntrariMIRequested += OnIntrariMIRequested;
            View.AdaugaIntrareMIRequested += OnAdaugaIntrareMIRequested;
            View.ClientiMIRequested += OnClientiMIRequested;

            View.AdaugaClientMIRequested += OnAdaugaClientMIRequested;
            View.StatisticiClientiMIRequested += OnStatisticiClientiMIRequested;
            View.AdaugaNumarMIRequested += OnAdaugaNumarMIRequested;
            View.NumereMasinaMIRequested += OnNumereMasinaMIRequested;
            View.AdaugaPretMIRequested += OnAdaugaPretMIRequested;

            View.PreturiMIRequested += OnPreturiMIRequested;

            View.AdaugaFurnizorMIRequested += OnAdaugaFurnizorMIRequested;
            View.FurnizoriMIRequested += OnFurnizoriMIRequested;

            View.AdaugaContractMIRequested += OnAdaugaContractMIRequested;
            View.ContracteMIRequested += OnContracteMIRequested;

            View.AdaugaFacturaMIRequested += OnAdaugaFacturaMIRequested;

            View.FacturiMIRequested += OnFacturiMIRequested;

            View.VanzariMIRequested += OnVanzariMIRequested;

            View.VanzariProduseMIRequested += OnVanzariProduseMIRequested;
            View.VanzariServiciiMIRequested += OnVanzariServiciiMIRequested;

        }

        public HomePage getView()
        {
            return this.View;
        }

        private void OnHomePageLoaded(object sender, EventArgs e)
        {
            if (Service.AttemptDatabaseConnection())
            {

                View.DatabaseConnectionOk();

            }
            else
            {

                View.DatabaseConnectionFailed();
            }
        }


        private void OnProduseMIRequested(object sender, EventArgs e)
        {

            Produse_Menu_Item ProduseView = new Produse_Menu_Item();

            Home_ProdusePageController.setView(ProduseView);

            View.openChildForm(Home_ProdusePageController.getView());

        }

        private void OnAdaugaProdusMIRequested(object sender, EventArgs e)
        {

            AdaugaProdus_Menu_Item AdaugaProdusView = new AdaugaProdus_Menu_Item();

            Home_AdaugaProdusPageController.setView(AdaugaProdusView);

            View.openChildForm(Home_AdaugaProdusPageController.getView());
        }


        private void OnStatisticiProduseMIRequested(object sender, EventArgs e)
        {

            StatisticiProduse_Menu_Item StatisticiProduseView = new StatisticiProduse_Menu_Item();

            Home_StatisticiProdusePageController.setView(StatisticiProduseView);

            View.openChildForm(Home_StatisticiProdusePageController.getView());
        }

        private void OnServiciiMIRequested(object sender, EventArgs e)
        {

            Servicii_Menu_Item ServiciiView = new Servicii_Menu_Item();

            Home_ServiciiPageController.setView(ServiciiView);

            View.openChildForm(Home_ServiciiPageController.getView());
        }

        private void OnAdaugaServiciuMIRequested(object sender, EventArgs e)
        {

            AdaugaServiciu_Menu_Item AdaugaServiciuView = new AdaugaServiciu_Menu_Item();

            Home_AdaugaServiciuPageController.setView(AdaugaServiciuView);

            View.openChildForm(Home_AdaugaServiciuPageController.getView());
        }

        private void OnStatisticiServiciiMIRequested(object sender, EventArgs e)
        {

            StatisticiServicii_Menu_Item StatisticiServiciiView = new StatisticiServicii_Menu_Item();

            Home_StatisticiServiciiPageController.setView(StatisticiServiciiView);

            View.openChildForm(Home_StatisticiServiciiPageController.getView());
        }

        private void OnVeziImaginiProduseMIRequested(object sender, EventArgs e)
        {

            VeziImaginiProduse_Menu_Item VeziImaginiProduseView = new VeziImaginiProduse_Menu_Item();

            Home_VeziImaginiProdusePageController.setView(VeziImaginiProduseView);

            View.openChildForm(Home_VeziImaginiProdusePageController.getView());
        }

        private void OnIntrariMIRequested(object sender, EventArgs e)
        {

            Intrari_Menu_Item IntrariView = new Intrari_Menu_Item();

            Home_IntrariPageController.setView(IntrariView);

            View.openChildForm(Home_IntrariPageController.getView());
        }

        private void OnAdaugaIntrareMIRequested(object sender, EventArgs e)
        {

            AdaugaIntrare_Menu_Item AdaugaIntrareView = new AdaugaIntrare_Menu_Item();

            Home_AdaugaIntrarePageController.setView(AdaugaIntrareView);

            View.openChildForm(Home_AdaugaIntrarePageController.getView());
        }

        private void OnClientiMIRequested(object sender, EventArgs e)
        {

            Clienti_Menu_Item ClientiView = new Clienti_Menu_Item();

            Home_ClientiPageController.setView(ClientiView);

            View.openChildForm(Home_ClientiPageController.getView());
        }

        private void OnAdaugaClientMIRequested(object sender, EventArgs e)
        {

            AdaugaClient_Menu_Item AdaugaClientView = new AdaugaClient_Menu_Item();

            Home_AdaugaClientPageController.setView(AdaugaClientView);

            View.openChildForm(Home_AdaugaClientPageController.getView());
        }

        private void OnStatisticiClientiMIRequested(object sender, EventArgs e)
        {

            StatisticiClienti_Menu_Item StatisticiClientiView = new StatisticiClienti_Menu_Item();

            Home_StatisticiClientiPageController.setView(StatisticiClientiView);

            View.openChildForm(Home_StatisticiClientiPageController.getView());
        }

        private void OnAdaugaNumarMIRequested(object sender, EventArgs e)
        {

            AdaugaNumar_Menu_Item AdaugaNumarView = new AdaugaNumar_Menu_Item();

            Home_AdaugaNumarPageController.setView(AdaugaNumarView);

            View.openChildForm(Home_AdaugaNumarPageController.getView());
        }

        private void OnNumereMasinaMIRequested(object sender, EventArgs e)
        {

            NumereMasina_Menu_Item NumereMasinaView = new NumereMasina_Menu_Item();

            Home_NumereMasinaPageController.setView(NumereMasinaView);

            View.openChildForm(Home_NumereMasinaPageController.getView());
        }

        private void OnAdaugaPretMIRequested(object sender, EventArgs e)
        {

            AdaugaPret_Menu_Item AdaugaPretView = new AdaugaPret_Menu_Item();

            Home_AdaugaPretPageController.setView(AdaugaPretView);

            View.openChildForm(Home_AdaugaPretPageController.getView());
        }

        private void OnPreturiMIRequested(object sender, EventArgs e)
        {

            Preturi_Menu_Item PreturiView = new Preturi_Menu_Item();

            Home_PreturiPageController.setView(PreturiView);

            View.openChildForm(Home_PreturiPageController.getView());
        }

        private void OnAdaugaFurnizorMIRequested(object sender, EventArgs e)
        {

            AdaugaFurnizor_Menu_Item AdaugaFurnizorView = new AdaugaFurnizor_Menu_Item();

            Home_AdaugaFurnizorPageController.setView(AdaugaFurnizorView);

            View.openChildForm(Home_AdaugaFurnizorPageController.getView());
        }
        private void OnFurnizoriMIRequested(object sender, EventArgs e)
        {

            Furnizori_Menu_Item FurnizoriView = new Furnizori_Menu_Item();

            Home_FurnizoriPageController.setView(FurnizoriView);

            View.openChildForm(Home_FurnizoriPageController.getView());
        }

        private void OnAdaugaContractMIRequested(object sender, EventArgs e)
        {

            AdaugaContract_Menu_Item AdaugaContractView = new AdaugaContract_Menu_Item();

            Home_AdaugaContractPageController.setView(AdaugaContractView);

            View.openChildForm(Home_AdaugaContractPageController.getView());
        }

        private void OnContracteMIRequested(object sender, EventArgs e)
        {

            Contracte_Menu_Item ContracteView = new Contracte_Menu_Item();

            Home_ContractePageController.setView(ContracteView);

            View.openChildForm(Home_ContractePageController.getView());
        }

        private void OnAdaugaFacturaMIRequested(object sender, EventArgs e)
        {

            AdaugaFactura_Menu_Item AdaugaFacturaView = new AdaugaFactura_Menu_Item();

            Home_AdaugaFacturaPageController.setView(AdaugaFacturaView);

            View.openChildForm(Home_AdaugaFacturaPageController.getView());
        }

        private void OnFacturiMIRequested(object sender, EventArgs e)
        {

            Facturi_Menu_Item FacturiView = new Facturi_Menu_Item();

            Home_FacturiPageController.setView(FacturiView);

            View.openChildForm(Home_FacturiPageController.getView());
        }

        private void OnVanzariMIRequested(object sender, EventArgs e)
        {

            Vanzari_Menu_Item VanzariView = new Vanzari_Menu_Item();

            Home_VanzariPageController.setView(VanzariView);

            View.openChildForm(Home_VanzariPageController.getView());
        }

        private void OnVanzariProduseMIRequested(object sender, EventArgs e)
        {

            VanzariProduse_Menu_Item VanzariProduseView = new VanzariProduse_Menu_Item();

            Home_VanzariProdusePageController.setView(VanzariProduseView);

            View.openChildForm(Home_VanzariProdusePageController.getView());
        }

        private void OnVanzariServiciiMIRequested(object sender, EventArgs e)
        {

            VanzariServicii_Menu_Item VanzariServiciiView = new VanzariServicii_Menu_Item();

            Home_VanzariServiciiPageController.setView(VanzariServiciiView);

            View.openChildForm(Home_VanzariServiciiPageController.getView());
        }






    }
}
