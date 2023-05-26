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
        public List<Models.VareLinje> currentLineItems = new List<VareLinje>();
        public List<Models.Vare> allProducts;
        Services.VareService vareservice = new VareService();

        public MainMenu(Models.Ansat loggedInEmployee)
        {
            allProducts = vareservice.GetAllVare();
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
            GetAllVareInBasketTab();
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
            if (listView_product_list_buytab.SelectedItems.Count <= 0)
            {
                MessageBox.Show("Vælg en vare fra varelisten du vil tilføje til din kurv", "Hovsa", MessageBoxButtons.OK);
                return;
            }
            int vareID = Convert.ToInt32(listView_product_list_buytab.SelectedItems[0].SubItems[0].Text);
            Models.Vare chosenProduct = allProducts.FirstOrDefault(v => v.VareNummer == vareID);
            Models.VareLinje lineItemForChosenProduct = currentLineItems.FirstOrDefault(x => x.Vare == chosenProduct);

            //Tjekker om der er mindst 1 af varen på lager
            if (Convert.ToInt32(listView_product_list_buytab.SelectedItems[0].SubItems[4].Text) <= 0)
            {
                MessageBox.Show("Hovsa, der er ikke flere tilbage af denne vare på lageret. Surt show", "Beklager", MessageBoxButtons.OK);
                return;
            }

            if (currentLineItems.Any(x => x.Vare == chosenProduct))
            {
                //Tjekker for at man ikke kan købe flere af en vare end hvad der er på lager
                if (lineItemForChosenProduct.Mængde == chosenProduct.LagerMængde)
                {
                    MessageBox.Show("Vil du købe flere af denne vare end vi har på lager din bøllebob?", "-3 i matematik", MessageBoxButtons.OK);
                    return;
                }
                lineItemForChosenProduct.Mængde += 1;
                UpdateBasketListView();
                return;
            }
            //Hvis alt er ok, tilføjes en ny varelinje til kurven
            Models.VareLinje varelinje = new VareLinje(chosenProduct, 1, chosenProduct.Pris);
            currentLineItems.Add(varelinje);
            UpdateBasketListView();

        }

        private void UpdateBasketListView()
        {
            listView_basket_list.Items.Clear();
            foreach (var lineItem in currentLineItems)
            {
                ListViewItem lineItemItem = new ListViewItem(lineItem.Vare.VareNummer.ToString());
                lineItemItem.SubItems.Add(lineItem.Vare.VareNavn);
                lineItemItem.SubItems.Add(lineItem.totalPris.ToString());
                lineItemItem.SubItems.Add(lineItem.Mængde.ToString());
                listView_basket_list.Items.Add(lineItemItem);
            }

        }

        private void btn_RemoveFromBasket_Click(object sender, EventArgs e)
        {
            if (listView_basket_list.SelectedItems.Count <= 0)
            {
                MessageBox.Show("Vælg en vare fra kurven du vil fjerne", "Hovsa", MessageBoxButtons.OK);
                return;
            }
            int vareID = Convert.ToInt32(listView_basket_list.SelectedItems[0].SubItems[0].Text);
            Models.Vare chosenProduct = allProducts.FirstOrDefault(v => v.VareNummer == vareID);
            Models.VareLinje lineItemForChosenProduct = currentLineItems.FirstOrDefault(x => x.Vare == chosenProduct);

            currentLineItems.Remove(lineItemForChosenProduct);
            UpdateBasketListView();

        }

        private void btn_SendInvoiceMail_Click(object sender, EventArgs e)
        {
            int orderID = 0;
            Services.Kunde_Services kundeService = new Kunde_Services();
            Services.Varelinje_service varelinjeService = new Varelinje_service();
            Services.Ordre_service ordreService = new Ordre_service();
            Services.VareService vareService = new VareService();
            string MessageBoxText = string.Empty;
            if (currentLineItems.Count() == 0)
            {
                MessageBoxText += "Tilføj vare til kurven.";
            }

            if (tb_customerToBuy.Text == string.Empty)
            {
                MessageBoxText += Environment.NewLine + "Indtast kundeID.";
            }
            else if (!kundeService.CheckIfCustomerExists(Convert.ToInt32(tb_customerToBuy.Text)))
            {
                MessageBoxText += Environment.NewLine + "Ugyldigt kundeID.";
            }

            if (MessageBoxText != string.Empty)
            {
                MessageBox.Show(MessageBoxText);
            }
            else
            {
                int kundeID = Convert.ToInt32(tb_customerToBuy.Text);
                DateTime orderDate = DateTime.Now.Date;
                double totalPrice = 0;
                foreach (var lineItem in currentLineItems)
                {
                    totalPrice += Convert.ToDouble(lineItem.totalPris);
                }
                orderID = ordreService.CreateOrder(kundeID, orderDate, totalPrice);

                foreach (var lineItem in currentLineItems)
                {
                    varelinjeService.CreateVarelinje(lineItem.Vare.VareNummer, orderID, lineItem.Mængde);
                    int itemsLeft = lineItem.Vare.LagerMængde - lineItem.Mængde;
                    vareService.UpdateVare(lineItem.Vare.VareNummer.ToString(), lineItem.Vare.VareBeskrivelse,
                                            itemsLeft, lineItem.Vare.VareNavn, lineItem.Vare.Styrke, lineItem.Vare.LevCVR, lineItem.Vare.Pris);
                }
                GetAllVareInBasketTab();
                UpdateBasketListView();
                MessageBox.Show("Salg færdig");
                currentLineItems.Clear();
            }

        }

        private void btn_PrintInvoice_Click(object sender, EventArgs e)
        {

        }

        private void listView_basket_list_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void tb_SearchProduct_TextChanged(object sender, EventArgs e)
        {
            listView_product_list_buytab.Items.Clear();
            Services.VareService vareService = new Services.VareService();
            if (tb_SearchProduct.Text == string.Empty)
            {
                GetAllVareInBasketTab();
            }
            else
            {
                List<Models.Vare> allVare = vareService.SearchVareByName(tb_SearchProduct.Text);
                foreach (var vare in allVare)
                {
                    ListViewItem vareItem = new ListViewItem(vare.VareNummer.ToString());
                    vareItem.SubItems.Add(vare.VareNavn);
                    vareItem.SubItems.Add(vare.Styrke.ToString());
                    vareItem.SubItems.Add(vare.Pris.ToString());
                    vareItem.SubItems.Add(vare.LagerMængde.ToString());
                    listView_product_list_buytab.Items.Add(vareItem);
                }

            }
        }

        private void listView_customers_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Tjek om en kunde er valgt i ListView
            if (listView_customers.SelectedItems.Count > 0)
            {

                Services.Kunde_Services kundeServices = new Kunde_Services();

                string selectedKundeID = listView_customers.SelectedItems[0].SubItems[0].Text;

                Models.Kunde selectedKunde = kundeServices.GetKunde(Convert.ToInt32(selectedKundeID));

                ListViewItem selectedItem = listView_customers.SelectedItems[0];

                // Opdater tekstbokse med de valgte kundens oplysninger
                tb_customerID.Text = selectedKunde.KundeId;
                tb_CustomerFirstName.Text = selectedKunde.Fornavn;
                tb_customerLastName.Text = selectedKunde.Efternavn;
                tb_customerPhoneNumber.Text = selectedKunde.TelefonNummer.ToString();
                tb_customerEmail.Text = selectedKunde.Mail;
                tb_customerAdress.Text = selectedKunde.Adresse;
                tb_customerPostNr.Text = selectedKunde.PostNr.ToString();
            }
        }

        private void tb_searchPhoneNumber_TextChanged(object sender, EventArgs e)
        {

        }


        private void btn_createCustomer_Click(object sender, EventArgs e)
        {
            Services.Kunde_Services kundeService = new Services.Kunde_Services();


            // Opret en ny kunde med dataene fra tekstboksene
            string forNavn = tb_CustomerFirstName.Text;
            string efterNavn = tb_customerLastName.Text;
            int telefonNummer;
            string Mail = tb_customerEmail.Text;
            string adresse = tb_customerAdress.Text;
            int postNr;
            string lokationId = tb_CustomerLokation.Text;


            if (!int.TryParse(tb_customerPhoneNumber.Text, out telefonNummer))
            {
                MessageBox.Show("Invalid telefonnummer value. Please enter a valid integer.");
                return;
            }

            if (!int.TryParse(tb_customerPostNr.Text, out postNr))
            {
                MessageBox.Show("Invalid postnr value. Please enter a valid integer.");
                return;
            }

            kundeService.CreateKunde(lokationId, Mail, forNavn,efterNavn,telefonNummer, adresse, postNr);

        }

        private void ClearTextBoxes()
        {
            tb_CustomerFirstName.Text = "";
            tb_customerLastName.Text = "";
            tb_customerPhoneNumber.Text = "";
            tb_customerEmail.Text = "";
            tb_customerAdress.Text = "";
            tb_customerID.Text = "";
            tb_customerPostNr.Text = "";
        }

        private void btn_updateCustomer_Click(object sender, EventArgs e)
        {
            Services.Kunde_Services kundeServices = new Services.Kunde_Services();

            string forNavn = tb_CustomerFirstName.Text;
            string efterNavn = tb_customerLastName.Text;
            string telefonNummerText = tb_customerPhoneNumber.Text;
            string Mail = tb_customerEmail.Text;
            string adresse = tb_customerAdress.Text;
            string postNrText = tb_customerPostNr.Text;
            string lokationId = tb_CustomerLokation.Text;

            int telefonNummer; // Declare telefonNummer as an int variable
            if (!int.TryParse(telefonNummerText, out telefonNummer))
            {
                MessageBox.Show("Ugyldig telefonnummer værdi. Indtast venligst et gyldigt heltal.");
                return;
            }

            int postNr; // Declare postNr as an int variable
            if (!int.TryParse(postNrText, out postNr))
            {
                MessageBox.Show("Ugyldig postnr værdi. Indtast venligst et gyldigt heltal.");
                return;
            }

            kundeServices.UpdateKunde(lokationId, Mail, forNavn, efterNavn, telefonNummer, adresse, postNr);

            MessageBox.Show("Kunden blev opdateret");
        }


        private void btn_deleteCustomer_Click(object sender, EventArgs e)
        {
            Services.Kunde_Services kunde_Services = new Services.Kunde_Services();
            if (string.IsNullOrEmpty(tb_customerID.Text))
            {
                MessageBox.Show("Vil du slette denne kunde", "Kunde slettet", MessageBoxButtons.OK);
                return;
            }

            DialogResult result = MessageBox.Show("Er du sikker du vil slette kunden", "Ja", MessageBoxButtons.YesNoCancel);

            if (result == DialogResult.Yes)
            {
                string fullname = tb_customerID.Text + " " + tb_customerLastName.Text;

                MessageBox.Show(fullname + "blev slettet", "Kunden blev slettet", MessageBoxButtons.OK);

                kunde_Services.DeleteKunde(int.Parse(tb_customerID.Text));

                if (tb_customerID.Text != string.Empty)
                {
                    GetAllKunder();
                }
                else
                {
                    tb_searchPhoneNumber_TextChanged(tb_searchPhoneNumber, new EventArgs());

                }
                foreach (System.Windows.Forms.TextBox textBox in tabPage_kundeoversigt.Controls.OfType<System.Windows.Forms.TextBox>())
                {
                    if (textBox != tb_searchPhoneNumber && textBox.Text != string.Empty)
                    {
                        textBox.Text = string.Empty;
                    }
                }
            }
        }


        private void UpdateCustomerListView()
        {
            Kunde_Services kundeServices = new Kunde_Services();


            // Hent opdaterede kundedata fra databasen eller en anden datakilde
            List<Kunde> customers = kundeServices.GetCustomers();

            // Tilføj kunder som elementer i ListView
            foreach (Kunde customer in customers)
            {
                ListViewItem item = new ListViewItem(customer.KundeId.ToString());
                item.SubItems.Add(customer.Fornavn);
                item.SubItems.Add(customer.Efternavn);

                item.SubItems.Add(customer.TelefonNummer.ToString());

                item.SubItems.Add(customer.Mail);
                item.SubItems.Add(customer.Adresse);

                ListViewItem.ListViewSubItem postNrSubItem = new ListViewItem.ListViewSubItem(item, customer.PostNr.ToString());
                item.SubItems.Add(postNrSubItem);

                listView_customers.Items.Add(item);


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
            bool lokationIDValid = int.TryParse(tb_locationID.Text, out int lokationID); ;
            //DateTime tidspunkt = DateTime.ParseExact(cb_timePicker.Text, "HH:mm:ss", CultureInfo.InvariantCulture);
            string tidspunkt = cb_timePicker.Text;
            tidspunkt = tidspunkt += ":00";
            DateTime dato = dateTimePicker_bookingInterval.Value;
            string bookingType = tb_bookingDescription.Text;
            bool kundeIDValid = int.TryParse(tb_customerBooking.Text, out int kundeID);

            if (lokationIDValid && kundeIDValid)
            {
                BookingService bookingServices = new BookingService();

                Booking newBooking = new Booking(lokationID, dato, tidspunkt, bookingType, kundeID);

                bookingServices.CreateBooking(newBooking);

                MessageBox.Show($"Booking oprettet!");
            }
            else
            {
                MessageBox.Show("Et eller flere inputs er ugyldige!");
            }
        }

        private void btn_updateBooking_Click(object sender, EventArgs e)
        {
            Services.BookingService bookingService = new BookingService();
            bool lokationIDValid = int.TryParse(tb_locationID.Text, out int lokationID);
            string tidspunkt = cb_timePicker.Text;
            tidspunkt = tidspunkt += ":00";
            DateTime dato = dateTimePicker_bookingInterval.Value;
            string bookingType = tb_bookingDescription.Text;
            bool kundeIDValid = int.TryParse(tb_customerBooking.Text, out int kundeID);

            if (lokationIDValid && kundeIDValid)
            {
                BookingService bookingServicevar = new BookingService();
                Booking updatedBooking = new Booking(lokationID, dato, tidspunkt, bookingType, kundeID);
                bookingServicevar.UpdateBooking(updatedBooking);
                MessageBox.Show($"Booking ændret!");
            }
            else
            {
                MessageBox.Show("Et eller flere inputs er ugyldige!");
            }

        }

        private void btn_deleteBooking_Click(object sender, EventArgs e)
        {

            Services.BookingService bookingService = new BookingService();
            int bookingId = Convert.ToInt32(tb_bookingID.Text);

            DialogResult dialogResult = MessageBox.Show("Er du sikker på, at du vil slette bookingen med ID: " + bookingId + "?", "ADVARSEL!", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                bookingService.DeleteBooking(bookingId);
                MessageBox.Show("Bookingen med ID: " + bookingId + " blev slettet fra databasen!", "SUCCESS!", MessageBoxButtons.OK);

                tb_bookingID.Text = string.Empty;
                tb_locationID.Text = string.Empty;
                cb_timePicker.Text = string.Empty;
                dateTimePicker_bookingInterval.Value = DateTime.Now;
                tb_bookingDescription.Text = string.Empty;
                tb_customerBooking.Text = string.Empty;
            }
            else
            {
                MessageBox.Show("Sletning afbrudt!", "SUCCESS!", MessageBoxButtons.OK);
            }

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
                                     out string styrke, out string levCVR, out string pris);

                tb_productID.Text = id2;
                rtb_productdescription.Text = vareBeskrivelse;
                tb_quantity.Text = lagerMængde;
                tb_productName.Text = vareNavn;
                tb_strengt.Text = styrke;
                tb_supplierCVR.Text = levCVR;
                tb_productPrice.Text = pris;
            }
        }

        private void tb_searchForProduct_TextChanged(object sender, EventArgs e)
        {
            listView1_listOfSuppliers.Items.Clear();
            Services.VareService vareService = new Services.VareService();
            if (tb_searchForProduct.Text == string.Empty)
            {
                GetAllVare();
            }
            else
            {
                List<Models.Vare> allVare = vareService.SearchVareByName(tb_searchForProduct.Text);
                foreach (var vare in allVare)
                {
                    ListViewItem vareItem = new ListViewItem(vare.VareNummer.ToString());
                    vareItem.SubItems.Add(vare.VareNavn);
                    vareItem.SubItems.Add(vare.LagerMængde.ToString());
                    listView1_listOfSuppliers.Items.Add(vareItem);
                }
            }
        }

        private void btn_createProduct_Click(object sender, EventArgs e)
        {
            Services.VareService vareService = new VareService();
            bool quantityValid = int.TryParse(tb_quantity.Text, out int quantity);
            bool strengthValid = decimal.TryParse(tb_strengt.Text, out decimal strength);
            bool priceValid = decimal.TryParse(tb_productPrice.Text, out decimal price);

            if (quantityValid && strengthValid && priceValid)
            {
                vareService.CreateVare(rtb_productdescription.Text, quantity, tb_productName.Text, strength, tb_supplierCVR.Text, price);
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
            bool priceValid = decimal.TryParse(tb_productPrice.Text, out decimal price);

            if (quantityValid && strengthValid && priceValid)
            {
                vareService.UpdateVare(tb_productID.Text, rtb_productdescription.Text, quantity, tb_productName.Text, strength, tb_supplierCVR.Text, price);
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
            Services.Ansat_Services ansatService = new Services.Ansat_Services();
            List<Models.Ansat> allAnsat = ansatService.GetAllAnsat();
            foreach (var ansat in allAnsat)
            {
                ListViewItem ansatItem = new ListViewItem(ansat.EmployeeID.ToString());
                ansatItem.SubItems.Add(ansat.FirstName);
                ansatItem.SubItems.Add(ansat.LastName);
                listView_employees.Items.Add(ansatItem);
            }
        }

        private void GetAllKunder()
        {
            listView_customers.Items.Clear();
            Services.Kunde_Services kunde_Services = new Services.Kunde_Services();
            var kundeList = kunde_Services.GetCustomers();
            foreach(var kunde in kundeList)
            {
                ListViewItem kundeItem = new ListViewItem(kunde.KundeId.ToString());
                kundeItem.SubItems.Add(kunde.Fornavn);
                kundeItem.SubItems.Add(kunde.Efternavn);
                listView_customers.Items.Add(kundeItem);

            }

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

        private void GetAllVareInBasketTab()
        {

            listView_product_list_buytab.Items.Clear();
            Services.VareService vareservice = new VareService();
            var vareList = vareservice.GetAllVare();
            foreach (var vare in vareList)
            {
                ListViewItem vareItem = new ListViewItem(vare.VareNummer.ToString());
                vareItem.SubItems.Add(vare.VareNavn);
                vareItem.SubItems.Add(vare.Styrke.ToString());
                vareItem.SubItems.Add(vare.Pris.ToString());
                vareItem.SubItems.Add(vare.LagerMængde.ToString());
                listView_product_list_buytab.Items.Add(vareItem);
            }

        }

        private void btn_createSupplier_Click(object sender, EventArgs e)
        {

        }

        private void btn_updateSupplier_Click(object sender, EventArgs e)
        {

        }

        private void btn_deleteSupplier_Click(object sender, EventArgs e)
        {

        }
    }
}
