using ManagerStoc.Models;
using ManagerStoc.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerStoc.Controllers
{
    public class AdaugaFactura_Menu_ItemController
    {
        enum AdaugaFacturaFormValidation
        {
            ADAUGAFACTURA_FORM_VALID,
            ADAUGAFACTURA_FORM_INPUTS_EMPTY,
            ADAUGAFACTURA_FORM_NEGATIVE_NULL_VALUES,
            ADAUGAFACTURA_FORM_LENGTH_NOT_OK,
            ADAUGAFACTURA_FORM_INPUTS_MISSING,

        }

        private readonly Service Service;
        private AdaugaFactura_Menu_Item View;

        public AdaugaFactura_Menu_ItemController(ref Service s, AdaugaFactura_Menu_Item v)
        {
            this.View = v;
            this.Service = s;

            View.AdaugaFacturaLoaded += OnAdaugaFacturaLoaded;
            View.AdaugaFacturaFormValidation += OnAdaugaFacturaFormValidation;
            View.AdaugaFacturaPressed += OnAdaugaFacturaPressed;

        }

        public AdaugaFactura_Menu_ItemController(ref Service s)
        {
            this.Service = s;
            this.View = null;
        }

        public void setView(AdaugaFactura_Menu_Item v)
        {
            this.View = v;

            View.AdaugaFacturaLoaded += OnAdaugaFacturaLoaded;
            View.AdaugaFacturaFormValidation += OnAdaugaFacturaFormValidation;
            View.AdaugaFacturaPressed += OnAdaugaFacturaPressed;

        }

        public AdaugaFactura_Menu_Item getView()
        {
            return this.View;
        }

        private void GetClientDataTable()
        {
            View.ClientDataTableDT = Service.ExecuteGetNumeClientiProcedure();
        }

        private void GetContractDataTable()
        {
            View.ContractDataTableDT = Service.ExecuteGetNumeContracteProcedure();
        }

        private void OnAdaugaFacturaLoaded(object sender, EventArgs e)
        {

            GetClientDataTable();
            GetContractDataTable();

            if (View.ClientDataTableDT.Rows.Count > 0 && View.ContractDataTableDT.Rows.Count > 0)
            {
                View.ClientContractTablesReadSuccessfull();
            }
            else
            {
                View.ClientContractTablesReadFailed();
            }

        }


        private AdaugaFacturaFormValidation ValidateAdaugaFacturaMenuItemForm()
        {
            AdaugaFacturaFormValidation retVal = AdaugaFacturaFormValidation.ADAUGAFACTURA_FORM_INPUTS_EMPTY;

            if (View.flagNoClientContract_bool == false)
            {

                if (!string.IsNullOrEmpty(View.DirectieFactura) && !string.IsNullOrEmpty(View.SumaTotala.ToString())
                    )
                {

                    if (View.DirectieFactura != "Directie factura" && View.SumaTotala.ToString() != "Suma totala")
                    {

                        if ((View.DirectieFactura.Length >= 6 && View.DirectieFactura.Length <= 10) && View.SumaTotala.ToString().Length >= 1
                            && View.SumaTotala.ToString().Length <= 5
                            )
                        {

                            if (View.SumaTotala > 0)
                            {

                                retVal = AdaugaFacturaFormValidation.ADAUGAFACTURA_FORM_VALID;
                            }
                            else
                            {
                                retVal = AdaugaFacturaFormValidation.ADAUGAFACTURA_FORM_NEGATIVE_NULL_VALUES;

                            }

                        }
                        else
                        {
                            retVal = AdaugaFacturaFormValidation.ADAUGAFACTURA_FORM_LENGTH_NOT_OK;
                        }


                    }
                    else
                    {
                        retVal = AdaugaFacturaFormValidation.ADAUGAFACTURA_FORM_INPUTS_MISSING;
                    }

                }
                else
                {
                    retVal = AdaugaFacturaFormValidation.ADAUGAFACTURA_FORM_INPUTS_EMPTY;
                }
            }


            return retVal;
        }

        private void OnAdaugaFacturaFormValidation(object sender, EventArgs e)
        {

            switch (ValidateAdaugaFacturaMenuItemForm())
            {

                case AdaugaFacturaFormValidation.ADAUGAFACTURA_FORM_VALID:
                    View.ValidationFormValid();
                    break;

                case AdaugaFacturaFormValidation.ADAUGAFACTURA_FORM_INPUTS_EMPTY:
                    View.ValidateInputsEmpty();
                    break;

                case AdaugaFacturaFormValidation.ADAUGAFACTURA_FORM_NEGATIVE_NULL_VALUES:
                    View.ValidationNegativeOrNullValues();
                    break;

                case AdaugaFacturaFormValidation.ADAUGAFACTURA_FORM_LENGTH_NOT_OK:
                    View.ValidateInputLengthNotOk();
                    break;

                case AdaugaFacturaFormValidation.ADAUGAFACTURA_FORM_INPUTS_MISSING:
                    View.ValidateInputsMissing();
                    break;

                default:
                    //should not be reached
                    break;

            }

        }


        private void OnAdaugaFacturaPressed(object sender, EventArgs e)
        {

            FacturaModel FModel = new FacturaModel(View.SumaTotala,View.DirectieFactura,View.FacturaAchitata,0);

            if (Service.ExecuteInsertFacturaProcedure(FModel, View.Client_Ales_int, View.Contract_Ales_int, View.DataScadenta))
            {
                View.FacturaAdaugataSuccessfull();
            }
            else
            {
                View.FacturaAdaugataFailed();
            }

        }
    }
}
