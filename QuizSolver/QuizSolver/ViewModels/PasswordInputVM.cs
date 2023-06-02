using System;

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
