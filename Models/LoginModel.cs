using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerStoc.Models
{
    public class LoginModel
    {

        private String username;
        private String parola;

        public string Username { get => username; set => username = value; }
        public string Parola { get => parola; set => parola = value; }

        public LoginModel(String u, String p)
        {
            this.Username = u;
            this.Parola = p;
        }



    }
}
