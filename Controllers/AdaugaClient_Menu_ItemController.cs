using ManagerStoc.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerStoc.Controllers
{
    public class AdaugaClient_Menu_ItemController
    {

        enum AdaugaClientFormValidation
        {
            ADAUGACLIENT_FORM_VALID,
            ADAUGACLIENT_FORM_INPUTS_EMPTY,
            ADAUGACLIENT_FORM_LENGTH_NOT_OK,
            ADAUGACLIENT_FORM_INPUTS_MISSING,
        }

        private readonly Service Service;
        private AdaugaClient_Menu_Item View;

        public AdaugaClient_Menu_ItemController(ref Service s, AdaugaClient_Menu_Item v)
        {
            this.View = v;
            this.Service = s;

            View.AdaugaClientFormValidation += OnAdaugaClientFormValidation;
            View.AdaugaClientPressed += OnAdaugaClientPressed;
        }

        public AdaugaClient_Menu_ItemController(ref Service s)
        {
            this.Service = s;
            this.View = null;
        }

        public void setView(AdaugaClient_Menu_Item v)
        {
            this.View = v;

            View.AdaugaClientFormValidation += OnAdaugaClientFormValidation;
            View.AdaugaClientPressed += OnAdaugaClientPressed;
        }

        public AdaugaClient_Menu_Item getView()
        {
            return this.View;
        }

        private AdaugaClientFormValidation ValidateAdaugaClientMenuItemForm()
        {
            AdaugaClientFormValidation retVal = AdaugaClientFormValidation.ADAUGACLIENT_FORM_INPUTS_EMPTY;

            if (!string.IsNullOrEmpty(View.NumeClient) && !string.IsNullOrEmpty(View.DescriereClient) && !string.IsNullOrEmpty(View.CodFiscal))
            {

                if (View.NumeClient != "Nume client" && View.DescriereClient != "Descriere client" && View.CodFiscal != "Cod fiscal")
                {

                    if ((View.NumeClient.Length >= 6 && View.NumeClient.Length <= 30) && (View.DescriereClient.Length >= 6 && View.DescriereClient.Length <= 30)
                        && (View.CodFiscal.Length >= 6 && View.CodFiscal.Length <= 10))
                    {
                        retVal = AdaugaClientFormValidation.ADAUGACLIENT_FORM_VALID;
                    }
                    else
                    {
                        retVal = AdaugaClientFormValidation.ADAUGACLIENT_FORM_LENGTH_NOT_OK;
                    }

                }
                else
                {
                    retVal = AdaugaClientFormValidation.ADAUGACLIENT_FORM_INPUTS_MISSING;
                }

            }
            else
            {
                retVal = AdaugaClientFormValidation.ADAUGACLIENT_FORM_INPUTS_EMPTY;
            }

            return retVal;
        }

        private void OnAdaugaClientFormValidation(object sender, EventArgs e)
        {

            switch (ValidateAdaugaClientMenuItemForm())
            {

                case AdaugaClientFormValidation.ADAUGACLIENT_FORM_VALID:
                    View.ValidationFormValid();
                    break;

                case AdaugaClientFormValidation.ADAUGACLIENT_FORM_INPUTS_EMPTY:
                    View.ValidateInputsEmpty();
                    break;

                case AdaugaClientFormValidation.ADAUGACLIENT_FORM_LENGTH_NOT_OK:
                    View.ValidateInputLengthNotOk();
                    break;

                case AdaugaClientFormValidation.ADAUGACLIENT_FORM_INPUTS_MISSING:
                    View.ValidateInputsMissing();
                    break;

                default:
                    //should not be reached
                    break;

            }

        }

        private void OnAdaugaClientPressed(object sender, EventArgs e)
        {

            ClientModel CModel = new ClientModel(View.NumeClient, View.DescriereClient, View.CodFiscal, 0);

            if (Service.ExecuteInsertClientProcedure(CModel))
            {
                View.ClientAdaugatSuccessfull();
            }
            else
            {
                View.ClientAdaugatFailed();
            }

        }
    }
}
