using SynsPunkt_ApS.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SynsPunkt_ApS.UI
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Martin: Hvis GetUserLogin returnere true(gør den på successfuld login), så gemmes LoginForm
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_login_Click(object sender, EventArgs e)
        {
            if (Services.UserLogIn.GetUserLogin(tb_username.Text, tb_password.Text))
            {
                Ansat_Services.GetUserID(tb_username.Text);
                this.Hide();
            };
        }

        public string GetUserID()
        {
            string username = tb_username.Text;
            return username;
        }
    }
}
