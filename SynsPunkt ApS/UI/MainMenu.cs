using SynsPunkt_ApS.Models;
using SynsPunkt_ApS.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
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
            GetAllAnsatte();
            GetAllBookings();
            GetAllKunder();
            GetAllLeverandører();
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
            DialogResult dialogResult = MessageBox.Show("Er du sikker på at du vil lukke programmet?", "ADVARSEL!", MessageBoxButtons.YesNoCancel);
            if (dialogResult == DialogResult.Yes)
            {
                Application.Exit();
            }
            else
            {
                MessageBox.Show("Log ud forsøg afbrudt!");
            }
        }

        private void btn_ChangePassWord_Click(object sender, EventArgs e)
        {
            Services.ChangePassword changePass = new Services.ChangePassword();
            bool changedPass = changePass.ChangeUserPassword(LoggedInEmployee.EmployeeID.ToString(), tb_OldPassword.Text, tb_NewPassword1.Text, tb_newPassword2.Text);
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

            try
            {
                Services.Kunde_Services kundeServices = new Kunde_Services();
                // Kald metoder på kundeServices eller udfør handlinger på kunder her
                MessageBox.Show("Kunden blev opdateret");
            }
            catch (Exception ex)
            {
                // Håndter undtagelsen her, f.eks. vis en fejlmeddelelse
                MessageBox.Show("Der opstod en fejl under opdatering af kunden: " + ex.Message);
            }


        }

        private void btn_updateCustomer_Click(object sender, EventArgs e)
        {
            try
            {
                Services.Kunde_Services kundeServices = new Kunde_Services();
                // Kald metoder på kundeServices eller udfør handlinger på kunder her
                MessageBox.Show("Kunden blev slettet");
            }
            catch (Exception ex)
            {
                // Håndter undtagelsen her, f.eks. vis en fejlmeddelelse
                MessageBox.Show("Der opstod en fejl under sletning af kunden: " + ex.Message);
            }

        }

        private void btn_deleteCustomer_Click(object sender, EventArgs e)
        {
            try
            {
                Services.Kunde_Services kundeServices = new Kunde_Services();
                // Kald metoder på kundeServices eller udfør handlinger på kunder her
                MessageBox.Show("Kunden blev slettet");
            }
            catch (Exception ex)
            {
                // Håndter undtagelsen her, f.eks. vis en fejlmeddelelse
                MessageBox.Show("Der opstod en fejl under sletning af kunden: " + ex.Message);
            }

        }

        private void listView_Bookings_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker_Bookings_ValueChanged(object sender, EventArgs e)
        {

            /*DateTime selectedDate = dateTimePicker_Bookings.Value.Date;

            // Få fat i bookingerne for den valgte dato (antaget at du har en metode til at hente bookinger baseret på dato)
            List<Booking> bookinger = GetBookingsPerDate(selectedDate);

            // Rens listen først
            listView_Bookings.Items.Clear();

            // Tilføj bookingerne til listview'en
            foreach (Booking booking in bookinger)
            {
                ListViewItem item = new ListViewItem(booking.BookingId.ToString());
                item.SubItems.Add(booking.LokationId.ToString());
                item.SubItems.Add(booking.Tidspunkt.ToString());
                item.SubItems.Add(booking.BookingType);
                item.SubItems.Add(booking.KundeId.ToString());

                listView_Bookings.Items.Add(item);
            }*/
        }

        private void btn_clearDate_Click(object sender, EventArgs e)
        {

        }

        private void btn_createBooking_Click(object sender, EventArgs e)
        {
            try
            {
                // Hent bookingværdier fra UI
                int lokationId = int.Parse(tb_locationID.Text);
                DateTime tidspunkt = DateTime.ParseExact(tb_bookingTime.Text, "HH:mm", CultureInfo.InvariantCulture);
                DateTime dato = DateTime.Parse(dateTimePicker_bookingInterval.Text);
                string bookingType = tb_bookingDescription.Text;
                int kundeId = int.Parse(tb_customerBooking.Text);

                // Opret en ny booking med de indtastede værdier
                Booking newBooking = new Booking(lokationId, tidspunkt, dato, bookingType, kundeId);


                BookingService bookingService = new BookingService();

                // Kald CreateBooking-metoden i BookingService for at oprette bookingen
                bookingService.CreateBooking(newBooking);


                MessageBox.Show("Booking oprettet!");
            }
            catch (FormatException)
            {
                MessageBox.Show("Forkert indtastningsformat! Kontroller de indtastede værdier og prøv igen.", "Fejl", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Der opstod en fejl under oprettelsen af bookingen: " + ex.Message, "Fejl", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
            if (listView_employees.SelectedItems.Count > 0)
            {
                Services.Ansat_Services ansatService = new Ansat_Services();


                string selectedEmployeeID = listView_employees.SelectedItems[0].SubItems[0].Text;

                Models.Ansat selectedEmployee = ansatService.GetAnsatByID(selectedEmployeeID);

                //Textbox tekst opdateres:
                tb_employeeId.Text = selectedEmployee.EmployeeID.ToString();
                tb_employeeFirstName.Text = selectedEmployee.FirstName;
                tb_employeeLastName.Text = selectedEmployee.LastName;
                tb_employeePhoneNo.Text = selectedEmployee.TelephoneNumber.ToString();
                tb_employeeEmail.Text = selectedEmployee.PrivateMail;
                tb_employeeAdress.Text = selectedEmployee.Adress;
                tb_employeeZip.Text = selectedEmployee.ZipCode.ToString();
                tb_employeeBU.Text = ansatService.GetDepartmentName(Convert.ToInt32(selectedEmployee.DepartmentID));
                tb_employeeRole.Text = ansatService.GetRoleName(Convert.ToInt32(selectedEmployee.RoleID));
                tb_employeeWorkMail.Text = selectedEmployee.WorkMail;
                tb_employeePassword.Text = selectedEmployee.Password;

            }
        }

        private void tb_searchEmployee_TextChanged(object sender, EventArgs e)
        {
            listView_employees.Items.Clear();
            Services.Ansat_Services ansatService = new Services.Ansat_Services();
            if (tb_searchEmployee.Text == string.Empty)
            {
                GetAllAnsatte();
            }
            else
            {
                List<Models.Ansat> allAnsat = ansatService.SearchAnsatByName(tb_searchEmployee.Text);
                foreach (var ansat in allAnsat)
                {
                    ListViewItem ansatItem = new ListViewItem(ansat.EmployeeID.ToString());
                    ansatItem.SubItems.Add(ansat.FirstName);
                    ansatItem.SubItems.Add(ansat.LastName);
                    listView_employees.Items.Add(ansatItem);
                }
            }
        }


        /// <summary>
        /// Martin: Opretter en ny ansat i systemet. Tjekker for om alle nødvendige 
        /// textbox ikke er tomme. Ved successfuld oprettelse gøres alle textboxe tomme.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_CreateEmployee_Click(object sender, EventArgs e)
        {
            Services.Ansat_Services ansatService = new Services.Ansat_Services();

            foreach (System.Windows.Forms.TextBox textBox in tabPage_Medarbejder.Controls.OfType<System.Windows.Forms.TextBox>())
            {
                if (textBox != tb_searchEmployee && textBox != tb_employeeId && textBox.Text == string.Empty)
                {
                    MessageBox.Show("Udfyld alle oplysninger for at oprette en ny kunde", "Hvad fanden laver du?????", MessageBoxButtons.OK);

                    return;
                }
            }
            string firstName = tb_employeeFirstName.Text;
            string lastName = tb_employeeLastName.Text;
            int phoneNumber = Convert.ToInt32(tb_employeePhoneNo.Text);
            string privateMail = tb_employeeEmail.Text;
            string adress = tb_employeeAdress.Text;
            int zipCode = Convert.ToInt32(tb_employeeZip.Text);
            string department = tb_employeeBU.Text;
            string role = tb_employeeRole.Text;
            string workMail = tb_employeeWorkMail.Text;
            string password = tb_employeePassword.Text;


            ansatService.CreateAnsat(firstName, lastName, phoneNumber, privateMail, adress, password, department, role, workMail, zipCode);

            MessageBox.Show(firstName + " " + lastName + " arbejder nu hos " + department, "Ansat oprettet", MessageBoxButtons.OK);


            foreach (System.Windows.Forms.TextBox textBox in tabPage_Medarbejder.Controls.OfType<System.Windows.Forms.TextBox>())
            {
                if (textBox != tb_searchEmployee && textBox != tb_employeeId && textBox.Text == string.Empty)
                {
                    textBox.Text = string.Empty; ;
                }
            }

            if (tb_searchEmployee.Text != string.Empty)
            {
                GetAllAnsatte();
            }
            else
            {
                tb_searchEmployee_TextChanged(tb_searchEmployee, new EventArgs());
            }
        }

        private void btn_UpdateEmployee_Click(object sender, EventArgs e)
        {


            Services.Ansat_Services ansatServices = new Services.Ansat_Services();

            int employeeID = Convert.ToInt32(tb_employeeId.Text);
            string firstName = tb_employeeFirstName.Text;
            string lastName = tb_employeeLastName.Text;
            int phoneNumber = Convert.ToInt32(tb_employeePhoneNo.Text);
            string privateMail = tb_employeeEmail.Text;
            string adress = tb_employeeAdress.Text;
            int zipCode = Convert.ToInt32(tb_employeeZip.Text);
            string department = tb_employeeBU.Text;
            string role = tb_employeeRole.Text;
            string workMail = tb_employeeWorkMail.Text;
            string password = tb_employeePassword.Text;

            ansatServices.UpdateAnsat(employeeID, firstName, lastName, phoneNumber, privateMail,
                adress, password, department, role, workMail, zipCode);

            //Opdaterer oplysningerne på listview
            if (tb_searchEmployee.Text == string.Empty)
            {
                GetAllAnsatte();
            }
            else
            {
                tb_searchEmployee_TextChanged(tb_searchEmployee, new EventArgs());
            }

            //bekræftelse på opdatering
            MessageBox.Show("Ansat Opdateret!", "Success", MessageBoxButtons.OK);

        }

        private void btn_deleteEmployee_Click(object sender, EventArgs e)
        {
            Services.Ansat_Services ansatServices = new Services.Ansat_Services();
            if (string.IsNullOrEmpty(tb_employeeId.Text))
            {
                MessageBox.Show("Vælg en ansat som du vil sende til The Shadow Realm", "Du fyret", MessageBoxButtons.OK);
                return;
            }

            DialogResult result = MessageBox.Show("Er du sikker på du vil gøre denne person arbejdsløs?", "XD", MessageBoxButtons.YesNoCancel);

            if (result == DialogResult.Yes)
            {
                string fullName = tb_employeeFirstName.Text + " " + tb_employeeLastName.Text;

                MessageBox.Show(fullName + " blev sendt til The Shadow Realm", "Ses fætter", MessageBoxButtons.OK);

                ansatServices.DeleteAnsat(Convert.ToInt32(tb_employeeId.Text));

                if (tb_searchEmployee.Text != string.Empty)
                {
                    GetAllAnsatte();
                }
                else
                {
                    tb_searchEmployee_TextChanged(tb_searchEmployee, new EventArgs());
                }
                foreach (System.Windows.Forms.TextBox textBox in tabPage_Medarbejder.Controls.OfType<System.Windows.Forms.TextBox>())
                {
                    if (textBox != tb_searchEmployee && textBox.Text != string.Empty)
                    {
                        textBox.Text = string.Empty;
                    }
                }

            }

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
            foreach (var vare in vareList)
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

        private List<string> originalLeverandør = new List<string>();
        private List<string> originalVare = new List<string>();

        /// <summary>
        /// Theis:
        /// Tilføjer alle originale vare til seperat klasse, der bruges til at nulstille søgefunktionen.
        /// </summary>
        private void SetOriginalVare()
        {

        }

        /// <summary>
        /// Theis:
        /// Tilføjer alle originale leverandører til seperat klasse, der bruges til at nulstille søgefunktionen.
        /// </summary>
        private void SetOriginalLeverandør()
        {

        }

    }
}
