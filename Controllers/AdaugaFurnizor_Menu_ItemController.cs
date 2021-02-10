using ManagerStoc.Models;
using ManagerStoc.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerStoc.Controllers
{
    public class AdaugaFurnizor_Menu_ItemController
    {

        enum AdaugaFurnizorFormValidation
        {
            ADAUGAFURNIZOR_FORM_VALID,
            ADAUGAFURNIZOR_FORM_INPUTS_EMPTY,
            ADAUGAFURNIZOR_FORM_LENGTH_NOT_OK,
            ADAUGAFURNIZOR_FORM_INPUTS_MISSING,
        }

        private readonly Service Service;
        private AdaugaFurnizor_Menu_Item View;

        public AdaugaFurnizor_Menu_ItemController(ref Service s, AdaugaFurnizor_Menu_Item v)
        {
            this.View = v;
            this.Service = s;

            View.AdaugaFurnizorFormValidation += OnAdaugaFurnizorFormValidation;
            View.AdaugaFurnizorPressed += OnAdaugaFurnizorPressed;

        }

        public AdaugaFurnizor_Menu_ItemController(ref Service s)
        {
            this.Service = s;
            this.View = null;
        }

        public void setView(AdaugaFurnizor_Menu_Item v)
        {
            this.View = v;
            View.AdaugaFurnizorFormValidation += OnAdaugaFurnizorFormValidation;
            View.AdaugaFurnizorPressed += OnAdaugaFurnizorPressed;

        }

        public AdaugaFurnizor_Menu_Item getView()
        {
            return this.View;
        }


        private AdaugaFurnizorFormValidation ValidateAdaugaFurnizorMenuItemForm()
        {
            AdaugaFurnizorFormValidation retVal = AdaugaFurnizorFormValidation.ADAUGAFURNIZOR_FORM_INPUTS_EMPTY;

            if (!string.IsNullOrEmpty(View.NumeFurnizor) && !string.IsNullOrEmpty(View.Detalii)
                )
            {

                if (View.NumeFurnizor != "Nume furnizor" && View.Detalii != "Detalii"
                   )
                {

                    if ((View.NumeFurnizor.Length >= 6 && View.NumeFurnizor.Length <= 30) && (View.Detalii.Length >= 6 && View.Detalii.Length <= 30)
                        )
                    {
                        retVal = AdaugaFurnizorFormValidation.ADAUGAFURNIZOR_FORM_VALID;

                    }
                    else
                    {
                        retVal = AdaugaFurnizorFormValidation.ADAUGAFURNIZOR_FORM_LENGTH_NOT_OK;
                    }


                }
                else
                {
                    retVal = AdaugaFurnizorFormValidation.ADAUGAFURNIZOR_FORM_INPUTS_MISSING;
                }

            }
            else
            {
                retVal = AdaugaFurnizorFormValidation.ADAUGAFURNIZOR_FORM_INPUTS_EMPTY;
            }

            return retVal;
        }


        private void OnAdaugaFurnizorFormValidation(object sender, EventArgs e)
        {

            switch (ValidateAdaugaFurnizorMenuItemForm())
            {

                case AdaugaFurnizorFormValidation.ADAUGAFURNIZOR_FORM_VALID:
                    View.ValidationFormValid();
                    break;

                case AdaugaFurnizorFormValidation.ADAUGAFURNIZOR_FORM_INPUTS_EMPTY:
                    View.ValidateInputsEmpty();
                    break;

                case AdaugaFurnizorFormValidation.ADAUGAFURNIZOR_FORM_LENGTH_NOT_OK:
                    View.ValidateInputLengthNotOk();
                    break;

                case AdaugaFurnizorFormValidation.ADAUGAFURNIZOR_FORM_INPUTS_MISSING:
                    View.ValidateInputsMissing();
                    break;

                default:
                    //should not be reached
                    break;

            }

        }

        private void OnAdaugaFurnizorPressed(object sender, EventArgs e)
        {

            FurnizorModel FModel = new FurnizorModel(0,View.NumeFurnizor,View.Detalii);

            if (Service.ExecuteInsertFurnizorProcedure(FModel))
            {
                View.FurnizorAdaugatSuccessfull();
            }
            else
            {
                View.FurnizorAdaugatFailed();
            }

        }

    }
}
