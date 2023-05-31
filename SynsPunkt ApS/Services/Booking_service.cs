using SynsPunkt_ApS.Database;
using SynsPunkt_ApS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SynsPunkt_ApS.Services
{
    internal class BookingService
    {
        private CRUD_Booking bookingService;

        public BookingService()
        {
            bookingService = new CRUD_Booking();
        }

        public void CreateBooking(Booking newBooking)
        {
            bookingService.CreateBooking(newBooking);
        }

        public List<Booking> GetBookingsPerDate(DateTime date)
        {
            return bookingService.GetBookingsPerDate(date);
        }

        public void UpdateBooking(Booking updatedBooking)
        {
            bookingService.UpdateBooking(updatedBooking);
        }

        public void DeleteBooking(int bookingId)
        {
            bookingService.DeleteBooking(bookingId);
        }

        internal int CreateBooking(int generatedBookingID, int lokationID, DateTime tidspunkt, DateTime dato, string bookingType, int kundeID)
        {
            throw new NotImplementedException();
        }

        public List<Models.Booking> GetAllBookings()
        {
            Database.CRUD_Booking crudBooking = new Database.CRUD_Booking();
            var AlleBooking = crudBooking.GetAllBookings();
            return AlleBooking;
        }

        public void ReadBooking(string id, out string bookingID, out string lokationid, out string dato, out string tidspunkt, out string bookingType, out string kundeID)
        {
            Database.CRUD_Booking crudBooking = new Database.CRUD_Booking();
            crudBooking.ReadBooking(id, out bookingID, out lokationid, out dato, out tidspunkt, out bookingType, out kundeID);
        }


    }
}
