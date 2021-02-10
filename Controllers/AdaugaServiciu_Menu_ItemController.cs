using ManagerStoc.Models;
using ManagerStoc.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerStoc.Controllers
{
        public class AdaugaServiciu_Menu_ItemController
        {
            enum AdaugaServiciuFormValidation
            {
                ADAUGASERVICIU_FORM_VALID,
                ADAUGASERVICIU_FORM_INPUTS_EMPTY,
                ADAUGASERVICIU_FORM_NEGATIVE_VALUES,
                ADAUGASERVICIU_FORM_LENGTH_NOT_OK,
                ADAUGASERVICIU_FORM_INPUTS_MISSING,
            }

            private readonly Service Service;
            private AdaugaServiciu_Menu_Item View;

            public AdaugaServiciu_Menu_ItemController(ref Service s, AdaugaServiciu_Menu_Item v)
            {
                this.View = v;
                this.Service = s;

                View.AdaugaServiciuFormValidation += OnAdaugaServiciuFormValidation;
                View.AdaugaServiciuPressed += OnAdaugaServiciuPressed;

            }

            public AdaugaServiciu_Menu_ItemController(ref Service s)
            { 
                this.Service = s;
                this.View = null;
            }

            public void setView(AdaugaServiciu_Menu_Item v)
            {
                this.View = v;
                View.AdaugaServiciuFormValidation += OnAdaugaServiciuFormValidation;
                View.AdaugaServiciuPressed += OnAdaugaServiciuPressed;

            }

            public AdaugaServiciu_Menu_Item getView()
            {
                return this.View;
            }

        private AdaugaServiciuFormValidation ValidateAdaugaServiciuMenuItemForm()
        {
            AdaugaServiciuFormValidation returnVal;

            if (!string.IsNullOrEmpty(View.NumeServiciu) && !string.IsNullOrEmpty(View.DescriereServiciu)
                && !string.IsNullOrEmpty(View.Durata.ToString()) && !string.IsNullOrEmpty(View.Pret.ToString())
                )
            {

                if (View.NumeServiciu != "Nume serviciu" && View.DescriereServiciu != "Descriere serviciu" && View.Durata.ToString() != "Durata"
                    && View.Pret.ToString() != "Pret")
                {

                    if ((View.NumeServiciu.Length >= 6 && View.NumeServiciu.Length <= 40) && (View.DescriereServiciu.Length >= 6 && View.DescriereServiciu.Length <= 40)
                        && View.Durata.ToString().Length <= 5 && View.Pret.ToString().Length <= 5
                        )
                    {
                        if (View.Durata > 0)
                        {
                            returnVal = AdaugaServiciuFormValidation.ADAUGASERVICIU_FORM_VALID;
                        }
                        else
                        {
                            returnVal = AdaugaServiciuFormValidation.ADAUGASERVICIU_FORM_NEGATIVE_VALUES;
                        }

                    }
                    else
                    {
                        returnVal = AdaugaServiciuFormValidation.ADAUGASERVICIU_FORM_LENGTH_NOT_OK;
                    }

                }
                else
                {
                    returnVal = AdaugaServiciuFormValidation.ADAUGASERVICIU_FORM_INPUTS_MISSING;
                }

            }
            else
            {
                returnVal = AdaugaServiciuFormValidation.ADAUGASERVICIU_FORM_INPUTS_EMPTY;
            }

            return returnVal;
        }

        private void OnAdaugaServiciuFormValidation(object sender, EventArgs e)
        {

            switch (ValidateAdaugaServiciuMenuItemForm())
            {

                case AdaugaServiciuFormValidation.ADAUGASERVICIU_FORM_VALID:
                    View.ValidationFormValid();
                    break;

                case AdaugaServiciuFormValidation.ADAUGASERVICIU_FORM_INPUTS_EMPTY:
                    View.ValidateInputsEmpty();
                    break;

                case AdaugaServiciuFormValidation.ADAUGASERVICIU_FORM_NEGATIVE_VALUES:
                    View.ValidationNegativeOrNullValues();
                    break;

                case AdaugaServiciuFormValidation.ADAUGASERVICIU_FORM_LENGTH_NOT_OK:
                    View.ValidateInputLengthNotOk();
                    break;

                case AdaugaServiciuFormValidation.ADAUGASERVICIU_FORM_INPUTS_MISSING:
                    View.ValidateInputsMissing();
                    break;

                default:
                    //should not be reached
                    break;

            }

        }


        private void OnAdaugaServiciuPressed(object sender, EventArgs e)
        {

            ServiciuModel SModel = new ServiciuModel(0, View.NumeServiciu, View.DescriereServiciu, View.Pret, View.Durata);

            if (Service.ExecuteAdaugaServiciuProcedure(SModel))
            {
                View.ServiciuAdaugatSuccessfull();
            }
            else
            {
                View.ServiciuAdaugatFailed();
            }

        }


    }
}
