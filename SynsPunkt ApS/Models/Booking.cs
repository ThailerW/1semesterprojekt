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
        public int LokationID { get; set; }
        public DateTime Tidspunkt { get; set; }
        public DateTime Dato { get; set; }
        public string BookingType { get; set; }
        public int KundeID { get; set; }

        public Booking(int bookingID, int lokationID, DateTime dato, DateTime tidspunkt, string bookingType, int kundeID)
        {
            BookingID = bookingID;
            LokationID = lokationID;
            Dato = dato;
            Tidspunkt = tidspunkt;
            BookingType = bookingType;
            KundeID = kundeID;
        }
    }
}
