using SynsPunkt_ApS.Database;
using SynsPunkt_ApS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SynsPunkt_ApS.Services
{
    
    //The BookingService Class provides a set methods to interact with booking data
    internal class BookingService
    {
        private CRUD_Booking bookingService;

        public BookingService()
        {
            bookingService = new CRUD_Booking();
        }

        //Sebastian; creates a new booking using the CreateBooking method
        public void CreateBooking(Booking newBooking)
        {
            bookingService.CreateBooking(newBooking);
        }

        //Sebastian: Creates a list of bookings on a certain date, GetBookingsPerDate method is called and takes in a DateTime var and returns booking on the given date.  
        public List<Booking> GetBookingsPerDate(DateTime date)
        {
            return bookingService.GetBookingsPerDate(date);
        }

        //Sebastian: Takes all booking parameters in using the updatedBooking var and calls the UpdateBooking method in CRUD_Bookings
        public void UpdateBooking(Booking updatedBooking)
        {
            bookingService.UpdateBooking(updatedBooking);
        }

        //Deletes a certain booking based on the parameter bookingID using the DeleteBooking method in CRUD_Booking
        public void DeleteBooking(int bookingId)
        {
            bookingService.DeleteBooking(bookingId);
        }

        //Creates a list of all bookings in the database using the GetAllBookings method in CRUD_Booking. 
        public List<Models.Booking> GetAllBookings()
        {
            Database.CRUD_Booking crudBooking = new Database.CRUD_Booking();
            var AlleBooking = crudBooking.GetAllBookings();
            return AlleBooking;
        }

        //Reads a booking with all 6 parameters which exist in Booking Class using the ReadBooking method in CRUD_Booking.
        public void ReadBooking(string id, out string bookingID, out string lokationid, out string dato, out string tidspunkt, out string bookingType, out string kundeID)
        {
            Database.CRUD_Booking crudBooking = new Database.CRUD_Booking();
            crudBooking.ReadBooking(id, out bookingID, out lokationid, out dato, out tidspunkt, out bookingType, out kundeID);
        }


    }
}
