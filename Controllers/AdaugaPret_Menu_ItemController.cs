using ManagerStoc.Models;
using ManagerStoc.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerStoc.Controllers
{
    public class AdaugaPret_Menu_ItemController
    {

        enum AdaugaPretFormValidation
        {
            ADAUGAPRET_FORM_VALID,
            ADAUGAPRET_FORM_INPUTS_EMPTY,
            ADAUGAPRET_FORM_NEGATIVE_NULL_VALUES,
            ADAUGAPRET_FORM_LENGTH_NOT_OK,
            ADAUGAPRET_FORM_INPUTS_MISSING,

        }

        private readonly Service Service;
        private AdaugaPret_Menu_Item View;

        public AdaugaPret_Menu_ItemController(ref Service s, AdaugaPret_Menu_Item v)
        {
            this.View = v;
            this.Service = s;

            View.AdaugaPretLoaded += OnAdaugaPretLoaded;
            View.AdaugaPretFormValidation += OnAdaugaPretFormValidation;
            View.AdaugaPretPressed += OnAdaugaPretPressed;

        }

        public AdaugaPret_Menu_ItemController(ref Service s)
        {
            this.Service = s;
            this.View = null;
        }

        public void setView(AdaugaPret_Menu_Item v)
        {
            this.View = v;
            View.AdaugaPretLoaded += OnAdaugaPretLoaded;
            View.AdaugaPretFormValidation += OnAdaugaPretFormValidation;
            View.AdaugaPretPressed += OnAdaugaPretPressed;

        }

        public AdaugaPret_Menu_Item getView()
        {
            return this.View;
        }

        private void GetContractTable()
        {
            View.ContractDataTableDT = Service.ExecuteGetContracteProcedure();
        }

        private void OnAdaugaPretLoaded(object sender, EventArgs e)
        {

            GetContractTable();

            if (View.ContractDataTableDT.Rows.Count > 0)
            {
                View.ContractTableReadSuccessfull();
            }
            else
            {
                View.ContractTableReadFailed();
            }

        }


        private void OnAdaugaPretFormValidation(object sender, EventArgs e)
        {

            switch (ValidateAdaugaPretMenuItemForm())
            {

                case AdaugaPretFormValidation.ADAUGAPRET_FORM_VALID:
                    View.ValidationFormValid();
                    break;

                case AdaugaPretFormValidation.ADAUGAPRET_FORM_INPUTS_EMPTY:
                    View.ValidateInputsEmpty();
                    break;

                case AdaugaPretFormValidation.ADAUGAPRET_FORM_NEGATIVE_NULL_VALUES:
                    View.ValidationNegativeOrNullValues();
                    break;

                case AdaugaPretFormValidation.ADAUGAPRET_FORM_LENGTH_NOT_OK:
                    View.ValidateInputLengthNotOk();
                    break;

                case AdaugaPretFormValidation.ADAUGAPRET_FORM_INPUTS_MISSING:
                    View.ValidateInputsMissing();
                    break;

                default:
                    //should not be reached
                    break;

            }

        }

        private AdaugaPretFormValidation ValidateAdaugaPretMenuItemForm()
        {
            AdaugaPretFormValidation retVal = AdaugaPretFormValidation.ADAUGAPRET_FORM_INPUTS_EMPTY;

            if (View.flagNoContract_bool == false)
            {

                if (!string.IsNullOrEmpty(View.NumeServiciuTotal) && !string.IsNullOrEmpty(View.Detalii)
                    && !string.IsNullOrEmpty(View.Pret.ToString())
                    )
                {

                    if (View.NumeServiciuTotal != "Nume serviciu complet" && View.Detalii != "Detalii"
                        && View.Pret.ToString() != "Pret"
                       )
                    {

                        if ((View.NumeServiciuTotal.Length >= 6 && View.NumeServiciuTotal.Length <= 40) && (View.Detalii.Length >= 6 && View.Detalii.Length <= 40)
                            && (View.Pret.ToString().Length >= 1) && (View.Pret.ToString().Length <= 5)
                            )
                        {

                            if (View.Pret > 0)
                            {
                                retVal = AdaugaPretFormValidation.ADAUGAPRET_FORM_VALID;
                            }
                            else
                            {
                                retVal = AdaugaPretFormValidation.ADAUGAPRET_FORM_NEGATIVE_NULL_VALUES;
                            }

                        }
                        else
                        {
                            retVal = AdaugaPretFormValidation.ADAUGAPRET_FORM_LENGTH_NOT_OK;
                        }

                    }
                    else
                    {
                        retVal = AdaugaPretFormValidation.ADAUGAPRET_FORM_INPUTS_MISSING;
                    }

                }
                else
                {
                    retVal = AdaugaPretFormValidation.ADAUGAPRET_FORM_INPUTS_EMPTY;
                }


            }


            return retVal;
        }

        private void OnAdaugaPretPressed(object sender, EventArgs e)
        {

            PretTotalModel PTModel = new PretTotalModel(0, View.NumeServiciuTotal, View.Detalii, View.Pret);

            if (Service.ExecuteInsertPretTotalProcedure(PTModel, View.Contract_Ales_int))
            {
                View.PretAdaugatSuccessfull();
            }
            else
            {
                View.PretAdaugatFailed();
            }

        }
    }
}
