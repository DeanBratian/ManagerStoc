using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManagerStoc.Models;
using ManagerStoc.Services;

namespace ManagerStoc.Controllers
{
    public class LogInPageController
    {
        enum LogInFormValidation
        {
            LOGIN_FORM_VALID,
            LOGIN_FORM_USER_OR_PW_MISSING,
            LOGIN_FORM_USER_OR_PW_LENGTH,
            LOGIN_FORM_USER_OR_PW_EMPTY
        }

        private readonly Service Service;
        private LogInPage View;


        public RegisterPageController Login_RegisterPageController;
        public HomePageController Login_HomePageController;

        public LogInPageController(ref Service s, LogInPage v)
        {

            this.Service = s;
            this.View = v;

            View.UtilizatorChanged += OnUtilizatorChanged;
            View.ParolaChanged += OnParolaChanged;
            View.LoginPressed += OnLoginPressed;
            View.RegisterPageRequested += OnRegisterPageRequested;

        }
        public LogInPage getView()
        {
            return this.View;
        }

        private void OnRegisterPageRequested(object sender, EventArgs e)
        {

            RegisterPage RegisterView = new RegisterPage();

            Login_RegisterPageController.setView(RegisterView);
            Login_RegisterPageController.getView().Show();

        }

        private void OnLoginPressed(object sender, EventArgs e)
        {

            LoginModel LModel = new LoginModel(View.Utilizator,View.Parola);
            
            if(Service.ExecLoginProcedure(LModel))
            {

                View.LoginSuccessfull();

                HomePage HomePageView = new HomePage(View.Utilizator);

                Login_HomePageController.setView(HomePageView);
                Login_HomePageController.getView().Show();
            }
            else
            {

                View.LoginFailed();
            }

        }

        private void OnParolaChanged(object sender, EventArgs e)
        {
            switch (ValidateLoginForm())
            {
                case LogInFormValidation.LOGIN_FORM_VALID:
                    View.LoginFormValid();
                    break;

                case LogInFormValidation.LOGIN_FORM_USER_OR_PW_MISSING:
                    View.LoginFormUserOrPwMissing();
                    break;
                
                case LogInFormValidation.LOGIN_FORM_USER_OR_PW_LENGTH:
                    View.LoginFormUserOrPwLength();
                    break;
                
                case LogInFormValidation.LOGIN_FORM_USER_OR_PW_EMPTY:
                    View.LoginFormUserOrPwEmpty();
                    break;

                default:
                    //should not be reached
                    break;
            }
        }

        private void OnUtilizatorChanged(object sender, EventArgs e)
        {
            switch (ValidateLoginForm())
            {
                case LogInFormValidation.LOGIN_FORM_VALID:
                    View.LoginFormValid();
                    break;

                case LogInFormValidation.LOGIN_FORM_USER_OR_PW_MISSING:
                    View.LoginFormUserOrPwMissing();
                    break;

                case LogInFormValidation.LOGIN_FORM_USER_OR_PW_LENGTH:
                    View.LoginFormUserOrPwLength();
                    break;

                case LogInFormValidation.LOGIN_FORM_USER_OR_PW_EMPTY:
                    View.LoginFormUserOrPwEmpty();
                    break;

                default:
                    //should not be reached
                    break;
            }
        }

        private LogInFormValidation ValidateLoginForm()
        {
            LogInFormValidation returnVal;

            if (!string.IsNullOrEmpty(View.Utilizator) && !string.IsNullOrEmpty(View.Parola))
            {

                if ((View.Utilizator.Length >= 6 && View.Utilizator.Length <= 20) && (View.Parola.Length >= 6 && View.Parola.Length <= 20))
                {

                    if (View.Utilizator != "Utilizator" && View.Parola != "Parola")
                    {
                        returnVal = LogInFormValidation.LOGIN_FORM_VALID;
                    }
                    else
                    {
                        returnVal = LogInFormValidation.LOGIN_FORM_USER_OR_PW_MISSING;
                    }

                }
                else
                {
                    returnVal = LogInFormValidation.LOGIN_FORM_USER_OR_PW_LENGTH;
                }

            }
            else
            {
                returnVal = LogInFormValidation.LOGIN_FORM_USER_OR_PW_EMPTY;
            }

            return returnVal;
        }


    }
}
