using ManagerStoc.Models;
using ManagerStoc.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ManagerStoc.Controllers
{
    public class RegisterPageController
    {
        public enum RegisterFormValidation
        {
            REGISTER_FORM_VALID,
            REGISTER_FORM_PW_RPW_NOT_SAME,
            REGISTER_FORM_USER_OR_PW_LENGTH,
            REGISTER_FORM_USER_OR_PW_EMPTY
        }


        private readonly Service Service;
        private RegisterPage View;

        public RegisterPageController(ref Service s, RegisterPage v)
        {
            this.View = v;
            this.Service = s;

            View.UtilizatorChanged += OnUtilizatorChanged;
            View.ParolaChanged += OnParolaChanged;
            View.RepetaParolaChanged += OnRepetaParolaChanged;
            View.RegisterPressed += OnRegisterPressed;
            View.LoginPageRequested += OnLoginPageRequested;
        }

        public RegisterPageController(ref Service s)
        {
            this.Service = s;
            this.View = null;
        }

        public void setView(RegisterPage v)
        {
            this.View = v;

            View.UtilizatorChanged += OnUtilizatorChanged;
            View.ParolaChanged += OnParolaChanged;
            View.RepetaParolaChanged += OnRepetaParolaChanged;
            View.RegisterPressed += OnRegisterPressed;
            View.LoginPageRequested += OnLoginPageRequested;
        }

        public RegisterPage getView()
        {
            return this.View;
        }


        private void OnLoginPageRequested(object sender, EventArgs e)
        {
            if ((Application.OpenForms["LogInPage"] as LogInPage) != null)
            {

                LogInPage LogInPageViewInstance = (LogInPage)Application.OpenForms["LogInPage"];
                LogInPageViewInstance.Show();

            }
            else
            {
               //should not be reached
            }

        }

        private void OnRegisterPressed(object sender, EventArgs e)
        {

            LoginModel LModel = new LoginModel(View.Utilizator,View.Parola);

            if(Service.ExecRegisterProcedure(LModel))
            {

                View.RegisterSuccessfull();

                if ((Application.OpenForms["LogInPage"] as LogInPage) != null)
                {

                    LogInPage LogInPageViewInstance = (LogInPage)Application.OpenForms["LogInPage"];
                    LogInPageViewInstance.Show();

                }
                else
                {
                    //should not be reached
                }

            }
            else
            {
                View.RegisterFailed();

            }
        }

        private void OnRepetaParolaChanged(object sender, EventArgs e)
        {
            switch (ValidateFormInregistrare())
            {
                case RegisterFormValidation.REGISTER_FORM_VALID:
                    View.RegisterFormValid();
                    break;

                case RegisterFormValidation.REGISTER_FORM_PW_RPW_NOT_SAME:
                    View.RegisterFormPwsNotSame();
                    break;

                case RegisterFormValidation.REGISTER_FORM_USER_OR_PW_LENGTH:
                    View.RegisterFormUserOrPwLength();
                    break;

                case RegisterFormValidation.REGISTER_FORM_USER_OR_PW_EMPTY:
                    View.RegisterFormUserOrPwEmpty();
                    break;

                default:
                    //should not be reached
                    break;
            }
        }

        private void OnParolaChanged(object sender, EventArgs e)
        {
            switch (ValidateFormInregistrare())
            {
                case RegisterFormValidation.REGISTER_FORM_VALID:
                    View.RegisterFormValid();
                    break;

                case RegisterFormValidation.REGISTER_FORM_PW_RPW_NOT_SAME:
                    View.RegisterFormPwsNotSame();
                    break;

                case RegisterFormValidation.REGISTER_FORM_USER_OR_PW_LENGTH:
                    View.RegisterFormUserOrPwLength();
                    break;

                case RegisterFormValidation.REGISTER_FORM_USER_OR_PW_EMPTY:
                    View.RegisterFormUserOrPwEmpty();
                    break;

                default:
                    //should not be reached
                    break;
            }
        }

        private void OnUtilizatorChanged(object sender, EventArgs e)
        {
            switch (ValidateFormInregistrare())
            {
                case RegisterFormValidation.REGISTER_FORM_VALID:
                    View.RegisterFormValid();
                    break;

                case RegisterFormValidation.REGISTER_FORM_PW_RPW_NOT_SAME:
                    View.RegisterFormPwsNotSame();
                    break;

                case RegisterFormValidation.REGISTER_FORM_USER_OR_PW_LENGTH:
                    View.RegisterFormUserOrPwLength();
                    break;

                case RegisterFormValidation.REGISTER_FORM_USER_OR_PW_EMPTY:
                    View.RegisterFormUserOrPwEmpty();
                    break;

                default:
                    //should not be reached
                    break;
            }

        }

        public RegisterFormValidation ValidateFormInregistrare()
        {
            RegisterFormValidation retVal;

            if (!string.IsNullOrEmpty(View.Utilizator) && !string.IsNullOrEmpty(View.Parola) && !string.IsNullOrEmpty(View.RepetaParola))
            {

                if ((View.Utilizator.Length >= 6 && View.Utilizator.Length <= 20) && (View.Parola.Length >= 6 && View.Parola.Length <= 20) && (View.RepetaParola.Length >= 6 && View.RepetaParola.Length <= 20))
                {

                    if (View.Parola == View.RepetaParola)
                    {
                        retVal = RegisterFormValidation.REGISTER_FORM_VALID;
                    }
                    else
                    {
                        retVal = RegisterFormValidation.REGISTER_FORM_PW_RPW_NOT_SAME;
                    }
                }
                else
                {
                    retVal = RegisterFormValidation.REGISTER_FORM_USER_OR_PW_LENGTH;
                }

            }
            else
            {
                retVal = RegisterFormValidation.REGISTER_FORM_USER_OR_PW_EMPTY;
            }

            return retVal;
        }
    }
}
