using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SynsPunkt_ApS.Services
{
    internal class TextFileGenerator
    {



        /// <summary>
        /// Martin: Generates a textfile over sales based on chosen dates
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="ordersWithinDateInterval"></param>
        public void GenerateSalesReport(DateTime startDate, DateTime endDate, List<Models.Order> ordersWithinDateInterval)
        {
            Services.Customer_service kundeService = new Services.Customer_service();

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
                Models.Customer customer = kundeService.GetCustomer(order.customerID);

                string orderID = order.orderID.ToString().PadRight(10);
                string customerID = order.customerID.ToString().PadRight(12);
                string customerName = (customer.firstName + " " + customer.lastName).PadRight(40);
                string orderDate = order.orderDate.ToString().PadRight(25);
                string totalPrice = order.totalPrice.ToString("C2");

                saleReport += orderID + customerID + customerName + orderDate + totalPrice + Environment.NewLine;

                totalPriceForAllOrdersInReport += order.totalPrice;
            }
            saleReport += "-------------------------------------------------------------------------------------------------------------" + Environment.NewLine;
            saleReport += "Antal Ordrer: " + ordersWithinDateInterval.Count + Environment.NewLine;
            saleReport += "Samlet salgspris for alle ordrer: " + totalPriceForAllOrdersInReport.ToString("C2");
            System.IO.File.WriteAllText("Salgsrapport (" + startDateCorrectFormat + ") - (" + endDateCorrectFormat + ").txt", saleReport);

        }

        /// <summary>
        /// Martin: Generates a textfile over all products
        /// </summary>
        /// <param name="allProducts"></param>
        public void GenerateProductReport(List<Models.Product> allProducts)
        {
            StringBuilder productReport = new StringBuilder();

            string productIDHeader = "VareID".PadRight(10);
            string productNameHeader = "Varenavn".PadRight(40);
            string productStockQuantityHeader = "Lagermængde".PadRight(20);
            string productPriceHeader = "Pris".PadRight(15);
            string productSupplierIDHeader = "LeverandørCVR".PadRight(15);

            productReport.AppendLine("Komplet Vareraport for SYNSPUNKT APS   " + DateTime.Now.Date.ToString().Substring(0, 10));
            productReport.AppendLine();
            productReport.AppendLine(productIDHeader + productNameHeader + productStockQuantityHeader + productPriceHeader + productSupplierIDHeader);
            productReport.Append("----------------------------------------------------------------------------------------------------");

            foreach (var product in allProducts)
            {
                productReport.AppendLine();
                productReport.Append(product.productNumber.ToString().PadRight(10));
                productReport.Append(product.productName.PadRight(40));
                productReport.Append(product.stockQuantity.ToString().PadRight(20));
                productReport.Append(product.price.ToString("N0").PadRight(15));
                productReport.Append(product.levCVR.ToString().PadRight(15));
            }
            productReport.AppendLine();
            productReport.AppendLine("----------------------------------------------------------------------------------------------------");

            System.IO.File.WriteAllText("Vareraport " + DateTime.Now.Date.ToString().Substring(0, 10) + ".txt", productReport.ToString());
        }
    }
}
