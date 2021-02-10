using ManagerStoc.Models;
using ManagerStoc.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerStoc.Controllers
{
    public class AdaugaContract_Menu_ItemController
    {

        enum AdaugaContractFormValidation
        {
            ADAUGACONTRACT_FORM_VALID,
            ADAUGACONTRACT_FORM_INPUTS_EMPTY,
            ADAUGACONTRACT_FORM_NEGATIVE_NULL_VALUES,
            ADAUGACONTRACT_FORM_LENGTH_NOT_OK,
            ADAUGACONTRACT_FORM_INPUTS_MISSING,

        }

        private readonly Service Service;
        private AdaugaContract_Menu_Item View;

        public AdaugaContract_Menu_ItemController(ref Service s, AdaugaContract_Menu_Item v)
        {
            this.View = v;
            this.Service = s;

            View.AdaugaContractLoaded += OnAdaugaContractLoaded;
            View.AdaugaContractFormValidation += OnAdaugaContractFormValidation;
            View.AdaugaContractPressed += OnAdaugaContractPressed;

        }

        public AdaugaContract_Menu_ItemController(ref Service s)
        {
            this.Service = s;
            this.View = null;
        }

        public void setView(AdaugaContract_Menu_Item v)
        {
            this.View = v;

            View.AdaugaContractLoaded += OnAdaugaContractLoaded;
            View.AdaugaContractFormValidation += OnAdaugaContractFormValidation;
            View.AdaugaContractPressed += OnAdaugaContractPressed;

        }

        public AdaugaContract_Menu_Item getView()
        {
            return this.View;
        }

        private void GetClientTable()
        {
            View.ClientDataTableDT = Service.ExecuteGetNumeClientiProcedure();
        }

        private void OnAdaugaContractLoaded(object sender, EventArgs e)
        {

            GetClientTable();

            if (View.ClientDataTableDT.Rows.Count > 0)
            {
                View.ClientTableReadSuccessfull();
            }
            else
            {
                View.ClientTableReadFailed();
            }

        }

        private AdaugaContractFormValidation ValidateAdaugaContractMenuItemForm()
        {
            AdaugaContractFormValidation retVal = AdaugaContractFormValidation.ADAUGACONTRACT_FORM_INPUTS_EMPTY;

            if (View.flagNoClient_bool == false)
            {

                if (!string.IsNullOrEmpty(View.DetaliiContract) && !string.IsNullOrEmpty(View.Durata.ToString())
                    )
                {

                    if (View.DetaliiContract != "Detalii contract" && View.Durata.ToString() != "Durata")
                    {

                        if ((View.DetaliiContract.Length >= 6 && View.DetaliiContract.Length <= 30) &&
                            (View.Durata.ToString().Length >= 1 && View.Durata.ToString().Length <= 3)
                           )
                        {

                            if (View.Durata > 0)
                            {
                                retVal = AdaugaContractFormValidation.ADAUGACONTRACT_FORM_VALID;
                            }
                            else
                            {

                                retVal = AdaugaContractFormValidation.ADAUGACONTRACT_FORM_NEGATIVE_NULL_VALUES;
                            }

                        }
                        else
                        {
                            retVal = AdaugaContractFormValidation.ADAUGACONTRACT_FORM_LENGTH_NOT_OK;
                        }


                    }
                    else
                    {
                        retVal = AdaugaContractFormValidation.ADAUGACONTRACT_FORM_INPUTS_MISSING;
                    }

                }
                else
                {
                    retVal = AdaugaContractFormValidation.ADAUGACONTRACT_FORM_INPUTS_EMPTY;
                }
            }

            return retVal;
        }


        private void OnAdaugaContractFormValidation(object sender, EventArgs e)
        {

            switch (ValidateAdaugaContractMenuItemForm())
            {

                case AdaugaContractFormValidation.ADAUGACONTRACT_FORM_VALID:
                    View.ValidationFormValid();
                    break;

                case AdaugaContractFormValidation.ADAUGACONTRACT_FORM_INPUTS_EMPTY:
                    View.ValidateInputsEmpty();
                    break;

                case AdaugaContractFormValidation.ADAUGACONTRACT_FORM_NEGATIVE_NULL_VALUES:
                    View.ValidationNegativeOrNullValues();
                    break;

                case AdaugaContractFormValidation.ADAUGACONTRACT_FORM_LENGTH_NOT_OK:
                    View.ValidateInputLengthNotOk();
                    break;

                case AdaugaContractFormValidation.ADAUGACONTRACT_FORM_INPUTS_MISSING:
                    View.ValidateInputsMissing();
                    break;

                default:
                    //should not be reached
                    break;

            }

        }

        private void OnAdaugaContractPressed(object sender, EventArgs e)
        {

            ContractModel CModel = new ContractModel(0,View.DetaliiContract,View.Durata);

            if (Service.ExecuteInsertContractProcedure(CModel, View.Client_Ales_int))
            {
                View.ContractAdaugatSuccessfull();
            }
            else
            {
                View.ContractAdaugatFailed();
            }

        }

    }
}
