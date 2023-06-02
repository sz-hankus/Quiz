using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizSolver
{
    public class PasswordInputVM
    {
        private String password;
        public String Password
        {
            get { return password; }
            set { password = value; }
        }

        public PasswordInputVM()
        {
            this.password = "";
        }
    }
}
