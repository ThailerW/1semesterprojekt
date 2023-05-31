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
        public Models.Employee LoggedInEmployee;
        public List<Models.ProductLine> currentLineItems = new List<ProductLine>();
        public List<Models.Product> allProducts;
        public List<Models.Order> allOrders;
        public List<Models.Order> ordersWithinDateInterval;
        Services.Product_service productService = new Product_service();
        Services.Ordre_service orderService = new Ordre_service();

        public MainMenu(Models.Employee loggedInEmployee)
        {
            allOrders = orderService.GetAllOrders();
            allProducts = productService.GetAllProduct();
            LoggedInEmployee = loggedInEmployee;
            InitializeComponent();
        }

        /// <summary>
        /// Martin: Many methods are called so the listviews are ready to display the correct things
        /// Theis: Checks the users RoleID, and gives permissions according to the ID.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            GetAndDisplayAllProducts();
            GetAndDisplayAllEmployees();
            GetAndDisplayAllBookings();
            GetAndDisplayAllCustomer();
            GetAndDisplayAllSuppliers();
            GetAndDisplayAllProductsInBasketTab();
            GetAndDisplayAllOrdersWithinDateInterval();

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

        /// <summary>
        /// Theis: If the user has inputted the old password, and two identical new passwords, the password of the logged in user
        /// will be changed in the database.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Martin: Error messages appear if the user tries to add nothing, an out-of-stock item to the basket, or more of an item than there is in stock. 
        ///         Otherwise an item is added to the basket
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_addToBasket_Click(object sender, EventArgs e)
        {
            //Tjekker om en vare er valgt
            if (listView_product_list_buytab.SelectedItems.Count <= 0)
            {
                MessageBox.Show("Vælg en vare fra varelisten du vil tilføje til din kurv", "Hovsa", MessageBoxButtons.OK);
                return;
            }
            int vareID = Convert.ToInt32(listView_product_list_buytab.SelectedItems[0].SubItems[0].Text);
            Models.Product chosenProduct = allProducts.FirstOrDefault(v => v.productNumber == vareID);
            Models.ProductLine lineItemForChosenProduct = currentLineItems.FirstOrDefault(x => x.product == chosenProduct);

            //Tjekker om der er mindst 1 af varen på lager
            if (Convert.ToInt32(listView_product_list_buytab.SelectedItems[0].SubItems[4].Text) <= 0)
            {
                MessageBox.Show("Hovsa, der er ikke flere tilbage af denne vare på lageret. Surt show", "Beklager", MessageBoxButtons.OK);
                return;
            }

            if (currentLineItems.Any(x => x.product == chosenProduct))
            {
                //Tjekker for at man ikke kan købe flere af en vare end hvad der er på lager
                if (lineItemForChosenProduct.quantity == chosenProduct.stockQuantity)
                {
                    MessageBox.Show("Vil du købe flere af denne vare end vi har på lager din bøllebob?", "-3 i matematik", MessageBoxButtons.OK);
                    return;
                }
                lineItemForChosenProduct.quantity += 1;
                UpdateBasketListView();
                return;
            }
            //Hvis alt er ok, tilføjes en ny varelinje til kurven
            Models.ProductLine varelinje = new ProductLine(chosenProduct, 1, chosenProduct.price);
            currentLineItems.Add(varelinje);
            UpdateBasketListView();
        }

        /// <summary>
        /// Martin: Updates the listview of current basket items
        /// </summary>
        private void UpdateBasketListView()
        {
            listView_basket_list.Items.Clear();
            foreach (var lineItem in currentLineItems)
            {
                ListViewItem lineItemItem = new ListViewItem(lineItem.product.productNumber.ToString());
                lineItemItem.SubItems.Add(lineItem.product.productName);
                lineItemItem.SubItems.Add(lineItem.totalPrice.ToString("N0"));
                lineItemItem.SubItems.Add(lineItem.quantity.ToString());
                listView_basket_list.Items.Add(lineItemItem);
            }

        }

        /// <summary>
        /// Martin: Removes all of one product from the basket if an item from the basket is selected
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_RemoveFromBasket_Click(object sender, EventArgs e)
        {
            if (listView_basket_list.SelectedItems.Count <= 0)
            {
                MessageBox.Show("Vælg en vare fra kurven du vil fjerne", "Hovsa", MessageBoxButtons.OK);
                return;
            }
            int productID = Convert.ToInt32(listView_basket_list.SelectedItems[0].SubItems[0].Text);
            Models.Product chosenProduct = allProducts.FirstOrDefault(v => v.productNumber == productID);
            Models.ProductLine lineItemForChosenProduct = currentLineItems.FirstOrDefault(x => x.product == chosenProduct);

            currentLineItems.Remove(lineItemForChosenProduct);
            UpdateBasketListView();

        }

        /// <summary>
        /// Martin: Checks if various conditions are met (such as if the basket is empty). If not, then a string gets added some text. 
        ///         If that string is not empty the string is displayed in a messagebox. If it is empty it means there are no others with the order,
        ///         and the order is completed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_SendInvoiceMail_Click(object sender, EventArgs e)
        {
            Services.Customer_service kundeService = new Customer_service();
            Services.ProductLine_service varelinjeService = new ProductLine_service();
            Services.Ordre_service ordreService = new Ordre_service();
            Services.Product_service vareService = new Product_service();

            string MessageBoxText = string.Empty;

            //Checks if the basket is empty
            if (currentLineItems.Count() == 0)
            {
                MessageBoxText += "Tilføj vare til kurven.";
            }

            //Checks if a customerID is entered and if it is valid
            if (tb_customerToBuy.Text == string.Empty)
            {
                MessageBoxText += Environment.NewLine + "Indtast kundeID.";
            }
            else if (!kundeService.CheckIfCustomerExists(Convert.ToInt32(tb_customerToBuy.Text)))
            {
                MessageBoxText += Environment.NewLine + "Ugyldigt kundeID.";
            }

            //Displays error message if there are errors
            if (!string.IsNullOrEmpty(MessageBoxText))
            {
                MessageBox.Show(MessageBoxText);
            }
            else
            {
                int customerID = Convert.ToInt32(tb_customerToBuy.Text);
                DateTime orderDate = DateTime.Now;
                double totalPrice = 0;
                foreach (var lineItem in currentLineItems)
                {
                    totalPrice += Convert.ToDouble(lineItem.totalPrice);
                }
                int orderID = ordreService.CreateOrder(customerID, orderDate, totalPrice);

                foreach (var lineItem in currentLineItems)
                {
                    varelinjeService.CreateProductLine(lineItem.product.productNumber, orderID, lineItem.quantity);
                    lineItem.product.stockQuantity -= lineItem.quantity;
                    vareService.UpdateProduct(lineItem.product.productNumber.ToString(), lineItem.product.productDescription,
                                            lineItem.product.stockQuantity, lineItem.product.productName, lineItem.product.lensStrength, lineItem.product.levCVR, lineItem.product.price);
                }
                GetAndDisplayAllProductsInBasketTab();
                MessageBoxText += "Salg udført! " + Environment.NewLine + "OrderID: " + orderID + Environment.NewLine + "KundeID: " + customerID;
                MessageBox.Show(MessageBoxText);

                currentLineItems.Clear();
                UpdateBasketListView();
                allOrders = ordreService.GetAllOrders();
                GetAndDisplayAllOrdersWithinDateInterval();

                tb_customerToBuy.Text = string.Empty;
            }

        }
        private void btn_PrintInvoice_Click(object sender, EventArgs e)
        {

        }

        private void listView_basket_list_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        /// <summary>
        /// Martin: Changes what the listview with products displayes based on what is searched
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tb_SearchProduct_TextChanged(object sender, EventArgs e)
        {
            listView_product_list_buytab.Items.Clear();
            Services.Product_service productService = new Services.Product_service();
            if (tb_SearchProduct.Text == string.Empty)
            {
                GetAndDisplayAllProductsInBasketTab();
            }
            else
            {
                List<Models.Product> allProducts = productService.SearchProductByName(tb_SearchProduct.Text);
                foreach (var product in allProducts)
                {
                    ListViewItem productItem = new ListViewItem(product.productNumber.ToString());
                    productItem.SubItems.Add(product.productName);
                    productItem.SubItems.Add(product.lensStrength.ToString());
                    productItem.SubItems.Add(product.price.ToString());
                    productItem.SubItems.Add(product.stockQuantity.ToString());
                    listView_product_list_buytab.Items.Add(productItem);
                }

            }
        }

        private void listView_customers_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Tjek om en kunde er valgt i ListView
            if (listView_customers.SelectedItems.Count > 0)
            {

                Services.Customer_service customerService = new Customer_service();

                string selectedCustomerID = listView_customers.SelectedItems[0].SubItems[0].Text;

                Models.Customer selectedCustomer = customerService.GetCustomer(Convert.ToInt32(selectedCustomerID));

                ListViewItem selectedItem = listView_customers.SelectedItems[0];

                // Opdater tekstbokse med de valgte kundens oplysninger
                tb_customerID.Text = selectedCustomer.customerID;
                tb_CustomerFirstName.Text = selectedCustomer.firstName;
                tb_customerLastName.Text = selectedCustomer.lastName;
                tb_customerPhoneNumber.Text = selectedCustomer.phoneNumber.ToString();
                tb_customerEmail.Text = selectedCustomer.mail;
                tb_customerAdress.Text = selectedCustomer.adress;
                tb_customerPostNr.Text = selectedCustomer.zipCode.ToString();
            }
        }

        private void tb_searchPhoneNumber_TextChanged(object sender, EventArgs e)
        {
            listView_customers.SelectedItems.Clear();
            Services.Customer_service customerService = new Services.Customer_service();
            listView_customers.Items.Clear(); // Fjerner eksisterende elementer i listView_customers

            if (tb_searchPhoneNumber.Text == string.Empty)
            {
                GetAndDisplayAllCustomer();
            }
            else
            {
                if (int.TryParse(tb_searchPhoneNumber.Text, out int customerID))
                {
                    Models.Customer customer = customerService.GetCustomer(customerID);
                    if (customer != null)
                    {
                        ListViewItem listViewItem = new ListViewItem(customer.phoneNumber.ToString());
                        listViewItem.SubItems.Add(customer.customerID.ToString());
                        listView_customers.Items.Add(listViewItem);
                    }
                }
            }
        }


        private void btn_createCustomer_Click(object sender, EventArgs e)
        {
            Services.Customer_service customerService = new Services.Customer_service();

            // Opret en ny kunde med dataene fra tekstboksene
            string locationId = tb_CustomerLokation.Text;
            string Mail = tb_customerEmail.Text;
            string firstName = tb_CustomerFirstName.Text;
            string lastName = tb_customerLastName.Text;
            int phoneNumber;
            string adress = tb_customerAdress.Text;
            int zipCode;

            if (!int.TryParse(tb_customerPhoneNumber.Text, out phoneNumber))
            {
                MessageBox.Show("Ugyldig telefonnummer-værdi. Indtast venligst et gyldigt heltal.");
                return;
            }

            if (!int.TryParse(tb_customerPostNr.Text, out zipCode))
            {
                MessageBox.Show("Ugyldig postnr-værdi. Indtast venligst et gyldigt heltal.");
                return;
            }

            customerService.CreateCustomer(locationId, Mail, firstName, lastName, phoneNumber, adress, zipCode);

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
            Services.Customer_service customerService = new Services.Customer_service();

            int customerID = Convert.ToInt32(tb_customerID.Text);
            string firstName = tb_CustomerFirstName.Text;
            string lastName = tb_customerLastName.Text;
            int phoneNumber = Convert.ToInt32(tb_customerPhoneNumber.Text);
            string mail = tb_customerEmail.Text;
            string adress = tb_customerAdress.Text;
            int zipCode = Convert.ToInt32(tb_customerPostNr.Text);
            string locationId = lb_customerLocation.Text;

            customerService.UpdateCustomer(locationId, customerID, mail, firstName, lastName, phoneNumber, adress, zipCode);

            if (tb_searchPhoneNumber.Text == string.Empty)
            {
                GetAndDisplayAllCustomer();
            }
            else
            {
                tb_searchPhoneNumber_TextChanged(tb_searchPhoneNumber, new EventArgs());
            }

            MessageBox.Show("Kunde blev Opdateret", "Det virkede", MessageBoxButtons.OK);
        }

        private void btn_deleteCustomer_Click(object sender, EventArgs e)
        {
            Services.Customer_service kundeServices = new Services.Customer_service();

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

                kundeServices.DeleteCustomer(int.Parse(tb_customerID.Text));

                ClearTextBoxes();

                UpdateCustomerListView();
            }
        }


        private void UpdateCustomerListView()
        {
            Customer_service kundeServices = new Customer_service();

            // Ryd alle eksisterende elementer i listView_customers
            listView_customers.Items.Clear();

            // Hent opdaterede kundedata fra databasen eller en anden datakilde
            List<Customer> customers = kundeServices.GetCustomers();

            // Tilføj kunder som elementer i ListView
            foreach (Customer customer in customers)
            {
                ListViewItem item = new ListViewItem(customer.customerID.ToString());
                item.SubItems.Add(customer.firstName);
                item.SubItems.Add(customer.lastName);
                item.SubItems.Add(customer.phoneNumber.ToString());
                item.SubItems.Add(customer.mail);
                item.SubItems.Add(customer.adress);

                ListViewItem.ListViewSubItem postNrSubItem = new ListViewItem.ListViewSubItem(item, customer.zipCode.ToString());
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
                if (booking.Date.Date == selectedDate.Date)
                {
                    filteredBookings.Add(booking);
                }
            }
            listView_Bookings.Items.Clear();
            foreach (Booking booking in filteredBookings)
            {

                ListViewItem listViewItem = new ListViewItem(booking.BookingID.ToString());
                listViewItem.SubItems.Add(booking.LocationID.ToString());
                listViewItem.SubItems.Add(booking.Time);
                listViewItem.SubItems.Add(booking.Date.ToShortDateString());
                listViewItem.SubItems.Add(booking.BookingType);
                listViewItem.SubItems.Add(booking.CustomerID.ToString());
                listView_Bookings.Items.Add(listViewItem);
            }

        }

        private void btn_clearDate_Click(object sender, EventArgs e)
        {

        }

        private void btn_createBooking_Click(object sender, EventArgs e)
        {
            bool locationIDValid = int.TryParse(tb_locationID.Text, out int locationID); ;
            //DateTime tidspunkt = DateTime.ParseExact(cb_timePicker.Text, "HH:mm:ss", CultureInfo.InvariantCulture);
            string time = cb_timePicker.Text;
            time = time += ":00";
            DateTime date = dateTimePicker_bookingInterval.Value;
            string bookingType = tb_bookingDescription.Text;
            bool customerIDValid = int.TryParse(tb_customerBooking.Text, out int customerID);

            if (locationIDValid && customerIDValid)
            {
                BookingService bookingServices = new BookingService();

                Booking newBooking = new Booking(locationID, date, time, bookingType, customerID);

                bookingServices.CreateBooking(newBooking);

                MessageBox.Show($"Booking oprettet!");
                GetAndDisplayAllBookings();
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
            bool lokationIDValid = int.TryParse(tb_locationID.Text, out int locationID);
            string time = cb_timePicker.Text;
            time = time += ":00";
            DateTime date = dateTimePicker_bookingInterval.Value;
            string bookingType = tb_bookingDescription.Text;
            bool customerIDValid = int.TryParse(tb_customerBooking.Text, out int customerID);

            if (lokationIDValid && customerIDValid)
            {
                BookingService bookingServicevar = new BookingService();
                Booking updatedBooking = new Booking(bookingID, locationID, date, time, bookingType, customerID);
                bookingServicevar.UpdateBooking(updatedBooking);
                MessageBox.Show($"Booking ændret!");
                GetAndDisplayAllBookings();
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

                GetAndDisplayAllBookings();
            }
            else
            {
                MessageBox.Show("Sletning afbrudt!", "SUCCESS!", MessageBoxButtons.OK);
            }

        }

        /// <summary>
        /// Theis: Displays the chosen supplier in the textboxes to the right.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listView_suppliers_SelectedIndexChanged(object sender, EventArgs e)
        {
            Services.Supplier_Service supplierService = new Supplier_Service();

            if (listView_suppliers.SelectedItems.Count > 0)
            {
                ListViewItem selectedItem = listView_suppliers.SelectedItems[0];
                var item = selectedItem.SubItems[0].Text;

                supplierService.ReadSupplier(item, out string id2, out string navn, out string adresse, out string postNr, out string email,
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

        /// <summary>
        /// Theis: Searches the database for a supplier with a name that contains what the user inputs in this search bar.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tb_supplierID_TextChanged(object sender, EventArgs e)
        {
            listView_suppliers.Items.Clear();
            Services.Supplier_Service supplierService = new Supplier_Service();
            if (tb_supplierID.Text == string.Empty)
            {
                GetAndDisplayAllSuppliers();
            }
            else
            {
                List<Models.Supplier> allSuppliers = supplierService.SearchSupplierByName(tb_supplierID.Text);
                foreach (var supplier in allSuppliers)
                {
                    ListViewItem supplierItem = new ListViewItem(supplier.CVRnummer.ToString());
                    supplierItem.SubItems.Add(supplier.supplierName);
                    supplierItem.SubItems.Add(supplier.mail);
                    listView_suppliers.Items.Add(supplierItem);
                }
            }
        }

        private void btn_searchSupplierID_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Martin: When an employee from the listview is selected, the textboxes displays the employees data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listView_employees_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView_employees.SelectedItems.Count > 0)
            {
                Services.Employee_service employeeService = new Employee_service();


                string selectedEmployeeID = listView_employees.SelectedItems[0].SubItems[0].Text;

                Models.Employee selectedEmployee = employeeService.GetEmployeeByID(selectedEmployeeID);

                //Textbox tekst opdateres:
                tb_employeeId.Text = selectedEmployee.EmployeeID.ToString();
                tb_employeeFirstName.Text = selectedEmployee.FirstName;
                tb_employeeLastName.Text = selectedEmployee.LastName;
                tb_employeePhoneNo.Text = selectedEmployee.TelephoneNumber.ToString();
                tb_employeeEmail.Text = selectedEmployee.PrivateMail;
                tb_employeeAdress.Text = selectedEmployee.Adress;
                tb_employeeZip.Text = selectedEmployee.ZipCode.ToString();
                tb_employeeBU.Text = employeeService.GetDepartmentName(Convert.ToInt32(selectedEmployee.DepartmentID));
                tb_employeeRole.Text = employeeService.GetRoleName(Convert.ToInt32(selectedEmployee.RoleID));
                tb_employeeWorkMail.Text = selectedEmployee.WorkMail;
                tb_employeePassword.Text = selectedEmployee.Password;

            }
        }

        /// <summary>
        /// Martin: Changes what the listview of employees displays based on what is searched
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tb_searchEmployee_TextChanged(object sender, EventArgs e)
        {
            listView_employees.Items.Clear();
            Services.Employee_service ansatService = new Services.Employee_service();
            if (tb_searchEmployee.Text == string.Empty)
            {
                GetAndDisplayAllEmployees();
            }
            else
            {
                List<Models.Employee> allEmployees = ansatService.SearchEmployeeByName(tb_searchEmployee.Text);
                foreach (var employee in allEmployees)
                {
                    ListViewItem employeeItem = new ListViewItem(employee.EmployeeID.ToString());
                    employeeItem.SubItems.Add(employee.FirstName);
                    employeeItem.SubItems.Add(employee.LastName);
                    listView_employees.Items.Add(employeeItem);
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
            Services.Employee_service ansatService = new Services.Employee_service();

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


            ansatService.CreateEmployee(firstName, lastName, phoneNumber, privateMail, adress, password, department, role, workMail, zipCode);

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
                GetAndDisplayAllEmployees();
            }
            else
            {
                tb_searchEmployee_TextChanged(tb_searchEmployee, new EventArgs());
            }
        }

        /// <summary>
        /// Martin: Check if all the necessary textbox are non-empty in order to update an employee
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_UpdateEmployee_Click(object sender, EventArgs e)
        {
            foreach (System.Windows.Forms.TextBox textBox in tabPage_Medarbejder.Controls.OfType<System.Windows.Forms.TextBox>())
            {
                if (textBox != tb_searchEmployee && textBox.Text == string.Empty)
                {
                    MessageBox.Show("Udfyld alle oplysninger for at opdatere en kunde", "Hvad fanden laver du?????", MessageBoxButtons.OK);

                    return;
                }
            }
            Services.Employee_service ansatServices = new Services.Employee_service();

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

            ansatServices.UpdateEmployee(employeeID, firstName, lastName, phoneNumber, privateMail,
                adress, password, department, role, workMail, zipCode);

            //Opdaterer oplysningerne på listview
            if (tb_searchEmployee.Text == string.Empty)
            {
                GetAndDisplayAllEmployees();
            }
            else
            {
                tb_searchEmployee_TextChanged(tb_searchEmployee, new EventArgs());
            }

            //bekræftelse på opdatering
            MessageBox.Show("Ansat Opdateret!", "Success", MessageBoxButtons.OK);

        }

        /// <summary>
        /// Martin: Checks if an employee is chosen before deleting the employee and then updates the listview of employees
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_deleteEmployee_Click(object sender, EventArgs e)
        {
            Services.Employee_service ansatServices = new Services.Employee_service();
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

                ansatServices.DeleteEmployee(Convert.ToInt32(tb_employeeId.Text));

                if (tb_searchEmployee.Text != string.Empty)
                {
                    GetAndDisplayAllEmployees();
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

        /// <summary>
        /// Martin: Sends input to textfileGenerator to generate a sales report
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_GenerateReport_Click(object sender, EventArgs e)
        {
            Services.TextFileGenerator textFileGenerator = new Services.TextFileGenerator();
            DateTime startDate = dateTimePicker_reportStartTime.Value.Date;
            DateTime endDate = dateTimePicker_reportEndTime.Value.Date;
            textFileGenerator.GenerateSalesReport(startDate, endDate, ordersWithinDateInterval);

            MessageBox.Show("Rapporten blev udskrevet som tekstfil");
        }

        /// <summary>
        /// Displays the selected product in the textboxes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listView1_listOfSuppliers_SelectedIndexChanged(object sender, EventArgs e)
        {
            Services.Product_service productSerice = new Product_service();

            if (listView1_listOfSuppliers.SelectedItems.Count > 0)
            {
                ListViewItem selectedItem = listView1_listOfSuppliers.SelectedItems[0];
                var item = selectedItem.SubItems[0].Text;

                productSerice.ReadProduct(item, out string id2, out string productDescription, out string stockQuantity, out string productName,
                                     out string lensStrength, out string levCVR, out string price);

                tb_productID.Text = id2;
                rtb_productdescription.Text = productDescription;
                tb_quantity.Text = stockQuantity;
                tb_productName.Text = productName;
                tb_strengt.Text = lensStrength;
                tb_supplierCVR.Text = levCVR;
                tb_productPrice.Text = price;
            }
        }

        /// <summary>
        /// Theis: Filters the List of Products by gathering a list of products from the database that contains the user input in its name.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tb_searchForProduct_TextChanged(object sender, EventArgs e)
        {
            listView1_listOfSuppliers.Items.Clear();
            Services.Product_service productService = new Services.Product_service();
            if (tb_searchForProduct.Text == string.Empty)
            {
                GetAndDisplayAllProducts();
            }
            else
            {
                List<Models.Product> allProducts = productService.SearchProductByName(tb_searchForProduct.Text);
                foreach (var product in allProducts)
                {
                    ListViewItem productItem = new ListViewItem(product.productNumber.ToString());
                    productItem.SubItems.Add(product.productName);
                    productItem.SubItems.Add(product.stockQuantity.ToString());
                    listView1_listOfSuppliers.Items.Add(productItem);
                }
            }
        }

        /// <summary>
        /// Martin: Sends data to textfileGenerator to create a product report
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_printproductreport_Click(object sender, EventArgs e)
        {
            Services.TextFileGenerator textFileGenerator = new Services.TextFileGenerator();
            textFileGenerator.GenerateProductReport(allProducts);
            MessageBox.Show("Varerapporten blev udskrevet som tekstfil","Success");
        }

        /// <summary>
        /// Theis: Creates a new product in the DB with the same data as the user has inputted.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_createProduct_Click(object sender, EventArgs e)
        {
            Services.Product_service vareService = new Product_service();
            bool quantityValid = int.TryParse(tb_quantity.Text, out int quantity);
            bool strengthValid = decimal.TryParse(tb_strengt.Text, out decimal strength);
            bool priceValid = decimal.TryParse(tb_productPrice.Text, out decimal price);

            if (quantityValid && strengthValid && priceValid)
            {
                vareService.CreateProduct(rtb_productdescription.Text, quantity, tb_productName.Text, strength, tb_supplierCVR.Text, price);
                MessageBox.Show(tb_productName.Text + " blev tilføjet til databasen!", "SUCCESS!", MessageBoxButtons.OK);
                GetAndDisplayAllProducts();
                GetAndDisplayAllProductsInBasketTab();
            }
            else
            {
                MessageBox.Show("Et eller flere inputs er ugyldige!", "OOPS!", MessageBoxButtons.OK);
            }
        }

        /// <summary>
        /// Theis: Updates the product in the DB with the selected productID, with the new user inputs.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_updateProduct_Click(object sender, EventArgs e)
        {
            Services.Product_service vareService = new Product_service();
            bool quantityValid = int.TryParse(tb_quantity.Text, out int quantity);
            bool strengthValid = decimal.TryParse(tb_strengt.Text, out decimal strength);
            bool priceValid = decimal.TryParse(tb_productPrice.Text, out decimal price);

            if (quantityValid && strengthValid && priceValid)
            {
                vareService.UpdateProduct(tb_productID.Text, rtb_productdescription.Text, quantity, tb_productName.Text, strength, tb_supplierCVR.Text, price);
                MessageBox.Show(tb_productName.Text + " blev opdateret i databasen!", "SUCCESS!", MessageBoxButtons.OK);
                GetAndDisplayAllProducts();
                GetAndDisplayAllProductsInBasketTab();
            }
            else
            {
                MessageBox.Show("Et eller flere inputs er ugyldige!", "OOPS!", MessageBoxButtons.OK);
            }
        }

        /// <summary>
        /// Theis: Deletes the selected product from the database.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_deleteProduct_Click(object sender, EventArgs e)
        {
            Services.Product_service vareService = new Product_service();
            if (tb_productID != null)
            {
                DialogResult dialogResult = MessageBox.Show("Er du sikker på at du vil slette " + tb_productName.Text + "?",
                    "ADVARSEL!", MessageBoxButtons.YesNoCancel);
                if (dialogResult == DialogResult.Yes)
                {
                    vareService.DeleteProduct(tb_productID.Text);
                    MessageBox.Show(tb_productName.Text + " blev slettet fra databasen!", "SUCCESS!", MessageBoxButtons.OK);

                    tb_productID.Text = null;
                    tb_productName.Text = null;
                    tb_strengt.Text = null;
                    tb_supplierCVR.Text = null;
                    tb_quantity.Text = null;
                    rtb_productdescription.Text = null;
                    GetAndDisplayAllProducts();
                    GetAndDisplayAllProductsInBasketTab();
                }
                else
                {
                    MessageBox.Show("Sletning afbrudt!", "SUCCESS!", MessageBoxButtons.OK);
                }
            }
        }

        private void GetAndDisplayAllOrdersWithinDateInterval()
        {
            ordersWithinDateInterval = allOrders.Where
                (order => (order.orderDate.Date >= dateTimePicker_reportStartTime.Value && order.orderDate.Date <= dateTimePicker_reportEndTime.Value)
                || (order.orderDate.Date == dateTimePicker_reportStartTime.Value.Date && order.orderDate.Date == dateTimePicker_reportEndTime.Value.Date)).ToList();

            listView_report.Items.Clear();
            Services.Customer_service kundeService = new Services.Customer_service();
            Services.Ordre_service orderService = new Ordre_service();


            foreach (var order in ordersWithinDateInterval)
            {
                Models.Customer customerWithThisOrder = kundeService.GetCustomer(order.customerID);

                ListViewItem orderItem = new ListViewItem(order.orderID.ToString());
                orderItem.SubItems.Add(order.customerID.ToString());
                orderItem.SubItems.Add(customerWithThisOrder.firstName + " " + customerWithThisOrder.lastName);
                orderItem.SubItems.Add(order.orderDate.ToString());
                orderItem.SubItems.Add(order.totalPrice.ToString("N0"));
                listView_report.Items.Add(orderItem);
            }
        }

        /// <summary>
        /// Martin: Displays all Employees from the DB.
        /// </summary>
        private void GetAndDisplayAllEmployees()
        {
            Services.Employee_service employeeService = new Services.Employee_service();
            List<Models.Employee> allEmployees = employeeService.GetAllEmployees();
            listView_employees.Items.Clear();
            foreach (var employee in allEmployees)
            {
                ListViewItem employeeItem = new ListViewItem(employee.EmployeeID.ToString());
                employeeItem.SubItems.Add(employee.FirstName);
                employeeItem.SubItems.Add(employee.LastName);
                listView_employees.Items.Add(employeeItem);
            }
        }

        /// <summary>
        /// Marinh: Displays all Customers from the DB.
        /// </summary>
        private void GetAndDisplayAllCustomer()
        {
            listView_customers.Items.Clear();
            Services.Customer_service kunde_Services = new Services.Customer_service();
            var customerList = kunde_Services.GetCustomers();
            foreach (var customer in customerList)
            {
                ListViewItem customerItem = new ListViewItem(customer.customerID.ToString());
                customerItem.SubItems.Add(customer.firstName);
                customerItem.SubItems.Add(customer.lastName);
                listView_customers.Items.Add(customerItem);

            }

        }

        /// <summary>
        /// Theis: Displays all Products from the DB.
        /// </summary>
        private void GetAndDisplayAllProducts()
        {
            listView1_listOfSuppliers.Items.Clear();
            Services.Product_service productService = new Product_service();
            var productList = productService.GetAllProduct();
            foreach (var product in productList)
            {
                ListViewItem productItem = new ListViewItem(product.productNumber.ToString());
                productItem.SubItems.Add(product.productName);
                productItem.SubItems.Add(product.stockQuantity.ToString());
                listView1_listOfSuppliers.Items.Add(productItem);
            }
        }

        /// <summary>
        /// Sebastian: Displays all Bookings from the DB.
        /// </summary>
        private void GetAndDisplayAllBookings()
        {
            listView_Bookings.Items.Clear();
            Services.BookingService bookingservice = new BookingService();
            var bookingList = bookingservice.GetAllBookings();
            foreach (var booking in bookingList)
            {
                ListViewItem bookingItem = new ListViewItem(booking.BookingID.ToString());
                bookingItem.SubItems.Add(booking.BookingType);
                bookingItem.SubItems.Add(booking.LocationID.ToString());
                listView_Bookings.Items.Add(bookingItem);
            }
        }

        /// <summary>
        /// Theis: Displays all Suppliers from the DB.
        /// </summary>
        private void GetAndDisplayAllSuppliers()
        {
            listView_suppliers.Items.Clear();
            Services.Supplier_Service levService = new Supplier_Service();
            var levList = levService.GetAllSupplier();
            foreach (var leverandør in levList)
            {
                ListViewItem levItem = new ListViewItem(leverandør.CVRnummer.ToString());
                levItem.SubItems.Add(leverandør.supplierName);
                levItem.SubItems.Add(leverandør.mail);
                listView_suppliers.Items.Add(levItem);
            }
        }

        /// <summary>
        /// Martin: Displays all Employees from the DB.
        /// </summary>
        private void GetAndDisplayAllProductsInBasketTab()
        {

            listView_product_list_buytab.Items.Clear();
            Services.Product_service productService = new Product_service();
            var productList = productService.GetAllProduct();
            foreach (var product in productList)
            {
                ListViewItem productItem = new ListViewItem(product.productNumber.ToString());
                productItem.SubItems.Add(product.productName);
                productItem.SubItems.Add(product.lensStrength.ToString());
                productItem.SubItems.Add(product.price.ToString());
                productItem.SubItems.Add(product.stockQuantity.ToString());
                listView_product_list_buytab.Items.Add(productItem);
            }

        }

        /// <summary>
        /// Theis: Creates a new supplier with the inputted information.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_createSupplier_Click(object sender, EventArgs e)
        {
            Services.Supplier_Service levService = new Supplier_Service();
            bool phoneNumValid = int.TryParse(tb_supplierPhone.Text, out int tlf);
            bool zipValid = int.TryParse(tb_levpostnr.Text, out int zip);

            if (phoneNumValid && zipValid)
            {
                levService.CreateSupplier(tb_supplierName.Text, tb_supplierAdress.Text, zip, tb_supplierEmail.Text,
                    tb_supplierBankName.Text + " " + tb_supplierRegNo.Text + " " + tb_supplierAccountNo.Text, tlf);

                MessageBox.Show(tb_supplierName.Text + " blev tilføjet til databasen!", "SUCCESS!", MessageBoxButtons.OK);
                GetAndDisplayAllSuppliers();
            }
            else
            {
                MessageBox.Show("Et eller flere inputs er ugyldige!", "OOPS!", MessageBoxButtons.OK);
            }
        }

        /// <summary>
        /// Theis: Updates the supplier with the selected ID.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_updateSupplier_Click(object sender, EventArgs e)
        {
            Services.Supplier_Service levService = new Supplier_Service();
            bool phoneNumValid = int.TryParse(tb_supplierPhone.Text, out int tlf);
            bool zipValid = int.TryParse(tb_levpostnr.Text, out int zip);

            if (phoneNumValid && zipValid)
            {
                levService.UpdateSupplier(tb_supplierIdLine.Text, tb_supplierName.Text, tb_supplierAdress.Text, zip, tb_supplierEmail.Text,
                    tb_supplierBankName.Text + " " + tb_supplierRegNo.Text + " " + tb_supplierAccountNo.Text, tlf);

                MessageBox.Show(tb_supplierName.Text + " blev opdateret i databasen!", "SUCCESS!", MessageBoxButtons.OK);
                GetAndDisplayAllSuppliers();
            }
            else
            {
                MessageBox.Show("Et eller flere inputs er ugyldige!", "OOPS!", MessageBoxButtons.OK);
            }
        }

        /// <summary>
        /// Theis: Deletes the supplier with the selected ID.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_deleteSupplier_Click(object sender, EventArgs e)
        {
            Services.Supplier_Service levService = new Supplier_Service();
            if (tb_supplierIdLine != null)
            {
                DialogResult dialogResult = MessageBox.Show("Er du sikker på at du vil slette " + tb_supplierName.Text + "?",
                    "ADVARSEL!", MessageBoxButtons.YesNoCancel);
                if (dialogResult == DialogResult.Yes)
                {
                    levService.DeleteSupplier(tb_supplierIdLine.Text);
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
                    GetAndDisplayAllSuppliers();
                }
                else
                {
                    MessageBox.Show("Sletning afbrudt!", "SUCCESS!", MessageBoxButtons.OK);
                }
            }
        }

        /// <summary>
        /// Martin: Updates what the listview of orders displays
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dateTimePicker_reportStartTime_ValueChanged(object sender, EventArgs e)
        {
            GetAndDisplayAllOrdersWithinDateInterval();
        }

        /// <summary>
        /// Martin: Updates what the listview of orders displays
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dateTimePicker_reportEndTime_ValueChanged(object sender, EventArgs e)
        {
            GetAndDisplayAllOrdersWithinDateInterval();
        }

        /// <summary>
        /// Martin: Makes it only possible to input numbers in the textbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
