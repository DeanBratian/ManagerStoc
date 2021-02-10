using ManagerStoc.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerStoc.Controllers
{
    public class AdaugaNumar_Menu_ItemController
    {

        enum AdaugaNumarFormValidation
        {
            ADAUGANUMAR_FORM_VALID,
            ADAUGANUMAR_FORM_INPUTS_EMPTY,
            ADAUGANUMAR_FORM_LENGTH_NOT_OK,
            ADAUGANUMAR_FORM_INPUTS_MISSING,
        }

        private readonly Service Service;
        private AdaugaNumar_Menu_Item View;

        public AdaugaNumar_Menu_ItemController(ref Service s, AdaugaNumar_Menu_Item v)
        {
            this.View = v;
            this.Service = s;

            View.AdaugaNumarFormValidation += OnAdaugaNumarFormValidation;
            View.AdaugaNumarPressed += OnAdaugaNumarPressed;
        }

        public AdaugaNumar_Menu_ItemController(ref Service s)
        {
            this.Service = s;
            this.View = null;
        }

        public void setView(AdaugaNumar_Menu_Item v)
        {
            this.View = v;

            View.AdaugaNumarFormValidation += OnAdaugaNumarFormValidation;
            View.AdaugaNumarPressed += OnAdaugaNumarPressed;
        }

        public AdaugaNumar_Menu_Item getView()
        {
            return this.View;
        }

        private AdaugaNumarFormValidation ValidateAdaugaNumarMenuItemForm()
        {
            AdaugaNumarFormValidation retVal = AdaugaNumarFormValidation.ADAUGANUMAR_FORM_INPUTS_EMPTY;

            if (!string.IsNullOrEmpty(View.NumarMasina))
            {

                if (View.NumarMasina != "Numar masina")
                {

                    if ((View.NumarMasina.Length >= 3 && View.NumarMasina.Length <= 10))
                    {
                        retVal = AdaugaNumarFormValidation.ADAUGANUMAR_FORM_VALID;
                    }
                    else
                    {
                        retVal = AdaugaNumarFormValidation.ADAUGANUMAR_FORM_LENGTH_NOT_OK;
                    }

                }
                else
                {
                    retVal = AdaugaNumarFormValidation.ADAUGANUMAR_FORM_INPUTS_MISSING;
                }

            }
            else
            {
                retVal = AdaugaNumarFormValidation.ADAUGANUMAR_FORM_INPUTS_EMPTY;
            }

            return retVal;
        }

        private void OnAdaugaNumarFormValidation(object sender, EventArgs e)
        {

            switch (ValidateAdaugaNumarMenuItemForm())
            {

                case AdaugaNumarFormValidation.ADAUGANUMAR_FORM_VALID:
                    View.ValidationFormValid();
                    break;

                case AdaugaNumarFormValidation.ADAUGANUMAR_FORM_INPUTS_EMPTY:
                    View.ValidateInputsEmpty();
                    break;

                case AdaugaNumarFormValidation.ADAUGANUMAR_FORM_LENGTH_NOT_OK:
                    View.ValidateInputLengthNotOk();
                    break;

                case AdaugaNumarFormValidation.ADAUGANUMAR_FORM_INPUTS_MISSING:
                    View.ValidateInputsMissing();
                    break;

                default:
                    //should not be reached
                    break;

            }

        }

        private void OnAdaugaNumarPressed(object sender, EventArgs e)
        {

            if (Service.ExecuteInsertNumarMasinaProcedure(View.NumarMasina))
            {
                View.NumarMasinaAdaugatSuccessfull();
            }
            else
            {
                View.NumarMasinaAdaugatFailed();
            }

        }
    }
}
