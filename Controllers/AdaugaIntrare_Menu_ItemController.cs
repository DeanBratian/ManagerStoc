using ManagerStoc.Models;
using ManagerStoc.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerStoc.Controllers
{
    public class AdaugaIntrare_Menu_ItemController
    {
        enum AdaugaIntrareFormValidation
        {
            ADAUGAINTRARE_FORM_VALID,
            ADAUGAINTRARE_FORM_INPUTS_EMPTY,
            ADAUGAINTRARE_FORM_NEGATIVE_NULL_VALUES,
            ADAUGAINTRARE_FORM_LENGTH_NOT_OK,
            ADAUGAINTRARE_FORM_INPUTS_MISSING,

        }

        private readonly Service Service;
        private AdaugaIntrare_Menu_Item View;

        public AdaugaIntrare_Menu_ItemController(ref Service s, AdaugaIntrare_Menu_Item v)
        {
            this.View = v;
            this.Service = s;

            View.AdaugaIntrareLoaded += OnAdaugaIntrareLoaded;
            View.AdaugaIntrareFormValidation += OnAdaugaIntrareFormValidation;
            View.AdaugaIntrarePressed += OnAdaugaIntrarePressed;
            View.ProdusSelectionChanged += OnProdusSelectionChanged;

        }

        public AdaugaIntrare_Menu_ItemController(ref Service s)
        {
            this.Service = s;
            this.View = null;
        }

        public void setView(AdaugaIntrare_Menu_Item v)
        {
            this.View = v;
            View.AdaugaIntrareLoaded += OnAdaugaIntrareLoaded;
            View.AdaugaIntrareFormValidation += OnAdaugaIntrareFormValidation;
            View.AdaugaIntrarePressed += OnAdaugaIntrarePressed;
            View.ProdusSelectionChanged += OnProdusSelectionChanged;

        }

        public AdaugaIntrare_Menu_Item getView()
        {
            return this.View;
        }

        private void GetFurnizorTable()
        {
            View.FurnizorDataTableDT = Service.ExecuteGetNumeFurnizoriPentruProdusProcedure(View.Produs_Ales_int);
        }

        private void GetProdusTable()
        {
            View.ProdusDataTableDT = Service.ExecuteGetNumeProduseProcedure();
        }

        private void OnAdaugaIntrareLoaded(object sender, EventArgs e)
        {

            GetProdusTable();

            if (View.ProdusDataTableDT.Rows.Count > 0)
            {
                View.ProdusTableReadSuccessfull();
            }
            else
            {
                View.ProdusTableReadFailed();
            }

        }


        private AdaugaIntrareFormValidation ValidateAdaugaIntrareMenuItemForm()
        {
            AdaugaIntrareFormValidation returnVal = AdaugaIntrareFormValidation.ADAUGAINTRARE_FORM_INPUTS_EMPTY;

            if (View.flagNoSupplierNoProduct_bool == false)
            {

                if (!string.IsNullOrEmpty(View.Cantitate.ToString()) && !string.IsNullOrEmpty(View.PretCumparare.ToString()))
                {

                    if (View.Cantitate.ToString().Trim() != "Cantitate" && View.PretCumparare.ToString().Trim() != "Pret cumparare")
                    {

                        if (View.Cantitate.ToString().Length <= 3 && View.PretCumparare.ToString().Length <= 5)
                        {

                            if (View.Cantitate > 0 && View.PretCumparare > 0)
                            {
                                returnVal = AdaugaIntrareFormValidation.ADAUGAINTRARE_FORM_VALID;
                            }
                            else
                            {
                                returnVal = AdaugaIntrareFormValidation.ADAUGAINTRARE_FORM_NEGATIVE_NULL_VALUES;
                            }

                        }
                        else
                        {
                            returnVal = AdaugaIntrareFormValidation.ADAUGAINTRARE_FORM_LENGTH_NOT_OK;
                        }

                    }
                    else
                    {
                        returnVal = AdaugaIntrareFormValidation.ADAUGAINTRARE_FORM_INPUTS_MISSING;
                    }

                }
                else
                {
                    returnVal = AdaugaIntrareFormValidation.ADAUGAINTRARE_FORM_INPUTS_EMPTY;
                }
            }


            return returnVal;
        }


        private void OnAdaugaIntrareFormValidation(object sender, EventArgs e)
        {

            switch (ValidateAdaugaIntrareMenuItemForm())
            {

                case AdaugaIntrareFormValidation.ADAUGAINTRARE_FORM_VALID:
                    View.ValidationFormValid();
                    break;

                case AdaugaIntrareFormValidation.ADAUGAINTRARE_FORM_INPUTS_EMPTY:
                    View.ValidateInputsEmpty();
                    break;

                case AdaugaIntrareFormValidation.ADAUGAINTRARE_FORM_NEGATIVE_NULL_VALUES:
                    View.ValidationNegativeOrNullValues();
                    break;

                case AdaugaIntrareFormValidation.ADAUGAINTRARE_FORM_LENGTH_NOT_OK:
                    View.ValidateInputLengthNotOk();
                    break;

                case AdaugaIntrareFormValidation.ADAUGAINTRARE_FORM_INPUTS_MISSING:
                    View.ValidateInputsMissing();
                    break;

                default:
                    //should not be reached
                    break;

            }

        }


        private void OnAdaugaIntrarePressed(object sender, EventArgs e)
        {

            IntrareModel IModel = new IntrareModel(View.Cantitate,View.Produs_Ales_int,View.Furnizor_Ales_int,View.PretCumparare);

            if (Service.ExecuteAdaugaIntrareProcedure(IModel))
            {
                View.IntrareAdaugataSuccessfull();
            }
            else
            {
                View.IntrareAdaugataFailed();
            }

        }

        private void OnProdusSelectionChanged(object sender, EventArgs e)
        {

            GetFurnizorTable();

            if (View.FurnizorDataTableDT.Rows.Count > 0)
            {

                View.BindDtToComboBoxFurnizori();
            }
            else
            {

                View.FurnizorTableReadFailed();
            }

        }




    }
}
