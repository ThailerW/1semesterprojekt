using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SynsPunkt_ApS.Models
{
    public class Booking
    {
        public int BookingID { get; set; }
        public int LocationID { get; set; }
        public string Time { get; set; }
        public DateTime Date { get; set; }
        public string BookingType { get; set; }
        public int CustomerID { get; set; }

        public Booking(int locationid, DateTime dato, string time, string bookingType, int customerID)
        {

            LocationID = locationid;
            Date = dato;
            Time = time;
            BookingType = bookingType;
            CustomerID = customerID;
        }

        public Booking(int bookingID, int lokationid, DateTime dato, string tidspunkt, string bookingType, int kundeID)
        {
            BookingID = bookingID;
            LocationID = lokationid;
            Date = dato;
            Time = tidspunkt;
            BookingType = bookingType;
            CustomerID = kundeID;
        }
    }
}
