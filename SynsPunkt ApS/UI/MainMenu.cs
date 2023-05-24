using SynsPunkt_ApS.Models;
using SynsPunkt_ApS.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace SynsPunkt_ApS
{
    public partial class MainMenu : Form
    {
        public Models.Ansat LoggedInEmployee;
        public MainMenu(Models.Ansat loggedInEmployee)
        {
            LoggedInEmployee = loggedInEmployee;
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            GetAllVare();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void userNameLabel_Click(object sender, EventArgs e)
        {

        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        private void tabPage4_Click(object sender, EventArgs e)
        {

        }

        private void tabPage6_Click(object sender, EventArgs e)
        {

        }

        private void tabPage7_Click(object sender, EventArgs e)
        {

        }

        private void tabPage8_Click(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {

        }

        private void btn_Vareoversigt_Click_1(object sender, EventArgs e)
        {
            tabControl.SelectedTab = tabPage_basket;
        }

        private void btn_Kundeoversigt_Click(object sender, EventArgs e)
        {
            tabControl.SelectedTab = tabPage_kundeoversigt;
        }

        private void btn_Bookingoversigt_Click(object sender, EventArgs e)
        {
            tabControl.SelectedTab = tabPage_BookingOversigt;
        }

        private void btn_Leverandoeroversigt_Click(object sender, EventArgs e)
        {
            tabControl.SelectedTab = tabPage_LeverandoerOversigt;
        }

        private void btn_Rapport_Click(object sender, EventArgs e)
        {
            tabControl.SelectedTab = tabPage_Rapport;
        }

        private void btn_Indstillinger_Click(object sender, EventArgs e)
        {
            tabControl.SelectedTab = tabPage_Indstillinger;
        }

        private void X(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btn_ChangePassWord_Click(object sender, EventArgs e)
        {
            Services.ChangePassword changePass = new Services.ChangePassword();
            bool changedPass = changePass.ChangeUserPassword(LoggedInEmployee.MedarbejderNummer.ToString(), tb_OldPassword.Text, tb_NewPassword1.Text, tb_newPassword2.Text);
            if (changedPass)
            {
                MessageBox.Show("Adgangskode ændret!", "SUCCESS!", MessageBoxButtons.OK);
            }
        }

        private void btn_Medarbejder_Click(object sender, EventArgs e)
        {
            tabControl.SelectedTab = tabPage_Medarbejder;
        }

        private void btn_addToBasket_Click(object sender, EventArgs e)
        {

        }

        

        private void btn_RemoveFromBasket_Click(object sender, EventArgs e)
        {

        }

        private void btn_SendInvoiceMail_Click(object sender, EventArgs e)
        {

        }

        private void btn_PrintInvoice_Click(object sender, EventArgs e)
        {

        }

        private void listView_basket_list_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void tb_SearchProduct_TextChanged(object sender, EventArgs e)
        {

        }

        private void listView_customers_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void tb_searchPhoneNumber_TextChanged(object sender, EventArgs e)
        {

        }

       
        private void btn_createCustomer_Click(object sender, EventArgs e)
        {
            //// Opret en kunde med opdaterede oplysninger baseret på værdierne i tekstboksene
            //Kunde opdateretKunde = new Kunde
            //{
            //    Fornavn = tb_CustomerFirstName.Text,
            //    Efternavn = tb_customerLastName.Text,
            //    TelefonNummer = Convert.ToInt32(tb_customerPhoneNumber.Text),
            //    PrivatEmail = tb_customerEmail.Text,
            //    Adresse = tb_customerAdress.Text,
            //    KundeNummer = tb_customerID.Text,
            //    KundeInfo = tb_customerInfo.Text,
            //    PostNr = Convert.ToInt32(tb_postNr.Text)
            //};
        }

        private void btn_updateCustomer_Click(object sender, EventArgs e)
        {
            //// Opret en kunde med opdaterede oplysninger baseret på værdierne i tekstboksene
            //Kunde opdateretKunde = new Kunde
            //{
            //    Fornavn = tb_CustomerFirstName.Text,
            //    Efternavn = tb_customerLastName.Text,
            //    TelefonNummer = Convert.ToInt32(tb_customerPhoneNumber.Text),
            //    PrivatEmail = tb_customerEmail.Text,
            //    Adresse = tb_customerAdress.Text,
            //    KundeNummer = tb_customerID.Text,
            //    KundeInfo = tb_customerInfo.Text,
            //    PostNr = Convert.ToInt32(tb_postNr.Text)
            //};
        }
        
        private void btn_deleteCustomer_Click(object sender, EventArgs e)
        {
            /// mangler
        }

        private void listView_Bookings_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker_Bookings_ValueChanged(object sender, EventArgs e)
        {

        }

        private void btn_clearDate_Click(object sender, EventArgs e)
        {

        }

        private void btn_createBooking_Click(object sender, EventArgs e)
        {

        }

        private void btn_updateBooking_Click(object sender, EventArgs e)
        {

        }

        private void btn_deleteBooking_Click(object sender, EventArgs e)
        {

        }

        private void listView_suppliers_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void tb_supplierID_TextChanged(object sender, EventArgs e)
        {

        }

        private void btn_searchSupplierID_Click(object sender, EventArgs e)
        {

        }

        private void listView_employees_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void tb_searchEmployee_TextChanged(object sender, EventArgs e)
        {

        }

       
        private void btn_CreateEmployee_Click(object sender, EventArgs e)
        {

        }

        private void btn_UpdateEmployee_Click(object sender, EventArgs e)
        {

        }

        private void btn_deleteEmployee_Click(object sender, EventArgs e)
        {

        }

        private void btn_GenerateReport_Click(object sender, EventArgs e)
        {

        }

        private void btn_sendReportMail_Click(object sender, EventArgs e)
        {

        }

        private void btn_printReport_Click(object sender, EventArgs e)
        {

        }

        private void listView1_listOfSuppliers_SelectedIndexChanged(object sender, EventArgs e)
        {
            Services.VareService vareService = new VareService();

            if (listView1_listOfSuppliers.SelectedItems.Count > 0)
            {
                ListViewItem selectedItem = listView1_listOfSuppliers.SelectedItems[0];
                var item = selectedItem.SubItems[0].Text;

                vareService.ReadVare(item, out string id2, out string vareBeskrivelse, out string lagerMængde, out string vareNavn,
                                     out string styrke, out string levCVR);

                tb_productID.Text = id2;
                rtb_productdescription.Text = vareBeskrivelse;
                tb_quantity.Text = lagerMængde;
                tb_productName.Text = vareNavn;
                tb_strengt.Text = styrke;
                tb_supplierCVR.Text = levCVR;
            }
        }

        private void tb_searchForProduct_TextChanged(object sender, EventArgs e)
        {
            /*  if (tb_searchForProduct.Text.Length == 0)
              {
                  GetAllVare();
              }
              else
              {
                  string searchText = tb_searchForProduct.Text.ToLower();
                  listView1_listOfSuppliers.Items.Clear();
                  foreach (var vare in originalVare)
                  {
                      if (vare.ToString().ToLower().Contains(searchText))
                      {
                          listView1_listOfSuppliers.Items.Add(vare);
                      }
                  }
              }
            */
        }

        private void btn_createProduct_Click(object sender, EventArgs e)
        {
            Services.VareService vareService = new VareService();
            bool quantityValid = int.TryParse(tb_quantity.Text, out int quantity);
            bool strengthValid = decimal.TryParse(tb_strengt.Text, out decimal strength);

            if (quantityValid && strengthValid)
            {
                vareService.CreateVare(rtb_productdescription.Text, quantity, tb_productName.Text, strength, tb_supplierCVR.Text);
                MessageBox.Show(tb_productName.Text + " blev tilføjet til databasen!", "SUCCESS!", MessageBoxButtons.OK);
                GetAllVare();
            }
            else
            {
                MessageBox.Show("Et eller flere inputs er ugyldige!", "OOPS!", MessageBoxButtons.OK);
            }
        }

        private void btn_updateProduct_Click(object sender, EventArgs e)
        {
            Services.VareService vareService = new VareService();
            bool quantityValid = int.TryParse(tb_quantity.Text, out int quantity);
            bool strengthValid = decimal.TryParse(tb_strengt.Text, out decimal strength);

            if (quantityValid && strengthValid)
            {
                vareService.UpdateVare(tb_productID.Text, rtb_productdescription.Text, quantity, tb_productName.Text, strength, tb_supplierCVR.Text);
                MessageBox.Show(tb_productName.Text + " blev opdateret i databasen!", "SUCCESS!", MessageBoxButtons.OK);
            }
            else
            {
                MessageBox.Show("Et eller flere inputs er ugyldige!", "OOPS!", MessageBoxButtons.OK);
            }
        }

        private void btn_deleteProduct_Click(object sender, EventArgs e)
        {
            Services.VareService vareService = new VareService();
            if (tb_productID != null)
            {
                DialogResult dialogResult = MessageBox.Show("Er du sikker på at du vil slette " + tb_productName.Text + "?",
                    "ADVARSEL!", MessageBoxButtons.YesNoCancel);
                if (dialogResult == DialogResult.Yes)
                {
                    vareService.DeleteVare(tb_productID.Text);
                    MessageBox.Show(tb_productName.Text + " blev slettet fra databasen!", "SUCCESS!", MessageBoxButtons.OK);

                    tb_productID.Text = null;
                    tb_productName.Text = null;
                    tb_strengt.Text = null;
                    tb_supplierCVR.Text = null;
                    tb_quantity.Text = null;
                    rtb_productdescription.Text = null;
                    GetAllVare();
                }
                else
                {
                    MessageBox.Show("Sletning afbrudt!", "SUCCESS!", MessageBoxButtons.OK);
                }
            }
        }

        private void GetAllAnsatte()
        {

        }

        private void GetAllKunder()
        {

        }

        private void GetAllVare()
        {
            listView1_listOfSuppliers.Items.Clear();
            Services.VareService vareservice = new VareService();
            var vareList = vareservice.GetAllVare();
            foreach ( var vare in vareList )
            {
                ListViewItem vareItem = new ListViewItem(vare.VareNummer.ToString());
                vareItem.SubItems.Add(vare.VareNavn);
                vareItem.SubItems.Add(vare.LagerMængde.ToString());
                listView1_listOfSuppliers.Items.Add(vareItem);
            }
        }

        private void GetAllBookings()
        {

        }

        private void GetAllLeverandører()
        {

        }

    }
}
