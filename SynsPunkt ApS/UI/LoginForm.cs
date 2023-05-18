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
            this.KeyPreview = true;
            InitializeComponent();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            
        }

        /// <summary>
        /// Martin: Hvis GetUserLogin returnere true(gør den på successfuld login), så gemmes LoginForm
        /// </summary>
        private void btn_login_Click(object sender, EventArgs e)
        {
            if (Services.UserLogIn.GetUserLogin(tb_username.Text, tb_password.Text))
            {
                this.Hide();
            };
        }

        /// <summary>
        /// Martin: Gør det muligt at logge ind med ENTER-tasten
        /// </summary>
        private void LoginForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (Services.UserLogIn.GetUserLogin(tb_username.Text, tb_password.Text))
                {
                    this.Hide();
                };
            }

        }

        //Martin: Kun tal kan indtastes i brugernavnfeltet
        private void tb_username_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

    }
}
