using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ManagerStoc.Controllers;
using ManagerStoc.Services;

namespace ManagerStoc
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            bool MSOpen = false;

            using (Mutex mutex = new Mutex(true, "ManagerStoc", out MSOpen))
            {

                if(MSOpen)
                {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);

                    //unique instance of DB Service
                    Service Service = Service.Instance;

                    //Starting view
                    LogInPage LoginView = new LogInPage();

                    //main views controllers
                    LogInPageController LogInViewController = new LogInPageController(ref Service, LoginView);
                    RegisterPageController RegisterViewController = new RegisterPageController(ref Service);
                    HomePageController HomeViewController = new HomePageController(ref Service);

                    //child views controllers
                    Produse_Menu_ItemController ProduseViewController = new Produse_Menu_ItemController(ref Service);
                    AdaugaProdus_Menu_ItemController AdaugaProdusViewController = new AdaugaProdus_Menu_ItemController(ref Service);
                    StatisticiProduse_Menu_ItemController StatisticiProduseViewController = new StatisticiProduse_Menu_ItemController(ref Service);
                    Servicii_Menu_ItemController ServiciiViewController = new Servicii_Menu_ItemController(ref Service);
                    AdaugaServiciu_Menu_ItemController AdaugaServiciuViewController = new AdaugaServiciu_Menu_ItemController(ref Service);
                    StatisticiServicii_Menu_ItemController StatisticiServiciiViewController = new StatisticiServicii_Menu_ItemController(ref Service);
                    VeziImaginiProduse_Menu_ItemController VeziImaginiProduseViewController = new VeziImaginiProduse_Menu_ItemController(ref Service);
                    Intrari_Menu_ItemController IntrariViewController = new Intrari_Menu_ItemController(ref Service);
                    AdaugaIntrare_Menu_ItemController AdaugaIntrareViewController = new AdaugaIntrare_Menu_ItemController(ref Service);
                    
                    Clienti_Menu_ItemController ClientiViewController = new Clienti_Menu_ItemController(ref Service);
                    AdaugaClient_Menu_ItemController AdaugaClientViewController = new AdaugaClient_Menu_ItemController(ref Service);
                    StatisticiClienti_Menu_ItemController StatisticiClientiViewController = new StatisticiClienti_Menu_ItemController(ref Service);
                    AdaugaNumar_Menu_ItemController AdaugaNumarViewController = new AdaugaNumar_Menu_ItemController(ref Service);
                    NumereMasina_Menu_ItemController NumereMasinaViewController = new NumereMasina_Menu_ItemController(ref Service);

                    AdaugaPret_Menu_ItemController AdaugaPretViewController = new AdaugaPret_Menu_ItemController(ref Service);
                    Preturi_Menu_ItemController PreturiViewController = new Preturi_Menu_ItemController(ref Service);

                    AdaugaFurnizor_Menu_ItemController AdaugaFurnizorViewController = new AdaugaFurnizor_Menu_ItemController(ref Service);
                    Furnizori_Menu_ItemController FurnizoriViewController = new Furnizori_Menu_ItemController(ref Service);

                    AdaugaContract_Menu_ItemController AdaugaContractViewController = new AdaugaContract_Menu_ItemController(ref Service);
                    Contracte_Menu_ItemController ContracteViewController = new Contracte_Menu_ItemController(ref Service);

                    AdaugaFactura_Menu_ItemController AdaugaFacturaViewController = new AdaugaFactura_Menu_ItemController(ref Service);
                    Facturi_Menu_ItemController FacturiViewController = new Facturi_Menu_ItemController(ref Service);

                    Vanzari_Menu_ItemController VanzariViewController = new Vanzari_Menu_ItemController(ref Service);
                    VanzariProduse_Menu_ItemController VanzariProduseViewController = new VanzariProduse_Menu_ItemController(ref Service);
                    VanzariServicii_Menu_ItemController VanzariServiciiViewController = new VanzariServicii_Menu_ItemController(ref Service);

                    //used to register new views with controllers, shallow copy, orginal objects are affected through copies
                    //for login page
                    LogInViewController.Login_RegisterPageController = RegisterViewController;
                    LogInViewController.Login_HomePageController = HomeViewController;
                    
                    //for home page
                    HomeViewController.Home_ProdusePageController = ProduseViewController;
                    HomeViewController.Home_AdaugaProdusPageController = AdaugaProdusViewController;
                    HomeViewController.Home_StatisticiProdusePageController = StatisticiProduseViewController;
                    HomeViewController.Home_ServiciiPageController = ServiciiViewController;
                    HomeViewController.Home_AdaugaServiciuPageController = AdaugaServiciuViewController;
                    HomeViewController.Home_StatisticiServiciiPageController = StatisticiServiciiViewController;
                    HomeViewController.Home_VeziImaginiProdusePageController = VeziImaginiProduseViewController;
                    HomeViewController.Home_IntrariPageController = IntrariViewController;
                    HomeViewController.Home_AdaugaIntrarePageController = AdaugaIntrareViewController;
                    
                    HomeViewController.Home_ClientiPageController = ClientiViewController;
                    HomeViewController.Home_AdaugaClientPageController = AdaugaClientViewController;
                    HomeViewController.Home_StatisticiClientiPageController = StatisticiClientiViewController;
                    HomeViewController.Home_AdaugaNumarPageController = AdaugaNumarViewController;
                    HomeViewController.Home_NumereMasinaPageController = NumereMasinaViewController;

                    HomeViewController.Home_AdaugaPretPageController = AdaugaPretViewController;
                    HomeViewController.Home_PreturiPageController = PreturiViewController;

                    HomeViewController.Home_AdaugaFurnizorPageController = AdaugaFurnizorViewController;
                    HomeViewController.Home_FurnizoriPageController = FurnizoriViewController;

                    HomeViewController.Home_AdaugaContractPageController = AdaugaContractViewController;
                    HomeViewController.Home_ContractePageController = ContracteViewController;

                    HomeViewController.Home_AdaugaFacturaPageController = AdaugaFacturaViewController;
                    HomeViewController.Home_FacturiPageController = FacturiViewController;

                    HomeViewController.Home_VanzariPageController = VanzariViewController;
                    HomeViewController.Home_VanzariProdusePageController = VanzariProduseViewController;
                    HomeViewController.Home_VanzariServiciiPageController = VanzariServiciiViewController;

                    Application.Run(LogInViewController.getView());

                    mutex.ReleaseMutex();

                }
                else
                {
                    MessageBox.Show("Aplicatia Manager Stoc ruleaza!","Aplicatia este deja deschisa!",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
            }
            
            

        }
    }
}
