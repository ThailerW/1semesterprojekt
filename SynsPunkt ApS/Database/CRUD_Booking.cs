using SynsPunkt_ApS.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SynsPunkt_ApS.Database
{
    public class CRUD_Booking
    {

        private string connectionString = "Data Source=mssql15.unoeuro.com;Initial Catalog=oversaftigt_dk_db_test;User ID=oversaftigt_dk;Password=prfHFR9546nEtyb2xDmc";

        public void CreateBooking(Booking newBooking)
        {
            string query = "INSERT INTO Booking (BookingID, LokationID, tidspunkt, dato, bookingType, KundeID) " +
                           "VALUES (@BookingID, @LokationID, @tidspunkt, @dato, @bookingType, @KundeID)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    //command.Parameters.AddWithValue("@BookingID", newBooking.BookingID);
                    command.Parameters.AddWithValue("@LokationID", newBooking.LokationID);
                    command.Parameters.AddWithValue("@tidpunkt", newBooking.Tidspunkt);
                    command.Parameters.AddWithValue("@dato", newBooking.Dato);
                    command.Parameters.AddWithValue("@bookinType", newBooking.BookingType);
                    command.Parameters.AddWithValue("@KundeID", newBooking.KundeID);
                    command.ExecuteNonQuery();

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                        MessageBox.Show("Booking oprettet");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Fejl under oprettelse: " + ex.Message);
                    }
                }
            }

            MessageBox.Show("Booking oprettet!");
        }


        public List<Booking> GetBookingsPerDate(DateTime dato)
        {
            List<Booking> bookings = new List<Booking>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Booking WHERE dato = @dato";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@dato", dato);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Booking booking = new Booking
                        (
                            //(int)reader["bookingID"],
                            (int)reader["lokationID"],
                            (DateTime)reader["tidspunkt"],
                            (DateTime)reader["dato"],
                            (string)reader["bookingtype"],
                            (int)reader["kundeid"]
                        );

                        bookings.Add(booking);
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Fejl ved hentning af bookinger: " + ex.Message);
                }
            }

            return bookings;
        }


        public void UpdateBooking(Booking updatedBooking)
        {
            string query = "UPDATE Booking SET tidspunkt = @tidspunkt, dato = @dato, kundeid = @kundeid, bookingType = @bookingType" +
                           "VALUES (@tidspunkt, @dato, @kundeid, @bookingType)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@BookingID", updatedBooking.BookingID);
                    command.Parameters.AddWithValue("@LokationID", updatedBooking.LokationID);
                    command.Parameters.AddWithValue("@tidpunkt", updatedBooking.Tidspunkt);
                    command.Parameters.AddWithValue("@dato", updatedBooking.Dato);
                    command.Parameters.AddWithValue("@bookinType", updatedBooking.BookingType);
                    command.Parameters.AddWithValue("@KundeID", updatedBooking.KundeID);
                    command.ExecuteNonQuery();
                }
            }

            MessageBox.Show("Booking opdateret!");
        }

        public void DeleteBooking(int bookingId)
        {
            string query = "DELETE FROM Booking WHERE BookingID = @BookingID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@BookingID", bookingId);

                    command.ExecuteNonQuery();
                }
            }

            MessageBox.Show("Bookingen er slettet!");
        }


    }
}
