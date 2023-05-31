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
        public string Tidspunkt { get; set; }
        public DateTime Dato { get; set; }
        public string BookingType { get; set; }
        public int KundeID { get; set; }

        public Booking(int lokationid, DateTime dato, string tidspunkt, string bookingType, int kundeID)
        {

            LokationID = lokationid;
            Dato = dato;
            Tidspunkt = tidspunkt;
            BookingType = bookingType;
            KundeID = kundeID;
        }

        public Booking(int bookingID, int lokationid, DateTime dato, string tidspunkt, string bookingType, int kundeID)
        {
            BookingID = bookingID;
            LokationID = lokationid;
            Dato = dato;
            Tidspunkt = tidspunkt;
            BookingType = bookingType;
            KundeID = kundeID;
        }
    }
}
