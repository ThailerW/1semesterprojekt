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
        public List<Models.Ordre> allOrders;
        public List<Models.Ordre> ordersWithinDateInterval;
        Services.VareService vareservice = new VareService();
        Services.Ordre_service ordreService = new Ordre_service();

        public MainMenu(Models.Ansat loggedInEmployee)
        {
            allOrders = ordreService.GetAllOrders();
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
            GetAllOrdersWithinDateInterval();

            if (LoggedInEmployee.RoleID == 7)
            {
                btn_Leverandoeroversigt.Enabled = false;
                btn_crudvare.Enabled = false;
                btn_Medarbejder.Enabled = false;
                btn_Rapport.Enabled = false;
            }
            else if (LoggedInEmployee.RoleID == 8)
            {
                btn_Medarbejder.Enabled = false;
                btn_Leverandoeroversigt.Enabled = false;
            }

            label_UserName.Text = LoggedInEmployee.FirstName + " " + LoggedInEmployee.LastName;
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
            //Tjekker om en vare er valgt
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
                lineItemItem.SubItems.Add(lineItem.totalPris.ToString("N0"));
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

            if (!string.IsNullOrEmpty(MessageBoxText))
            {
                MessageBox.Show(MessageBoxText);
            }
            else
            {
                int kundeID = Convert.ToInt32(tb_customerToBuy.Text);
                DateTime orderDate = DateTime.Now;
                double totalPrice = 0;
                foreach (var lineItem in currentLineItems)
                {
                    totalPrice += Convert.ToDouble(lineItem.totalPris);
                }
                int orderID = ordreService.CreateOrder(kundeID, orderDate, totalPrice);

                foreach (var lineItem in currentLineItems)
                {
                    varelinjeService.CreateVarelinje(lineItem.Vare.VareNummer, orderID, lineItem.Mængde);
                    lineItem.Vare.LagerMængde -= lineItem.Mængde;
                    vareService.UpdateVare(lineItem.Vare.VareNummer.ToString(), lineItem.Vare.VareBeskrivelse,
                                            lineItem.Vare.LagerMængde, lineItem.Vare.VareNavn, lineItem.Vare.Styrke, lineItem.Vare.LevCVR, lineItem.Vare.Pris);
                }
                GetAllVareInBasketTab();
                MessageBoxText += "Salg udført! " + Environment.NewLine + "OrderID: " + orderID + Environment.NewLine + "KundeID: " + kundeID;
                MessageBox.Show(MessageBoxText);
                currentLineItems.Clear();
                UpdateBasketListView();
                allOrders = ordreService.GetAllOrders();
                GetAllOrdersWithinDateInterval();
                tb_customerToBuy.Text = string.Empty;
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
            listView_customers.SelectedItems.Clear();
            Services.Kunde_Services kundeServices = new Services.Kunde_Services();
            listView_customers.Items.Clear(); // Fjerner eksisterende elementer i listView_customers

            if (tb_searchPhoneNumber.Text == string.Empty)
            {
                GetAllKunder();
            }
            else
            {
                if (int.TryParse(tb_searchPhoneNumber.Text, out int kundeID))
                {
                    Models.Kunde kunde = kundeServices.GetKunde(kundeID);
                    if (kunde != null)
                    {
                        ListViewItem listViewItem = new ListViewItem(kunde.TelefonNummer.ToString());
                        listViewItem.SubItems.Add(kunde.KundeId.ToString());
                        listView_customers.Items.Add(listViewItem);
                    }
                }
            }
        }


        private void btn_createCustomer_Click(object sender, EventArgs e)
        {
            Services.Kunde_Services kundeService = new Services.Kunde_Services();

            // Opret en ny kunde med dataene fra tekstboksene
            string lokationId = tb_CustomerLokation.Text;
            string Mail = tb_customerEmail.Text;
            string forNavn = tb_CustomerFirstName.Text;
            string efterNavn = tb_customerLastName.Text;
            int telefonNummer;
            string adresse = tb_customerAdress.Text;
            int postNr;

            if (!int.TryParse(tb_customerPhoneNumber.Text, out telefonNummer))
            {
                MessageBox.Show("Ugyldig telefonnummer-værdi. Indtast venligst et gyldigt heltal.");
                return;
            }

            if (!int.TryParse(tb_customerPostNr.Text, out postNr))
            {
                MessageBox.Show("Ugyldig postnr-værdi. Indtast venligst et gyldigt heltal.");
                return;
            }

            kundeService.CreateKunde(lokationId, Mail, forNavn, efterNavn, telefonNummer, adresse, postNr);

            MessageBox.Show("Kunde oprettet med succes!");

            foreach (System.Windows.Forms.TextBox textBox in tabPage_kundeoversigt.Controls.OfType<System.Windows.Forms.TextBox>())
            {
                if (textBox.Text != tb_customerPhoneNumber.Text && textBox.Text == string.Empty)
                {
                    textBox.Text = string.Empty;
                }
            }

            // Nulstil tekstboksene
            tb_CustomerLokation.Text = "";
            tb_customerEmail.Text = "";
            tb_CustomerFirstName.Text = "";
            tb_customerLastName.Text = "";
            tb_customerPhoneNumber.Text = "";
            tb_customerAdress.Text = "";
            tb_customerPostNr.Text = "";

            UpdateCustomerListView();

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

            int KundeID = Convert.ToInt32(tb_customerID.Text);
            string forNavn = tb_CustomerFirstName.Text;
            string efterNavn = tb_customerLastName.Text;
            int telefonNummer = Convert.ToInt32(tb_customerPhoneNumber.Text);
            string Mail = tb_customerEmail.Text;
            string adresse = tb_customerAdress.Text;
            int postNr = Convert.ToInt32(tb_customerPostNr.Text);
            string lokationId = lb_customerLocation.Text;

            kundeServices.UpdateKunde(lokationId, KundeID, Mail, forNavn, efterNavn, telefonNummer, adresse, postNr);

            if (tb_searchPhoneNumber.Text == string.Empty)
            {
                GetAllKunder();
            }
            else
            {
                tb_searchPhoneNumber_TextChanged(tb_searchPhoneNumber, new EventArgs());
            }

            MessageBox.Show("Kunde blev Opdateret", "Det virkede", MessageBoxButtons.OK);
        }

        private void btn_deleteCustomer_Click(object sender, EventArgs e)
        {
            Services.Kunde_Services kundeServices = new Services.Kunde_Services();

            if (string.IsNullOrEmpty(tb_customerID.Text))
            {
                MessageBox.Show("Vælg venligst en kunde at slette.", "Ingen kunde valgt", MessageBoxButtons.OK);
                return;
            }

            DialogResult result = MessageBox.Show("Er du sikker på, at du vil slette kunden?", "Bekræft sletning", MessageBoxButtons.YesNoCancel);

            if (result == DialogResult.Yes)
            {
                string fullname = tb_customerID.Text + " " + tb_customerLastName.Text;

                MessageBox.Show(fullname + " blev slettet.", "Kunden blev slettet", MessageBoxButtons.OK);

                kundeServices.DeleteKunde(int.Parse(tb_customerID.Text));

                ClearTextBoxes();

                UpdateCustomerListView();
            }
        }


        private void UpdateCustomerListView()
        {
            Kunde_Services kundeServices = new Kunde_Services();

            // Ryd alle eksisterende elementer i listView_customers
            listView_customers.Items.Clear();

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
            if (listView_Bookings.SelectedItems.Count > 0)
            {

                ListViewItem selectedItem = listView_Bookings.SelectedItems[0];
                string bookingID = selectedItem.SubItems[0].Text;

                BookingService bookingService = new BookingService();

                bookingService.ReadBooking(bookingID, out string bookingIDouted, out string lokationID, out string dato, out string tidspunkt, out string bookingType, out string kundeId);

                // Opdater felterne med bookingdata
                tb_bookingID.Text = bookingIDouted;
                dateTimePicker_bookingInterval.Value = DateTime.Parse(dato);
                cb_timePicker.Text = tidspunkt;
                tb_customerBooking.Text = kundeId;
                tb_bookingDescription.Text = bookingType;
                tb_locationID.Text = lokationID;
            }
        }

        private void dateTimePicker_Bookings_ValueChanged(object sender, EventArgs e)
        {

            DateTime selectedDate = dateTimePicker_Bookings.Value;
            List<Booking> filteredBookings = new List<Booking>();


            foreach (Booking booking in listView_Bookings.Items)
            {
                if (booking.Dato.Date == selectedDate.Date)
                {
                    filteredBookings.Add(booking);
                }
            }
            listView_Bookings.Items.Clear();
            foreach (Booking booking in filteredBookings)
            {

                ListViewItem listViewItem = new ListViewItem(booking.BookingID.ToString());
                listViewItem.SubItems.Add(booking.LokationID.ToString());
                listViewItem.SubItems.Add(booking.Tidspunkt);
                listViewItem.SubItems.Add(booking.Dato.ToShortDateString());
                listViewItem.SubItems.Add(booking.BookingType);
                listViewItem.SubItems.Add(booking.KundeID.ToString());
                listView_Bookings.Items.Add(listViewItem);
            }

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
                GetAllBookings();
            }
            else
            {
                MessageBox.Show("Et eller flere inputs er ugyldige!");
            }
        }

        private void btn_updateBooking_Click(object sender, EventArgs e)
        {
            Services.BookingService bookingService = new BookingService();
            bool bookingIDValid = int.TryParse(tb_bookingID.Text, out int bookingID);
            bool lokationIDValid = int.TryParse(tb_locationID.Text, out int lokationID);
            string tidspunkt = cb_timePicker.Text;
            tidspunkt = tidspunkt += ":00";
            DateTime dato = dateTimePicker_bookingInterval.Value;
            string bookingType = tb_bookingDescription.Text;
            bool kundeIDValid = int.TryParse(tb_customerBooking.Text, out int kundeID);

            if (lokationIDValid && kundeIDValid)
            {
                BookingService bookingServicevar = new BookingService();
                Booking updatedBooking = new Booking(bookingID, lokationID, dato, tidspunkt, bookingType, kundeID);
                bookingServicevar.UpdateBooking(updatedBooking);
                MessageBox.Show($"Booking ændret!");
                GetAllBookings();
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

                GetAllBookings();
            }
            else
            {
                MessageBox.Show("Sletning afbrudt!", "SUCCESS!", MessageBoxButtons.OK);
            }

        }

        private void listView_suppliers_SelectedIndexChanged(object sender, EventArgs e)
        {
            Services.LeverandørService levService = new LeverandørService();

            if (listView_suppliers.SelectedItems.Count > 0)
            {
                ListViewItem selectedItem = listView_suppliers.SelectedItems[0];
                var item = selectedItem.SubItems[0].Text;

                levService.ReadLeverandør(item, out string id2, out string navn, out string adresse, out string postNr, out string email,
                    out string info, out string tlf);

                tb_supplierIdLine.Text = id2;
                tb_supplierName.Text = navn;
                tb_supplierAdress.Text = adresse;
                tb_levpostnr.Text = postNr;
                tb_supplierPhone.Text = tlf;
                tb_supplierEmail.Text = email;
                rtb_faktuInfo.Text = info;
            }
        }

        private void tb_supplierID_TextChanged(object sender, EventArgs e)
        {
            listView_suppliers.Items.Clear();
            Services.LeverandørService levService = new LeverandørService();
            if (tb_supplierID.Text == string.Empty)
            {
                GetAllLeverandører();
            }
            else
            {
                List<Models.Leverandør> allLeverandør = levService.SearchLeverandørByName(tb_supplierID.Text);
                foreach (var lev in allLeverandør)
                {
                    ListViewItem levItem = new ListViewItem(lev.CVRnummer.ToString());
                    levItem.SubItems.Add(lev.LeverandørNavn);
                    levItem.SubItems.Add(lev.Email);
                    listView_suppliers.Items.Add(levItem);
                }
            }
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
            Services.Kunde_Services kundeService = new Services.Kunde_Services();
            DateTime startDate = dateTimePicker_reportStartTime.Value.Date;
            DateTime endDate = dateTimePicker_reportEndTime.Value.Date;

            // Får datetime uden tiden
            string startDateCorrectFormat = startDate.ToString("yyyy-MM-dd");
            string endDateCorrectFormat = endDate.ToString("yyyy-MM-dd");

            if (ordersWithinDateInterval.Count <= 0)
            {
                MessageBox.Show("Nice, så du prøver på at lave en rapport over ingen ordre??" + Environment.NewLine +
                                "Lidt ligesom du ikke lavede dine rapporter i skolen!?", "Nice business, 0 salg, sælg firma til ELON MUSK nu)", MessageBoxButtons.OK);
                return;
            }

            double totalPriceForAllOrdersInReport = 0;
            var saleReport = "SYNSPUNKT APS KØBSRAPPORT I TIDSINTERVALLET:     " + startDateCorrectFormat + "  -  " + endDateCorrectFormat + Environment.NewLine + Environment.NewLine;


            string orderIDHeader = "OrderID".PadRight(10);
            string customerIDHeader = "KundeID".PadRight(12);
            string customerNameHeader = "KundeNavn".PadRight(40);
            string orderDateHeader = "OrderDato".PadRight(25);
            string totalPriceHeader = "Total pris for ordren";

            saleReport += orderIDHeader + customerIDHeader + customerNameHeader + orderDateHeader + totalPriceHeader + Environment.NewLine;
            saleReport += "-------------------------------------------------------------------------------------------------------------" + Environment.NewLine;

            foreach (var order in ordersWithinDateInterval)
            {
                Models.Kunde customer = kundeService.GetKunde(order.customerID);

                string orderID = order.orderID.ToString().PadRight(10);
                string customerID = order.customerID.ToString().PadRight(12);
                string customerName = (customer.Fornavn + " " + customer.Efternavn).PadRight(40);
                string orderDate = order.orderDate.ToString().PadRight(25);
                string totalPrice = order.totalPrice.ToString("C2");

                saleReport += orderID + customerID + customerName + orderDate + totalPrice + Environment.NewLine;

                totalPriceForAllOrdersInReport += order.totalPrice;
            }
            saleReport += "-------------------------------------------------------------------------------------------------------------" + Environment.NewLine;
            saleReport += "Antal Ordrer: " + ordersWithinDateInterval.Count + Environment.NewLine;
            saleReport += "Samlet salgspris for alle ordrer: " + totalPriceForAllOrdersInReport.ToString("C2");
            System.IO.File.WriteAllText("Salgsrapport (" + startDateCorrectFormat + ") - (" + endDateCorrectFormat + ").txt", saleReport);

            MessageBox.Show("Rapporten blev udskrevet som tekstfil");
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

        private void btn_printproductreport_Click(object sender, EventArgs e)
        {
            StringBuilder productReport = new StringBuilder();

            string productIDHeader = "VareID".PadRight(10);
            string productNameHeader = "Varenavn".PadRight(40);
            string productStockQuantityHeader = "Lagermængde".PadRight(20);
            string productPriceHeader = "Pris".PadRight(15);
            string productSupplierIDHeader = "LeverandørCVR".PadRight(15);

            productReport.AppendLine("Komplet Vareraport for SYNSPUNKT APS   " + DateTime.Now.Date.ToString().Substring(0,10));
            productReport.AppendLine();
            productReport.AppendLine(productIDHeader + productNameHeader + productStockQuantityHeader + productPriceHeader + productSupplierIDHeader);
            productReport.Append("----------------------------------------------------------------------------------------------------");

            foreach (var product in allProducts)
            {
                productReport.AppendLine();
                productReport.Append(product.VareNummer.ToString().PadRight(10));
                productReport.Append(product.VareNavn.PadRight(40));
                productReport.Append(product.LagerMængde.ToString().PadRight(20));
                productReport.Append(product.Pris.ToString("N0").PadRight(15));
                productReport.Append(product.LevCVR.ToString().PadRight(15));
            }
            productReport.AppendLine();
            productReport.AppendLine("----------------------------------------------------------------------------------------------------");

            System.IO.File.WriteAllText("Vareraport "+ DateTime.Now.Date.ToString().Substring(0,10) +".txt", productReport.ToString());

            MessageBox.Show("Varerapporten blev udskrevet som tekstfil","Success");
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
                GetAllVareInBasketTab();
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
                GetAllVare();
                GetAllVareInBasketTab();
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
                    GetAllVareInBasketTab();
                }
                else
                {
                    MessageBox.Show("Sletning afbrudt!", "SUCCESS!", MessageBoxButtons.OK);
                }
            }
        }

        private void GetAllOrdersWithinDateInterval()
        {
            ordersWithinDateInterval = allOrders.Where
                (order => (order.orderDate.Date >= dateTimePicker_reportStartTime.Value && order.orderDate.Date <= dateTimePicker_reportEndTime.Value)
                || (order.orderDate.Date == dateTimePicker_reportStartTime.Value.Date && order.orderDate.Date == dateTimePicker_reportEndTime.Value.Date)).ToList();

            listView_report.Items.Clear();
            Services.Kunde_Services kundeService = new Services.Kunde_Services();
            Services.Ordre_service orderService = new Ordre_service();


            foreach (var order in ordersWithinDateInterval)
            {
                Models.Kunde customerWithThisOrder = kundeService.GetKunde(order.customerID);

                ListViewItem orderItem = new ListViewItem(order.orderID.ToString());
                orderItem.SubItems.Add(order.customerID.ToString());
                orderItem.SubItems.Add(customerWithThisOrder.Fornavn + " " + customerWithThisOrder.Efternavn);
                orderItem.SubItems.Add(order.orderDate.ToString());
                orderItem.SubItems.Add(order.totalPrice.ToString("N0"));
                listView_report.Items.Add(orderItem);
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
            foreach (var kunde in kundeList)
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
            listView_Bookings.Items.Clear();
            Services.BookingService bookingservice = new BookingService();
            var bookingList = bookingservice.GetAllBookings();
            foreach (var booking in bookingList)
            {
                ListViewItem bookingItem = new ListViewItem(booking.BookingID.ToString());
                bookingItem.SubItems.Add(booking.BookingType);
                bookingItem.SubItems.Add(booking.LokationID.ToString());
                listView_Bookings.Items.Add(bookingItem);
            }
        }

        private void GetAllOrders()
        {
            //listView_report.Items.Clear();
            //Services.Kunde_Services kundeService = new Services.Kunde_Services();
            //Services.Ordre_service orderService = new Ordre_service();
            //var orderList = orderService.GetAllOrders();

            //foreach (var order in orderList  )
            //{
            //    Models.Kunde customerWithThisOrder =  kundeService.GetKunde(order.customerID);

            //    ListViewItem orderItem = new ListViewItem(order.orderID.ToString());
            //    orderItem.SubItems.Add(order.customerID.ToString());
            //    orderItem.SubItems.Add(customerWithThisOrder.Fornavn + " " + customerWithThisOrder.Efternavn);
            //    orderItem.SubItems.Add(order.orderDate.ToString());
            //    orderItem.SubItems.Add(order.totalPrice.ToString());
            //    listView_report.Items.Add(orderItem);
            //}
        }

        private void GetAllLeverandører()
        {
            listView_suppliers.Items.Clear();
            Services.LeverandørService levService = new LeverandørService();
            var levList = levService.GetAllLeverandør();
            foreach (var leverandør in levList)
            {
                ListViewItem levItem = new ListViewItem(leverandør.CVRnummer.ToString());
                levItem.SubItems.Add(leverandør.LeverandørNavn);
                levItem.SubItems.Add(leverandør.Email);
                listView_suppliers.Items.Add(levItem);
            }
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
            Services.LeverandørService levService = new LeverandørService();
            bool phoneNumValid = int.TryParse(tb_supplierPhone.Text, out int tlf);
            bool zipValid = int.TryParse(tb_levpostnr.Text, out int zip);

            if (phoneNumValid && zipValid)
            {
                levService.CreateLeverandør(tb_supplierName.Text, tb_supplierAdress.Text, zip, tb_supplierEmail.Text,
                    tb_supplierBankName.Text + " " + tb_supplierRegNo.Text + " " + tb_supplierAccountNo.Text, tlf);

                MessageBox.Show(tb_supplierName.Text + " blev tilføjet til databasen!", "SUCCESS!", MessageBoxButtons.OK);
                GetAllLeverandører();
            }
            else
            {
                MessageBox.Show("Et eller flere inputs er ugyldige!", "OOPS!", MessageBoxButtons.OK);
            }
        }

        private void btn_updateSupplier_Click(object sender, EventArgs e)
        {
            Services.LeverandørService levService = new LeverandørService();
            bool phoneNumValid = int.TryParse(tb_supplierPhone.Text, out int tlf);
            bool zipValid = int.TryParse(tb_levpostnr.Text, out int zip);

            if (phoneNumValid && zipValid)
            {
                levService.UpdateLeverandør(tb_supplierIdLine.Text, tb_supplierName.Text, tb_supplierAdress.Text, zip, tb_supplierEmail.Text,
                    tb_supplierBankName.Text + " " + tb_supplierRegNo.Text + " " + tb_supplierAccountNo.Text, tlf);

                MessageBox.Show(tb_supplierName.Text + " blev opdateret i databasen!", "SUCCESS!", MessageBoxButtons.OK);
                GetAllLeverandører();
            }
            else
            {
                MessageBox.Show("Et eller flere inputs er ugyldige!", "OOPS!", MessageBoxButtons.OK);
            }
        }

        private void btn_deleteSupplier_Click(object sender, EventArgs e)
        {
            Services.LeverandørService levService = new LeverandørService();
            if (tb_supplierIdLine != null)
            {
                DialogResult dialogResult = MessageBox.Show("Er du sikker på at du vil slette " + tb_supplierName.Text + "?",
                    "ADVARSEL!", MessageBoxButtons.YesNoCancel);
                if (dialogResult == DialogResult.Yes)
                {
                    levService.DeleteLeverandør(tb_supplierIdLine.Text);
                    MessageBox.Show(tb_supplierName.Text + " blev slettet fra databasen!", "SUCCESS!", MessageBoxButtons.OK);

                    tb_supplierIdLine.Text = null;
                    tb_supplierName.Text = null;
                    tb_supplierAdress.Text = null;
                    tb_levpostnr.Text = null;
                    tb_supplierPhone.Text = null;
                    tb_supplierEmail.Text = null;
                    tb_supplierBankName.Text = null;
                    tb_supplierRegNo.Text = null;
                    tb_supplierAccountNo.Text = null;
                    rtb_faktuInfo.Text = null;
                    GetAllLeverandører();
                }
                else
                {
                    MessageBox.Show("Sletning afbrudt!", "SUCCESS!", MessageBoxButtons.OK);
                }
            }
        }

        private void dateTimePicker_reportStartTime_ValueChanged(object sender, EventArgs e)
        {
            GetAllOrdersWithinDateInterval();
        }

        private void dateTimePicker_reportEndTime_ValueChanged(object sender, EventArgs e)
        {
            GetAllOrdersWithinDateInterval();
        }

        private void textBox_OnlyNumbersKeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void btn_crudvare_Click(object sender, EventArgs e)
        {
            tabControl.SelectedTab = tabPage_Products;
        }


    }
}
