using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SynsPunkt_ApS.Models
{
    public class Booking
    {
        public int BookingNummer { get; set; }
        public DateTime Dato { get; set; }
        public DateTime Tidspunkt { get; set; }
        public string BookingType { get; set; }

        public Booking(int bookingNummer, DateTime dato, DateTime tidspunkt, string bookingType)
        {
            BookingNummer = bookingNummer;
            Dato = dato;
            Tidspunkt = tidspunkt;
            BookingType = bookingType;
        }
    }
}
