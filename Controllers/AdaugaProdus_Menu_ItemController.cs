using ManagerStoc.Models;
using ManagerStoc.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerStoc.Controllers
{
    public class AdaugaProdus_Menu_ItemController
    {
        enum AdaugaProdusFormValidation
        {
            ADAUGAPRODUS_FORM_VALID,
            ADAUGAPRODUS_FORM_INPUTS_EMPTY,
            ADAUGAPRODUS_FORM_NEGATIVE_VALUES,
            ADAUGAPRODUS_FORM_LENGTH_NOT_OK,
            ADAUGAPRODUS_FORM_INPUTS_MISSING,

        }

        private readonly Service Service;
        private AdaugaProdus_Menu_Item View;

        public AdaugaProdus_Menu_ItemController(ref Service s, AdaugaProdus_Menu_Item v)
        {
            this.View = v;
            this.Service = s;

            View.AdaugaProdusLoaded += OnAdaugaProdusLoaded;
            View.AdaugaProdusFormValidation += OnAdaugaProdusFormValidation;
            View.AdaugaProdusPressed += OnAdaugaProdusPressed;

        }

        public AdaugaProdus_Menu_ItemController(ref Service s)
        {
            this.Service = s;
            this.View = null;
        }

        public void setView(AdaugaProdus_Menu_Item v)
        {
            this.View = v;
            View.AdaugaProdusLoaded += OnAdaugaProdusLoaded;
            View.AdaugaProdusFormValidation += OnAdaugaProdusFormValidation;
            View.AdaugaProdusPressed += OnAdaugaProdusPressed;

        }

        public AdaugaProdus_Menu_Item getView()
        {
            return this.View;
        }


        private void GetFurnizorTable()
        {
            View.FurnizorDataTableDT = Service.ExecuteGetNumeFurnizorProcedure();
        }

        private void OnAdaugaProdusLoaded(object sender, EventArgs e)
        {

            GetFurnizorTable();

            if (View.FurnizorDataTableDT.Rows.Count > 0)
            {
                View.FurnizorTableReadSuccessfull();
            }
            else
            {
                View.FurnizorTableReadFailed();
            }

        }


        private void OnAdaugaProdusFormValidation(object sender, EventArgs e)
        {
            
            switch(ValidateAdaugaProdusMenuItemForm())
            {

                case AdaugaProdusFormValidation.ADAUGAPRODUS_FORM_VALID:
                    View.ValidationFormValid();
                    break;

                case AdaugaProdusFormValidation.ADAUGAPRODUS_FORM_INPUTS_EMPTY:
                    View.ValidateInputsEmpty();
                    break;

                case AdaugaProdusFormValidation.ADAUGAPRODUS_FORM_NEGATIVE_VALUES:
                    View.ValidationNegativeOrNullValues();
                    break;

                case AdaugaProdusFormValidation.ADAUGAPRODUS_FORM_LENGTH_NOT_OK:
                    View.ValidateInputLengthNotOk();
                    break;

                case AdaugaProdusFormValidation.ADAUGAPRODUS_FORM_INPUTS_MISSING:
                    View.ValidateInputsMissing();
                    break;

                default:
                    //should not be reached
                    break;

            }

        }

        private AdaugaProdusFormValidation ValidateAdaugaProdusMenuItemForm()
        {
            AdaugaProdusFormValidation retVal = AdaugaProdusFormValidation.ADAUGAPRODUS_FORM_INPUTS_EMPTY;

            if (View.flagNoSupplier_bool == false)
            {
                if (!string.IsNullOrEmpty(View.NumeProdus) && !string.IsNullOrEmpty(View.DescriereProdus) && !string.IsNullOrEmpty(View.PretCumparare.ToString())
                    && !string.IsNullOrEmpty(View.PretVanzare.ToString()) && !string.IsNullOrEmpty(View.UnitateMasura)
                    )
                {

                    if (View.NumeProdus != "Nume produs" && View.DescriereProdus != "Descriere produs" && View.PretCumparare.ToString() != "Pret cumparare"
                        && View.PretVanzare.ToString() != "Pret vanzare" && View.UnitateMasura != "U/M")
                    {

                        if ((View.NumeProdus.Length >= 6 && View.NumeProdus.Length <= 30) && (View.DescriereProdus.Length >= 6 && View.DescriereProdus.Length <= 30)
                            && View.PretCumparare.ToString().Length <= 5 && View.PretVanzare.ToString().Length <= 5 && (View.UnitateMasura.Length >= 1 && View.UnitateMasura.Length <= 10)
                            )
                        {

                            if (View.PretCumparare > 0 && View.PretVanzare > 0)
                            {
                                retVal = AdaugaProdusFormValidation.ADAUGAPRODUS_FORM_VALID;
                            }
                            else
                            { 
                                retVal = AdaugaProdusFormValidation.ADAUGAPRODUS_FORM_NEGATIVE_VALUES;
                            }

                        }
                        else
                        {
                            retVal = AdaugaProdusFormValidation.ADAUGAPRODUS_FORM_LENGTH_NOT_OK;
                        }


                    }
                    else
                    {
                        retVal = AdaugaProdusFormValidation.ADAUGAPRODUS_FORM_INPUTS_MISSING;
                    }

                }
                else
                {
                    retVal = AdaugaProdusFormValidation.ADAUGAPRODUS_FORM_INPUTS_EMPTY;
                }
            }


            return retVal;
        }


        private void OnAdaugaProdusPressed(object sender, EventArgs e)
        {

            ProdusModel PModel = new ProdusModel(0,View.NumeProdus,View.DescriereProdus,View.PretCumparare,View.PretVanzare,View.UnitateMasura);

            if(Service.ExecuteAdaugaProdusProcedure(PModel, View.Furnizor_Ales_int))
            {
                View.ProdusAdaugatSuccessfull();
            }
            else
            {
                View.ProdusAdaugatFailed();
            }

        }

    }
}
